using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LexiConsole
{

    class Methods:Data
    {

        #region Main Menu Settings

        // 0. Main Menu teljes metódus
        public static void ShowMainMenuMethod()
        {
            Console.Title = $@"Hello {Environment.UserName}! Welcome to the LexiC#onsol!   ¯\_(☉ᴗ☉)_/¯   [A|B]";

            Console.Clear();
            Console.WriteLine(lineChar2);
            Console.WriteLine($" Jó napot, {Environment.UserName}!");

            DictionaryProcess.ShowExistsDictionaries(true,false); // Aktuális szótárak betöltése, ha vannak. par.: true > A lista elrendezési módja: egy soros.  

            Console.WriteLine($"\n Üdv a főmenüben! Kérlek add meg a menüpont sorszámát [?] a továbblépéshez!");

            CreateMenu(MainMenuTags); // A paraméterként átadott tömb elemeiből egy listát hoz létre a menüpontokhoz.

            int MenuTag = SelectMenuTag(MainMenuTags); // A paraméterként átadott tömb elemei alapján egy létező tömb indexet ad vissza eredményül. Ez választja ki a menüpontot.

            SelectMainMenuMethod(MenuTag); // A MenuTag a tömb egy létező indexével tér vissza, és ezt paraméterként tovább adva hívja meg a hozzá tartozó következő metódust.
        }

        // 1. Menüpontok felépítése a csatolt tömb alapján
        public static void CreateMenu(Array arrayOfMenu)
        {
            Console.WriteLine(lineChar1);

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in arrayOfMenu)
            {
                Console.WriteLine($" [{Array.IndexOf(arrayOfMenu, item)}] {item}");

                if (Console.ForegroundColor == ConsoleColor.Red)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
               
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(lineChar1);
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

        #endregion

        #region Main Menu Methods
        public static void SelectMainMenuMethod(int menuTag)
        {

            switch (menuTag)
            {
                case 1:
                    Menu_Practice(menuTag);
                    break;
                case 2:
                    Menu_ListOfDict(menuTag);
                    break;
                case 3:
                    Menu_EditDictionaries(menuTag);
                    break;
                case 4:
                    Menu_DictionaryOperations(menuTag);
                    break;
                case 5:
                    Menu_CreateDictionary(menuTag);
                    break;
                case 6:
                    Menu_ListOfDictionaries(menuTag);
                    break;
                case 7:
                    Menu_Settings(menuTag);
                    break;
                case 8:
                    Menu_LoadRecordsMethod();
                    break;
                case 9:
                    ShowMainMenuMethod();
                    break;
                case 0:
                    Environment.Exit(menuTag);
                    break;
                default:
                ShowMainMenuMethod();
                break;
            }
        }

        #endregion


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
                               Excercise.LoadExcerciseMethod(mainMenuTag, subMenuTag, dictionaryName);
                            break;
                        }
                        break;

                    case 2:
                        switch (subMenuTag)
                        {
                            case 1: 
                            DictionaryProcess.EditDictionaryItemMethod(dictionaryName);
                                break;
                            case 2:
                            DictionaryProcess.DeleteFromDictionaryFileMethod(dictionaryName);
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
                            DictionaryProcess.WriteToDictionaryFileMethod(dictionaryName);
                            break;
                            case 2:
                            DictionaryProcess.EditDictionaryItemMethod(dictionaryName);
                            break;
                            case 3:
                            DictionaryProcess.DeleteFromDictionaryFileMethod(dictionaryName);
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
                            FileProcess.RenameDictionaryFileMethod(dictionaryName, MainMenuTags[mainMenuTag] + " -> " + SubMenuTags_4[subMenuTag]);
                                break;

                            case 2:
                            FileProcess.DeleteDictionaryFileMethod(dictionaryName, MainMenuTags[mainMenuTag] + " -> " + SubMenuTags_4[subMenuTag]);
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
            Console.WriteLine(lineChar1);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" [0] Kilépés");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" [1] Vissza");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(lineChar1);

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

        public static bool RepeatActualMethod()
        {
            bool repeat = false;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" [0] Kilépés");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(" [1] Vissza a főmenübe\n [2] Művelet ismétlése");
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
                repeat = true;
            }

            return repeat;

        }


        #region MainMenu_01 Gyakorlás
        public static void Menu_Practice(int MenuPoint)
        {
            Console.Clear();
            string dictionaryName = DictionaryProcess.SelectDictionary(MenuPoint);

            FileProcess.ReadDataFromFile(dictionaryName);

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

        #region MainMenu_02 Szótár tartalma
        public static void Menu_ListOfDict(int MenuPoint)
        {
            Console.Clear();
            string name = DictionaryProcess.SelectDictionary(MenuPoint);
            FileProcess.OpenDictionaryMethod(name);
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

            string dictionaryName = DictionaryProcess.SelectDictionary(MenuPoint);

            Console.Clear();
            ShowSubMenu(MainMenuTags[MenuPoint] + " -> " + dictionaryName, SubMenuTags_3);

            int MenuTag = SelectMenuTag(SubMenuTags_3);

            SelectSubMenuMethod(MenuPoint, MenuTag, dictionaryName);

            ShowFooterMenu();
        }
        #endregion

        #region MainMenu_04 Művelet szótárakkal
        public static void Menu_DictionaryOperations(int MenuPoint)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n Lehetséges műveletek: [Szótár átnevezése | Szótár törlése]");
            Console.ForegroundColor = ConsoleColor.White;

            string dictionaryName = DictionaryProcess.SelectDictionary(MenuPoint);

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
            FileProcess.CreateDictionaryMethod(MainMenuTags[MenuPoint]);
            ShowFooterMenu();
        }
        #endregion

        #region MainMenu_06 Összes szótáram
        public static void Menu_ListOfDictionaries(int MenuPoint)
        {
            Console.Clear();
            Console.WriteLine(lineChar2);
            Console.WriteLine($" {MainMenuTags[MenuPoint]}");
            Console.WriteLine(lineChar2);
            DictionaryProcess.ShowExistsDictionaries(false,false);
            ShowFooterMenu();
        }

        #endregion

        #region MainMenu_07 Rendszer beállítások
        public static void Menu_Settings(int MenuPoint)
        {
            Global g = new Global();

            Console.Clear();
            Console.WriteLine(lineChar2);
            Console.WriteLine($" {MainMenuTags[MenuPoint]}");
            Console.WriteLine(lineChar2);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n Fileok alapértelemzett helye:\n" + " " + g.GetDefDictionaryPath());
            Console.WriteLine("\n Alapértelemzett kiterjesztés:\n" + " " + g.GetDefExtension() + "\n");
            Console.ForegroundColor = ConsoleColor.White;

            ShowFooterMenu();
        }

        #endregion

        #region MainMenu_08 Rekordok
        public static void Menu_LoadRecordsMethod()
        {
            int userInput = Scores.LoadScores();
            Scores.SelectRecordDetails(userInput);
        }
        #endregion

    }
}
