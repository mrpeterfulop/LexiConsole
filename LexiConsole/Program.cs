using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LexiConsole
{

    class Program
    {
            static void Main(string[] args)
            {
                Console.Title = @"MyLex [☉ᴗ☉]";

                Methods.ShowMainMenuMethod();

                Console.ReadLine();
            }
    }
    
}
