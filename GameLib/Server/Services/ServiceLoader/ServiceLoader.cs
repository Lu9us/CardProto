using GameLib.DataStructures.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Server.Services.ServiceLoader
{
  public static  class ServiceLoader
    {
        public static string CLASS_FILE = "class_file.ini";
        public static string MODULE_FILE = "module_file.ini";
        public static readonly List<Assembly> loadedAssemblies = new List<Assembly>();
        public static string WORKING_DIR = Environment.CurrentDirectory;
         static ServiceLoader()
        {
            System.Console.WriteLine("Starting Service loader with filepaths: ");
            System.Console.WriteLine("CLASS_FILE: "+CLASS_FILE);
            System.Console.WriteLine("MODULE_FILE: " + MODULE_FILE);

            if (!File.Exists(CLASS_FILE))
            {
                File.Create(CLASS_FILE).Close();
            }
            if (!File.Exists(MODULE_FILE))
            {
                File.Create(MODULE_FILE).Close();
            }

        }

        public static void ClassHook()
        {
            System.Console.WriteLine("Begining Class Hook");
            foreach (string s in File.ReadLines(CLASS_FILE))
            {
                System.Console.WriteLine("Class: " + s);
                Type t = Type.GetType(s);
                if (typeof(IService).IsAssignableFrom(t))
                {
                    System.Console.WriteLine("Type is service loading");
                    IService service = (IService)Activator.CreateInstance(t);
                    ServiceController.StartNewService(service);
                }
            }

        }



        public static void ModuleHook()
        {
            System.Console.WriteLine("Begining module Hook");
            foreach (string s in File.ReadLines(MODULE_FILE))
            {
                System.Console.WriteLine("Module file path: "+s);
                try
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, s);
                    var assembly = Assembly.LoadFile(path);
                    foreach (Type t in assembly.ExportedTypes)
                    {
                        System.Console.WriteLine("Reading type :" + t.AssemblyQualifiedName);
                        if (typeof(IService).IsAssignableFrom(t))
                        {
                            System.Console.WriteLine("Type is service loading");
                            var service =  (IService)Activator.CreateInstance(t);
                            ServiceController.StartNewService(service);
                        }
                    }
                }
                catch (Exception e)
                {

                }
                
            }
            System.Console.WriteLine("Module hook finished");
        }

    }
}
