using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrequencyAnalysis
{
    public class Analyzer
    {
        public string Analyze(string toLearn,string toAnalyze, Dictionary<char, float> letters)
        {
            toLearn = PrepareTextForAnalyze(toLearn);
            int[] charOccurrences = Analyze(toLearn);
            double[] charPercentageOccurrences = CalculatePercentageOccurrences(toLearn, charOccurrences);
            var predictedCharsByPrecentage = PredictChars(letters, charPercentageOccurrences);
            return DecodeByFrequency(toLearn, predictedCharsByPrecentage);
        }

        public string DecodeByFrequency(string encoded, Dictionary<char, char> precidtedCharsByPrecentage)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encoded.Length; i++)
            {
                sb.Append(precidtedCharsByPrecentage.FirstOrDefault(x => x.Value == encoded[i]).Key);
            }

            return sb.ToString();
        }

        public string PrepareTextForAnalyze(string text)
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

        public int[] Analyze(string toAnalyze)
        { 
            int[] charOccurrences = new int['Z'-'@'];
            for (int i = 0; i < toAnalyze.Length; i++)
            {
                charOccurrences[toAnalyze[i] - 'A']++;
            }

            return charOccurrences;
        }

        public double[] CalculatePercentageOccurrences(string toAnalyze, int[] charOccurrences)
        {
            double[] charPercentageOccurrences = new double['Z'-'@'];
            for (int i = 0; i < charOccurrences.Length; i++)
            {
                charPercentageOccurrences[i] = Math.Round(charOccurrences[i]/((double)toAnalyze.Length/100), 9);
            }

            return charPercentageOccurrences;
        }

        public Dictionary<char, char> PredictChars(Dictionary<char, float> letters,double[] charPercentageOccurrences)
        {

            Dictionary<char, char> precidtedCharsByPrecentage = new Dictionary<char, char>();
            for (int i = 0; i < letters.Count; i++)
            {
                char key = (char)('A' + i);
                float value = letters[key];
                int difference = int.MaxValue;
                int closestIndex = 0;
                for (int j = 0; j < charPercentageOccurrences.Length; j++)
                {
                    if (Math.Abs(value-charPercentageOccurrences[j])<difference)
                    {
                        difference = (int)Math.Abs(value - charPercentageOccurrences[j]);
                        closestIndex = j;
                    }
                }
                precidtedCharsByPrecentage.Add(key,(char)(closestIndex+'A'));
            }

            return precidtedCharsByPrecentage;
        }

        
    }
}