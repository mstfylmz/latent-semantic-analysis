using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using net.zemberek.yapi.kok;
using net.zemberek.yapi;
using net.zemberek.erisim;
using net.zemberek.bilgi.kokler;
using net.zemberek.tr.yapi;

namespace LSAApp
{
    public class Sentence
    {
        public string WholeSentence { get; set; }
        public List<string> Words { get; set; }
        public Dictionary<string, int> TermCount { get; set; }
        public bool IsInSummary { get; set; }

        public Sentence(string wholeSentence)
        {
            this.WholeSentence = wholeSentence;
            IsInSummary = false;
            string[] words = wholeSentence.Split(' ').ToArray();
            this.Words = new List<string>();
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Equals("") == false)
                {
                    Zemberek zemberek = new Zemberek(new TurkiyeTurkcesi());
                    Kelime[] klm = zemberek.kelimeCozumle(words[i]);
                    for (int k = 0; k < klm.Length; k++)
                    {
                        this.Words.Add(klm[k].kok().icerik());
                    }
                }
            }
        }
        public void IncludeSummary()
        {
            IsInSummary = true;
        }
        public void CountTerms()
        {
            TermCount = new Dictionary<string, int>();
            foreach (string word in Words)
            {
                if (TermCount.ContainsKey(word))
                {
                    int count = Convert.ToInt32(TermCount.Where(x => x.Key == word).Select(x => x.Value).FirstOrDefault());
                    //TermCount.Add(word, count + 1);
                    TermCount.Remove(word);
                    TermCount.Add(word, count + 1);
                }
                else
                {
                    TermCount.Add(word, 1);
                }
            }
        }
    }
}

