using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class WorkingWithPasswordsWindow : Window
    {
         string selectedValue;
        string PFLPTF = "PasswordFolder.ListPasswordTextFile.txt";

        public WorkingWithPasswordsWindow()
        {
            InitializeComponent();
            LoadDataIntoComboBox();
        }

        private void FileBtn_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe", "PasswordFolder.ListPasswordTextFile.txt");
        }

        private void ExitWPBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EncryptionBtn_Click(object sender, RoutedEventArgs e)
        {
            PasswordCode();
        }

        private void SeveBtn_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(PFLPTF, true))
            {
                sw.WriteLine(NameTBox.Text + "|" + EncryptedPasswordTBox.Text);
            }
            MessageBox.Show("Seve!");
            LoadDataIntoComboBox();
        }
        
        private void GetAPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            Decoding();
        }

        private void ListOfPasswordsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedValue = comboBox.SelectedValue.ToString();
        }

        private void PasswordCode()
        {
            string textCode = PasswordTBox.Text;
            string ValidСharacters = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxZz0123456789!@$%&*_-+=";
            DateTime dateTime = DateTime.Now;
            string DT = dateTime.ToString("G");
            int key = DT.Where(char.IsDigit).Select(c => int.Parse(c.ToString())).Sum();
            string keyString = key.ToString();
            string code = "";
            for (int i = 0; i < textCode.Length; i++)
            {
                char currentChar = textCode[i];

                if (ValidСharacters.Contains(currentChar))
                {
                    int newIndex = (ValidСharacters.IndexOf(currentChar) + key) % ValidСharacters.Length;
                    code += ValidСharacters[newIndex];
                }
                else
                {
                    code += currentChar;
                }
            }
            char[] charArray = code.ToCharArray();
            Array.Reverse(charArray);
            string revCode = new string(charArray);
            EncryptedPasswordTBox.Text = keyString + "/" + revCode;
        }
        
        private void Decoding()
        {
           
            int index = selectedValue.IndexOf("|");
            if (index != -1)
            {
                PFLPTF = selectedValue.Substring(index + 1);
            }
            string[] parts = PFLPTF.Split('/');
            string beforeSlash = parts[0];
            string afterSlash = parts[1];
            int key = Convert.ToInt32(beforeSlash);

            string ValidСharacters = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxZz0123456789!@$%&*_-+=";
            string code = "";
            char[] charArray = afterSlash.ToCharArray();
            Array.Reverse(charArray);
            string textCode = new string(charArray);

            for (int i = 0; i < textCode.Length; i++)
            {
                char currentChar = textCode[i];

                if (ValidСharacters.Contains(currentChar))
                {
                    int newIndex = (ValidСharacters.IndexOf(currentChar) - key + ValidСharacters.Length) % ValidСharacters.Length;
                    code += ValidСharacters[newIndex];
                }
                else
                {
                    code += currentChar;
                }
            }
            OutputPasswordTBox.Text = code.ToString();
            MessageBox.Show("Пароль готов!");
        }

        private void LoadDataIntoComboBox()
        {
            ListOfPasswordsCB.Items.Clear();

            if (File.Exists(PFLPTF))
            {
                using (StreamReader sr = new StreamReader(PFLPTF))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null) 
                    { 
                        ListOfPasswordsCB.Items.Add(line);
                    }
                }
            }
            else
            {
                MessageBox.Show("No file!");
            }
        }

        
    }
}
