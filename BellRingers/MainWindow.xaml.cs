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
using System.IO;
using Microsoft.Win32;
using System.Threading;

namespace BellRingers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ContextMenu windowContextMenu = null;
        private string[] towers = { "Great Shevington", "Little Mudford", "Upper Gumtree", "Downley Hatch" };
        private string[] ringingMethods = { "Plain Bob", "Reverse Canterbury",
                                            "Grandsire", "Kent Treble Bob", "Old Oxford Delight",
                                            "Wincendon Place", "Norwitch Suprise",
                                            "Crayford Little Court"};

        public MainWindow()
        {
            InitializeComponent();
            this.Reset();

            MenuItem saveMemberMenuItem = new MenuItem();
            saveMemberMenuItem.Header = "Save Member Details";
            saveMemberMenuItem.Click += new RoutedEventHandler(saveMember_Click);

            MenuItem clearFormMenuItem = new MenuItem();
            clearFormMenuItem.Header = "Clear Form";
            saveMemberMenuItem.Click += new RoutedEventHandler(buttonClear_Click);

            windowContextMenu = new ContextMenu();
            windowContextMenu.Items.Add(saveMemberMenuItem);
            windowContextMenu.Items.Add(clearFormMenuItem);
        }

        public void Reset()
        {
            textBoxFirstName.Text = String.Empty;
            textBoxLastName.Text = String.Empty;

            comboBoxTowers.Items.Clear();
            foreach (String towerName in towers)
            {
                comboBoxTowers.Items.Add(towerName);
            }
            comboBoxTowers.Text = comboBoxTowers.Items[0] as string;

            listBoxMethods.Items.Clear();
            CheckBox method = null;
            foreach (string methodName in ringingMethods)
            {
                method = new CheckBox();
                method.Margin = new Thickness(0, 0, 0, 10);
                method.Content = methodName;
                listBoxMethods.Items.Add(method);
            }
            isCaptain.IsChecked = false;
            novice.IsChecked = true;
            dateMemberSince.Text = DateTime.Today.ToString();
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            this.Reset();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult key = MessageBox.Show(
                "Are you sure you want to quit", "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No);
            e.Cancel = (key == MessageBoxResult.No);
        }

        private void newMember_Click(object sender, RoutedEventArgs e)
        {
            this.Reset();
            saveMember.IsEnabled = true;
            textBoxFirstName.IsEnabled = true;
            textBoxLastName.IsEnabled = true;
            comboBoxTowers.IsEnabled = true;
            isCaptain.IsEnabled = true;
            dateMemberSince.IsEnabled = true;
            groupYearsOfExperience.IsEnabled = true;
            listBoxMethods.IsEnabled = true;
            buttonClear.IsEnabled = true;
            this.ContextMenu = windowContextMenu;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveMember_Click(object sender, RoutedEventArgs e)
        {

            Member member = new Member();
            member.FirstName = textBoxFirstName.Text;
            member.LastName = textBoxLastName.Text;
            member.TowerName = comboBoxTowers.Text;
            member.IsCaptain = isCaptain.IsChecked.Value;
            member.MemberSince = dateMemberSince.SelectedDate.Value;
            member.Methods = new List<string>();

            foreach (CheckBox cb in listBoxMethods.Items)
            {
                member.Methods.Add(cb.Content.ToString());
            }


            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "txt";
            saveDialog.AddExtension = true;
            saveDialog.FileName = "Members";
            saveDialog.InitialDirectory = @"C:\Users\GAMEOVER\Documents";
            saveDialog.OverwritePrompt = true;
            saveDialog.Title = "Bell Ringers";
            saveDialog.ValidateNames = true;

            if (saveDialog.ShowDialog().Value)
            {
                Thread workerThread = new Thread(
                        () => { this.saveData(saveDialog.FileName, member); });
                workerThread.Start();

            }


        }

        private void saveData(string fileName, Member member)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("First Name: {0}", member.FirstName);
                writer.WriteLine("Last Name: {0}", member.LastName);
                writer.WriteLine("Tower: {0}", member.TowerName);
                writer.WriteLine("Captain: {0}", member.IsCaptain.ToString());
                writer.WriteLine("Member Since: {0}", member.MemberSince.ToString());
                writer.WriteLine("Methods: ");
                foreach (String method in member.Methods)
                {
                    writer.WriteLine(method);
                }
            }
            Thread.Sleep(10000);
            MessageBox.Show("Member details saved", "Saved");
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            About aboutWindow = new About();
            aboutWindow.ShowDialog();
        }

        private void clearName_Click(object sender, RoutedEventArgs e)
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
        }

    }
}
