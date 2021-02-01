using System;
using System.IO;
using System.Linq;
using System.Text;

namespace LexiConsole
{
    class DictionaryProcess :Data
    {

        #region Dictionary Methods

        // Létező .csv fileok nevének kiíratása, 2 választható módon
        public static void ShowExistsDictionaries(bool list, bool index)
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

                        Console.Write($"... [+{g.myDictionaries.Count - 4}]");
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


                if (index)
                {
                    foreach (var item in g.myDictionaries)
                    {
                        Console.WriteLine($" [{g.myDictionaries.IndexOf(item) + 1}] {item}");
                    }
                }
                else
                {
                    foreach (var item in g.myDictionaries)
                    {
                        Console.WriteLine($" {item}");
                    }
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
                Console.WriteLine($"\n Jelenleg nincsenek még szótáraid, előbb hozz létre egyet!");
                Methods.ShowFooterMenu();
            }

            else
            {
                Console.WriteLine($"\n {MainMenuTags[MenuPoint]} - Válaszd ki a szótár sorszámát [?] a továbblépéshez!");
                Console.WriteLine(lineChar2);

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(" [0] Vissza");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                ShowExistsDictionaries(false, true);
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(lineChar1);
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
                Methods.ShowMainMenuMethod();
            }



            return int.Parse(dictTag);
        }


        #endregion

        #region Edit Dictionary Items

        public static void EditDictionaryItemMethod(string DictionaryFile)
        {
            Console.Clear();
            FileProcess.ReadDataFromFile(DictionaryFile);
            LoadActiveDictionaryItems(DictionaryFile);
            int index = SelectIndexFromDictionary();
            SplitItemToEdit(index, DictionaryFile);
            RefreshActiveListFile(DictionaryFile);

            bool repeat = Methods.RepeatActualMethod();
            if (repeat)
            {
                EditDictionaryItemMethod(DictionaryFile);
            }
        }

        public static void SplitItemToEdit(int index, string DictionaryFile)
        {

            Console.Clear();
            var selected = activeDictionary.ElementAt(index);
            int lineIndex = index;

            string selectedKey = selected.Key;
            string selectedValue = selected.Value;


            Console.WriteLine(lineChar1);
            Console.WriteLine(" Add meg a módosítani kívánt kifejezés sorszámát [?] a továbblépéshez!\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" [0] Vissza");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine($" [1] {selectedKey}\n [2] {selectedValue}\n");
            Console.WriteLine(lineChar1);


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

            Console.WriteLine(lineChar1);

        }

        public static void EditCurrentValue(string input, int index, int lineIndex)
        {

            string userInput;
            bool next;
            Console.Clear();
            do
            {
                Console.WriteLine(lineChar1);
                Console.Write($" A(z) '{input}' kifejezés új értéke: ");
                userInput = Console.ReadLine().Trim();
                next = KeyValueCheckerMethod(userInput);

            } while (next);


            var oldData = activeDictionary.ElementAt(lineIndex);

            switch (index)
            {
                case 1:
                    activeDictionary.Remove(oldData.Key);
                    activeDictionary.Add(userInput, oldData.Value);
                    break;

                case 2:
                    activeDictionary.Remove(oldData.Key);
                    activeDictionary.Add(oldData.Key, userInput);
                    break;

                default:
                    break;
            }

            Console.Clear();
            Console.WriteLine(lineChar1);
            Console.WriteLine($" A(z) '{input}' kifejezés új megfelelője '{userInput}' sikeresen frissítve! Mit szeretnél tenni?");

        }

        #endregion

        #region Add Dictionary Items

        public static void WriteToDictionaryFileMethod(string DictionaryFile)
        {
            //Adatbekérés a felhasználótól (2 szó)
            WriteToDictionaryFile(DictionaryFile, GetDataFromUser(DictionaryFile));

            bool repeat = Methods.RepeatActualMethod();
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
                Console.WriteLine(lineChar1);

                if (valueOfInput.Length > 0)
                {
                    Console.WriteLine($" Hiba! A bevitt érték '{userInput}' már szerepel a szótárban, mint '{valueOfInput}'!");
                }
                if (keyOfInput.Length > 0)
                {
                    Console.WriteLine($" Hiba! A bevitt érték '{userInput}' már szerepel a szótárban, mint '{keyOfInput}'!");
                }
                Console.WriteLine(lineChar1);
                Console.ForegroundColor = ConsoleColor.White;


            }

            return restart;
        }

