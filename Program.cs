using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lab_1
{
    public class Coding
    {
        Dictionary<byte, string> CRC_table;
        public static bool CtoB(char value)
        {
            if (value == '1') return true;
            else return false;
        }
        public Coding()
        {
            CRC_table = new Dictionary<byte, string>
            {
                { 0, "a" },
                { 1, "b" },
                { 2, "c" },
                { 3, "d" },
                { 4, "e" },
                { 5, "f" },
                { 6, "g" },
                { 7, "h" },
                { 8, "i" },
                { 9, "j" },
                { 10, "k" },
                { 11, "l" },
                { 12, "m" },
                { 13, "n" },
                { 14, "o" },
                { 15, "p" },
                { 16, "q" },
                { 17, "r" },
                { 18, "s" },
                { 19, "t" },
                { 20, "u" },
                { 21, "v" },
                { 22, "w" },
                { 23, "x" },
                { 24, "y" },
                { 25, "z" },
                { 26, "" },
                { 27, "" },
                { 28, "" },
                { 29, "" },
                { 30, "" },
                { 31, "" },
                { 32, "" },
                { 33, "," },
                { 34, ":" },
                { 35, ";" },
                { 36, "_" },
                { 37, "?" },
                { 38, "!" },
                { 39, "(" },
                { 40, ")" },
                { 41, "конец абзаца" },
                { 42, "Перенос" },
                { 43, "строчная" },
                { 44, "прописная" },
                { 45, ".пробел" },
                { 46, "." },
                { 47, "все строчные" },
                { 48, "" },
                { 49, "все прописные" },
                { 50, "" },
                { 51, "" },
                { 52, "" },
                { 53, "" },
                { 54, "" },
                { 55, "" },
                { 56, "" },
                { 57, "" },
                { 58, "" },
                { 59, "" },
                { 60, "" },
                { 61, "" },
                { 62, " " },
                { 63, "" }
            };
        }
        public List<byte> CodeCRC(string message)
        {
            List<byte> data = new List<byte>();
            for (int i = 0; i < message.Length; i++)
            {
                char symbol = message[i];
                if (Char.IsUpper(message[i]))
                {
                    data.Add(44);
                    symbol = Char.ToLower(symbol);
                }
                var myKey = CRC_table.FirstOrDefault(x => x.Value == symbol.ToString()).Key;
                data.Add(myKey);
            }
            return data;
        }
        public string Initia_CRC(string message)
        {
            List<byte> data = CodeCRC(message);
            string raw_data = "";
            string coded_crc = "";
            foreach (byte b in data)
            {
                raw_data += Convert.ToString(b, 2).PadLeft(8, '0');
            }
            for (int i = 0; i < raw_data.Length; i += 3)
            {
                if (i + 2 <= raw_data.Length - 1)
                {
                    coded_crc += String.Concat(raw_data[i], raw_data[i + 1], raw_data[i + 2]);
                    coded_crc += Convert.ToInt32((CtoB(raw_data[i]) ^
                        CtoB(raw_data[i + 1]) ^
                        CtoB(raw_data[i + 2])));
                }
                else
                {
                    char dummy = '0';
                    if (i + 1 > raw_data.Length)
                    {
                        coded_crc += String.Concat(raw_data[i], dummy, dummy);
                        coded_crc += Convert.ToInt32((CtoB(raw_data[i]) ^
                       CtoB(dummy) ^
                      CtoB(dummy)));

                    }
                    else
                    {
                        coded_crc += String.Concat(raw_data[i], raw_data[i + 1], dummy);
                        coded_crc += Convert.ToInt32((CtoB(raw_data[i]) ^
                            CtoB(raw_data[i + 1]) ^
                            CtoB(dummy)));
                    }
                }
            }
            return coded_crc;
        }
        public string Decode_CRC(string coded_message)
        {
            int error_counter = 0,sum_flag=0;
            string output = "";
            string letter = "";
            string buffer = "";
            bool parity_bit_new = false;
            bool upper_case = false;
            for (int i = 0; i < coded_message.Length; i++)
            {
                if (sum_flag == 3)
                {
                    //new sum
                    parity_bit_new = (CtoB(coded_message[i - 3]) ^
                        CtoB(coded_message[i - 2]) ^
                        CtoB(coded_message[i - 1]));
                    if (parity_bit_new != CtoB(coded_message[i]))
                    {
                        error_counter++;
                    }
                    sum_flag = 0;
                }
                else
                {
                    sum_flag++;
                    if (letter.Length >= 8) 
                    {
                        if (CRC_table.TryGetValue(Convert.ToByte(letter, 2), out buffer))
                        {
                            if (buffer == "прописная") upper_case = true;
                            else
                            {
                                if (upper_case)
                                {
                                    upper_case = false;
                                    output += buffer.ToUpper();

                                }
                                else
                                    output += buffer;
                            }
                        }
                        letter = "";
                    }
                    letter += coded_message[i];
                }


            }
            return output;
        }

    }
    static class Program
    {

        public static bool GetBit(this byte b, int bitNumber)
        {
            return (b & (1 << bitNumber)) != 0;
        }
        public static bool CtoB(char value)
        {
            if (value == '1') return true;
            else return false;
        }
        public static void Initia(ref List<List<bool>> code, int i, byte b, int push)
        {
            code.Add(new List<bool>());
            for (int k = 0; k < 7; k++)
            {
                code[i].Add(false);
            }
            string raw_data = "";
            raw_data += Convert.ToString(b, 2).PadLeft(8, '0');

            code[i][0] = CtoB(raw_data[push]) ^
               CtoB(raw_data[push + 1]) ^
                CtoB(raw_data[push + 3]);

            code[i][1] = CtoB(raw_data[push]) ^
                CtoB(raw_data[push + 2]) ^
                CtoB(raw_data[push + 3]);

            code[i][2] = CtoB(raw_data[push]);

            code[i][3] = CtoB(raw_data[push + 1]) ^
                CtoB(raw_data[push + 2]) ^
                CtoB(raw_data[push + 3]);

            code[i][4] = CtoB(raw_data[push + 1]);
            code[i][5] = CtoB(raw_data[push + 2]);
            code[i][6] = CtoB(raw_data[push + 3]);
        }
        public static string Decode_Hamming(List<bool> data)
        {
            //new check bits
            bool p_1 = data[2] ^ data[4] ^ data[6];
            bool p_2 = data[2] ^ data[5] ^ data[6];
            bool p_3 = data[4] ^ data[5] ^ data[6];

            if (p_1 != data[0])
            {
                if (p_2 != data[1])
                {
                    if (p_3 != data[3])
                    {
                        data[6] = !data[6];//last data error
                    }
                    else
                    {
                        data[2] = !data[2];//firs data error
                    }
                }
                else if(p_3 != data[3])
                {
                    data[4] = !data[4];//second data error
                }
                //else error in p_1

            }
            else if (p_2 != data[1])
            {
                if (p_3 != data[3])
                {
                    data[5] = !data[5];//third data error
                }
                //else error in p_2
            }
            //else error in p_3
            //partial data
            return String.Concat(Convert.ToInt32(data[2]), Convert.ToInt32(data[4]), 
                Convert.ToInt32(data[5]), Convert.ToInt32(data[6]));
        }
        static void Main(string[] args)
        {
            List<List<bool>> coded_Hemming = new List<List<bool>>();
                        string FIO = "Rublova Viktoriia Hryhorevna";

            int i = 0;
            Coding exa = new Coding();

            string coded_crc = exa.Initia_CRC(FIO);
            string decoded_crc = exa.Decode_CRC(coded_crc);

            byte[] data = Encoding.ASCII.GetBytes(FIO);

            foreach (byte b in data)
            {
                Initia(ref coded_Hemming, i, b, 0);
                i++;
                Initia(ref coded_Hemming, i, b, 4);
                i++;
            }
            //decode hamming
            string letter = "";
            string output = "";
            foreach (List<bool> l_b in coded_Hemming)
            {
                letter += Decode_Hamming(l_b);
                if (letter.Length == 8)
                {
                    byte b = Convert.ToByte(letter,2);
                    char symbol = (char)b;
                    output += symbol;
                    letter = "";
                }
            }

            foreach (byte b in data)
            {
                Console.WriteLine(Convert.ToString(b, 2));
            }
            Console.ReadKey();
        }
    }
}
