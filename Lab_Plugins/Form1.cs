using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PluginInterface;

namespace Lab_Plugins
{
    public partial class Form1 : Form
    {
        List<IPlugin> info = new List<IPlugin>();
        Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();
        public Form1()
        {
            InitializeComponent();
            FindPlugins();
            CreatePluginsMenu();
        }
        
        void FindPlugins()
        {
            List<string> allowedPlugins = new List<string>();
            StreamReader f = new StreamReader("config.txt");
            while (!f.EndOfStream)
            {
                allowedPlugins.Add(f.ReadLine());
            }
            f.Close();
            // папка с плагинами
            string folder = System.AppDomain.CurrentDomain.BaseDirectory;

            // dll-файлы в этой папке
            string[] files = Directory.GetFiles(folder, "*.dll");

            foreach (string file in files)
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);

                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("PluginInterface.IPlugin");

                        if (iface != null)
                        {
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                            if (allowedPlugins.Any(s => s.Contains(plugin.Name)))
                            {
                                //VersionAttribute MyAttribute = (VersionAttribute)Attribute.GetCustomAttribute(typeof(Form1), typeof(VersionAttribute));
                                plugins.Add(plugin.Name, plugin);
                                info.Add(plugin);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки плагина\n" + ex.Message);
                }
        }
        void CreatePluginsMenu()
        {
            foreach (IPlugin p in plugins.Values)
            {
                var menuItem = new ToolStripMenuItem(p.Name);
                menuItem.Click += OnPluginClick;

                плагиныToolStripMenuItem.DropDownItems.Add(menuItem);
            }

        }

        private void OnPluginClick(object sender, EventArgs args)
        {
            IPlugin plugin = plugins[((ToolStripMenuItem)sender).Text];
            plugin.Transform((Bitmap)pictureBox1.Image);
            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 me = new Form2(info);
            me.ShowDialog();
        }
    }
}
