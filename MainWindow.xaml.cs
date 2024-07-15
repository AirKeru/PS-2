using System;
using System.Collections.Generic;
using System.IO;
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

namespace PS_2
{
    public partial class MainWindow : Window
    {
        string mainPasswordFile = "PasswordFolder.MainPasswordTextFile.txt";
        int unsuccessfulAttempts = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void EtranceBtn_Click(object sender, RoutedEventArgs e)
        {
            string etrancePassword = MainPasswordBox.Password;

            if (File.Exists(mainPasswordFile))
            {
                string storedPassword = File.ReadAllText(mainPasswordFile);

                if (etrancePassword == storedPassword)
                {
                    WorkingWithPasswordsWindow workingWithPasswordsWindow = new WorkingWithPasswordsWindow();
                    workingWithPasswordsWindow.Show();
                    PS_2.MainWindow mainWindow = MainWindow.GetWindow(this) as PS_2.MainWindow;
                    if (mainWindow != null)
                    {
                        mainWindow.Close();
                    }
                }
                else
                {   
                    unsuccessfulAttempts++;
                    if (unsuccessfulAttempts >= 3)
                    {
                        MessageBox.Show("Error!");
                        PasswordRecoveryBtn.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Password" + " " + unsuccessfulAttempts);
                    }
                }
            }
            else
            {
                File.WriteAllText(mainPasswordFile, etrancePassword);
                MessageBox.Show("The password is saved");
            }
            

        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PasswordRecoveryBtn_Click(object sender, RoutedEventArgs e)
        {
            PasswordRecoveryWindow passwordRecoveryWindow = new PasswordRecoveryWindow();
            passwordRecoveryWindow.Show();
            PS_2.MainWindow mainWindow = MainWindow.GetWindow(this) as PS_2.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Close();
            }
        }
    }
}
