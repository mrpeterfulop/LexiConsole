using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LexiConsole
{
    class Scores:Data
    {
        public static List<Scores> ScoreList = new List<Scores>();

        public Scores(DateTime dateStart, DateTime dateEnd, string lexicon, double score, double words, string failedWords)
        {
            DateStart = dateStart;
            DateEnd = dateEnd;
            Lexicon = lexicon;
            Score = score;
            Words = words;
            FailedWords = failedWords;
        }

        public DateTime DateStart {get; set; }
        public DateTime DateEnd { get; set; }
        public string Lexicon { get; set; }
        public double Score { get; set; }
        public double Words { get; set; }
        public string FailedWords { get; set; }

        

        public static void CreateRecordsHeader(int maxScoreCountLength)
        {

            string stringID = PaddingString("ID", maxScoreCountLength + 2, false);
            string space = "    ";
            var dinamic = space.Remove(space.Length - maxScoreCountLength);

            Console.WriteLine($" +--------------------------------------------------------------------------------------------------+");
            Console.WriteLine($" | {stringID} |          DATE          |   %   |                       DICTIONARY NAME{dinamic}                  |");
            Console.WriteLine($" +--------------------------------------------------------------------------------------------------+");

        }

        public static string PaddingString(object anyObject, int padding, bool alignLeft = true, string firstCharacter = "", string lastCharacter = "")
        {
            string textContent = anyObject.ToString();
            int extras = 0;
            char space = (char)32;

            if (firstCharacter.Length != 0)
            {
                textContent = firstCharacter + textContent;
                extras += firstCharacter.Length;
            }

            if (lastCharacter.Length != 0)
            {
                textContent = textContent + lastCharacter;
                extras += lastCharacter.Length;
            }

            if (alignLeft)
            {
                textContent = textContent.PadLeft(padding + extras, space);
            }
            else
            {
                textContent = textContent.PadRight(padding + extras, space);
            }

            return textContent;

        }

        public static int LoadScores()
        {
            Console.Clear();
            Console.WriteLine(lineChar2);
            Console.WriteLine(" Mentett eredmények > Válassz egy listaelemet [?] a részletes nézethez!");
            Console.WriteLine(lineChar2);

            LoadScoresFile();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("  [0] Vissza a főmenübe");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkGray;

            var index = 1;

            var maxScoreCountLength = Convert.ToInt32(ScoreList.Count().ToString().Length);

            CreateRecordsHeader(maxScoreCountLength);

            foreach (var item in ScoreList)
            {
                string[] puffer = item.FailedWords.Split('|');

                string idText = PaddingString(index, maxScoreCountLength, false, "[", "]");
                string dateText = PaddingString(item.DateStart, 22);
                string scoreText = PaddingString(Math.Round(item.Score / item.Words * 100, 0), 3, true, "", "%");
                string lexiconText = PaddingString(item.Lexicon, 54, false);
                string space = "    ";
                var dinamic = space.Remove(space.Length - maxScoreCountLength);
                Console.WriteLine($" | {idText} | {dateText} |  {scoreText} | {lexiconText}{dinamic} |");
                index++;

            }

            Console.WriteLine($" +--------------------------------------------------------------------------------------------------+");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(" Választott menüpont: ");
            var userInput = Console.ReadLine();

            while (!int.TryParse(userInput, out int a) || int.Parse(userInput) > ScoreList.Count || int.Parse(userInput) < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Hiba! Nem létező szám!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Választott menüpont: ");
                userInput = Console.ReadLine();
            }

            return int.Parse(userInput);

        }

        public static void SelectRecordDetails(int userInput)
        {

            if (userInput == 0)
            {
                Console.Clear();
                Methods.ShowMainMenuMethod();
            }

            else
            {
                var listIndex = userInput - 1;
                Console.Clear();
                Console.WriteLine(lineChar2);
                Console.WriteLine(" Mentett eredmények > Eredmény részletei");
                Console.WriteLine(lineChar2);

                string[] puffer = ScoreList[listIndex].FailedWords.Split('|');

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($" Azonosító: [{listIndex + 1}]");
                Console.WriteLine($"\n Szótár neve: {ScoreList[listIndex].Lexicon}");
                Console.WriteLine($"\n Kezdési időpont:      {ScoreList[listIndex].DateStart}");
                Console.WriteLine($" Befejezési időpont:   {ScoreList[listIndex].DateEnd}");
                Console.WriteLine($" Időtartam:                          {ScoreList[listIndex].DateEnd - ScoreList[listIndex].DateStart}");
                Console.WriteLine($"\n Pontok: {ScoreList[listIndex].Score}/{ScoreList[listIndex].Words}");
                Console.WriteLine($" Százalékos eredmény: {Math.Round((ScoreList[listIndex].Score / ScoreList[listIndex].Words * 100), 0)}%");

                Console.WriteLine($"\n {puffer.Length} hibázott kifejezés:");

                int counter = 0;

                if (puffer.Length >= 5)
                {

                    for (int i = 0; i < puffer.Length; i++)
                    {
                        Console.Write(" | " + puffer[i]);
                        counter++;

                        if (counter == 5)
                        {
                            Console.WriteLine();
                            counter = 0;
                        }
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(" " + string.Join(" | ", puffer));
                }

                Console.ForegroundColor = ConsoleColor.White;
                ShowBackMenu(listIndex);
            }
        }

        public static void ShowBackMenu(int listIndex)
        {
            Console.WriteLine(lineChar);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" [0] Kilépés");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" [1] Vissza");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" [2] Eredmény törlése");

            Console.WriteLine(lineChar);

            Console.Write(" Választott menüpont: ");

            var userInput = Console.ReadLine();

            while (!int.TryParse(userInput, out int a) || int.Parse(userInput) > 2 || int.Parse(userInput) < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Hiba! Nem létező szám!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Választott menüpont: ");
                userInput = Console.ReadLine();
            }

            if (userInput == "0")
            {
                Environment.Exit(0);
            }

            if (userInput == "1")
            {
                Console.Clear();
                Methods.Menu_LoadRecordsMethod();
            }

            if (userInput == "2")
            {
                Console.Clear();
                DeleteActualRecord(listIndex);
            }
        }

        public static void DeleteActualRecord(int listIndex)
        {

            Console.WriteLine($"\n Biztosan törölni szeretnéd a kijelölt [{listIndex + 1}]. rekordot?");
            Console.WriteLine(lineChar);
            Console.WriteLine(" [0] Nem\n [1] Igen");
            Console.WriteLine(lineChar);
            Console.Write($" Választott menüpont: ");

            var userInput = Console.ReadLine();

            while (!int.TryParse(userInput, out int a) || int.Parse(userInput) > 1 || int.Parse(userInput) < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Hiba! Nem létező szám!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Választott menüpont: ");
                userInput = Console.ReadLine();
            }

            if (userInput == "0")
            {
                SelectRecordDetails(1);
            }

            if (userInput == "1")
            {
                Console.Clear();

                ScoreList.RemoveAt(listIndex);

                WriteRecordsToFile();
                LoadScoresFile();
                int reload = LoadScores();
                SelectRecordDetails(reload);
            
            }
        }

        public static void WriteRecordsToFile()
        {
            string filePath = Excercise.DefineScoreDictionary("MyScores");

            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }
            }

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {

                        int index = 0;
                        foreach (var item in ScoreList)
                        {
                            string dataLine;
                            if (index != ScoreList.Count-1)
                            {
                                 dataLine = $"{item.DateStart};{item.DateEnd};{item.Lexicon};{item.Score};{item.Words};{string.Join("|", item.FailedWords)}\n";
                            }
                            else
                            {
                                dataLine = $"{item.DateStart};{item.DateEnd};{item.Lexicon};{item.Score};{item.Words};{string.Join("|", item.FailedWords)}";
                            }
                            sw.Write(dataLine);
                            index++;
                        }
                    }
                }

            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void LoadScoresFile()
        {
            string filePath = Excercise.DefineScoreDictionary("MyScores");

            if (!File.Exists(filePath))
            {
                using (File.Create(filePath))
                { }
            }

            ScoreList.Clear();

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        while (!sr.EndOfStream)
                        {
                            string[] line = sr.ReadLine().Split(';');
                            string score;

                            if (line[5].Length == 0)
                            {
                                score = "-";
                            }
                            else
                            {
                                score = line[5];
                            }
                            Scores sc = new Scores(Convert.ToDateTime(line[0]), Convert.ToDateTime(line[1]), line[2], Convert.ToDouble(line[3]), Convert.ToDouble(line[4]), score);
                            ScoreList.Add(sc);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public static void ExportScore(DateTime startTime, DateTime endTime, int score, int WordCount, string dictionaryName, List<string> faliedList)
        {
            string filePath = Excercise.DefineScoreDictionary("MyScores");

            LoadScoresFile();

            var dataLine = $"{startTime};{endTime};{dictionaryName};{score};{WordCount};{string.Join("|", faliedList)}";

            if (ScoreList.Count == 0)
            {
                File.AppendAllText(filePath, $"{dataLine}", Encoding.UTF8);
            }
            else
            {
                File.AppendAllText(filePath, $"\n{dataLine}", Encoding.UTF8);
            }

            Console.Clear();
            Console.WriteLine("\n A mentés sikeres volt!");
        }
    }

}
