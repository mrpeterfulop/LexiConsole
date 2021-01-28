using System;
using System.IO;
using System.Linq;
using System.Text;

namespace LexiConsole
{
    class FileProcess : Data
    {
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


            bool repeat = Methods.FileMethodRepeat();
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

                if (userInput.Length < 3)
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
                if (illegal.Contains(c) == true)
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
                    using (File.Create(newFile)) { }

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
                Methods.ShowMainMenuMethod();
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
                Methods.ShowMainMenuMethod();
            }

        }

        #endregion

        #region Rename Dictionary File

        public static void RenameDictionaryFileMethod(string dictionaryName, string MenuPoint)
        {

            Console.Clear();
            // 1. Név meghatározása 
            string userInput = dictionaryName;
            //string userInput = SetDictionaryName(MenuPoint);

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
                RenameDictionaryFileMethod(dictionaryName, MenuPoint);
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

                        var newName = Console.ReadLine().Trim();
                        var newFilePath = DefineDictionary(newName);


                        while (newName.Length > 64 || newName.Length < 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" Hiba!");
                            Console.ForegroundColor = ConsoleColor.White;

                            Console.Write(" Add meg a szótár új nevét: ");
                             newName = Console.ReadLine().Trim();
                             newFilePath = DefineDictionary(newName);
                        }

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
                                    Methods.ShowMainMenuMethod();
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
                Methods.ShowMainMenuMethod();
            }

        }

        #endregion

    }
    
}
