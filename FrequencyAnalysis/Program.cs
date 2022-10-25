using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FrequencyAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
        }

        public Program()
        {
            //var cipher = new Cipher(); - prototype
            var analyzer = new Analyzer();
            string text = LoadText("text.txt");
            
            Console.WriteLine(PrepareTextForAnalisis(text).Substring(100,100));
            text = CaesarCipher(text);
            var output = analyzer.Analyze(text,text.Substring(0,100), LoadLetters("letters.csv"));
            Console.WriteLine(output.Substring(100,100));
        }
        
        private string PrepareTextForAnalisis(string text)
        {
            text = text.ToUpper();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] < 'A' || text[i] > 'Z')
                {
                    text = text.Replace(text[i].ToString(), "");
                }
            }

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] < 'A' || text[i] > 'Z')
                {
                    text = text.Replace(text[i].ToString(), "");
                }
            }
            return text;
        }

        private string CaesarCipher(string text)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                sb.Append((char)(text[i] + 1));
            }

            return sb.ToString();
        }

        private string LoadText(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Soubor neexistuje");
                return null;
            }

            return File.ReadAllText(path).Replace(" ", "");
        }


        private Dictionary<char, float> LoadLetters(string path)
        {
            Dictionary<char, float> letters = new Dictionary<char, float>();
            if (!File.Exists(path))
                return null;
            var data = File.ReadAllLines(path);
            foreach (var line in data)
            {
                var temp = line.Split(';');
                float percentage = 0;
                if (!float.TryParse(temp[1], out percentage))
                    return null;
                letters.Add(temp[0][0], percentage);
            }

            return letters;
        }
        
        
    }
}