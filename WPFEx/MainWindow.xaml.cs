using System;
using System.IO;
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


namespace WPFEx
{
    public partial class MainWindow : Window
    {
        List<User> users = new List<User>();
        int loads = 0;
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            users = new List<User>();
            lbUsers.Items.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open text file";
            openFileDialog.Filter = "Text files|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] attributes = line.Split(';');
                            User newUser = new User(attributes[0], attributes[1], attributes[2], attributes[3]);
                            lbUsers.Items.Add(newUser.ID +" " + newUser.Name);
                            users.Add(newUser);
                        }
                    }
                }
                catch (IOException exep)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(exep.Message);
                }
                loads++;
                tbStatus.Text = "Loads:" + loads + ", Users:" + users.Count;
            }
        }

        private void lbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lbUsers.SelectedIndex >= 0)
            {
                int index = lbUsers.SelectedIndex;
                User thisUser = users[index];
                string userText = "Name:" + thisUser.Name + " , ID:" + thisUser.ID + " , Age:" + thisUser.Age + " , Score:" + thisUser.Score;
                tbUser.Text = userText;
            }
        }
    }
    public class User
    {
        public string ID { get;}
        public string Name{ get; }
        public string Age { get; }
        public string Score { get; }
        public User(string id, string name, string age, string score)
        {
            ID = id;
            Name = name;
            Age = age;
            Score = score;
        }
    }
}

    