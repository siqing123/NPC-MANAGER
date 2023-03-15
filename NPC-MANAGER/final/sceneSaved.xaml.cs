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
using System.Windows.Shapes;
using SceneManager;
using Scene;
using Npc;
using Microsoft.Win32;

namespace final
{
   
    /// <summary>
    /// Interaction logic for sceneSaved.xaml
    /// </summary>
    public partial class sceneSaved : Window
    {
        public SceneClass mScene { get; set; }
        public sceneSaved()
        {
            InitializeComponent();
            mScene = new SceneClass();
            lbNpc.ItemsSource = mScene.mNPC;
            lbNpc.SelectedIndex = -1;
            this.lbNpc.DisplayMemberPath = "Name";
            txtSceneName.Text = string.Empty;
           
        }

        public void Setup()
        {
            txtSceneNameShow.Text = mScene.nameForScene;
            if(mScene.backGroundPath != null)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(mScene.backGroundPath);
                image.EndInit();
                backGroundImage.Source = image;
            }
            lbNpc.ItemsSource = mScene.mNPC;
            lbNpc.Items.Refresh();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            npcSave t = new npcSave();//窗口

            if (t.ShowDialog() == true)
            {
                mScene.mNPC.Add(t.npc);
                lbNpc.Items.Refresh();             
            }
        }

        private void load_BG_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                mScene.backGroundPath = dialog.FileName;
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(dialog.FileName);
                image.EndInit();
                backGroundImage.Source = image;
            }
        }

 

        private void pushNameButton_Click(object sender, RoutedEventArgs e)
        {
            mScene.nameForScene = txtSceneName.Text;
            txtSceneNameShow.Text = mScene.nameForScene;
            txtSceneName.Text = string.Empty;
        }

        private void saveAndReturnButton_Click(object sender, RoutedEventArgs e)
        {
            if (mScene.nameForScene != null)
            {
                this.DialogResult = true;
            }
            
        }

        private void delete_Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lbNpc.SelectedIndex != -1)
            {
                mScene.mNPC.RemoveAt(lbNpc.SelectedIndex);
                lbNpc.Items.Refresh();
            }
        }

        private void edit_Button_Click(object sender, RoutedEventArgs e)
        {         
            if (lbNpc.SelectedIndex != -1)
            {
                npcSave t = new npcSave();
                t.npc = mScene.mNPC[lbNpc.SelectedIndex];
                t.Setup();
                if (t.ShowDialog() == true)//判断是点击save还是cancle
                {
                    mScene.mNPC[lbNpc.SelectedIndex] = t.npc;
                    lbNpc.Items.Refresh();
                }
            }          
        }
    }
}
