using System;
using System.Collections.Generic;
using System.IO;

namespace LexiConsole
{
    public class Global
        {
            public List<string> myDictionaries = new List<string>();
            private string DefaultExtension { get; set; }
            private string DefaultDictionaryPath { get; set; }

            public Global()
            {
                SetDefDictionaryPath();
                SetDefExtension();
                GetAllDictionaryName();
            }

            public void GetAllDictionaryName()
            {
                var fullpath = GetDefDictionaryPath();

                string[] fileArray = Directory.GetFiles(fullpath);
                foreach (var item in fileArray)
                {
                    myDictionaries.Add(Path.GetFileNameWithoutExtension(item));
                }
            }

            public string GetDefExtension()
            {
                return DefaultExtension;
            }

            private void SetDefExtension()
            {
                DefaultExtension = ".csv";
            }

            public string GetDefDictionaryPath()
            {
                return DefaultDictionaryPath;
            }

            private void SetDefDictionaryPath()
            {
                DefaultDictionaryPath = Path.GetPathRoot(Environment.SystemDirectory) + @"Users\" + Environment.UserName + @"\Documents\LexiConsole\Dictionaries\";

                if (!Directory.Exists(DefaultDictionaryPath))
                {
                    Directory.CreateDirectory(DefaultDictionaryPath);
                }

            }
        }
    
}
