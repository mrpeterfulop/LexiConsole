﻿using System;
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
        public static string[] MainMenuTags = new string[] { };
        public static string[] SubMenuTags_1 = new string[] { };
        public static string[] SubMenuTags_3 = new string[] { };
        public static string[] SubMenuTags_4 = new string[] { };
        public static string lineChar = " ----------------------------------------------------------------------------------------------------";
        public static string lineChar2 = " ====================================================================================================";

        public Methods()
        {


            Console.Title = $@"Hello {Environment.UserName}! Welcome to the LexiConsol!   ¯\_(☉ᴗ☉)_/¯   [A|B]";
            // Menüpontokat tartalmazó tömbök deklarálása, értékadás a konstruktor hívása közben
            MainMenuTags = new string[] { "Kilépés", "Gyakorlás", "Szótár tartalma", "Szótár szerkesztése", "Szótárműveletek", "Új szótár létrehozása", "Összes szótáram", "Beállítások", "Frissítés"};
            SubMenuTags_1 = new string[] { "Vissza", "idegen nyelvről -> magyar nyelvre", "magyar nyelvről -> idegen nyelvre", "véletlenszerű kikérdezés" };
            SubMenuTags_3 = new string[] { "Vissza", "Szavak bevitele", "Szavak módosítása","Szavak törlése"};
            SubMenuTags_4 = new string[] { "Vissza", "Szótár átnevezése", "Szótár törlése" };
        }

        // Üdvözlő szöveg, felhasználóra szabva
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
                    Console.Write($" Jeneleg nincsenek még szótáraid!");
                }

                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Console.Write($"[#{g.myDictionaries[i]}] ");
                    }

                    if (g.myDictionaries.Count > 4)
                    {
                        Console.Write($"... [+{g.myDictionaries.Count-4}]");
                    }
                }
                Console.WriteLine("\n");
            }

            else
            {
                foreach (var item in g.myDictionaries)
                {
                    Console.WriteLine($" # {g.myDictionaries.IndexOf(item) + 1} # {item}");
                }
            }
            Console.ForegroundColor = ConsoleColor.White;

        }

        // Szótárfájlok listából történő kiválasztása. Visszatérési érték: szótár neve
        public static string SelectDictionary(int MenuPoint)
        {
            Global g = new Global();
            int index = 0;

            // Ha nincsenek szótárfájlok, nem t
            if (g.myDictionaries.Count == 0 && MenuPoint != 5)
            {
                Console.WriteLine($" Jeneleg nincsenek még szótáraid!");
                ShowFooterMenu();
            }

            else
            {
                Console.WriteLine($"\n {MainMenuTags[MenuPoint]} - Válaszd ki a szótár sorszámát (# ? #) a továbblépéshez!");
                Console.WriteLine(lineChar);
                Console.ForegroundColor = ConsoleColor.Magenta;

                ShowExistsDictionaries(false);
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(lineChar);
                index = SelectDictionaryTag();

                return g.myDictionaries[index - 1];
            }

            return null;
        }

        #endregion

        #region Main Menu Settings

        // 0. Main Menu teljes metódus
        public static void ShowMainMenuMethod()
        {
            Console.Clear();
            ShowGreeting();
            Methods m = new Methods(); // Methods osztály konstruktor hívása minden alkalommal, amikor betölt a függvény. (Ezáltal mindig létrejönnek a menüpontokat tartalamzó tömbök)

            ShowExistsDictionaries(true); // Aktuális szótárak betöltése, ha vannak. par.: true > A lista elrendezési módja: egy soros.  

            Console.WriteLine($"\n Üdv a főmenüben! Kérlek add meg a menüpont sorszámát (# ? #) a továbblépéshez!");

            CreateMenu(MainMenuTags); // A paraméterként átadott tömb elemeiből egy listát hoz létre a menüpontokhoz.

            int MenuTag = SelectMenuTag(MainMenuTags); // A paraméterként átadott tömb elemei alapján egy létező tömb indexet ad vissza eredményül. Ez választja ki a menüpontot.

            SelectMainMenuMethod(MenuTag); // A MenuTag a tömb egy létező indexével tér vissza, és ezt paraméterként tovább adva hívja meg a hozzá tartozó következő metódust.
        }

        // 1. Menüpontok felépítése a csatolt tömb alapján
        public static void CreateMenu(Array arrayOfMenu)
        {
            Console.WriteLine(lineChar);
            
            foreach (var item in arrayOfMenu)
            {

                if ((Array.IndexOf(arrayOfMenu, item)) % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }

                Console.WriteLine($" # {Array.IndexOf(arrayOfMenu, item)} # {item}");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(lineChar);
        }

        // 2. Menüponthoz tartozó index lekérdezése
        public static int SelectMenuTag(Array menuArray)
        {
            Console.Write(" Választott menüpont: ");
            var menuTag = Console.ReadLine();

            

            while (!int.TryParse(menuTag, out int a) || int.Parse(menuTag) > menuArray.Length - 1)
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
        public static int SelectMainMenuMethod(int submenuTag)
        {
            var mainCase = 0;

            switch (submenuTag)
            {
                case 1:
                    Menu_Practice(1);
                    mainCase = 1;
                    break;
                case 2:
                    Menu_ListOfDict(2);
                    mainCase = 2;
                    break;
                case 3:
                    Menu_EditDictionaries(3);
                    mainCase = 3;
                    break;
                case 4:
                    Menu_DictionaryOperations(4);
                    mainCase = 4;
                    break;
                case 5:
                    Menu_CreateDictionary(5);
                    mainCase = 5;
                    break;
                case 6:
                    Menu_ListOfDictionaries(6);
                    break;
                case 7:
                    Menu_Settings(7);
                    break;
                case 8:
                    ShowMainMenuMethod();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                ShowMainMenuMethod();
                break;
        }
            return mainCase;
        }

        #endregion

        #region Main Menu Methods
        public static void SelectSubMenuMethod(int mainCase, int submenuMethod, string dictionaryName)
            {
                switch (mainCase)
                    {
                    case 1: // "Gyakorlás" menüponthoz tartozó metódus hívások
                        switch (submenuMethod)
                        {
                            case 1: // "Gyakorlás" menüponthoz tartozó 1. metódus hívás
                                Console.WriteLine(" Metódus: idegen nyelvről -> magyar nyelvre");
                                break;

                            case 2: // "Gyakorlás" menüponthoz tartozó 2. metódus hívás
                                Console.WriteLine(" Metódus: magyar nyelvről -> idegen nyelvre");

                                break;
                            case 3: // "Gyakorlás" menüponthoz tartozó 3. metódus hívás
                                Console.WriteLine(" Metódus: véletlenszerű kikérdezés");

                                break;
                            case 0: // FŐMENÜ!
                                Console.Clear();
                                ShowMainMenuMethod();
                                break;
                        }
                        break;

                    case 2: // "Szótár tartalma
                        break;

                    case 3: // "Szótár szerkesztése" menüponthoz tartozó metódus hívások
                        switch (submenuMethod)
                        {
                            case 1: // "Szótár szerkesztése" - Szavak bevitele
                            WriteToDictionaryFileMethod(dictionaryName);
                            break;
                            case 2: // "Szótár szerkesztése"- Szavak szerkesztése

                            break;
                            case 3: // "Szótár szerkesztése"- Szavak törlése
                            deleteFromDictionaryFileMethod(dictionaryName);
                            break;
                            case 0: // FŐMENÜ!
                                Console.Clear();
                                ShowMainMenuMethod();
                                break;
                        }
                        break;

                    case 4: // "Művelet a szótárakkal" menüponthoz tartozó metódus hívások
                        switch (submenuMethod)
                        {
                            case 1: // Szótár átnevezése
                            RenameDictionaryFileMethod(dictionaryName, MainMenuTags[mainCase] + " -> " + SubMenuTags_4[submenuMethod]);
                                break;

                            case 2: // Szótár törlése
                            DeleteDictionaryFileMethod(dictionaryName, MainMenuTags[mainCase] + " -> " + SubMenuTags_4[submenuMethod]);
                                break;

                            case 0: // FŐMENÜ!
                                Console.Clear();
                                ShowMainMenuMethod();
                                break;
                        }
                        break;
                }
            }
        public static void ShowSubMenu(string MenuPoint, Array subMenuArray)
        {
            Console.WriteLine($"\n {MenuPoint} -> Kérlek add meg a menüpont sorszámát (# ? #) a továbblépéshez!");
            CreateMenu(subMenuArray);
        }
        public static void ShowFooterMenu()
        {
            Console.WriteLine(lineChar);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(" # 0 # Kilépés\n # 1 # Vissza a főmenübe");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(lineChar);

            Console.Write(" Választott menüpont: ");

            var userInput = Console.ReadLine();

            while (!int.TryParse(userInput, out int a) || int.Parse(userInput) > 1)
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
        public static int SelectDictionaryTag()
        {
            Global g = new Global();
            Console.Write(" Választott szótár: ");
            var dictTag = Console.ReadLine();

            while (!int.TryParse(dictTag, out int a) || int.Parse(dictTag) > g.myDictionaries.Count() || dictTag == "0")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Hiba! Nem létező szám!");
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write(" Választott szótár: ");
                dictTag = Console.ReadLine();
            }

            return int.Parse(dictTag);
        }


        #region MainMenu_01 Gyakorlás
        public static void Menu_Practice(int MenuPoint)
        {
            Console.Clear();
            // Létező szótárak listázása
            string name = SelectDictionary(MenuPoint);

            Console.Clear();
            ShowSubMenu(MainMenuTags[MenuPoint] + " -> " + name, SubMenuTags_1);
            int MenuTag = SelectMenuTag(SubMenuTags_1);
            //SelectSubMenuMethod(MenuPoint, MenuTag);
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n Lehetséges műveletek [Új szavak bevitele | Szavak módosítása | Szavak törlése]");
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
            Console.WriteLine("\n Lehetséges műveletek [Szótár átnevezése | Szótár törlése]");
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
            Console.WriteLine(lineChar);
            Console.WriteLine($" Összes szótáram:");
            Console.WriteLine(lineChar);
            ShowExistsDictionaries(false);
            ShowFooterMenu();
        }

        #endregion

        #region MainMenu_08 Beállítások
        public static void Menu_Settings(int MenuPoint)
        {
            Global g = new Global();

            Console.Clear();
            Console.WriteLine(lineChar);
            Console.WriteLine($" {MainMenuTags[MenuPoint]}");
            Console.WriteLine(lineChar);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n Fileok alapértelemzett helye:\n" + " " + g.GetDefDictionaryPath());
            Console.WriteLine("\n Alapértelemzett kiterjesztés:\n" + " " + g.GetDefExtension()+"\n");
            Console.ForegroundColor = ConsoleColor.White ;

            ShowFooterMenu();
        }

        #endregion

        #endregion

        #region Write To Dictionary File

        public static void WriteToDictionaryFileMethod(string DictionaryFile)
        {
            //Adatbekérés a felhasználótól (2 szó)
            WriteToDictionaryFile(DictionaryFile, GetDataFromUser(DictionaryFile));
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
                    Console.WriteLine($" Hiba! A bevitt érték '{userInput}' már szerelep a szótárban, mint '{valueOfInput}'!");
                }
                if (keyOfInput.Length > 0)
                {
                    Console.WriteLine($" Hiba! A bevitt érték '{userInput}' már szerelep a szótárban, mint '{keyOfInput}'!");
                }
                Console.WriteLine(lineChar);
                Console.ForegroundColor = ConsoleColor.White;


            }

            return restart;
        }

        public static string GetDataFromUser(string DictionaryFile)
        {

            ReadDataFromFile(DictionaryFile);

            // ==============================================================================

            Console.WriteLine(lineChar);
            Console.Write(" Add meg a kifejezés 1. jelentését: ");

            string userInput_1 = Console.ReadLine();


            bool existKeyProblem = KeyValueCheckerMethod(userInput_1);

            while (existKeyProblem)
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

            while (existKeyProblem)
            {
                Console.Write(" Add meg a kifejezés 2. jelentését újra: ");
                userInput_2 = Console.ReadLine();
                existKeyProblem = KeyValueCheckerMethod(userInput_2);
            }
            // ==============================================================================


            return $"{userInput_1};{userInput_2}";


        }



        public static void WriteToDictionaryFile(string DictionaryFile,string newDoublet)
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

            Console.WriteLine(lineChar);
            Console.WriteLine($" A '{newDoublet}' szópár bekerült a(z) '{DictionaryFile}' szótárba!");

            Console.WriteLine(lineChar);
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine(" # 0 # Kilépés\n # 1 # Vissza a főmenübe\n # 2 # Új szavak bevitele!");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(lineChar);

            Console.Write(" Választott menüpont: ");

            var userInput = Console.ReadLine();

            while (!int.TryParse(userInput, out int a) || int.Parse(userInput) > 2)
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
                WriteToDictionaryFileMethod(DictionaryFile);
            }

            Console.WriteLine(lineChar);
        }

        #endregion

        #region Delete From Dictionary File

        public static void deleteFromDictionaryFileMethod(string DictionaryFile)
        {
            Console.Clear();
            ReadDataFromFile(DictionaryFile);

            // Aktuális szótárfile tartalmának listázása
            loadActiveDictionaryItems();

            // Sor kiválasztása a szótárból
            int index = SelectIndexFromDictionary();


            bool refresh = removeItemfromActiveList(index);

            if (refresh)
            {
                refreshActiveListFile(DictionaryFile);
            }


        }


        public static void loadActiveDictionaryItems()
        {

            Console.WriteLine(lineChar);
            Console.WriteLine(" Válaszd ki a törölni kívánt elem sorszámát [?] a továbblépéshez!");
            Console.WriteLine(lineChar);

            Console.ForegroundColor = ConsoleColor.Cyan;
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

            while (!int.TryParse(userInput, out int a) || int.Parse(userInput) > activeDictionary.Count)
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

            index = int.Parse(userInput)-1;

            return index;
        }

        public static bool removeItemfromActiveList(int index)
        {
            var selected = activeDictionary.ElementAt(index);

            bool refresh = false;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(lineChar);
            Console.WriteLine($" A következő sor '{selected}' törlésére készülsz! Biztosan törölni szeretnéd?\n # 0 # NEM\n # 1 # IGEN");
            Console.WriteLine(lineChar);
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(" Választott menüpont: ");
            string answer = Console.ReadLine();

            if (answer == "1")
            {
                activeDictionary.Remove(selected.Key);
                activeDictionary.Remove(selected.Value);
                Console.Clear();
                Console.WriteLine(lineChar);
                Console.WriteLine($" A következő szópár '{selected}' törlésre került! Mit szeretnél tenni?");
                Console.WriteLine(lineChar);

                refresh = true;            }

            else
            {
                Console.Clear();
                loadActiveDictionaryItems();
                index = SelectIndexFromDictionary();
                refresh = removeItemfromActiveList(index);
            }

            return refresh;
        }



        public static void refreshActiveListFile(string DictionaryFile)
        {
            string filePath = DefineDictionary(DictionaryFile);

            using (FileStream fs = new FileStream(filePath,FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {

                    foreach (var item in activeDictionary)
                    {
                        sw.WriteLine(item.Key + ";" + item.Value);
                    }

                }
            }


            //Console.WriteLine(" A szótár frissítése megtörtént!");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(" # 0 # Kilépés\n # 1 # Vissza a főmenübe\n # 2 # Újabb művelet");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(lineChar);

            Console.Write(" Választott menüpont: ");

            var userInput = Console.ReadLine();

            while (!int.TryParse(userInput, out int a) || int.Parse(userInput) > 2)
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
                deleteFromDictionaryFileMethod(DictionaryFile);
            }


        }

        #endregion



        #region Create Dictionary File
        public static void CreateDictionaryMethod(string MenuPoint)
        {
            // 1. Név meghatározása 
            string userInput = SetDictionaryName(MenuPoint);

            // 2.1 Név karaktervizsgálat
            var valid = IsDictionaryNameValid(userInput);

            // 2.2 Név létezésének vizsgálata
            if (valid)
            {
                var exist = IsDictionaryExist(userInput);
                CreateDictionaryFile(exist, userInput);
            }
            else
            {
                Console.WriteLine($"A megadott '{userInput}' érvénytelen!");
                CreateDictionaryMethod(MenuPoint);
            }
        }

        // 1. Név meghatározása 
        public static string SetDictionaryName(string MenuPoint)
        {
            var userInput = "";
            do
            {
                Console.Write($"\n {MenuPoint} -> Add meg az új szótár nevét: ");
                userInput = Console.ReadLine().Trim();

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
            if (exist == true)
            {
                Console.WriteLine(lineChar);
                Console.WriteLine(" A file már létezik! Szeretnéd felülírni?\n # 0 # NEM\n # 1 # IGEN");
                Console.WriteLine(lineChar);
                Console.Write(" Választott menüpont: ");
            }

            else
            {
                Console.WriteLine(lineChar);
                Console.WriteLine(" A file még nem létezik! Szeretnéd létrehozni?\n # 0 # NEM\n # 1 # IGEN");
                Console.WriteLine(lineChar);
                Console.Write(" Választott menüpont: ");
            }

            string answer = Console.ReadLine().Trim();

            if (answer == "1")
            {
                try
                {
                    var newFile = DefineDictionary(userInput);
                    using (File.Create(newFile)){}

                    if (File.Exists(newFile))
                    {
                        Console.Clear();
                        Console.WriteLine($"\n Az új szótár: '{userInput}' létrejött!");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine(" A szótár létrehozása sikertelen volt!");
                }

            }
            else
            {
                Console.Clear();
                ShowMainMenuMethod();
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

        public static void ReadDataFromFile(string DictionaryFile)
        {
            string filePath = DefineDictionary(DictionaryFile);
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
            Console.Clear();
            Console.WriteLine(lineChar);
            Console.WriteLine($" {DictionaryFile}");
            Console.WriteLine(lineChar);

            ReadDataFromFile(DictionaryFile);

            foreach (var szavak in activeDictionary)
            {
                Console.WriteLine(" " + szavak.Key + " - " + szavak.Value);
            }

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
            var answer = "";

            do
            {
            Console.WriteLine(lineChar);
            Console.WriteLine($" A következő szótár '{userInput}' törlésére készülsz. Biztosan törölni szeretnéd?\n # 0 # NEM\n # 1 # IGEN");
            Console.WriteLine(lineChar);
            Console.Write(" Választott menüpont: ");
            answer = Console.ReadLine().Trim();

            } while (answer!="0" && answer!="1");

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
            var answer = "";

            do
            {
                Console.Clear();
                Console.WriteLine(lineChar);
                Console.WriteLine($" A következő szótár '{userInput}' átnevezésére készülsz! Biztosan folytatod?\n # 0 # NEM\n # 1 # IGEN");
                Console.WriteLine(lineChar);
                Console.Write(" Választott menüpont: ");
                answer = Console.ReadLine().Trim();

            } while (answer != "0" && answer != "1");


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
                                Console.WriteLine($" A(z) '{newName}' szótár már létezik! Mit szeretnél tenni?\n # 0 # Vissza\n # 1 # Felülírás\n # 2 # Új név megadása");
                                Console.WriteLine(lineChar);
                                Console.Write(" Választott menüpont: ");

                                var answer2 = Console.ReadLine().Trim().ToUpper();

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
                catch (Exception e)
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
