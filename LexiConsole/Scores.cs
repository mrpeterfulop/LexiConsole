using System;

namespace LexiConsole
{
    class Scores
    {

        private DateTime date;
        private string lexicon;
        private string score;
        private string failedWords;

        public Scores(DateTime date, string lexicon, string score, string failedWords)
        {
            Date = date;
            Lexicon = lexicon;
            Score = score;
            FailedWords = failedWords;
        }

        public DateTime Date { get => date; set => date = value; }
        public string Lexicon { get => lexicon; set => lexicon = value; }
        public string Score { get => score; set => score = value; }
        public string FailedWords { get => failedWords; set => failedWords = value; }
    }
    
}
