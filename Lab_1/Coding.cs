using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab_1
{
    public class Coding
    {
        public Coding()
        {
            Globals.stopwatch = new System.Diagnostics.Stopwatch();
            Globals.calculation_time_Hamming = new List<double>();
            Globals.calculation_time_CRC = new List<double>();

            Globals.Field_13 = new Dictionary<string, int>
            {
                {"001",0 },
                {"010",1 },
                {"100",2 },
                {"101",3 },
                {"111",4 },
                {"011",5 },
                {"110",6 }
            };
            Globals.Field_11 = new Dictionary<string, int>
            {
                {"001",0 },
                {"010",1 },
                {"100",2 },
                {"011",3 },
                {"110",4 },
                {"111",5 },
                {"101",6 }
            };
            Globals.Field_14 = new Dictionary<string, int>
            {
                {"001",0 },
                {"010",1 },//cycle starts here
                {"100",2 },
                {"110",0 }
            };
            Globals.Field_15 = new Dictionary<string, int>
            {
                {"001",0 },//cycle starts here
                {"010",1 },
                {"100",2 },
                {"111",3 }
            };

            Globals.CRC_table_1 = new Dictionary<byte, string>
            {
                { 0, "a" },
                { 3, "b" },
                { 5, "c" },
                { 6, "d" },
                { 9, "e" },
                { 10, "f" },
                { 12, "g" },
                { 15, "h" },

                { 48, "i" },
                { 51, "j" },
                {53, "k" },
                { 54, "l" },
                { 57, "m" },
                { 58, "n" },
                {60, "o" },
                { 63, "p" },

                {80 , "q" },
                { 83, "r" },
                { 85, "s" },
                { 86, "t" },
                { 89, "u" },
                { 90, "v" },
                { 92, "w" },
                { 95, "x" },

                { 96, "y" },
                { 99, "z" },
                { 101, "" },
                { 102, "" },
                { 105, "" },
                { 106, "" },
                { 108, "" },
                { 111, "" },

                { 128, "" },
                { 131, "," },
                { 133, ":" },
                { 134, ";" },
                { 137, "_" },
                { 138, "?" },
                { 140, "!" },
                { 143, "(" },

                { 160, ")" },
                { 163, "конец абзаца" },
                { 165, "перенос" },
                { 166, "строчная" },
                { 169, "прописная" },
                { 170, ".пробел" },
                { 172, "." },
                { 175, "все строчные" },

                { 192, "" },
                { 195, "все прописные" },
                { 197, "" },
                { 198, "" },
                {201, "alph2" },
                { 202, "" },
                { 204, "" },
                { 207, "" },

                { 240, "alph3" },
                { 243, "" },
                { 245, "" },
                { 246, "" },
                { 249, "alph4" },
                { 250, "" },
                { 252, " " },
                { 255, "alph5" },
            };
            Globals.CRC_table_2 = new Dictionary<byte, string>
            {
                { 0, "а" },
                { 3, "б" },
                { 5, "в" },
                { 6, "г" },
                { 9, "д" },
                { 10, "е" },
                { 12, "ж" },
                { 15, "з" },

                { 48, "и" },
                { 51, "й" },
                {53, "к" },
                { 54, "л" },
                { 57, "м" },
                { 58, "н" },
                {60, "о" },
                { 63, "п" },

                {80 , "р" },
                { 83, "с" },
                { 85, "т" },
                { 86, "у" },
                { 89, "ф" },
                { 90, "х" },
                { 92, "ц" },
                { 95, "ч" },

                { 96, "ш" },
                { 99, "щ" },
                { 101, "ъ" },
                { 102, "ы" },
                { 105, "ь" },
                { 106, "э" },
                { 108, "ю" },
                { 111, "я" },

                { 128, "" },
                { 131, "," },
                { 133, ":" },
                { 134, ";" },
                { 137, "" },
                { 138, "?" },
                { 140, "!" },
                { 143, "(" },

                { 160, ")" },
                { 163, "конец абзаца" },
                { 165, "перенос" },
                { 166, "строчная" },
                { 169, "прописная" },
                { 170, ".пробел" },
                { 172, "." },
                { 175, "все строчные" },

                { 192, "" },
                { 195, "все прописные" },
                { 197, "" },
                { 198, "alph1" },
                {201, "alph2" },
                { 202, "" },
                { 204, "" },
                { 207, "" },

                { 240, "" },
                { 243, "" },
                { 245, "" },
                { 246, "" },
                { 249, "alph4" },
                { 250, "" },
                { 252, " " },
                { 255, "alph5" },
            };
            Globals.CRC_table_3 = new Dictionary<byte, string>
            {
                { 0, "а" },
                { 3, "б" },
                { 5, "в" },
                { 6, "г" },
                { 9, "д" },
                { 10, "е" },
                { 12, "є" },
                { 15, "ж" },

                { 48, "з" },
                { 51, "и" },
                {53, "і" },
                { 54, "ї" },
                { 57, "й" },
                { 58, "к" },
                {60, "л" },
                { 63, "м" },

                {80 , "н" },
                { 83, "о" },
                { 85, "п" },
                { 86, "р" },
                { 89, "с" },
                { 90, "т" },
                { 92, "у" },
                { 95, "ф" },

                { 96, "х" },
                { 99, "ц" },
                { 101, "ч" },
                { 102, "ш" },
                { 105, "щ" },
                { 106, "ь" },
                { 108, "ю" },
                { 111, "я" },

                { 128, "'" },
                { 131, "," },
                { 133, ":" },
                { 134, ";" },
                { 137, "" },
                { 138, "?" },
                { 140, "!" },
                { 143, "(" },

                { 160, ")" },
                { 163, "конец абзаца" },
                { 165, "перенос" },
                { 166, "строчная" },
                { 169, "прописная" },
                { 170, ".пробел" },
                { 172, "." },
                { 175, "все строчные" },

                { 192, "" },
                { 195, "все прописные" },
                { 197, "" },
                { 198, "alph1" },
                {201, "" },
                { 202, "" },
                { 204, "" },
                { 207, "" },

                { 240, "alph3" },
                { 243, "" },
                { 245, "" },
                { 246, "" },
                { 249, "alph4" },
                { 250, "" },
                { 252, " " },
                { 255, "alph5" },
            };
            Globals.CRC_table_4 = new Dictionary<byte, string>
            {
                { 0, "α" },
                { 3, "β" },
                { 5, "χ" },
                { 6, "δ" },
                { 9, "ε" },
                { 10, "φ" },
                { 12, "γ" },
                { 15, "η" },

                { 48, "ι" },
                { 51, "φ" },
                {53, "κ" },
                { 54, "λ" },
                { 57, "μ" },
                { 58, "ν" },
                {60, "ο" },
                { 63, "π" },

                {80 , "θ" },
                { 83, "ρ" },
                { 85, "σ" },
                { 86, "τ" },
                { 89, "υ" },
                { 90, "ω" },
                { 92, "ψ" },
                { 95, "ξ" },

                { 96, "ψ" },
                { 99, "ζ" },
                { 101, "" },
                { 102, "" },
                { 105, "" },
                { 106, "" },
                { 108, "" },
                { 111, "" },

                { 128, "" },
                { 131, "," },
                { 133, ":" },
                { 134, ";" },
                { 137, "" },
                { 138, "?" },
                { 140, "!" },
                { 143, "(" },

                { 160, ")" },
                { 163, "конец абзаца" },
                { 165, "перенос" },
                { 166, "строчная" },
                { 169, "прописная" },
                { 170, ".пробел" },
                { 172, "." },
                { 175, "все строчные" },

                { 192, "" },
                { 195, "все прописные" },
                { 197, "" },
                { 198, "alph1" },
                {201, "alph2" },
                { 202, "" },
                { 204, "" },
                { 207, "" },

                { 240, "alph3" },
                { 243, "" },
                { 245, "" },
                { 246, "" },
                { 249, "" },
                { 250, "" },
                { 252, " " },
                { 255, "alph5" },

            };
            Globals.CRC_table_5 = new Dictionary<byte, string>
            {
                { 0, "0" },
                { 3, "1" },
                { 5, "2" },
                { 6, "3" },
                { 9, "4" },
                { 10, "5" },
                { 12, "6" },
                { 15, "7" },

                { 48, "8" },
                { 51, "9" },
                {53, "-" },
                { 54, "+" },
                { 57, "--" },
                { 58, "}" },
                {60, "{" },
                { 63, "]" },

                {80 , "[" },
                { 83, "'" },
                { 85, "" },
                { 86, "" },
                { 89, "" },
                { 90, "" },
                { 92, "" },
                { 95, "" },

                { 96, "*" },
                { 99, "&" },
                { 101, "#" },
                { 102, "@" },
                { 105, "$" },
                { 106, "%" },
                { 108, "^" },
                { 111, "№" },

                { 128, "~" },
                { 131, "," },
                { 133, ":" },
                { 134, ";" },
                { 137, "" },
                { 138, "?" },
                { 140, "!" },
                { 143, "(" },

                { 160, ")" },
                { 163, "конец абзаца" },
                { 165, "перенос" },
                { 166, "строчная" },
                { 169, "прописная" },
                { 170, ".пробел" },
                { 172, "." },
                { 175, "все строчные" },

                { 192, "" },
                { 195, "все прописные" },
                { 197, "" },
                { 198, "" },
                {201, "alph2" },
                { 202, "" },
                { 204, "" },
                { 207, "" },

                { 240, "alph3" },
                { 243, "" },
                { 245, "" },
                { 246, "" },
                { 249, "alph4" },
                { 250, "" },
                { 252, " " },
                { 255, "alph5" },

            };
        }
        #region CRC_1
        byte[] MessagetoBinary_H(string message)
        {
            return Encoding.ASCII.GetBytes(message);
        }
        List<byte> MessagetoBinary_C(string message)
        {
            List<byte> data = new List<byte>();
            byte mykey;
            for (int i = 0; i < message.Length; i++)
            {
                char symbol = message[i];
                if (Char.IsUpper(message[i]))
                {
                    mykey = Globals.CRC_table_1.FirstOrDefault(x => x.Value == "прописная").Key;
                    data.Add(mykey);
                    symbol = Char.ToLower(symbol);
                }
                mykey = Globals.CRC_table_1.FirstOrDefault(x => x.Value == symbol.ToString()).Key;
                data.Add(mykey);
            }
            return data;
        }
        public string CRC_Coded(string message)
        {
            List<byte> data = MessagetoBinary_C(message);
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
                    coded_crc += Convert.ToInt32((Globals.ChartoBit(raw_data[i]) ^
                        Globals.ChartoBit(raw_data[i + 1]) ^
                        Globals.ChartoBit(raw_data[i + 2])));
                }
                else
                {
                    char dummy = '0';
                    if (i + 1 > raw_data.Length)
                    {
                        coded_crc += String.Concat(raw_data[i], dummy, dummy);
                        coded_crc += Convert.ToInt32((Globals.ChartoBit(raw_data[i]) ^
                       Globals.ChartoBit(dummy) ^
                     Globals.ChartoBit(dummy)));

                    }
                    else
                    {
                        coded_crc += String.Concat(raw_data[i], raw_data[i + 1], dummy);
                        coded_crc += Convert.ToInt32((Globals.ChartoBit(raw_data[i]) ^
                            Globals.ChartoBit(raw_data[i + 1]) ^
                            Globals.ChartoBit(dummy)));
                    }
                }
            }
            return coded_crc;
        }
        #endregion CRC_1
        #region Hamming
        public void CRC4_Coded(string message, ref List<string> coded_data)
        {
            coded_data = new List<string>();

            List<byte> data = MessagetoBinary_C(message);

            for (int i = 0; i < data.Count; i += 4)
            {
                string raw_data = "";
                for (int j = 0; j < 4 && j + i < data.Count; j++)
                {
                    raw_data += Convert.ToString(data[i + j], 2).PadLeft(8, '0');
                }
                //11
                string check_1 = Globals.CreateChecksum(String.Concat(raw_data, "000"),
                    new List<bool> { true, true, false, true });
                //13
                string check_2 = Globals.CreateChecksum(String.Concat(raw_data, "000"),
                    new List<bool> { true, false, true, true });
                //14
                string check_3 = Globals.CreateChecksum(String.Concat(raw_data, "000"),
                    new List<bool> { false, true, true, true });
                //15
                string check_4 = Globals.CreateChecksum(String.Concat(raw_data, "000"),
                    new List<bool> { true, true, true, true });

                coded_data.Add(String.Concat(raw_data, check_1, check_2, check_3, check_4));
            }
        }

        void Block_Coding(ref List<List<bool>> code, int i, byte b, int push)
        {
            code.Add(new List<bool>());
            for (int k = 0; k < 7; k++)
            {
                code[i].Add(false);
            }
            string raw_data = "";
            raw_data += Convert.ToString(b, 2).PadLeft(8, '0');

            code[i][0] = Globals.ChartoBit(raw_data[push]) ^
               Globals.ChartoBit(raw_data[push + 1]) ^
                Globals.ChartoBit(raw_data[push + 3]);

            code[i][1] = Globals.ChartoBit(raw_data[push]) ^
                Globals.ChartoBit(raw_data[push + 2]) ^
                Globals.ChartoBit(raw_data[push + 3]);

            code[i][2] = Globals.ChartoBit(raw_data[push]);

            code[i][3] = Globals.ChartoBit(raw_data[push + 1]) ^
                Globals.ChartoBit(raw_data[push + 2]) ^
                Globals.ChartoBit(raw_data[push + 3]);

            code[i][4] = Globals.ChartoBit(raw_data[push + 1]);
            code[i][5] = Globals.ChartoBit(raw_data[push + 2]);
            code[i][6] = Globals.ChartoBit(raw_data[push + 3]);
        }
        public void Hamming_Coded(string message, ref List<List<bool>> code)
        {
            int i = 0;
            byte[] data = MessagetoBinary_H(message);

            foreach (byte b in data)
            {
                Block_Coding(ref code, i, b, 0);
                i++;
                Block_Coding(ref code, i, b, 4);
                i++;
            }
        }
        #endregion Hamming

    }
}
