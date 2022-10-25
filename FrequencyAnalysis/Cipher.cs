using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FrequencyAnalysis
{
    public class Cipher
    {
        private Dictionary<char, float> _letters;
        private string _text;
        private int[] _charOccurrences;
        private double[] _charPercentageOccurrences;
        private Dictionary<char, char> _precidtedCharsByPrecentage;
        public Cipher()
        {
            
            _letters = new Dictionary<char, float>();
            _charOccurrences = new int['Z'-'@'];
            _charPercentageOccurrences = new double['Z'-'@'];
            _precidtedCharsByPrecentage = new Dictionary<char, char>();
            LoadLetters("letters.csv");
            LoadText("text.txt");
            PrepareTextForAnalisis();
            string textToDecode = _text.Substring(0, 100);
            _text = CeasarCipher(_text);
            Analyze();
            CalculatePercentageOccurrences();
            PredictChars();
            Console.WriteLine(textToDecode);
            var decoded = DecodeByFrequency(_text.Substring(0,100));
            Console.WriteLine(decoded);
        }

        public string DecodeByFrequency(string encoded )
        {
            string output = String.Empty;
            for (int i = 0; i < encoded.Length; i++)
            {
                output += _precidtedCharsByPrecentage.FirstOrDefault(x => x.Value == encoded[i]).Key;
            }

            return output;
        }

        public string CeasarCipher(string text)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                char currentChar = text[i];
                if (currentChar < 'Z')
                {
                    sb.Append( (char)(currentChar + 1));
                    continue;
                }

                sb.Append( 'A');

            }
            return sb.ToString();
        }

        private void PrepareTextForAnalisis()
        {
            _text = _text.ToUpper();
            for (int i = 0; i < _text.Length; i++)
            {
                if (_text[i]<'A'||_text[i]>'Z')
                {
                    _text = _text.Replace(_text[i].ToString(), "");
                }
            }
            for (int i = 0; i < _text.Length; i++)
            {
                if (_text[i]<'A'||_text[i]>'Z')
                {
                    _text = _text.Replace(_text[i].ToString(), "");
                }
            }
            
        }

        private void Analyze()
        {
            for (int i = 0; i < _text.Length; i++)
            {
                _charOccurrences[_text[i] - 'A']++;
            }
        }

        private void CalculatePercentageOccurrences()
        {
            for (int i = 0; i < _charOccurrences.Length; i++)
            {
                _charPercentageOccurrences[i] = Math.Round(_charOccurrences[i]/((double)_text.Length/100), 9);
            }
        }

        private void PredictChars()
        {
            for (int i = 0; i < _letters.Count; i++)
            {
                char key = (char)('A' + i);
                float value = _letters[key];
                int difference = int.MaxValue;
                int closestIndex = 0;
                for (int j = 0; j < _charPercentageOccurrences.Length; j++)
                {
                    if (Math.Abs(value-_charPercentageOccurrences[j])<difference)
                    {
                        difference = (int)Math.Abs(value - _charPercentageOccurrences[j]);
                        closestIndex = j;
                    }
                }
                _precidtedCharsByPrecentage.Add(key,(char)(closestIndex+'A'));
            }
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
                _letters.Add(temp[0][0], percentage);
            }
        }
    }
}