using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;


namespace GC_Capstone_PigLatin
{
    class Program
    {
        static void Main(string[] args)
        {

            /// Ask for word
            while (true)
            {
                Console.WriteLine("\n Lets go, piggy. Give me a line: ");
                string line = Console.ReadLine();

                if (Regex.IsMatch(line, @"^[A-z_.,!'/$ ]+$")) /// works with contractions and allows punctuation
                {
                    string[] words = line.Split(' ');

                    foreach (string x in words)
                    {
                        if (DetectCase(x) == "upper")
                        {
                            Console.Write(Pigify(x).ToUpper() + " ");
                        }
                        if (DetectCase(x) == "lower")
                        {
                            Console.Write(Pigify(x).ToLower() + " ");
                        }
                        if (DetectCase(x) == "title")
                        {
                            Console.Write(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Pigify(x)) + " ");
                        }
                        else
                        {
                            Console.Write(Pigify(x));
                        }
                    }
                   
                }
                else
                {
                    Console.WriteLine("Did you follow the rules?"); /// catches words with numbers and symbols or null text
                }

            }
        }
        //Translate
        ///convert word to lowercase
        ///if word starts with voewl, add "way" to end of word
        ///if word starts with consonant, move all before first vowel to end of word, then add "ay" to end of word.
        ///
        public static string DetectCase(string x)
        {
            
            if (x == x.ToLower())
            { return "lower"; }
            else if (x == x.ToUpper())
                { return "upper"; }
            else if (x == CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x))
            { return "title"; }
            else { return "normal"; }


        }

        public static string Pigify(string x)
        {
            x = x.ToLower();
            List<char> chopped = new List<char>();
            chopped.AddRange(x);
            
            if ("aeiou".Contains(chopped[0].ToString()))
            {
                return string.Join("", chopped) + "way";
            }
            else
            {
                //start at -1
                ///check 1st char
                ///if not vowel, got to next. # += 1
                ///at vowel, # is index of char before vowel

                string vowels = "aeiou";
                int num = 0;
                foreach (char y in chopped)
                {
                    if (vowels.Contains(y))
                    {
                        break;
                    }
                    else
                    {
                        num += 1;
                    }
                }

                string chunk = string.Join("", chopped.GetRange(0,num));
                chopped.RemoveRange(0, num);
                

                return string.Join("",chopped) + chunk + "ay";
            }
            
        }
    }
}
