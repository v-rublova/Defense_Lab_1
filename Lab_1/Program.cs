using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Lab_1
{
    public static class Globals
    {
        #region globals
        public static Dictionary<byte, string> CRC_table_1;
        public static Dictionary<byte, string> CRC_table_2;
        public static Dictionary<byte, string> CRC_table_3;
        public static Dictionary<byte, string> CRC_table_4;
        public static Dictionary<byte, string> CRC_table_5;

        public static Dictionary<string, int> Field_11;
        public static Dictionary<string, int> Field_13;
        public static Dictionary<string, int> Field_14;
        public static Dictionary<string, int> Field_15;

        public static char Not(char c)
        {
            if (c == '1') return '0';
            else return '1';
        }
        public static bool ChartoBit(char c)
        {
            if (c == '1') return true;
            else return false;
        }
        public static char BitToChar(bool b)
        {
            if (b) return '1';
            else return '0';
        }
        public static List<bool> StringtoBitArray(string str)
        {
            List<bool> data = new List<bool>();
            for(int i = 0; i < str.Length; i++)
            {
                data.Add(ChartoBit(str[i]));
            }
            return data;
        }
        public static string BitArraytoString(BitArray data)
        {
            string str = "";
            for (int i = 0; i < data.Count; i++)
            {
                str += BitToChar(data[i]);
            }
            return str;
        }
        public static string CreateChecksum(string raw, List<bool> divider)
        {
            BitArray buffer;
            for (int i = 0; (i + 3) < raw.Length; i++)
            {
                if (raw[i] == '0') continue;

                var segment = new BitArray(new[] { Globals.ChartoBit(raw[i+3]),
                    Globals.ChartoBit(raw[i+2]),
                    Globals.ChartoBit(raw[i+1]),
                    Globals.ChartoBit(raw[i])});

                var dividerArray = new BitArray(divider.ToArray());

                buffer = segment.Xor(dividerArray);

                //write result back to raw data

                raw = raw.Remove(i, 4).Insert(i, string.Concat(Globals.BitToChar(buffer[3]),
                    Globals.BitToChar(buffer[2]), Globals.BitToChar(buffer[1]),
                    Globals.BitToChar(buffer[0])));
                i--;

            }
            return raw.Substring(raw.Length - 3, 3);
        }
        #endregion globals
    }

    static class Program
    {
        static void Main(string[] args)
        {
            //message
            string FIO = "Rublova Viktoriia Hryhorevna";
            //string FIO = "Rub";
            //coded storage
            List<List<bool>> coded_Hamming = new List<List<bool>>();
            List<string> coded_crc = new List<string>();
            //decoded storage
            string decoded_Hamming = "";
            string decoded_crc = "";
            //coding
            Coding coder = new Coding();

            coder.Hamming_Coded(FIO, ref coded_Hamming);
            coded_crc = coder.CRC4_Coded(FIO);

            Decoding decoder = new Decoding();
            //5 * Math.Pow(10, -2)
            coded_Hamming = Error_Gen.ApplyErrors(coded_Hamming, 0.1);
            coded_crc =Error_Gen.ApplyErrors(coded_crc, 0.1);

            decoded_crc=decoder.CRC4_Decoded(coded_crc);
            decoded_Hamming = decoder.Hamming_Decoded(coded_Hamming);

            Console.WriteLine(decoded_Hamming + " " + decoded_crc);
            Console.ReadKey();
        }
    }
}
