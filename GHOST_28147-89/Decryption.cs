using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GHOST_28147_89
{
    public partial class Decryption : Form
    {
        StreamReader read;
        StreamWriter write;

        public Decryption()
        {
            InitializeComponent();
            this.FormClosing += DecryptionClosed;
        }

        // Actions

        private void DecryptionClosed(object sender, FormClosingEventArgs e)
        {
            try
            {
                read.Close();
                write.Close();
            }
            catch (Exception)
            { }
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            saveButton.Enabled = false;
            decryptionButton.Enabled = false;

            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Text files|*.txt";
            file.ShowDialog();

            if ((file.FileName) != "")
            {
                path.Text = file.FileName;
                closeText.Clear();

                read = new StreamReader(file.FileName, Encoding.Unicode);
                closeText.AppendText(read.ReadToEnd());
            }
        }

        private void decryptionButton_Click(object sender, EventArgs e)
        {
            // В алгоритме фигурируют блоки открытого текста по 64 бит (8 байт) каждый
            // и ключ длиной в 256 бит (32 байта)

            // Считаем весь текст из поля ввода и распределим его по 8-байтным блокам:
            List<List<byte>> text = new List<List<byte>>();
            Helpers.GetText(ref text, ref closeText);

            // Считываем весь ключ, введенный пользователем
            List<byte> full_key = new List<byte>();

            full_key.AddRange(Encoding.Unicode.GetBytes(key.Text));
            
            // Мы можем оперировать только ключами фиксированной длины: 256 бит = 32 байт
            // Поэтому, если введенный клч короткий - увеличиваем его,
            // если длинный - укорачиваем.

            Helpers.MakeValidKey(ref full_key, ref initialVector);
            
            // Весь ключ разбивается на 8 частей по 4 байта,
            // эти части будут в последствии использованы на отдельных раундах шифрования 
            List<List<byte>> K = new List<List<byte>>();

            // Всего 32 раунда расшифрования, соответственно нам надо 32шт Ki
            // Они получаются следующим образом:
            // K0 - K7 - 4-байтные части полного ключа (K0 - K7),
            // K8 - K31 - циклические повторения K7 - K0
            Helpers.FillK(ref K, ref full_key, false);

            List<byte> resList = new List<byte>();

            for (int i = 0; i < text.Count; i++)
            {
                // Блоки A и B проходят 32 раунда расшифрования
                for (int j = 0; j < 1; j++)
                {
                    text[i] = Helpers.Basic(text[i], K[j], false, ref initialVector);
                }
                text[i] = Helpers.Basic(text[i], K[1], true, ref initialVector);
                resList.AddRange(text[i]);
            }

            // Записываем результат в блок зашифрованного текста:
            StringBuilder resSB = new StringBuilder();
            resSB.Append(Encoding.Unicode.GetString(resList.ToArray()));

            openText.Text = resSB.ToString();
        }

        private void closeText_TextChanged(object sender, EventArgs e)
        {
            openText.Clear();
        
            if ((sender as TextBox).Text != "")
            {
                decryptionButton.Enabled = true;
            }
            else
            {
                decryptionButton.Enabled = false;
            }
        }

        private void openText_TextChanged(object sender, EventArgs e)
        {
            completeSaving.Visible = false;

            if ((sender as TextBox).Text != "")
            {
                saveButton.Enabled = true;
            }
            else
            {
                saveButton.Enabled = false;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Text files|*.txt";
            file.ShowDialog();

            if ((file.FileName) != "")
            {
                try
                {
                    write = new StreamWriter(file.FileName, false, Encoding.Unicode);
                    write.Write(closeText.Text);
                    write.Close();
                }
                catch (Exception)
                {
                    write = new StreamWriter("result.txt", false, Encoding.Unicode);
                    write.Write(closeText.Text);
                    write.Close();
                }
                finally
                {
                    completeSaving.Visible = true;
                }
            }

        }

        private void copy_Click(object sender, EventArgs e)
        {
            Encryption own = this.Owner as Encryption;
            
            key.Text = own.Controls["key"].Text;
            initialVector.Text = this.Owner.Controls["initialVector"].Text;
        }

    }
}