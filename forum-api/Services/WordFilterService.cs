using forum_api.DataAccess.DataObjects;
using forum_api.Repositories;
using System.IO;
using forum_api.Services;

namespace forum_api.Services
{
    public class WordFilterService : IWordFilterService
    {
        private string[] _banWords;

        string filePath = "C:\\code\\TestsUnitaire\\forum-api\\insults.txt"; //C:\Users\corae\OneDrive\Bureau\projet forum\TDD-forum-api

        public WordFilterService()
        {
            _banWords = File.ReadAllLines(filePath);
        }

        public string WordFilterSentence(string sentence)
        {
            bool isMotCensured = false;
            string newMot = "";

            if (sentence != null && sentence != "")
            {
                foreach (var mot in _banWords)
                {
                    if (sentence.Contains(mot))
                    {
                        newMot = "" + mot[0];

                        for (int i = 1; i < mot.Length - 1; i++)
                        {
                            newMot += "*";

                        }

                        newMot += mot[mot.Length - 1];

                        //gère les insultes à deux lettres
                        if (mot.Length <= 2)
                        {
                            newMot = "**";
                        }

                        sentence = sentence.Replace(mot, newMot);
                    }
                }
            }
            return sentence;
        }
    }
}
