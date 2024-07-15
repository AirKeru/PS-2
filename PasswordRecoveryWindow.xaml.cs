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
using System.Windows.Shapes;

namespace PS_2
{
    public partial class PasswordRecoveryWindow : Window
    {
        string filePath = "PasswordFolder.MainPasswordTextFile.txt";

        public PasswordRecoveryWindow()
        {
            InitializeComponent();
            Key();
        }

        string generatedKey = "";

        private void CheckingTheKeyBtn_Click(object sender, RoutedEventArgs e)
        {
            string key = generatedKey;
            string keyPasswordBox = KeyPasswordBox.Password;
            if (keyPasswordBox == key)
            {
                File.WriteAllText(filePath, string.Empty);
                MessageBox.Show("The password has been reset");
            }
            else
            {
                MessageBox.Show("Error!");
            }
        }

        private void SeveNewMainPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            string newEtrancePassword = NewMainPasswordBox.Password;

            File.WriteAllText(filePath, newEtrancePassword);
            MessageBox.Show("The password is saved");

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            PS_2.PasswordRecoveryWindow passwordRecoveryWindow = PasswordRecoveryWindow.GetWindow(this) as PS_2.PasswordRecoveryWindow;
            if (passwordRecoveryWindow != null) 
            {
                passwordRecoveryWindow.Close();
            }

        }

        private string Key()
        {
            if (generatedKey == "")
            {
                string Key = "PS-2/admin/help/me/";
                Random random = new Random();
                int randomNumber = random.Next(0, 10000);
                string Code = randomNumber.ToString("D4");
                generatedKey = Key + Code;
                KeyCodeTBox.Text = Code;
            }

            return generatedKey;
        }
    }
}
