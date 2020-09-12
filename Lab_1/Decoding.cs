using System;
using System.Collections.Generic;

namespace Lab_1
{
    class Decoding
    {
        public Decoding() { }
        
        public string CRC_Decoded(string coded_message)
        {
            string output = "";
            string letter = "";
            string buffer = "";
            bool upper_case = false;
            for (int i = 0; i < coded_message.Length; i++)
            {
                letter += coded_message[i];
                if (letter.Length >= 8)
                    {
                        if (Globals.CRC_table_1.TryGetValue(Convert.ToByte(letter, 2), out buffer))
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
                    
            }
            return output;
        }
        string GenerateErrorMarkers_4(string check, int data_length, int number_switch)
        {
            string marker = "";
            int key = -1;
            bool error_trigger = false;
            switch (number_switch)
            {
                case 11:
                    {
                        error_trigger = Globals.Field_11.TryGetValue(check, out key);
                        if (error_trigger)
                        {
                            int back_cycle = 0;
                            for (int i = 0; i < data_length + 3; i++)
                            {
                                if (back_cycle == key) marker=marker.Insert(0, "1");
                                else
                                {
                                    marker=marker.Insert(0, "0");
                                }
                                back_cycle++;
                                if (back_cycle == 7) back_cycle = 0;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < data_length + 3; i++)
                            {
                                marker=marker.Insert(0, "0");
                            }
                        }
                        break;
                    }
                case 13:
                    {
                        error_trigger = Globals.Field_13.TryGetValue(check, out key);
                        if (error_trigger)
                        {
                            int back_cycle = 0;
                            for (int i = 0; i < data_length + 3; i++)
                            {
                                if (back_cycle == key) marker=marker.Insert(0, "1");
                                else
                                {
                                    marker = marker.Insert(0, "0");
                                }
                                back_cycle++;
                                if (back_cycle == 7) back_cycle = 0;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < data_length + 3; i++)
                            {
                                marker = marker.Insert(0, "0");
                            }
                        }
                        break;
                    }
                case 14:
                    {
                        error_trigger = Globals.Field_14.TryGetValue(check, out key);
                        if (error_trigger)
                        {
                            int back_cycle = 0;
                            for (int i = 0; i < data_length + 3; i++)
                            {
                                if (back_cycle == key) marker = marker.Insert(0, "1");
                                else
                                {
                                    marker = marker.Insert(0, "0");
                                }
                                back_cycle++;
                                if (back_cycle == 3) back_cycle = 0;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < data_length + 3; i++)
                            {
                                marker = marker.Insert(0, "0");
                            }
                        }
                        break;
                    }
                case 15:
                    {
                        error_trigger = Globals.Field_15.TryGetValue(check, out key);
                        if (error_trigger)
                        {
                            int back_cycle = 0;
                            for (int i = 0; i < data_length + 3; i++)
                            {
                                if (back_cycle == key) marker = marker.Insert(0, "1");
                                else
                                {
                                    marker = marker.Insert(0, "0");
                                }
                                back_cycle++;
                                if (back_cycle == 4) back_cycle = 0;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < data_length + 3; i++)
                            {
                                marker = marker.Insert(0, "0");
                            }
                        }
                        break;
                    }
                default: break;
            }
            return marker.Substring(0,marker.Length-3);
        }
        string GenerateErrorMarkers_1(string data)
        {
            bool sum = true;
            bool is_empty = true;
            string marker = "";
            int sum_flag = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (is_empty)
                {
                    sum = Globals.ChartoBit(data[i]);
                    is_empty = false;
                    sum_flag++;
                }
                else
                {
                    if (sum_flag < 3)
                    {
                        sum ^= Globals.ChartoBit(data[i]);
                        sum_flag++;
                    }
                    else
                    {
                        if (Globals.BitToChar(sum) == data[i])
                        {
                            marker += "0000";
                        }
                        else
                        {
                            marker += "1110";
                        }
                        is_empty = true;
                        sum_flag = 0;
                    }
                }
            }
            return marker;
        }
        public string CRC4_Decoded(List<string> message)
        {
            string decoded = "";
            Globals.stopwatch = System.Diagnostics.Stopwatch.StartNew();
            foreach (string coded_message in message)
            {
                string data = coded_message.Substring(0, coded_message.Length - 12);
                string cs_1_data = String.Concat(data, coded_message.Substring(coded_message.Length - 12, 3));//11
                string cs_2_data = String.Concat(data, coded_message.Substring(coded_message.Length - 9, 3));//13
                string cs_3_data = String.Concat(data, coded_message.Substring(coded_message.Length - 6, 3));//14
                string cs_4_data = String.Concat(data, coded_message.Substring(coded_message.Length - 3, 3));//15
                                                                                                             //11
                string check_1 = Globals.CreateChecksum(cs_1_data,
                    new List<bool> { true, true, false, true });
                //13
                string check_2 = Globals.CreateChecksum(cs_2_data,
                    new List<bool> { true, false, true, true });
                //14
                string check_3 = Globals.CreateChecksum(cs_3_data,
                    new List<bool> { false, true, true, true });
                //15
                string check_4 = Globals.CreateChecksum(cs_4_data,
                    new List<bool> { true, true, true, true });
                List<string> error_markers = new List<string>();
                error_markers.Add(GenerateErrorMarkers_1(data));
                error_markers.Add(GenerateErrorMarkers_4(check_1, data.Length, 11));
                error_markers.Add(GenerateErrorMarkers_4(check_2, data.Length, 13));
                error_markers.Add(GenerateErrorMarkers_4(check_3, data.Length, 14));
                error_markers.Add(GenerateErrorMarkers_4(check_4, data.Length, 15));

                //fix
                for (int i = 0; i < error_markers[0].Length; i++)
                {
                    int majority = 1;
                    if (error_markers[0][i] == '1')
                    {
                        for (int j = 1; j < error_markers.Count; j++)
                        {
                            if (error_markers[j][i] == '1') majority++;
                            else majority--;
                        }
                        if (majority > -1) data = data.Remove(i, 1).Insert(i, Globals.Not(data[i]).ToString());
                    }
                }
                //decode at last
                decoded+= this.CRC_Decoded(data);
            }
            Globals.stopwatch.Stop();
            Globals.calculation_time_CRC.Add(Globals.stopwatch.ElapsedMilliseconds);
            return decoded;
        }
        string Block_Decoding(List<bool> coded_data)
        {
            //new check bits
            bool p_1 = coded_data[2] ^ coded_data[4] ^ coded_data[6];
            bool p_2 = coded_data[2] ^ coded_data[5] ^ coded_data[6];
            bool p_3 = coded_data[4] ^ coded_data[5] ^ coded_data[6];

            if (p_1 != coded_data[0])
            {
                if (p_2 != coded_data[1])
                {
                    if (p_3 != coded_data[3])
                    {
                        coded_data[6] = !coded_data[6];//last data error
                    }
                    else
                    {
                        coded_data[2] = !coded_data[2];//first data error
                    }
                }
                else if (p_3 != coded_data[3])
                {
                    coded_data[4] = !coded_data[4];//second data error
                }
                //else error in p_1

            }
            else if (p_2 != coded_data[1])
            {
                if (p_3 != coded_data[3])
                {
                    coded_data[5] = !coded_data[5];//third data error
                }
                //else error in p_2
            }
            //else error in p_3
            //partial data
            return String.Concat(Convert.ToInt32(coded_data[2]), Convert.ToInt32(coded_data[4]),
                Convert.ToInt32(coded_data[5]), Convert.ToInt32(coded_data[6]));
        }
        public string Hamming_Decoded(List<List<bool>> code)
        {
            string letter = "";
            string output = "";
            Globals.stopwatch = System.Diagnostics.Stopwatch.StartNew();
            foreach (List<bool> l_b in code)
            {
                letter += Block_Decoding(l_b);
                if (letter.Length == 8)
                {
                    byte b = Convert.ToByte(letter, 2);
                    char symbol = (char)b;
                    output += symbol;
                    letter = "";
                }
            }
            Globals.stopwatch.Stop();
            Globals.calculation_time_CRC.Add(Globals.stopwatch.ElapsedMilliseconds);
            return output;
        }
    }
}
