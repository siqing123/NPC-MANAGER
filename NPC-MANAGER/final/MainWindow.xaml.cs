using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Npc;
using Scene;
using SceneManager;
namespace final
{

    public partial class MainWindow : Window
    {
        public SceneManagerClass mSceneManager { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            mSceneManager = new SceneManagerClass();
            this.DataContext = mSceneManager;
            lbforScene.ItemsSource = mSceneManager.mScenes;
            lbforScene.SelectedIndex = -1;
            this.lbforScene.DisplayMemberPath = "nameForScene";
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            sceneSaved t = new sceneSaved();

            if (t.ShowDialog() == true)
            {
               // mSceneManager.Add(t.mScene);
                mSceneManager.mScenes.Add(t.mScene);
                lbforScene.Items.Refresh();
            }                     
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (lbforScene.SelectedIndex != -1)
            {
                sceneSaved t = new sceneSaved();
                t.mScene = mSceneManager.mScenes[lbforScene.SelectedIndex];
                t.Setup();
                if (t.ShowDialog() == true)
                {
                    mSceneManager.mScenes[lbforScene.SelectedIndex] = t.mScene;
                    lbforScene.Items.Refresh();
                }             
            }
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (lbforScene.SelectedIndex != -1)
            {
                mSceneManager.mScenes.RemoveAt(lbforScene.SelectedIndex);
                lbforScene.Items.Refresh();                
            }
        }

        private void save_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML|*.xml";
            if (saveFileDialog.ShowDialog() == true)
            {
               this.mSceneManager.SaveAsXML(saveFileDialog.FileName);
            }
        }

        private void load_Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string temp = System.IO.Path.GetExtension(openFileDialog.FileName);
                if (temp == ".xml")
                {
                    this.mSceneManager.loadXML(openFileDialog.FileName);
                    lbforScene.ItemsSource = mSceneManager.mScenes;
                    lbforScene.SelectedIndex = -1;
                    lbforScene.Items.Refresh();
                }               
            }
        }
    }
}
