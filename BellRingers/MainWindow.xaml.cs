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

namespace BellRingers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] towers = { "Great Shevington", "Little Mudford", "Upper Gumtree", "Downley Hatch" };
        private string[] ringingMethods = { "Plain Bob", "Reverse Canterbury",
                                            "Grandsire", "Kent Treble Bob", "Old Oxford Delight",
                                            "Wincendon Place", "Norwitch Suprise",
                                            "Crayford Little Court"};

        public MainWindow()
        {
            InitializeComponent();
            this.Reset();
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

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            string nameAndTower = String.Format(
                "Member name: {0} {1} from the tower at {2} rings the following methods:",
                textBoxFirstName.Text, textBoxLastName.Text, comboBoxTowers.Text
                );

            StringBuilder details = new StringBuilder();
            details.AppendLine(nameAndTower);

            foreach (CheckBox cb in listBoxMethods.Items)
            {
                if (cb.IsChecked.Value)
                {
                    details.AppendLine(cb.Content.ToString());
                }
            }

            MessageBox.Show(details.ToString(), "Member Information");
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
            buttonAdd.IsEnabled = true;
        }
    }
}
