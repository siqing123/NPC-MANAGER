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
using Microsoft.Win32;
using  Npc;
using Scene;
namespace final
{
   
    public partial class npcSave : Window
    {
        public NpcClass npc { get; set; }
        public npcSave()
        {
            InitializeComponent();
            if (npc == null)
            {
                npc = new NpcClass();
                npc.Dialogues = new List<string>();             
            }

            txtType.ItemsSource = Enum.GetNames(typeof(NpcClass.npcType));


            txtType.SelectedIndex = txtType.Items.IndexOf("Weapons");

            txtDialogue.Text = String.Empty;
            txtName.Text = String.Empty;

            lbDialogue.ItemsSource = npc.Dialogues;
            lbDialogue.SelectedIndex = -1;
         
        }

        public void Setup()
        {
            txtName.Text = npc.Name;
            lbDialogue.ItemsSource = npc.Dialogues;
            string temp = npc.Type.ToString();
            txtType.SelectedIndex = txtType.Items.IndexOf(temp);
            if (npc.FaceImage != null)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(npc.FaceImage);
                image.EndInit();
                faceImage.Source = image;
            }
           // faceImage.Item
        }

        private void loadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                npc.FaceImage = dialog.FileName;
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(dialog.FileName);
                image.EndInit();
                faceImage.Source = image;
            }
        }

        private void pushDialogueButton_Click(object sender, RoutedEventArgs e)
        {   
                string temp = txtDialogue.Text;
            if (temp != string.Empty)
            {
                npc.Dialogues.Add(temp);
                lbDialogue.Items.Refresh();
                txtDialogue.Text = String.Empty;
            }
        }

        private void saveAndBack_Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text != string.Empty)
            {
                npc.Type = (NpcClass.npcType)Enum.Parse(typeof(NpcClass.npcType), txtType.SelectedItem.ToString());
                npc.Name = txtName.Text;

                this.DialogResult = true;
            }
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (lbDialogue.SelectedIndex == -1)
            {
                return;
            }
            else
            {
                npc.Dialogues.RemoveAt(lbDialogue.SelectedIndex);
                lbDialogue.Items.Refresh();
            }
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (lbDialogue.SelectedIndex == -1)
            {
                return;
            }
            else
            {
                txtDialogue.Text = npc.Dialogues[lbDialogue.SelectedIndex];
                npc.Dialogues.RemoveAt(lbDialogue.SelectedIndex);
                lbDialogue.Items.Refresh();
            }
        }   
    }
}