        public static string GetDataFromUser(string DictionaryFile)
        {

            Console.Clear();

            FileProcess.ReadDataFromFile(DictionaryFile);
            Console.WriteLine($"\n {DictionaryFile} -> Új szavak bevitele");

            Console.WriteLine(lineChar1);
            Console.Write(" Add meg a kifejezés 1. jelentését: ");

            string userInput_1 = Console.ReadLine();


            bool existKeyProblem = KeyValueCheckerMethod(userInput_1);

            while (existKeyProblem || userInput_1.Length < 2)
            {
                Console.Write(" Add meg a kifejezés 1. jelentését újra: ");
                userInput_1 = Console.ReadLine();
                existKeyProblem = KeyValueCheckerMethod(userInput_1);
            }


            Console.WriteLine(lineChar1);
            Console.Write(" Add meg a kifejezés 2. jelentését: ");

            string userInput_2 = Console.ReadLine();


            existKeyProblem = KeyValueCheckerMethod(userInput_2);

            while (existKeyProblem | userInput_2.Length < 2)
            {
                Console.Write(" Add meg a kifejezés 2. jelentését újra: ");
                userInput_2 = Console.ReadLine();
                existKeyProblem = KeyValueCheckerMethod(userInput_2);
            }

            return $"{userInput_1};{userInput_2}";

        }

        public static void WriteToDictionaryFile(string DictionaryFile, string newDoublet)
        {

            string filePath = FileProcess.DefineDictionary(DictionaryFile);

            if (activeDictionary.Count == 0)
            {
                File.AppendAllText(filePath, $"{newDoublet}", Encoding.UTF8);
            }
            else
            {
                File.AppendAllText(filePath, $"\n{newDoublet}", Encoding.UTF8);
            }

            Console.Clear();
            Console.WriteLine(lineChar1);
            Console.WriteLine($" A(z) '{newDoublet}' szópár bekerült a(z) '{DictionaryFile}' szótárba! Mit szeretnél tenni?");
            Console.WriteLine(lineChar1);

        }

        #endregion

        #region Delete Dictionary Items

        public static void DeleteFromDictionaryFileMethod(string DictionaryFile)
        {
            Console.Clear();
            FileProcess.ReadDataFromFile(DictionaryFile);

            // Aktuális szótárfile tartalmának listázása
            LoadActiveDictionaryItems(DictionaryFile);

            // Sor kiválasztása a szótárból
            int index = SelectIndexFromDictionary();


            bool refresh = RemoveItemfromActiveList(index, DictionaryFile);

            if (refresh)
            {
                RefreshActiveListFile(DictionaryFile);
                DeleteMethodRepeat(DictionaryFile);

                bool repeat = Methods.RepeatActualMethod();
                if (repeat)
                {
                    DeleteMethodRepeat(DictionaryFile);
                }
            }

        }

        public static void LoadActiveDictionaryItems(string DictionaryFile)
        {
            Console.WriteLine(lineChar1);
            Console.WriteLine($" {DictionaryFile} > Válaszd ki a módosítani kívánt elem sorszámát [?] a továbblépéshez!");
            Console.WriteLine(lineChar1);

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
            Console.WriteLine(lineChar1);
            Console.Write(" Választott sorszám: ");

            string userInput = Console.ReadLine();
            int index = -1;

            if (int.Parse(userInput) == 0)
            {
                Methods.ShowMainMenuMethod();
            }

            while (!int.TryParse(userInput, out int a) || int.Parse(userInput) > activeDictionary.Count || int.Parse(userInput) < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(lineChar1);
                Console.WriteLine(" Hiba! Nem létező sorszám!");
                Console.WriteLine(lineChar1);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Választott sorszám: ");

                userInput = Console.ReadLine();

                if (int.Parse(userInput) == 0)
                {
                    Methods.ShowMainMenuMethod();
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
            Console.WriteLine(lineChar1);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($" A következő sor '{selected}' törlésére készülsz! Biztosan törölni szeretnéd?");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(lineChar1);
            Console.WriteLine($" [0] Nem\n [1] Igen");
            Console.WriteLine(lineChar1);

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
                Console.WriteLine(lineChar1);
                Console.WriteLine($" A következő szópár '{selected}' törlésre került! Mit szeretnél tenni?");
                Console.WriteLine(lineChar1);
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
            Console.WriteLine(lineChar1);

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
                Methods.ShowMainMenuMethod();
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
            string filePath = FileProcess.DefineDictionary(DictionaryFile);

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
    }


}
