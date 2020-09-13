using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab_1
{
    public static class Error_Gen
    {
        //errors for entire coded message
        private static void GenerateErrorPool(double probability, int size, ref List<bool> target)
        {
            int error_number = (int)Math.Ceiling((double)size / ((double)size * probability));
            int error_idicator = 0;

            target = new List<bool>();

            for (int i = 0; i < size; i++)
            {
                if (error_idicator == error_number)
                {
                    error_idicator = 0;
                    target.Add(true);
                }
                else
                {
                    error_idicator++;
                    target.Add(false);
                }
            }
        }
        //crc_errors
        public static List<string> ApplyErrors(List<string> data, double prob)
        {
            List<string> dat = new List<string>();
            int block = 0;
            GenerateErrorPool(prob, Globals.ListLength(data), ref Globals.errors_CRC);
            for (int i = 0; i < data.Count; i++)
            {
                if (i == 0) block = 0;
                else block = data[i - 1].Length;
                List<bool> errors = Globals.errors_CRC.GetRange(i * block, data[i].Length);
                BitArray err = new BitArray(errors.ToArray());
                BitArray dt = new BitArray(Globals.StringtoBitArray(data[i]).ToArray());
                dt.Xor(err);
                dat.Add(Globals.BitArraytoString(dt));
            }
            return dat;
        }
        //hamming_errors
        public static List<List<bool>> ApplyErrors(List<List<bool>> data, double prob)
        {
            List<List<bool>> dat = new List<List<bool>>();
            int block = 0;
            GenerateErrorPool(prob, Globals.ListLength(data), ref Globals.errors_Hamming);
            for (int i = 0; i < data.Count; i++)
            {
                if (i == 0) block = 0;
                else block = data[i - 1].Count;
                List<bool> errors = Globals.errors_Hamming.GetRange(i * block, data[i].Count);
                BitArray err = new BitArray(errors.ToArray());
                BitArray dt = new BitArray(data[i].ToArray());
                dt.Xor(err);
                dat.Add(new List<bool> { dt[0], dt[1], dt[2], dt[3], dt[4], dt[5], dt[6] });
            }
            return dat;
        }
    }
}
