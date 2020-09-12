using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;

namespace Lab_1
{
    /// <summary>
    /// Globally used constants and methods
    /// </summary>
    public static class Globals
    {
        #region globals
        //CRC dictionary
        public static Dictionary<byte, string> CRC_table_1;
        public static Dictionary<byte, string> CRC_table_2;
        public static Dictionary<byte, string> CRC_table_3;
        public static Dictionary<byte, string> CRC_table_4;
        public static Dictionary<byte, string> CRC_table_5;

        //Galois fields for polynomials
        public static Dictionary<string, int> Field_11;
        public static Dictionary<string, int> Field_13;
        public static Dictionary<string, int> Field_14;
        public static Dictionary<string, int> Field_15;

        //measurements
        public static List<double> calculation_time_Hamming;
        public static List<double> calculation_time_CRC;

        public static List<bool> errors_Hamming;
        public static List<bool> errors_CRC;

        public static Stopwatch stopwatch;

        public static int ListLength(List<string> list)
        {
            int length = 0;
            foreach(string val in list)
            {
                length += val.Length;
            }
            return length;
        }
        public static int ListLength(List<List<bool>> list)
        {
            int length = 0;
            foreach (List<bool> val in list)
            {
                length += val.Count;
            }
            return length;
        }
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
            for (int i = 0; i < str.Length; i++)
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
            Coding coder = new Coding();
            Decoding decoder = new Decoding();

            //message
            //string FIO = "Rublova Viktoriia Hryhorivna";
            string FIO = "Rublova Viktoriia";
            Console.WriteLine("Message: " + FIO);
            //error list
            List<double> error_prob = new List<double> {5*Math.Pow(10,-2),
            3*Math.Pow(10,-2),2*Math.Pow(10,-2),Math.Pow(10,-2),9*Math.Pow(10,-3)};

            //coded storage
            List<List<bool>> coded_Hamming = new List<List<bool>>();
            List<string> coded_CRC = new List<string>();

            List<string> coded_CRC_err = new List<string>();
            List<List<bool>> coded_Hamming_err = new List<List<bool>>();
            //decoded storage
            string decoded_Hamming = "";
            string decoded_crc = "";

            //coding   
            coder.Hamming_Coded(FIO, ref coded_Hamming);
            coder.CRC4_Coded(FIO,ref coded_CRC);
            for(int j = 0; j < 10; j++)
            {
                for (int i = 0; i < error_prob.Count; i++)
                {
                    //errors
                    coded_Hamming_err = Error_Gen.ApplyErrors(coded_Hamming, error_prob[i]);
                    coded_CRC_err = Error_Gen.ApplyErrors(coded_CRC, error_prob[i]);

                    //decode
                    decoded_crc = decoder.CRC4_Decoded(coded_CRC_err);
                    decoded_Hamming = decoder.Hamming_Decoded(coded_Hamming_err);

                    //output
                    Console.WriteLine("Error probability: " + error_prob[i].ToString());

                    //Console.WriteLine("Decoded using Hamming:" + decoded_Hamming);
                   // Console.WriteLine("Time:" + Globals.calculation_time_Hamming.Last().ToString());
                    //Console.WriteLine("Decoded using CRC:    " + decoded_crc);
                    //Console.WriteLine("Time:" + Globals.calculation_time_CRC.Last().ToString());
                }
                Console.WriteLine(Globals.calculation_time_CRC[j * 5 + 0] + " " +
                    Globals.calculation_time_CRC[j * 5 + 1] + " " +
                    Globals.calculation_time_CRC[j * 5 + 2] + " " +
                    Globals.calculation_time_CRC[j * 5 + 3] + " " +
                    Globals.calculation_time_CRC[j * 5 + 4] + " ");

                Console.WriteLine(Globals.calculation_time_Hamming[j * 5 + 0] + " "+
                    Globals.calculation_time_Hamming[j * 5 + 1] + " " +
                    Globals.calculation_time_Hamming[j * 5 + 2] + " " +
                    Globals.calculation_time_Hamming[j * 5 + 3] + " " +
                    Globals.calculation_time_Hamming[j * 5 + 4]);
            }
            


            Console.ReadKey();
        }
    }
}
