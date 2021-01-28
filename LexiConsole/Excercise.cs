using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LexiConsole
{
    class Excercise:Data
    {
        public static void LoadExcerciseMethod(int mainCase, int typeOfExcercise, string dictionaryName)
        {
            Console.Clear();
            FileProcess.ReadDataFromFile(dictionaryName);
            Console.WriteLine($"\n {MainMenuTags[mainCase]} -> {dictionaryName} -> {SubMenuTags_1[typeOfExcercise]}  -> Add meg, meddig gyakorolsz! [?]");
            Methods.CreateMenu(SubMenuTags_1_1);
            int typeOfRepeat = Methods.SelectMenuTag(SubMenuTags_1_1);

            if (typeOfRepeat != 0)
            {
                int userInput = 0;
                if (typeOfRepeat == 3)
                {
                    userInput = GetNumberFromUser();
                }
                StartExcercise(dictionaryName, typeOfExcercise, typeOfRepeat, userInput);
            }
            else
            {
                Methods.Menu_Practice(mainCase);
            }
        }

        public static int GetNumberFromUser()
        {
            Console.WriteLine($"\n A kérdések számának beállításához adj meg egy számot 1 és {activeDictionary.Count()} között!");
            Console.WriteLine(lineChar);
            Console.Write(" A szám: ");
            string userInput = Console.ReadLine();

            while (!int.TryParse(userInput, out int a) || int.Parse(userInput) > activeDictionary.Count() || int.Parse(userInput) <= 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n Hibásan megadott adat!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\n A kérdések számának beállításához adj meg egy számot 1 és {activeDictionary.Count()} között!");
                Console.WriteLine(lineChar);

                Console.Write(" A szám: ");
                userInput = Console.ReadLine();
            }

            return int.Parse(userInput);
        }

        public static void StartExcercise(string dictionaryName, int typeOfExcercise, int typeOfRepeat, int userInput = 0)
        {
            Console.Clear();

            List<int> Puffer = new List<int>();
            List<string> Failed = new List<string>();

            Puffer.Clear();
            Failed.Clear();
            Random rnd = new Random();
            int score = 0;
            var startTime = DateTime.Now;

            // Futási forma meghatározás a typeOfRepeat változóból
            int repeatCount;
            int WordCount;

            if (typeOfRepeat == 3)
            {
                repeatCount = userInput;
                WordCount = repeatCount;
            }
            else
            {
                repeatCount = activeDictionary.Count;
                WordCount = activeDictionary.Count;
            }

            bool endOfProgram = typeOfRepeat == 1 || typeOfRepeat == 3 ? Puffer.Count != repeatCount : true;

            while (endOfProgram)
            {

                Console.Clear();
                Console.WriteLine($"\n Gyakorlás -> {dictionaryName} -> {SubMenuTags_1[typeOfExcercise]} -> {SubMenuTags_1_1[typeOfRepeat]}");
                Console.WriteLine(lineChar);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(" [0] Kilépés");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(" [1] Következő szó");
                Console.ForegroundColor = ConsoleColor.White;


                // VÉgtelen futási idő esetében a puffer ürítésre kerül, ha minden szó szerepelt már egyszer
                if (typeOfRepeat == 2 && Puffer.Count == Data.activeDictionary.Count)
                {
                    Puffer.Clear();
                    Failed.Clear();
                }

                int nextId = rnd.Next(0, WordCount);
                while (Puffer.Contains(nextId))
                {
                    nextId = rnd.Next(0, WordCount);
                }
                Puffer.Add(nextId);



                string searchFor = "";
                string solution = "";

                switch (typeOfExcercise)
                {
                    case 1: // Key >> Value ?
                        searchFor = activeDictionary.ElementAt(nextId).Key;
                        solution = activeDictionary.ElementAt(nextId).Value;
                        break;

                    case 2: // Value >> Key ?
                        searchFor = activeDictionary.ElementAt(nextId).Value;
                        solution = activeDictionary.ElementAt(nextId).Key;
                        break;

                    case 3: // Random ?

                        bool random = rnd.Next(0, 2) == 0 ? false : true;

                        if (random)
                        {
                            searchFor = activeDictionary.ElementAt(nextId).Key;
                            solution = activeDictionary.ElementAt(nextId).Value;
                        }
                        else
                        {
                            searchFor = activeDictionary.ElementAt(nextId).Value;
                            solution = activeDictionary.ElementAt(nextId).Key;
                        }

                        break;
                }

                var question = $" {Puffer.Count}/{WordCount}  <<< {searchFor} >>>  -  ";

                Console.WriteLine(lineChar);
                Console.WriteLine($" Add meg a szó jelentését: ");
                Console.Write(question);

                string answer = Console.ReadLine().Trim();

                if (answer == "0")
                {
                    Console.Clear();
                    Methods.ShowMainMenuMethod();
                }
                if (answer == "1")
                {
                    // Ugratja a kérdéseket, értékelés nélkül!
                    Failed.Add(solution);

                }

                else
                {

                    Console.Clear();

                    Console.WriteLine($"\n Gyakorlás > {dictionaryName} > {SubMenuTags_1[typeOfExcercise]} > {SubMenuTags_1_1[typeOfRepeat]}");
                    Console.WriteLine(lineChar);
                    Console.WriteLine(" [0] Vissza a főmenübe! \n [1] Következő szó");
                    Console.WriteLine(lineChar);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($" Keresett kifejezés: {searchFor} ");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($" Szótári megfelelő: {solution}");

                    string match;


                    if (answer == solution)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        match = "helyes";
                        score++;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        match = "helytelen";
                        Failed.Add(solution);
                    }


                    Console.WriteLine($" A válaszod: '{answer}', {match}!");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(lineChar);
                    Console.Write(" Választott menüpont: ");
                    string next = Console.ReadLine();

                    if (next == "0")
                    {
                        Console.Clear();
                        Methods.ShowMainMenuMethod();
                    }

                    if (next == "1")
                    {

                    }

                    while (!int.TryParse(next, out int a) || int.Parse(next) > 1 || int.Parse(next) < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" Helytelen válasz!");
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.Write(" Választott menüpont: ");
                        next = Console.ReadLine();

                        if (next == "0")
                        {
                            Console.Clear();
                            Methods.ShowMainMenuMethod();
                        }
                        if (next == "1")
                        {

                        }
                    }

                }

                endOfProgram = typeOfRepeat == 1 || typeOfRepeat == 3 ? Puffer.Count != repeatCount : true;

                // Pontok kiírása a végén
                Console.WriteLine($"\n Gyakorlés vége! Elért pontszám: {score}/{WordCount}");
            }

            var endTime = DateTime.Now;
            AskAboutExport(startTime, endTime, score, repeatCount, dictionaryName, Failed);
        }

        public static void AskAboutExport(DateTime startTime, DateTime endTime, int score, int WordCount, string dictionaryName, List<string> Failed)
        {

            Console.WriteLine(lineChar);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" Mented az eredményeidet?\n [0] Nem\n [1] Igen");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(lineChar);

            Console.Write(" Választott menüpont: ");
            string exportAnswer = Console.ReadLine();


            while (!int.TryParse(exportAnswer, out int a) || int.Parse(exportAnswer) > 1 || int.Parse(exportAnswer) < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Hiba! Nem létező szám!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Választott menüpont: ");
                exportAnswer = Console.ReadLine();
            }

            if (exportAnswer == "0")
            {

            }
            if (exportAnswer == "1")
            {
                Scores.ExportScore(startTime, endTime, score, WordCount, dictionaryName, Failed);
            }

        }

        public static string DefineScoreDictionary(string userInput)
        {
            Global g = new Global();
            return $"{g.GetDefaultScorePath()}{userInput}{g.GetDefExtension()}";
        }

    }
    
}
