using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LexiConsole
{

    class Methods
    {
        // Menüpontokat tartalmazó tömbök definiálása
        public static Dictionary<string, string> activeDictionary = new Dictionary<string, string>();
        public static List<Scores> Scores = new List<Scores>();

        public static string lineChar = " ----------------------------------------------------------------------------------------------------";
        public static string lineChar2 = " ====================================================================================================";

      // Menüpontokat tartalmazó tömbök deklarálása, értékadás a konstruktor hívása közben
        public static string[] MainMenuTags = new string[] { "Kilépés", "Gyakorlás", "Szótár tartalma", "Szótár szerkesztése", "Művelet szótárakkal", "Új szótár létrehozása", "Összes szótáram", "Rendszer beállítások","Mentett eredmények", "Frissítés"};
        public static string[]  SubMenuTags_1 = new string[] { "Vissza", "idegen nyelvről > magyar nyelvre", "magyar nyelvről > idegen nyelvre", "véletlenszerű kikérdezés" };
        public static string[]  SubMenuTags_1_1 = new string[] { "Vissza", "Minden szó 1x", "Futás megszakításig" };
        public static string[]  SubMenuTags_3 = new string[] { "Vissza", "Szavak bevitele", "Szavak módosítása", "Szavak törlése" };
        public static string[]  SubMenuTags_4 = new string[] { "Vissza", "Szótár átnevezése", "Szótár törlése" };


        public static void ShowGreeting()
        {
            string a = "Jó napot";
            Console.WriteLine(lineChar2);
            Console.WriteLine($" {a}, {Environment.UserName}!");
        }

        #region Dictionary Methods

        // Létező .csv fileok nevének kiíratása, 2 választható módon
        public static void ShowExistsDictionaries(bool list)
        {

        Global g = new Global();

            if (list)
            {
                Console.WriteLine(lineChar2);
                Console.Write(" Szótáraid: ");

                Console.ForegroundColor = ConsoleColor.DarkGreen;

                if (g.myDictionaries.Count == 0)
                {
                    Console.Write($" Jelenleg nincsenek még szótáraid!");
                }

                else
                {
                    if (g.myDictionaries.Count > 4)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Console.Write($"[{g.myDictionaries[i]}] ");
                        }

                        Console.Write($"... [+{g.myDictionaries.Count-4}]");
                    }

                    else
                    {
                        for (int i = 0; i < g.myDictionaries.Count; i++)
                        {
                            Console.Write($"[{g.myDictionaries[i]}] ");
                        }
                    }
                }
                Console.WriteLine("\n");
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                foreach (var item in g.myDictionaries)
                {
                    Console.WriteLine($" [{g.myDictionaries.IndexOf(item) + 1}] {item}");
                }

            }
            Console.ForegroundColor = ConsoleColor.White;

        }

        // Szótárfájlok listából történő kiválasztása. Visszatérési érték: szótár neve
        public static string SelectDictionary(int MenuPoint)
        {
            Global g = new Global();
            int index = 0;

            // Ha nincsenek szótárfájlok
            if (g.myDictionaries.Count == 0 && MenuPoint != 5)
            {
                Console.WriteLine($"\n Jeneleg nincsenek még szótáraid, előbb hozz létre egyet!");
                ShowFooterMenu();
            }

            else
            {
                Console.WriteLine($"\n {MainMenuTags[MenuPoint]} - Válaszd ki a szótár sorszámát [?] a továbblépéshez!");
                Console.WriteLine(lineChar2);
                
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(" [0] Vissza");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                ShowExistsDictionaries(false);
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(lineChar);
                index = SelectDictionaryTag();

                return g.myDictionaries[index - 1];
            }

            return null;
        }


        public static int SelectDictionaryTag()
        {
            Global g = new Global();
            Console.Write(" Választott szótár: ");
            var dictTag = Console.ReadLine();

            while (!int.TryParse(dictTag, out int a) || int.Parse(dictTag) > g.myDictionaries.Count() || int.Parse(dictTag) < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Hiba! Nem létező szám!");
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write(" Választott szótár: ");
                dictTag = Console.ReadLine();
            }

            if (dictTag == "0")
            {
                ShowMainMenuMethod();
            }

            

            return int.Parse(dictTag);
        }


        #endregion

        #region Main Menu Settings

        // 0. Main Menu teljes metódus
        public static void ShowMainMenuMethod()
        {
            Console.Title = $@"Hello {Environment.UserName}! Welcome to the LexiC#onsol!   ¯\_(☉ᴗ☉)_/¯   [A|B]";

            Console.Clear();

            ShowGreeting();

            ShowExistsDictionaries(true); // Aktuális szótárak betöltése, ha vannak. par.: true > A lista elrendezési módja: egy soros.  

            Console.WriteLine($"\n Üdv a főmenüben! Kérlek add meg a menüpont sorszámát [?] a továbblépéshez!");

            CreateMenu(MainMenuTags); // A paraméterként átadott tömb elemeiből egy listát hoz létre a menüpontokhoz.

            int MenuTag = SelectMenuTag(MainMenuTags); // A paraméterként átadott tömb elemei alapján egy létező tömb indexet ad vissza eredményül. Ez választja ki a menüpontot.

            SelectMainMenuMethod(MenuTag); // A MenuTag a tömb egy létező indexével tér vissza, és ezt paraméterként tovább adva hívja meg a hozzá tartozó következő metódust.
        }

        // 1. Menüpontok felépítése a csatolt tömb alapján
        public static void CreateMenu(Array arrayOfMenu)
        {
            Console.WriteLine(lineChar);

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in arrayOfMenu)
            {
                Console.WriteLine($" [{Array.IndexOf(arrayOfMenu, item)}] {item}");

                if (Console.ForegroundColor == ConsoleColor.Red)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                /*
                if ((Array.IndexOf(arrayOfMenu, item)) % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }*/

                
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(lineChar);
        }

        // 2. Menüponthoz tartozó index lekérdezése
        public static int SelectMenuTag(Array menuArray)
        {
            Console.Write(" Választott menüpont: ");
            var menuTag = Console.ReadLine();


            while (!int.TryParse(menuTag, out int a) || int.Parse(menuTag) > menuArray.Length - 1 || int.Parse(menuTag) < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Hiba! Nem létező szám!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Választott menüpont: ");
                menuTag = Console.ReadLine();
            }

            return int.Parse(menuTag);
        }

        // 3. Menüponthoz tartozó metódusok hívása, a kapott index alapján
        public static void SelectMainMenuMethod(int menuTag)
        {
            switch (menuTag)
            {
                case 1:
                    Menu_Practice(1);
                    break;
                case 2:
                    Menu_ListOfDict(2);
                    break;
                case 3:
                    Menu_EditDictionaries(3);
                    break;
                case 4:
                    Menu_DictionaryOperations(4);
                    break;
                case 5:
                    Menu_CreateDictionary(5);
                    break;
                case 6:
                    Menu_ListOfDictionaries(6);
                    break;
                case 7:
                    Menu_Settings(7);
                    break;
                case 8:
                    Menu_LoadScores();
                    break;
                case 9:
                    ShowMainMenuMethod();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                ShowMainMenuMethod();
                break;
            }
        }


        #endregion

        #region Main Menu Methods
        public static void SelectSubMenuMethod(int mainMenuTag, int subMenuTag, string dictionaryName)
            {
                switch (mainMenuTag)
                {
                    case 1:
                        switch (subMenuTag)
                        {
                            case 0:
                                Console.Clear();
                                ShowMainMenuMethod();
                                break;
                            default:
                               LoadExcerciseMethod(mainMenuTag, subMenuTag, dictionaryName);
                            break;
                        }
                        break;

                    case 2:
                        switch (subMenuTag)
                        {
                            case 1: 
                                EditDictionaryItemMethod(dictionaryName);
                                break;
                            case 2: 
                                DeleteFromDictionaryFileMethod(dictionaryName);
                                break;
                            case 0: 
                                Console.Clear();
                                ShowMainMenuMethod();
                                break;
                        }
                        break;

                    case 3: 
                        switch (subMenuTag)
                        {
                            case 1: 
                            WriteToDictionaryFileMethod(dictionaryName);
                            break;
                            case 2: 
                            EditDictionaryItemMethod(dictionaryName);
                            break;
                            case 3: 
                            DeleteFromDictionaryFileMethod(dictionaryName);
                            break;
                            case 0: 
                                Console.Clear();
                                ShowMainMenuMethod();
                                break;
                        }
                        break;

                    case 4: 
                        switch (subMenuTag)
                        {
                            case 1: 
                            RenameDictionaryFileMethod(dictionaryName, MainMenuTags[mainMenuTag] + " -> " + SubMenuTags_4[subMenuTag]);
                                break;

                            case 2: 
                            DeleteDictionaryFileMethod(dictionaryName, MainMenuTags[mainMenuTag] + " -> " + SubMenuTags_4[subMenuTag]);
                                break;

                            case 0: 
                                Console.Clear();
                                ShowMainMenuMethod();
                                break;
                        }

                        break;
                }
            }

        public static void ShowSubMenu(string MenuPoint, Array subMenuArray)
        {
            Console.WriteLine($"\n {MenuPoint} -> Kérlek add meg a menüpont sorszámát [?] a továbblépéshez!");
            CreateMenu(subMenuArray);
        }
        public static void ShowFooterMenu()
        {
            Console.WriteLine(lineChar);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" [0] Kilépés");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" [1] Vissza");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(lineChar);

            Console.Write(" Választott menüpont: ");

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
                Environment.Exit(0);
            }

            if (userInput == "1")
            {
                Console.Clear();
                ShowMainMenuMethod();
            }

        }
        


        #region MainMenu_01 Gyakorlás
        public static void Menu_Practice(int MenuPoint)
        {
            Console.Clear();
            string dictionaryName = SelectDictionary(MenuPoint);

            ReadDataFromFile(dictionaryName);

            if (activeDictionary.Count <= 2)
            {
                Console.Clear();
                Console.WriteLine($"\n Túl kevés elem [{activeDictionary.Count}] van {dictionaryName} szótárban, sajnos nem tudsz még gyakorolni!");
            }
            else
            {
                Console.Clear();
                ShowSubMenu(MainMenuTags[MenuPoint] + " -> " + dictionaryName, SubMenuTags_1);
                int MenuTag = SelectMenuTag(SubMenuTags_1);
                SelectSubMenuMethod(MenuPoint, MenuTag, dictionaryName);
                ShowFooterMenu();
            }

            ShowFooterMenu();

        }
        #endregion

        #region MainMenu_02 Szótáram tartalma
        public static void Menu_ListOfDict(int MenuPoint)
        {
            Console.Clear();
            string name = SelectDictionary(MenuPoint);
            OpenDictionaryMethod(name);
            ShowFooterMenu();
        }

        #endregion

        #region MainMenu_03 Szótár szerkesztése
        public static void Menu_EditDictionaries(int MenuPoint)
        {
            Console.Clear();
            //Console.WriteLine(lineChar2);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n Lehetséges műveletek: [Új szavak bevitele | Szavak módosítása | Szavak törlése]");
            Console.ForegroundColor = ConsoleColor.White;

            string dictionaryName = SelectDictionary(MenuPoint);

            Console.Clear();
            ShowSubMenu(MainMenuTags[MenuPoint] + " -> " + dictionaryName, SubMenuTags_3);

            int MenuTag = SelectMenuTag(SubMenuTags_3);

            SelectSubMenuMethod(MenuPoint, MenuTag, dictionaryName);

            ShowFooterMenu();
        }
        #endregion

        #region MainMenu_04 Szótárműveletek
        public static void Menu_DictionaryOperations(int MenuPoint)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n Lehetséges műveletek: [Szótár átnevezése | Szótár törlése]");
            Console.ForegroundColor = ConsoleColor.White;

            string dictionaryName = SelectDictionary(MenuPoint);

            Console.Clear();
            ShowSubMenu(MainMenuTags[MenuPoint] + " -> " + dictionaryName, SubMenuTags_4);

            int MenuTag = SelectMenuTag(SubMenuTags_4);

            SelectSubMenuMethod(MenuPoint, MenuTag, dictionaryName);

            ShowFooterMenu();
        }
        #endregion

        #region MainMenu_05 Új szótár létrehozása
        public static void Menu_CreateDictionary(int MenuPoint)
        {
            Console.Clear();
            CreateDictionaryMethod(MainMenuTags[MenuPoint]);
            ShowFooterMenu();

        }
        #endregion

        #region MainMenu_06 Összes szótár listája
        public static void Menu_ListOfDictionaries(int MenuPoint)
        {
            Console.Clear();
            Console.WriteLine(lineChar2);
            Console.WriteLine($" {MainMenuTags[MenuPoint]}");
            Console.WriteLine(lineChar2);
            ShowExistsDictionaries(false);
            ShowFooterMenu();
        }

        #endregion

        #region MainMenu_08 Rekordok

        public static void Menu_LoadScores()
        {
            Console.Clear();
            Console.WriteLine(lineChar2);
            Console.WriteLine(" Mentett eredmények > Válassz egy listaelemet [?] a részletes nézethez!");
            Console.WriteLine(lineChar2);

            LoadScoreFile();


            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" [0] Vissza a főmenübe");
            Console.ForegroundColor = ConsoleColor.White;

            Console.ForegroundColor = ConsoleColor.DarkGray;

            var index = 1;

            foreach (var item in Scores)
            {
                string[] puffer = item.FailedWords.Split('|');

                Console.WriteLine($" [{index}] | {item.Date} | {item.Lexicon} | {item.Score} | [{puffer.Length}]");
                index++;
            }

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(lineChar);
            Console.Write(" Választott menüpont: ");

            var userInput = Console.ReadLine();

            while (!int.TryParse(userInput, out int a) || int.Parse(userInput) > Scores.Count || int.Parse(userInput) < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Hiba! Nem létező szám!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Választott menüpont: ");
                userInput = Console.ReadLine();
            }

            if (userInput == "0")
            {
                Console.Clear();
                ShowMainMenuMethod();
            }

            else
            {

                var listIndex = int.Parse(userInput)-1;
                Console.Clear();
                Console.WriteLine(lineChar2);
                Console.WriteLine(" Mentett eredmények > Eredmény részletei");
                Console.WriteLine(lineChar2);
                string[] puffer = Scores[listIndex].FailedWords.Split('|');

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"\n ID: [{listIndex+1}]");
                Console.WriteLine($" Dátum: {Scores[listIndex].Date}");
                Console.WriteLine($" Szótár: {Scores[listIndex].Lexicon}");
                Console.WriteLine($" Pontok: {Scores[listIndex].Score}");

                Console.WriteLine($" Hibázott kifejezések: [{puffer.Length}]");
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

                ShowFooterMenu();
            }
        }

        #endregion

        #region MainMenu_09 Beállítások
        public static void Menu_Settings(int MenuPoint)
        {
            Global g = new Global();

            Console.Clear();
            Console.WriteLine(lineChar2);
            Console.WriteLine($" {MainMenuTags[MenuPoint]}");
            Console.WriteLine(lineChar2);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n Fileok alapértelemzett helye:\n" + " " + g.GetDefDictionaryPath());
            Console.WriteLine("\n Alapértelemzett kiterjesztés:\n" + " " + g.GetDefExtension()+"\n");
            Console.ForegroundColor = ConsoleColor.White ;

            ShowFooterMenu();
        }

        #endregion

        #endregion

        #region Excercise 

        public static void LoadExcerciseMethod(int mainCase, int typeOfExcercise, string dictionaryName)
        {

            

                Console.Clear();
                ReadDataFromFile(dictionaryName);

                    Console.WriteLine($"\n {MainMenuTags[mainCase]} -> {dictionaryName} -> {SubMenuTags_1[typeOfExcercise]}  -> Add meg, meddig gyakorolsz! [?]");

                    CreateMenu(SubMenuTags_1_1);
                    int typeOfRepeat = SelectMenuTag(SubMenuTags_1_1);

                    if (typeOfRepeat != 0)
                    {
                        StartExcercise(dictionaryName, typeOfExcercise, typeOfRepeat);
                    }
                    else
                    {
                        Menu_Practice(mainCase);
                    }


        }

        public static void StartExcercise(string dictionaryName, int typeOfExcercise, int typeOfRepeat)
        {
            Console.Clear();

            List<int> Puffer = new List<int>();
            List<string> Failed = new List<string>();

            Puffer.Clear();
            Failed.Clear();
            Random rnd = new Random();
            int score = 0;

            // Futási forma meghatározás a typeOfRepeat változóból
            bool endOfProgram = typeOfRepeat == 1 ? Puffer.Count != activeDictionary.Count : true;

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

                int WordCount = activeDictionary.Count;

                // VÉgtelen futási idő esetében a puffer ürítésre kerül, ha minden szó szerepelt már egyszer
                if (typeOfRepeat == 2 && Puffer.Count == activeDictionary.Count)
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
                    ShowMainMenuMethod();
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
                        ShowMainMenuMethod();
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
                            ShowMainMenuMethod();
                        }
                        if (next == "1")
                        {
                            
                        }
                    }

                    
                }

                endOfProgram = typeOfRepeat == 1 ? Puffer.Count != activeDictionary.Count : true;

                // Pontok kiírása a végén
                Console.WriteLine($"\n Gyakorlés vége! Elért pontszám: {score}/{WordCount}");
            }

            AskAboutExport(score, activeDictionary.Count, dictionaryName, Failed);
        }

        public static void AskAboutExport(int score, int WordCount, string dictionaryName, List<string> Failed)
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
                ExportScore(score, WordCount, dictionaryName, Failed);
            }

        }

        public static void ExportScore(int score, int WordCount, string dictionaryName, List<string> Failed)
        {
            string filePath = DefineScoreDictionary("MyScores");

            LoadScoreFile();

            DateTime today = DateTime.Now;
            var dataLine = $"{today};{dictionaryName};{score}/{WordCount};{string.Join("|",Failed)}";

  
            if (Scores.Count == 0)
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

        public static string DefineScoreDictionary(string userInput)
        {
            Global g = new Global();
            return $"{g.GetDefaultScorePath()}{userInput}{g.GetDefExtension()}";
        }

        public static void LoadScoreFile()
        {
            string filePath = DefineScoreDictionary("MyScores");

            if (!File.Exists(filePath))
            {
                using (File.Create(filePath))
                { }
            }

            Scores.Clear();

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        while (!sr.EndOfStream)
                        {
                            string [] line = sr.ReadLine().Split(';');
                            string score;
                            if (line[3].Length == 0)
                            {
                                score = "-";
                            }
                            else
                            {
                                score = line[3];
                            }
                            Scores sc = new Scores(Convert.ToDateTime(line[0]),line[1],line[2],score);
                            Scores.Add(sc);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        #endregion

        #region Lexicon Items Processes

        #region Edit Dictionary Items

        public static void EditDictionaryItemMethod(string DictionaryFile)
        {
            Console.Clear();
            ReadDataFromFile(DictionaryFile);
            LoadActiveDictionaryItems(DictionaryFile);
            int index = SelectIndexFromDictionary();
            SplitItemToEdit(index, DictionaryFile);
            RefreshActiveListFile(DictionaryFile);

            bool repeat = FileMethodRepeat();
            if (repeat)
            {
                EditDictionaryItemMethod(DictionaryFile);
            }

        }

        public static void SplitItemToEdit(int index, string DictionaryFile){

            Console.Clear();
            var selected = activeDictionary.ElementAt(index);
            int lineIndex = index;
            
            string selectedKey = selected.Key;
            string selectedValue = selected.Value;


            Console.WriteLine(lineChar);
            Console.WriteLine(" Add meg a módosítani kívánt kifejezés sorszámát [?] a továbblépéshez!\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" [0] Vissza");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine($" [1] {selectedKey}\n [2] {selectedValue}\n");
            Console.WriteLine(lineChar);


            Console.Write(" Választott menüpont: ");
            var userInput = Console.ReadLine();

            while (!(int.TryParse(userInput, out int a)) || int.Parse(userInput) > 2 || int.Parse(userInput) < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Hiba! Nem létező szám!");
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write(" Választott menüpont: ");
                userInput = Console.ReadLine();
            }

            switch (int.Parse(userInput))
	        {
                case 0:
                Console.Clear();
                EditDictionaryItemMethod(DictionaryFile);
                break;

                case 1:
                EditCurrentValue(selectedKey, 1, lineIndex);
                break;

                case 2: 
                EditCurrentValue(selectedValue, 2, lineIndex);
                break;

		        default:
                break;
	        }

            Console.WriteLine(lineChar);

        }

        public static void EditCurrentValue(string input, int index, int lineIndex) {

            string userInput;
            bool next;
            Console.Clear();
            do
            {
                 Console.WriteLine(lineChar);
                 Console.Write($" A(z) '{input}' kifejezés új értéke: ");
                 userInput = Console.ReadLine().Trim();
                 next = KeyValueCheckerMethod(userInput);

            } while (next);



         //   Console.Write($" A(z) '{input}' kifejezés új értéke: ");
         //   string userInput = Console.ReadLine();
         //   bool next = KeyValueCheckerMethod(userInput);

         //   while (next)
	        //{
         //        Console.Write($" A(z) '{input}' kifejezés új értéke: ");
         //        userInput = Console.ReadLine();
         //        next = KeyValueCheckerMethod(userInput);
         //   }

            var oldData = activeDictionary.ElementAt(lineIndex);

            switch (index)
	        {
                case 1: 
                    activeDictionary.Remove(oldData.Key);
                    activeDictionary.Add(userInput,oldData.Value);
                break;

                case 2:
                    activeDictionary.Remove(oldData.Key);
                    activeDictionary.Add(oldData.Key,userInput);
                break;

		        default:
                break;
	        }

            Console.Clear();
            Console.WriteLine(lineChar);
            Console.WriteLine($" A(z) '{input}' kifejezés új megfelelője '{userInput}' sikeresen frissítve! Mit szeretnél tenni?");

        }

        public static bool FileMethodRepeat()
        {
            bool repeat = false;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" [0] Kilépés");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(" [1] Vissza a főmenübe\n [2] Művelet ismétlése");
            Console.ForegroundColor = ConsoleColor.White;
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
                ShowMainMenuMethod();
            }
            
            if (userInput == "2")
            {
                Console.Clear();
                repeat = true;
            }

            return repeat;

        }

        #endregion

        #region Add Dictionary Items

        public static void WriteToDictionaryFileMethod(string DictionaryFile)
        {
            //Adatbekérés a felhasználótól (2 szó)
            WriteToDictionaryFile(DictionaryFile, GetDataFromUser(DictionaryFile));

            bool repeat = FileMethodRepeat();
            if (repeat)
            {
                WriteToDictionaryFileMethod(DictionaryFile);
            }
        }

        public static string KeyChecker(string userInput)
        {
            string valuePair = "";
            foreach (var value in activeDictionary)
            {
                if (value.Key == userInput)
                {
                    valuePair = value.Value;
                }

            }

            return valuePair;
        }

        public static string ValueChecker(string userInput)
        {
            string keyPair = "";
            foreach (var value in activeDictionary)
            {
                if (value.Value == userInput)
                {
                    keyPair = value.Key;
                }
            }

            return keyPair;
        }

        public static bool KeyValueCheckerMethod(string userInput)
        {
            bool restart = false;

            string valueOfInput = KeyChecker(userInput);
            string keyOfInput = ValueChecker(userInput);

            if (valueOfInput.Length > 0 || keyOfInput.Length > 0)
            {
                restart = true;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(lineChar);

                if (valueOfInput.Length > 0)
                {
                    Console.WriteLine($" Hiba! A bevitt érték '{userInput}' már szerepel a szótárban, mint '{valueOfInput}'!");
                }
                if (keyOfInput.Length > 0)
                {
                    Console.WriteLine($" Hiba! A bevitt érték '{userInput}' már szerepel a szótárban, mint '{keyOfInput}'!");
                }
                Console.WriteLine(lineChar);
                Console.ForegroundColor = ConsoleColor.White;


            }

            return restart;
        }

        public static string GetDataFromUser(string DictionaryFile)
        {

            Console.Clear();

            ReadDataFromFile(DictionaryFile);
            Console.WriteLine($"\n {DictionaryFile} -> Új szavak bevitele");

            // ==============================================================================

            Console.WriteLine(lineChar);
            Console.Write(" Add meg a kifejezés 1. jelentését: ");

            string userInput_1 = Console.ReadLine();


            bool existKeyProblem = KeyValueCheckerMethod(userInput_1);

            while (existKeyProblem || userInput_1.Length <2)
            {
                Console.Write(" Add meg a kifejezés 1. jelentését újra: ");
                userInput_1 = Console.ReadLine();
                existKeyProblem = KeyValueCheckerMethod(userInput_1);
            }


            // ==============================================================================


            Console.WriteLine(lineChar);
            Console.Write(" Add meg a kifejezés 2. jelentését: ");

            string userInput_2 = Console.ReadLine();


            existKeyProblem = KeyValueCheckerMethod(userInput_2);

            while (existKeyProblem | userInput_2.Length < 2)
            {
                Console.Write(" Add meg a kifejezés 2. jelentését újra: ");
                userInput_2 = Console.ReadLine();
                existKeyProblem = KeyValueCheckerMethod(userInput_2);
            }
            // ==============================================================================


            return $"{userInput_1};{userInput_2}";


        }

        public static void WriteToDictionaryFile(string DictionaryFile, string newDoublet)
        {

            string filePath = DefineDictionary(DictionaryFile);

            if (activeDictionary.Count == 0)
            {
                File.AppendAllText(filePath, $"{newDoublet}", Encoding.UTF8);
            }
            else
            {
                File.AppendAllText(filePath, $"\n{newDoublet}", Encoding.UTF8);
            }

            Console.Clear();
            Console.WriteLine(lineChar);
            Console.WriteLine($" A(z) '{newDoublet}' szópár bekerült a(z) '{DictionaryFile}' szótárba! Mit szeretnél tenni?");
            Console.WriteLine(lineChar);

        }

        #endregion

        #region Delete Dictionary Items

        public static void DeleteFromDictionaryFileMethod(string DictionaryFile)
        {
            Console.Clear();
            ReadDataFromFile(DictionaryFile);

            // Aktuális szótárfile tartalmának listázása
            LoadActiveDictionaryItems(DictionaryFile);

            // Sor kiválasztása a szótárból
            int index = SelectIndexFromDictionary();


            bool refresh = RemoveItemfromActiveList(index, DictionaryFile);

            if (refresh)
            {
                RefreshActiveListFile(DictionaryFile);
                DeleteMethodRepeat(DictionaryFile);

                bool repeat = FileMethodRepeat();
                if (repeat)
                {
                    DeleteMethodRepeat(DictionaryFile);
                }
            }

        }

        public static void LoadActiveDictionaryItems(string DictionaryFile)
        {
            Console.WriteLine(lineChar);
            Console.WriteLine($" {DictionaryFile} > Válaszd ki a módosítani kívánt elem sorszámát [?] a továbblépéshez!");
            Console.WriteLine(lineChar);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" [0] Főmenü");
            Console.ForegroundColor = ConsoleColor.White;

            var index = 1;

            Console.ForegroundColor = ConsoleColor.DarkGray;

            foreach (var szavak in activeDictionary)
            {
                Console.WriteLine($" [{index}] {szavak.Key} - {szavak.Value}");
                index++;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static int SelectIndexFromDictionary()
        {
            Console.WriteLine(lineChar);
            Console.Write(" Választott sorszám: ");

            string userInput = Console.ReadLine();
            int index = -1;

            if (int.Parse(userInput) == 0)
            {
                ShowMainMenuMethod();
            }

            while (!int.TryParse(userInput, out int a) || int.Parse(userInput) > activeDictionary.Count || int.Parse(userInput) < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(lineChar);
                Console.WriteLine(" Hiba! Nem létező sorszám!");
                Console.WriteLine(lineChar);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Választott sorszám: ");

                userInput = Console.ReadLine();

                if (int.Parse(userInput) == 0)
                {
                    ShowMainMenuMethod();
                }
            }

            index = int.Parse(userInput) - 1;
            return index;
        }

        public static bool RemoveItemfromActiveList(int index, string DictionaryFile)
        {
            var selected = activeDictionary.ElementAt(index);

            bool refresh = false;

            Console.Clear();
            Console.WriteLine(lineChar);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($" A következő sor '{selected}' törlésére készülsz! Biztosan törölni szeretnéd?");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(lineChar);
            Console.WriteLine($" [0] Nem\n [1] Igen");
            Console.WriteLine(lineChar);

            Console.Write(" Választott menüpont: ");
            string answer = Console.ReadLine();


            while (!int.TryParse(answer, out int a) || int.Parse(answer) > 1 || int.Parse(answer) < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Hiba! Nem létező szám!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Választott menüpont: ");
                answer = Console.ReadLine();
            }


            if (answer == "1")
            {
                activeDictionary.Remove(selected.Key);
                activeDictionary.Remove(selected.Value);
                Console.Clear();
                Console.WriteLine(lineChar);
                Console.WriteLine($" A következő szópár '{selected}' törlésre került! Mit szeretnél tenni?");
                Console.WriteLine(lineChar);
                refresh = true;
            }

            else
            {
                Console.Clear();
                LoadActiveDictionaryItems(DictionaryFile);
                index = SelectIndexFromDictionary();
                refresh = RemoveItemfromActiveList(index, DictionaryFile);
            }

            return refresh;
        }

        public static void DeleteMethodRepeat(string DictionaryFile)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(" [0] Kilépés\n [1] Vissza a főmenübe\n [2] Újabb művelet");
            Console.ForegroundColor = ConsoleColor.White;
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
                ShowMainMenuMethod();
            }

            if (userInput == "2")
            {
                Console.Clear();
                DeleteFromDictionaryFileMethod(DictionaryFile);
            }
        }


        #endregion

        #region Update Dictionary Items
        public static void RefreshActiveListFile(string DictionaryFile)
        {
            string filePath = DefineDictionary(DictionaryFile);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    bool first = true;
                    foreach (var item in activeDictionary)
                    {
                        
                        if (first)
                        {
                            sw.Write(item.Key + ";" + item.Value);
                            first = false;
                        }
                        else
                        {
                            sw.Write("\n" + item.Key + ";" + item.Value);
                        }

                    }

                }
            }
        }

        #endregion

        #endregion

        #region Create Dictionary File
        public static void CreateDictionaryMethod(string MenuPoint)
        {
            // 1. Név meghatározása 
            string userInput = SetDictionaryName(MenuPoint);

            // 2.1 Név karaktervizsgálat
            bool valid = IsDictionaryNameValid(userInput);

            // 2.2 Név létezésének vizsgálata
            if (valid)
            {
                var exists = IsDictionaryExist(userInput);
                CreateDictionaryFile(exists, userInput);
            }
            else
            {
                Console.Clear();
                Console.WriteLine(lineChar);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" Hiba! A megadott '{userInput}' érvénytelen!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(lineChar);

                CreateDictionaryMethod(MenuPoint);
            }

            
            bool repeat = FileMethodRepeat();
            if (repeat)
            {
                CreateDictionaryMethod(MenuPoint);
            }
            
        }

        // 1. Név meghatározása 
        public static string SetDictionaryName(string MenuPoint)
        {
            var userInput = "";
            do
            {
                Console.WriteLine($"\n {MenuPoint}");
                Console.WriteLine(lineChar2);
                Console.Write(" Add meg az új szótár nevét: ");
                userInput = Console.ReadLine().Trim();

                if (userInput.Length<3)
                {
                    Console.Clear();
                    Console.WriteLine(lineChar);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" A szótár nevének hossza minimum 3 karakter kell hogy legyen!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(lineChar);

                }

            } while (userInput.Length < 3);

            return userInput;
        }

        // 2. Karaktervizsgálat
        public static bool IsDictionaryNameValid(string userInput)
        {
            string illegal = @"[\\/:*!?"".,+=()˝~ˇ^˘°˛`˙´¨¸÷×đĐłŁß¤§%'<>{}#@$|]";

            foreach (char c in userInput)
            {
                if (illegal.Contains(c) == true || userInput.Length > 64 || userInput.Length < 3)
                {
                    return false;
                }
            }
            return true;
        }

        // 3.1 File létezésének vizsgálata
        public static bool IsDictionaryExist(string userInput)
        {
            var file = DefineDictionary(userInput);
            if (File.Exists(file)) return true;
            return false;
        }

        // 3.2 File helyének definiálása
        public static string DefineDictionary(string userInput)
        {
            Global g = new Global();
            return $"{g.GetDefDictionaryPath()}{userInput}{g.GetDefExtension()}";
        }

        // 4. File létrehozása
        public static void CreateDictionaryFile(bool exist, string userInput)
        {
            Console.Clear();

            if (exist == true)
            {
                Console.WriteLine(lineChar);
                Console.WriteLine($" A szótár '{userInput}' már létezik! Szeretnéd felülírni?\n [0] Nem\n [1] Igen");
                Console.WriteLine(lineChar);
                Console.Write(" Választott menüpont: ");
            }

            else
            {
                Console.WriteLine(lineChar);
                Console.WriteLine($" A szótár '{userInput}' még nem létezik! Szeretnéd létrehozni?\n [0] Nem\n [1] Igen");
                Console.WriteLine(lineChar);
                Console.Write(" Választott menüpont: ");
            }

            string answer = Console.ReadLine().Trim();


            while (!int.TryParse(answer, out int a) || int.Parse(answer) > 1 || int.Parse(answer) < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Hiba! Nem létező szám!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Választott menüpont: ");
                answer = Console.ReadLine();
            }

            if (answer == "1")
            {
                try
                {
                    var newFile = DefineDictionary(userInput);
                    using (File.Create(newFile)){}

                    if (File.Exists(newFile))
                    {
                        Console.Clear();
                        Console.WriteLine($"\n Az új szótár: '{userInput}' létrejött! Mit szeretnél tenni?");
                        Console.WriteLine(lineChar);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine(" A szótár létrehozása sikertelen volt!");
                }

            }
            if (answer == "0")
            {
                Console.Clear();
                ShowMainMenuMethod();
            }
            else
            {

            }
        }

        #endregion

        #region Open Dictionary File
        public static void OpenDictionaryMethod(string userInput)
        {
            // 1. Név meghatározása 
            //  userInput = SetDictionaryName();

            // 2.1 Név karaktervizsgálat
            var valid = IsDictionaryNameValid(userInput);

            // 2.2 Név létezésének vizsgálata
            if (valid)
            {
                var exist = IsDictionaryExist(userInput);
                if (exist) OpenDictionaryFile(userInput);
                else Console.WriteLine("A file nem létezik!");
            }
            else
            {
                Console.WriteLine($"A megadott {userInput} érvénytelen!");
                OpenDictionaryMethod(userInput);
            }
        }

        public static void ReadDataFromFile(string DictionaryName)
        {
            string filePath = DefineDictionary(DictionaryName);
            activeDictionary.Clear();

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] puffer = sr.ReadLine().Split(';');
                        try
                        {
                            activeDictionary.Add(puffer[0], puffer[1]);
                        }
                        catch (Exception)
                        {
                            activeDictionary.Add("hiba?!", "hiba!?");
                        }
                  
                    }
                }
            }
            

           
        }

        public static void OpenDictionaryFile(string DictionaryFile)
        {
            ReadDataFromFile(DictionaryFile);

            Console.Clear();
            Console.WriteLine(lineChar);
            Console.WriteLine($" {DictionaryFile} | kifejezések száma: {activeDictionary.Count}");
            Console.WriteLine(lineChar);

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            foreach (var szavak in activeDictionary)
            {
                Console.WriteLine(" " + szavak.Key + " - " + szavak.Value);
            }
            Console.ForegroundColor = ConsoleColor.White;

        }

        #endregion

        #region Delete Dictionary File

        public static void DeleteDictionaryFileMethod(string dictionaryName, string MenuPoint)
        {
            Console.Clear();
            // 1. Név meghatározása 
            string userInput = dictionaryName;

                // 2.1 Név karaktervizsgálat
            var valid = IsDictionaryNameValid(userInput);

            // 2.2 Név létezésének vizsgálata
            if (valid)
            {
                var exist = IsDictionaryExist(userInput);
                if (exist) DeleteDictionaryFile(userInput);
                else Console.WriteLine(" A file nem létezik!");
            }

            else
            {
                Console.WriteLine($" A megadott {userInput} érvénytelen!");
                DeleteDictionaryFileMethod(userInput, MenuPoint);
            }
        }

        public static void DeleteDictionaryFile(string userInput)
        {
            Console.WriteLine(lineChar);
            Console.WriteLine($" A következő szótár '{userInput}' törlésére készülsz. Biztosan törölni szeretnéd?\n [0] Nem\n [1] Igen");
            Console.WriteLine(lineChar);
            Console.Write(" Választott menüpont: ");
            var answer = Console.ReadLine().Trim();


            while (!int.TryParse(answer, out int a) || int.Parse(answer) > 1 || int.Parse(answer) < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Hiba! Nem létező szám!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Választott menüpont: ");
                answer = Console.ReadLine();
            }


            if (answer == "1")
            {
                try
                {
                    bool exist = IsDictionaryExist(userInput);
                    var file = DefineDictionary(userInput);

                    if (exist)
                    {
                        Console.Clear();
                        File.Delete(file);
                        Console.WriteLine($"\n A következő szótár '{userInput}' törlésre került!");
                    }
                    else
                    {
                        Console.WriteLine($"\n Hiba a törlési művelet során! Nem létező file: {file}");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("\n Hiba! A törlés nem történt meg!");
                }
            }
            else
            {
                ShowMainMenuMethod();
            }

        }

        #endregion

        #region Rename Dictionary File

        public static void RenameDictionaryFileMethod(string dictionaryName, string MenuPoint)
        {

        Console.Clear();
        // 1. Név meghatározása 
        string userInput = dictionaryName;

        // 2.1 Név karaktervizsgálat
        var valid = IsDictionaryNameValid(userInput);

        // 2.2 Név létezésének vizsgálata
        if (valid)
        {
            var exist = IsDictionaryExist(userInput);
            if (exist) RenameDictionaryFile(userInput, MenuPoint);
            else Console.WriteLine(" A file nem létezik!");
        }

        else
        {
            Console.WriteLine($" A megadott {userInput} érvénytelen!");
            RenameDictionaryFileMethod(dictionaryName,MenuPoint);
        }
    }

        public static void RenameDictionaryFile(string userInput, string MenuPoint)
        {

            Console.Clear();
            Console.WriteLine(lineChar);
            Console.WriteLine($" A következő szótár '{userInput}' átnevezésére készülsz! Biztosan folytatod?\n [0] Nem\n [1] Igen");
            Console.WriteLine(lineChar);
            Console.Write(" Választott menüpont: ");
            var answer = Console.ReadLine().Trim();

            while (!int.TryParse(answer, out int a) || int.Parse(answer) > 1 || int.Parse(answer) < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Hiba! Nem létező szám!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Választott menüpont: ");
                answer = Console.ReadLine().Trim();
            }

            if (answer == "1")
            {
                try
                {
                    bool exist = IsDictionaryExist(userInput);
                    var oldFilePath = DefineDictionary(userInput);

                    if (exist)
                    {
                        Console.Write(" Add meg a szótár új nevét: ");

                        var newName = Console.ReadLine();
                        var newFilePath = DefineDictionary(newName);

                        // 2.1 Név karaktervizsgálat
                        var valid = IsDictionaryNameValid(newName);

                        // 2.2 Név létezésének vizsgálata
                        if (valid)
                        {
                            var existNewName = IsDictionaryExist(newName);

                            if (existNewName == false)
                            {
                                Console.Clear();
                                File.Copy(oldFilePath, newFilePath);
                                File.Delete(oldFilePath);
                                Console.WriteLine($"\n A szótár '{userInput}' új neve '{newName}'");

                            }

                            else
                            {
                                Console.Clear();
                                Console.WriteLine(lineChar);
                                Console.WriteLine($" A(z) '{newName}' szótár már létezik! Mit szeretnél tenni?\n [0] Vissza\n [1] Felülírás\n [2] Új név megadása");
                                Console.WriteLine(lineChar);
                                Console.Write(" Választott menüpont: ");
                                var answer2 = Console.ReadLine().Trim();



                                while (!int.TryParse(answer2, out int a) || int.Parse(answer2) > 2 || int.Parse(answer2) < 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(" Hiba! Nem létező szám!");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write(" Választott menüpont: ");
                                    answer = Console.ReadLine();
                                }

                                if (answer2 == "0")
                                {
                                    Console.Clear();
                                    ShowMainMenuMethod();
                                }

                                if (answer2 == "1")
                                {
                                    Console.Clear();
                                    Console.WriteLine("\n Az átnevezés megtörtént!");
                                    File.Delete(newFilePath);
                                    File.Copy(oldFilePath, newFilePath);
                                    File.Delete(oldFilePath);
                                }

                                if (answer2 == "2")
                                {
                                    Console.Clear();
                                    RenameDictionaryFile(userInput, MenuPoint);
                                }
                            }
                        }

                        else
                        {
                            Console.WriteLine($"\n A megadott {userInput} érvénytelen!");
                            RenameDictionaryFileMethod(userInput, MenuPoint);
                        }

                    }
                    else
                    {
                        Console.WriteLine($"\n Hiba az átnevezési művelet során! Nem létező file: {oldFilePath}");
                    }

                }
                catch (Exception)
                {

                    Console.WriteLine("\n Hiba történt az átnevezés közben!");
                }

            }
            else
            {
                Console.Clear();
                ShowMainMenuMethod();
            }

        }

        #endregion

    }
    
}
