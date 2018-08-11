using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util;
using Util.Interfaces;

namespace DataBuilder
{
    public partial class Form1 : Form
    {
        public List<Type> GetTypes()
        {
            return  Assembly.GetAssembly(typeof(GameLib.Client.Services.MapRenderer)).GetTypes().ToList();
        }
        public T Cast<T>(object input)
        {
            return (T)input;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(GetTypes().ToArray());
        }

        private void btbLoadObject_Click(object sender, EventArgs e)
        {
            var type =(Type)comboBox1.SelectedItem;
            object myObject = Activator.CreateInstance(type);
            ISerizilizer s = new JSONSerilizer();
            propertyGrid1.SelectedObject = myObject;
            

        }
    }
}
