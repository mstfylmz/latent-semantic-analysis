using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using net.zemberek.araclar.turkce;
using System.Drawing.Drawing2D;
using DotNetMatrix;


namespace LSAApp
{
    public class LSA
    {
        static GeneralMatrix A; /* terms by sequence matrix
                      * A = [A1 A2 A3 ... An] with each colum vector Ai
                      * representing the weighted term frequency vector of
                      * sentence i in the document under consideration
                      */
        static GeneralMatrix VTranspose;
        static SingularValueDecomposition S;

        public static string LSACalculate(Document doc)
        {
            List<int> OrderList = new List<int>();

            string output = string.Empty;

            // Proposed SVD-based document summarization method. 
            // get number of terms in the document
            int numberOfTerms = doc.NumberOfTerms;
            // get number of sentences in the document
            int numberOfSentences = doc.NumberOfSummarySentences;
            // create matrix
            A = new GeneralMatrix(numberOfTerms, numberOfSentences);


            // get all terms in the document
            List<String> allTerms = doc.AllTerms;

            for (int c = 0; c < numberOfSentences; ++c)
            {
                Sentence sen = doc.Sentences[c];
                for (int r = 0; r < numberOfTerms; ++r)
                {
                    String term = allTerms[r];
                    if (sen.WholeSentence.Contains(term) == false)
                    {
                        A.SetElement(r, c, 0);
                    }
                    else
                    {
                        A.SetElement(r, c, 1);
                    }
                }
            }

            S = new SingularValueDecomposition(A);
            GeneralMatrix V = S.GetV();
            VTranspose = V.Transpose();
            //VTranspose.print(10, 5);

            int k = 0; // number of selected sentences for summarization
            while (k < doc.NumberOfSummarySentences)
            {
                // burada algoritmik akışta bir problem var..
                double largest = Double.NegativeInfinity;
                int index = -1;
                for (int i = 0; i < numberOfSentences; ++i)
                {
                    if (largest < VTranspose.GetElement(k, i) && doc.Sentences[i].IsInSummary == false)
                    {
                        largest = VTranspose.GetElement(k, i);
                        index = i;
                    }
                }
                if (index != -1)
                {
                    OrderList.Add(index);
                    doc.Sentences[index].IncludeSummary();
                }
                k = k + 1;
            }

            OrderList = OrderList.Take(5).ToList();
            foreach (int itemx in OrderList)
            {
                string eleman = doc.Sentences[itemx].WholeSentence.ToString();
                output = output + "\n" + eleman;
            } 
            return output;


        }

    }
}
