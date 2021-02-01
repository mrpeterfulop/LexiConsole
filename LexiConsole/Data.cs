using System.Collections.Generic;

namespace LexiConsole
{
    class Data
    {
        public static Dictionary<string, string> activeDictionary = new Dictionary<string, string>();

        public static string[] MainMenuTags = new string[] { "Kilépés", "Gyakorlás", "Szótár tartalma", "Szótár szerkesztése", "Művelet szótárakkal", "Új szótár létrehozása", "Összes szótáram", "Rendszer beállítások", "Mentett eredmények", "Frissítés" };
        public static string[] SubMenuTags_1 = new string[] { "Vissza", "idegen nyelvről > magyar nyelvre", "magyar nyelvről > idegen nyelvre", "véletlenszerű kikérdezés" };
        public static string[] SubMenuTags_1_1 = new string[] { "Vissza", "Minden szó 1x", "Futás megszakításig", "Kérdésszám megadása manuálisan" };
        public static string[] SubMenuTags_3 = new string[] { "Vissza", "Szavak bevitele", "Szavak módosítása", "Szavak törlése" };
        public static string[] SubMenuTags_4 = new string[] { "Vissza", "Szótár átnevezése", "Szótár törlése" };

        public static string lineChar1 = " " + new string('-', 100);
        public static string lineChar2 = " " + new string('=', 100);

    }
    
}
