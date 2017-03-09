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
    public static class Helpers
    {
      /// <summary>
      /// Основной шаг
      /// </summary>
      /// <param name="A"></param>
      /// <param name="B"></param>
      /// <param name="K"></param>
      /// <param name="isLastRaund"></param>
      /// <param name="init"></param>
      /// <returns></returns>
        public static List<Byte> Basic(List<byte> block, List<byte> K, bool isLastRaund, ref TextBox init)
        {
            // 64-битные (8-байтные) блоки открытого текста разбиваются попалам
            // на фрагменты A и B (по 4 байта)
            List<byte> A = new List<byte>(block.GetRange(0, 4));
            List<byte> B = new List<byte>(block.GetRange(4, 4));

            List<byte> S = SUM1(B, Function(A, K, isLastRaund, ref init));
            if (!isLastRaund)
            {
                B = A;
                A = S;
            }
            else
            {
                B = S;
            }
            List<byte> result = new List<byte>();

            // Зашифрованные блоки A и B склеиваются
            result.AddRange(A);
            result.AddRange(B);

            return result;
        }

        public static List<byte> Function(List<byte> A, List<byte> K, bool isLastRaund, ref TextBox init)
        {
            // Сумма по модулю 2^32
            List<byte> sum = Helpers.SUM32(A, K);

            // Замены с помощью S-блоков
            List<List<int>> S = Helpers.CreateS(ref init);
            UInt16 s_index;

            for (int i = 0; i < sum.Count; i++)
            {
                s_index = Convert.ToUInt16(sum[i]);
                sum[i] = Convert.ToByte(S[i][s_index]);
            }

            // Циклический сдвиг влево на 11 позиций
            StringBuilder SB = Helpers.ToStringBuilder(sum, 4);
            for (int i = 0; i < 11; i++)
            {
                SB.Append(SB[0]);
                SB.Remove(0, 1);
            }

            // Формирование результирующего слова
            sum = Helpers.ToList(SB, 8);

            return sum;
        }

        /// <summary>
        /// Считывает текст из поля ввода открытого текста и распределяет его
        /// по 8-байтным блокам
        /// </summary>
        /// <param name="text">Переменная для хранения открытого текста</param>
        public static void GetText(ref List<List<byte>> text, ref TextBox field)
        {
            // Считаем весь текст, что есть
            List<byte> tmp_block = new List<byte>();
            tmp_block.AddRange(Encoding.Unicode.GetBytes(field.Text));

            text.Clear();

            // Так как на вход шифратору передаются 64 битные (8 байтные) блоки
            // данных, то сразу разобьем text на блоки по  байт каждый
            for (int i = 0; i < tmp_block.Count; i += 8)
            {
                try
                {
                    text.Add(tmp_block.GetRange(i, 8));
                }
                // если число байт в исходном тексте не кратно 8,
                // искуственно добавим их (доставим NULL'ы)
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
        public static void MakeValidKey(ref List<byte> full_key, ref TextBox init)
        {
            if (full_key.Count < 32)
            {
                // Чтобы синхронизировать генераторы севдослучайных чисел у
                // шифратора и дешифратора, явно инициализируем первое значение
                // генератора
                int initSTR;
                if (init.Text != "")
                {
                    initSTR = Convert.ToInt32(init.Text) % 256;
                }
                else
                {
                    initSTR = 0;
                }

                Random rand = new Random(initSTR);
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
        public static void FillK(ref List<List<byte>> K, ref List<byte> key, bool isEncryption)
        {
            if (isEncryption)
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
            else
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
        }
        /// <summary>
        /// Сумма по модулю 2
        /// </summary>
        /// <param name="N1">Первое слагаемое</param>
        /// <param name="N2">Второе слагаемое</param>
        /// <returns>Сумма по модулю 2</returns>
        public static List<byte> SUM1(List<byte> N1, List<byte> N2)
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
        public static List<byte> SUM32(List<byte> N1, List<byte> N2)
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
        public static StringBuilder ToStringBuilder(List<byte> block, int count)
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
        public static List<byte> ToList(StringBuilder SB, int count)
        {
            List<byte> result = new List<byte>();
            string str = SB.ToString();

            for (int i = 0; i < str.Length; i += count)
            {
                result.Add(Convert.ToByte(str.Substring(i, count), 2));
            }
            return result;
        }
        /// <summary>
        /// Генерация таблицы замен: 8 шт. некоторых перестановок чисел от 0 до 15.
        /// Для синхронизации генераторов случайных чисел у шифратора и дешифратора
        /// начальное значение генераторов задается с помощью вектора инициализации
        /// </summary>
        /// <returns>Таблица замен: 8 S-блоков</returns>
        public static List<List<int>> CreateS(ref TextBox init)
        {
            int initSTR;
            if (init.Text != "")
            {
                initSTR = Convert.ToInt32(init.Text) % 256;
            }
            else
            {
                initSTR = 0;
            }

            Random rand = new Random(initSTR);
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
    }
}
