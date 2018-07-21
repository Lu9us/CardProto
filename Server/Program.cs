using GameLib.Server;
using GameLib.Server.Services;
using GameLib.Server.Services.ServiceLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Util;
namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int frameCount = 0;
            
            NetworkInterface networkInterface = new NetworkInterface();
            GameLib.Server.GameState gs = new GameLib.Server.GameState();
            ServiceController.LoadGameState(gs);
            ServiceController.setRuntime(Runtime.SERVER);
            ServiceLoader.ClassHook();
            ServiceLoader.ModuleHook();
            

            networkInterface.HostServer();
           

            while (true)
            {
                    
                if (networkInterface.clients.Count > 0 && !gs.gameRunning )
                {
                   
                    for (int i = 0; i < networkInterface.clients.Count;i++)
                    {
                        gs.players[i] = new GameLib.Server.Player();
                        gs.players[i].playerID = i;
                        gs.players[i].Client = networkInterface.clients[i];
                        networkInterface.clients[i].Send(Util.Serilizer.Serilize<string>("Connected"));
                    }
                    gs.gameRunning = true;
                }

                if(gs.gameRunning)
                {

                    foreach(Player p in gs.players)
                    {
                        if (p != null)
                        {
                            p.Client.Send(Util.Serilizer.Serilize<string>("Running +" + frameCount));
                            Console.Clear();
                            Console.WriteLine("Running +" + frameCount);
                        }
                        

                    }
                    ServiceController.RunServices();

                    gs.FrameCount++;
                }
               
            }
        }
    }
}
