using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace LSAApp
{
    public class Document
    {
        public string Text { get; set; }
        public List<Sentence> Sentences { get; set; }
        public int NumberOfTerms { get; set; }
        public int MyProperty { get; set; }
        public List<String> AllTerms { get; set; }
        public int NumberOfSummarySentences { get; set; }

        private String text;

        public Document(string text, int numberOfSentences)
        {
            this.NumberOfSummarySentences = numberOfSentences;
            AllTerms = new List<string>();
            this.Text = text;
            Sentences = new List<Sentence>();
            List<string> sentenceData = text.Split('.').ToList();
            foreach (var item in sentenceData)
            {
                Sentences.Add(new Sentence(item));
            }
            numberOfSentences = Sentences.Count;
            //List<string> words = text.Replace(".", string.Empty).Replace(":", string.Empty).Split(' ').ToList();
            //foreach (string item in words)
            //{
            //    AllTerms.Add(item);
            //}

            foreach (Sentence item in Sentences)
            {
                item.CountTerms();
                foreach (string word in item.Words)
                {
                    AllTerms.Add(word);
                }
            }
            NumberOfTerms = AllTerms.Count;

        }

    }
}
