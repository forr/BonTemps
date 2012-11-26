using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonTemps
{
    public class CSVFile
    {
        /// <summary>
        /// This will construct a string for a "Comma-separated values(CSV)" file. US STANDARD
        /// </summary>
        /// <param name="csvArray">Use \n to define a new collumn. Each string added to list equals to a new row.</param>
        /// <returns>CSV formated string</returns>
        public static string[] CreateCsvString(List<String> stringArray)
        {
            List<String> csvArray = new List<String>();
            foreach (String s in stringArray)
            {
                string temp = s;
                temp = temp.Replace(",", ",,");
                temp = temp.Replace("\n", ",");
                temp = temp.Remove(temp.Length - 1);
                csvArray.Add(temp);
            }

            string[] solution = new string[csvArray.Count + 1];
            int solutioncounter = 0;
            solution[solutioncounter] = "types";

            foreach (String s in csvArray)
            {
                solutioncounter++;
                solution[solutioncounter] = s;
            }

            return solution;
        }

        /// <summary>
        /// This will construct a string for a "Comma-separated values(CSV)" file. REGION FREE
        /// </summary>
        /// <param name="csvArray">Use your separationcharacter(or \n) to define a new collumn. Each string added to list equals to a new row.</param>
        /// <param name="seperator">Add a custom seperator (this will likely edit the string drasticly).</param>
        /// <returns>CSV formated string</returns>
        public static string[] CreateCsvString(List<String> csvArray, string seperator)
        {
            foreach (String s in csvArray)
            {
                string temp = s;
                if (seperator == ",")
                {
                    //
                }
                else
                {
                    temp = temp.Replace(",", ",,");
                    temp = temp.Replace(seperator, ",");
                }
                if(temp.LastIndexOf(",") == temp.Length)
                {
                    temp = temp.Remove(temp.Length - 1);
                }
                csvArray.Add(temp);
            }

            string[] solution = new string[csvArray.Count + 1];
            int solutioncounter = 0;
            solution[solutioncounter] = "types";

            foreach (String s in csvArray)
            {
                solutioncounter++;
                solution[solutioncounter] = s;
            }

            return solution;
        }
    }
}
