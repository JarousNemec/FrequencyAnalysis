using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using FrequencyAnalysis;
namespace AnalyzerTest
{
    
    public class Tests
    {
       
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PrepereTextForAnalysisTest()
        {
            Analyzer analyzer = new Analyzer();
            
            var output = analyzer.PrepareTextForAnalyze("\nahoj56.");
            
            Assert.AreEqual("AHOJ", output);
        }

        [Test]
        public void CalculatePercentageOccurrencesTest()
        {
            Analyzer analyzer = new Analyzer();
            var output = analyzer.CalculatePercentageOccurrences("ABCDEFGHIJ", new []{1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0});
            Assert.AreEqual(10, output[0]);
        }

        [Test]
        public void AnalyzeTest()
        {
            Analyzer analyzer = new Analyzer();

            var output = analyzer.Analyze("HIJOEHOWAREYOU");
            
            Assert.AreEqual(3, output[14]);
        }

        [Test]
        public void PredictChars()
        {
            var letters = LoadLetters("letters.csv");
            Analyzer analyzer = new Analyzer();

            var output = analyzer.PredictChars(letters,new []{6.966,8.167,2.406,0,978});
            Assert.AreEqual('A', output['I']);
            Assert.AreEqual('B', output['A']);
            Assert.AreEqual('C', output['M']);
            Assert.AreEqual('D', output['V']);
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

        [Test]
        public void DecodingTest()
        {
            Analyzer analyzer = new Analyzer();
            Dictionary<char, char> predictedChars = new Dictionary<char, char>();
            predictedChars.Add('A','B');
            predictedChars.Add('B','C');
            predictedChars.Add('C','D');
            predictedChars.Add('D','E');
            predictedChars.Add('E','F');
            predictedChars.Add('F','G');
            var output = analyzer.DecodeByFrequency("BCFDFEB", predictedChars);
            Assert.AreEqual("ABECEDA", output);
        }
    }
}