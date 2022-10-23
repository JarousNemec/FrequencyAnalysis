using System;
using System.Collections.Generic;
using System.IO;

namespace FrequencyAnalysis
{
    public class Cipher
    {
        private Dictionary<string, float> _letters;
        private Dictionary<float, string> _percentage;
        private string _text;
        
        public Cipher()
        {
            _letters = new Dictionary<string, float>();
            _percentage = new Dictionary<float, string>();
            LoadLetters("letters.csv");
            LoadText("text.txt");
        }

        private void LoadText(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Soubor neexistuje");
                return;
            }
            _text = File.ReadAllText(path).Replace(" ", "");
        }
        

        private void LoadLetters(string path)
        {
            if(!File.Exists(path))
                return;
            var data = File.ReadAllLines(path);
            foreach (var line in data)
            {
                var temp = line.Split(';');
                float percentage=0;
                if (!float.TryParse(temp[1], out percentage))
                    return;
                _letters.Add(temp[0], percentage);
                _percentage.Add(percentage, temp[0]);
            }
        }
    }
}