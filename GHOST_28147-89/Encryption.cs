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
    public partial class Encryption : Form
    {


        public Encryption()
        {
            InitializeComponent();
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            saveButton.Enabled = false;
            encryptionButton.Enabled = false;

            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Text files|*.txt";
            file.ShowDialog();

            if ((file.FileName) != "")
            {
                path.Text = file.FileName;
                openText.Clear();

                StreamReader read = new StreamReader(file.FileName);
                openText.AppendText(read.ReadToEnd());
            }
        }
        private void encrypte_Click(object sender, EventArgs e)
        {
            // В алгоритме фигурируют блоки открытого текста по 64 бит (8 байт) каждый
            // и ключ длиной в 256 бит (32 байта)

            // Считаем весь текст из поля ввода и распределим его по 8-байтным блокам:
            List<List<byte>> text = new List<List<byte>>();

            try
            {
                GetText(ref text);
            }
            catch (System.OverflowException)
            {
                openText.Text = "WRONG INPUT";
            }


            // Считываем весь ключ, введенный пользователем
            List<byte> full_key = new List<byte>();

            full_key.AddRange(Encoding.Unicode.GetBytes(key.Text));
            
            // Мы можем оперировать только ключами фиксированной длины: 256 бит = 32 байт
            // Поэтому, если введенный клч короткий - увеличиваем его,
            // если длинный - укорачиваем.

            MakeValidKey(ref full_key);
            
            // Весь ключ разбивается на 8 частей по 4 байта,
            // эти части будут в последствии использованы на отдельных раундах шифрования 
            List<List<byte>> K = new List<List<byte>>();

            // Всего 32 раунда шифрования, соответственно нам надо 32шт Ki
            // Они получаются следующим образом:
            // K0 - K7 - 4-байтные части полного ключа,
            // K8 - k23 - циклические повторения K0 - K8
            // K24 - K31 == K7 - K0
            FillK(ref K, ref full_key);


            // 64-битные (8-байтные) блоки открытого текста разбиваются попалам
            // на фрагменты A и B (по 4 байта)
            List<byte> B;
            List<byte> A;
            List<byte> resList = new List<byte>();

            for (int i = 0; i < text.Count; i++)
            {
                B = new List<byte>(text[i].GetRange(0, 4));
                A = new List<byte>(text[i].GetRange(4, 4));

                // Блоки A и B проходят 32 раунда шифрования
                for (int j = 0; j < 32; j++)
                {
                    Encrypt(ref A, ref B, K[j]);
                }

                // Зашифрованные блоки A и B снова склеиваются
                resList.AddRange(B);
                resList.AddRange(A);
            }

            // Записываем результат в блок зашифрованного текста:
            StringBuilder resSB = new StringBuilder();
            foreach (byte block in resList)
            {
                resSB.Append(Convert.ToChar(block));
            }

            encryptedText.Text = resSB.ToString();
        }

        /// <summary>
        /// Раунд шифрования
        /// </summary>
        /// <param name="A">Подблок A</param>
        /// <param name="B">Подблок B</param>
        /// <param name="K">Ключ Ki</param>
        void Encrypt(ref List<byte> A, ref List<byte> B, List<byte> K)
        {
            List<byte> new_A = SUM1(B, Function(A, K));
            B = A;
            A = new_A;
        }
        List<byte> Function(List<byte> A, List<byte> K)
        {
            // Сумма по модулю 2^32
            List<byte> sum = SUM32(A, K);

            // Замены с помощью S-блоков
            List<List<int>> S = CreateS();
            UInt16 s_index;

            for (int i = 0; i < sum.Count; i++)
            {
                s_index = Convert.ToUInt16(sum[i]);
                sum[i] = Convert.ToByte(S[i][s_index]);
            }

            // Циклический сдвиг влево на 11 позиций
            StringBuilder SB = ToStringBuilder(sum, 4);
            for (int i = 0; i < 11; i++)
            {
                SB.Append(SB[0]);
                SB.Remove(0, 1);
            }

            // Формирование результирующего слова
            sum = ToList(SB, 8);

            return sum;
        }

        // helpers
        /// <summary>
        /// Считывает текст из поля ввода открытого текста и распределяет его
        /// по 8-байтным блокам
        /// </summary>
        /// <param name="text">Переменная для хранения открытого текста</param>
        void GetText(ref List<List<byte>> text)
        {
            List<byte> tmp_block = new List<byte>();
            tmp_block.AddRange(Encoding.Unicode.GetBytes(openText.Text));

            text.Clear();

            for (int i = 0; i < tmp_block.Count; i += 8)
            {
                try
                {
                    text.Add(tmp_block.GetRange(i, 8));
                }

                catch (ArgumentException)
                {
                    int difference = tmp_block.Count - i;

                    for (int j = 0; j < 8 - difference; j++)
                    {
                        tmp_block.Add(Convert.ToByte(null));
                    }

                    text.Add(tmp_block.GetRange(i, 8));
                }
            }
        }
        /// <summary>
        /// Формирование ключа нужной длинны
        /// </summary>
        /// <param name="full_key">Ключ, введенный пользователем</param>
        void MakeValidKey(ref List<byte> full_key)
        {
            if (full_key.Count < 32)
            {
                // Чтобы синхронизировать генераторы севдослучайных чисел у
                // шифратора и дешифратора, явно инициализируем первое значение
                // генератора
                int init;
                if (initialVector.Text != "")
                {
                    init = Convert.ToInt32(initialVector.Text) % 256;
                }
                else
                {
                    init = 0;
                }

                Random rand = new Random(init);
                while (full_key.Count < 32)
                {
                    full_key.Add(Convert.ToByte(rand.Next(256)));                    
                }
                return;
            }

            if (full_key.Count > 32)
            {
                full_key.RemoveRange(32, full_key.Count - 32);
            }
        }
        /// <summary>
        /// Заполнение массива Ki
        /// </summary>
        /// <param name="K">Массив Ki</param>
        /// <param name="key">Исходный полный ключ</param>
        void FillK(ref List<List<byte>> K, ref List<byte> key)
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
        /// <summary>
        /// Сумма по модулю 2
        /// </summary>
        /// <param name="N1">Первое слагаемое</param>
        /// <param name="N2">Второе слагаемое</param>
        /// <returns>Сумма по модулю 2</returns>
        List<byte> SUM1(List<byte> N1, List<byte> N2)
        {
            if (N1.Count != N2.Count)
            {
                throw new IndexOutOfRangeException("N1.Count isn't equal to N2.Count");
            }
            int count = N1.Count;
            List<byte> result = new List<byte>();

            UInt32 ui1;
            UInt32 ui2;

            for (int i = 0; i < count; i++)
            {
                ui1 = Convert.ToUInt32(N1[i]);
                ui2 = Convert.ToUInt32(N2[i]);
                UInt32 resUI = ui1 ^ ui2;

                result.Add(Convert.ToByte(resUI));
            }
            return result;
        }
        /// <summary>
        /// Сумма по модулю 2^32
        /// </summary>
        /// <param name="N1">Первое слагаемое</param>
        /// <param name="N2">Второе слагаемое</param>
        /// <returns>Сумма по модулю 2^32</returns>
        List<byte> SUM32(List<byte> N1, List<byte> N2)
        {
            StringBuilder sb1 = ToStringBuilder(N1, 8);
            StringBuilder sb2 = ToStringBuilder(N2, 8);

            UInt32 ui1 = Convert.ToUInt32(sb1.ToString(), 2);
            UInt32 ui2 = Convert.ToUInt32(sb2.ToString(), 2);
            UInt64 MAX = Convert.ToUInt64(UInt32.MaxValue) + 1;

            UInt32 resUI = Convert.ToUInt32((ui1 + ui2) % MAX);
            StringBuilder resSB = new StringBuilder(Convert.ToString(resUI, 2));

            while (resSB.Length < 32)
            {
                resSB.Insert(0, '0');
            }

            // На следующем шаге раунда нам нужно разбить блок на 8 шт. 4-битовых подблоков
            // Сделаем это сразу здесь
            List<byte> result = ToList(resSB, 4);
            return result;
        }
        /// <summary>
        /// Переводит данные, представленные в виде последовательности байт в
        /// в соответствующую последовательность из нулей и единиц 
        /// </summary>
        /// <param name="block">Исходный массив байтов</param>
        /// <param name="count">Разрядность числа в последовательности</param>
        /// <returns>Данные в виде последовательности из нулей и единиц </returns>
        StringBuilder ToStringBuilder(List<byte> block, int count)
        {
            StringBuilder result = new StringBuilder();
            StringBuilder tmp = new StringBuilder();
            foreach (byte element in block)
            {
                tmp.Clear();
                tmp.Append(Convert.ToString(element, 2));
                while (tmp.Length < count)
                {
                    tmp.Insert(0, '0');
                }
                result.Append(tmp);
            }

            return result;
        }
        /// <summary>
        /// Переводит текущее представление данных в виде последовательности из
        /// нулей и единиц в последовательность байтов
        /// </summary>
        /// <param name="SB">Данные в виде последовательности нулей и единиц</param>
        /// <param name="count">По сколько "отщипывать" от последовательности</param>
        /// <returns>Представление исходных данных в виде последовательности байт</returns>
        List<byte> ToList(StringBuilder SB, int count)
        {
            List<byte> result = new List<byte>();
            string str = SB.ToString();

            for (int i = 0; i < str.Length; i += count)
            {
                result.Add(Convert.ToByte(str.Substring(i, 4), 2));
            }
            return result;
        }
        /// <summary>
        /// Генерация таблицы замен: 8 шт. некоторых перестановок чисел от 0 до 15.
        /// Для синхронизации генераторов случайных чисел у шифратора и дешифратора
        /// начальное значение генераторов задается с помощью вектора инициализации
        /// </summary>
        /// <returns>Таблица замен: 8 S-блоков</returns>
        List<List<int>> CreateS()
        {
            int init;
            if (initialVector.Text != "")
            {
                init = Convert.ToInt32(initialVector.Text) % 256;
            }
            else
            {
                init = 0;
            }

            Random rand = new Random(init);
            List<List<int>> S = new List<List<int>>();
            List<int> Si;
            int Sij;

            for (int i = 0; i < 8; i++)
            {
                Si = new List<int>();
                for (int j = 0; j < 16; j++)
                {
                    do
                    {
                        Sij = rand.Next(16);
                    } while (Si.Contains(Sij));

                    Si.Add(Sij);
                }
                S.Add(Si);
            }
            return S;
        }

        private void openText_TextChanged(object sender, EventArgs e)
        {
            encryptedText.Clear();
        
            if ((sender as TextBox).Text != "")
            {
                encryptionButton.Enabled = true;
            }
            else
            {
                encryptionButton.Enabled = false;
            }
        }

        private void encryptedText_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text != "")
            {
                saveButton.Enabled = true;
            }
            else
            {
                saveButton.Enabled = false;
            }
        }
    }
}
