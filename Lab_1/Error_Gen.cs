using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Lab_1
{
    public static class Error_Gen
    {
        static Random random;

        public static List<bool> GenerateError(double probability, int size)
        {
            int error_number = (int)(size * probability);
            int error_idicator = 0;
            List<bool> error_pool = new List<bool>();

            for (int i = 0; i < size; i++)
            {
                if (error_idicator == error_number)
                {
                    error_idicator = 0;
                    error_pool.Add(true);
                }
                else
                {
                    error_idicator++;
                    error_pool.Add(false);
                }
            }
            //random = new Random(DateTime.Now.Millisecond);

            //List<bool> error_pool = new List<bool>();
            //for (int i = 0; i < size; i++)
            //{
            //    if (random.NextDouble() <= probability)
            //    {
            //        error_pool.Add(true);
            //    }
            //    else
            //    {
            //        error_pool.Add(false);
            //    }
            //}
            return error_pool;
        }
        public static List<string> ApplyErrors(List<string> data, double prob)
        {
            List<string> dat = new List<string>();

            for (int i = 0; i < data.Count; i++)
            {
                List<bool> errors = GenerateError(prob, data[i].Length);
                BitArray err = new BitArray(errors.ToArray());
                BitArray dt = new BitArray(Globals.StringtoBitArray(data[i]).ToArray());
                dt.Xor(err);
                dat.Add(Globals.BitArraytoString(dt));
            }
            return dat;
        }
        public static List<List<bool>> ApplyErrors(List<List<bool>> data, double prob)
        {
            List<List<bool>> dat = new List<List<bool>>();
            for(int i = 0; i < data.Count; i++)
            {
                List<bool> errors = GenerateError(prob, data[i].Count);
                BitArray err = new BitArray(errors.ToArray());
                BitArray dt = new BitArray(data[i].ToArray());
                dt.Xor(err);
                dat.Add(new List<bool> { dt[0], dt[1], dt[2], dt[3], dt[4], dt[5], dt[6] });
            }
            return dat;
        }
    }
}
