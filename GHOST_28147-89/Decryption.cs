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
        Encryption owner;
        public Decryption()
        {
            InitializeComponent();
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

                StreamReader read = new StreamReader(file.FileName, Encoding.Unicode);
                closeText.AppendText(read.ReadToEnd());
            }
        }
        private void decryptionButton_Click(object sender, EventArgs e)
        {
            owner = Owner as Encryption;
            // В алгоритме фигурируют блоки открытого текста по 64 бит (8 байт) каждый
            // и ключ длиной в 256 бит (32 байта)

            // Считаем весь текст из поля ввода и распределим его по 8-байтным блокам:
            List<List<byte>> text = new List<List<byte>>();
            owner.GetText(ref text, ref closeText);

            // Считываем весь ключ, введенный пользователем
            List<byte> full_key = new List<byte>();

            full_key.AddRange(Encoding.Unicode.GetBytes(key.Text));
            
            // Мы можем оперировать только ключами фиксированной длины: 256 бит = 32 байт
            // Поэтому, если введенный клч короткий - увеличиваем его,
            // если длинный - укорачиваем.

            owner.MakeValidKey(ref full_key, ref initialVector);
            
            // Весь ключ разбивается на 8 частей по 4 байта,
            // эти части будут в последствии использованы на отдельных раундах шифрования 
            List<List<byte>> K = new List<List<byte>>();

            // Всего 32 раунда расшифрования, соответственно нам надо 32шт Ki
            // Они получаются следующим образом:
            // K0 - K7 - 4-байтные части полного ключа (K0 - K7),
            // K8 - K31 - циклические повторения K7 - K0
            FillK(ref K, ref full_key);


            // 64-битные (8-байтные) блоки открытого текста разбиваются попалам
            // на фрагменты A и B (по 4 байта)
            List<byte> A;
            List<byte> B;
            List<byte> resList = new List<byte>();

            for (int i = 0; i < text.Count; i++)
            {
                B = new List<byte>(text[i].GetRange(0, 4));
                A = new List<byte>(text[i].GetRange(4, 4));
       
                // Блоки A и B проходят 32 раунда шифрования
                for (int j = 0; j < 1; j++)
                {
                    Decrypt(ref A, ref B, K[j], true);
                }
                Decrypt(ref A, ref B, K[1], true);

                // Зашифрованные блоки A и B снова склеиваются
                resList.AddRange(A);
                resList.AddRange(B);
            }

            // Записываем результат в блок зашифрованного текста:
            StringBuilder resSB = new StringBuilder();
            resSB.Append(Encoding.Unicode.GetString(resList.ToArray()));

            openText.Text = resSB.ToString();
        }

        /// <summary>
        /// Раунд шифрования
        /// </summary>
        /// <param name="A">Подблок A</param>
        /// <param name="B">Подблок B</param>
        /// <param name="K">Ключ Ki</param>
        void Decrypt(ref List<byte> A, ref List<byte> B, List<byte> K, bool isLastRaund)
        {
            List<byte> new_A = owner.SUM1(B, owner.Function(A, K, isLastRaund, ref initialVector));
            B = A;
            A = new_A;
        }

        // helpers
        /// <summary>
        /// Заполнение массива Ki
        /// </summary>
        /// <param name="K">Массив Ki</param>
        /// <param name="key">Исходный полный ключ</param>
        public void FillK(ref List<List<byte>> K, ref List<byte> key)
        {
            for (int i = 0; i < key.Count; i += 4)
            {
                K.Add(key.GetRange(i, 4));
            }

            // Для получения нужного результата нужно текущее значение K
            // повторить еще 3 раза (должно быть 32 шт Ki. Сейчас их 8. Нужно еще (32-8)/8 = 3)
            // На первой итерации мы добавляем в конец копию текущего K
            // получаем 8+8=16 элементов
            // На второй итерации мы снова добавляем копию K (где уже 16 элементов):
            // 16+16=32 - то, что нужно!
            for (int i = 0; i < 2; i++)
            {
                K.AddRange(K);
            }
            K.Reverse(24, 8);
        }

        // Actions
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
                StreamWriter write;
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