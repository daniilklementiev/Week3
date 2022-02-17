using System;
using System.Text.RegularExpressions;

namespace Events
{
    delegate void EventListener(string str);
    

    class Program
    {
        public int counterStrings = 1;
        public static SaveModule saveModule = null!;
        static void Main(string[] args)
        {
            Console.Title = "Блокнот";
            Console.WriteLine("Hello events");
            var editModule = new EditModule();
            var changeModule = new ChangeModule();
            var lengthModule = new LengthModule();
            saveModule = new SaveModule();
            var savedTimeModule = new SavedTimeModule();
            var restrictedModule = new CheckerModule();
            var counterStrings = new CounterStrings();
            var counterWords = new CounterWords();

            editModule.NewChar += changeModule.OnNewChar;
            editModule.NewChar += lengthModule.OnNewChar;
            editModule.NewChar += restrictedModule.OnNewChar;
            editModule.NewChar += counterWords.Counter;
            editModule.NewChar += counterStrings.Counter;

            saveModule.SaveText += changeModule.OnSaved;
            saveModule.SaveText += savedTimeModule.OnSaved;
            saveModule.SaveText += SaveCounter.OnSaved;
            editModule.Type();
        }
    }


    class EditModule
    {
        public event EventListener NewChar = null!;
        public void Type()
        {
            String str = String.Empty;
            // MatchCollection lines = Regex.Matches(str, "\n"); // регулярное выражение для подсчёта строк
            Int32 lineLength = 0;
            ConsoleKeyInfo keyPressed;
            Console.Clear();
            Console.WriteLine("Start typing... (ESC - exit; F2 - save)\n");
            do
            {
                keyPressed = Console.ReadKey();
                if (keyPressed.Key == ConsoleKey.Enter)
                {
                    lineLength = 0;
                    Console.Write((char)10);
                    str += '\n';
                }
                else if (lineLength % 100 == 0 && str.Length > 0)
                {
                    lineLength = 0;
                    Console.Write((char)10);
                    str += '\n';
                }
                else if (keyPressed.Key == ConsoleKey.F2)
                {
                    Program.saveModule.Save();
                    continue;
                }
                else str += keyPressed.KeyChar;
                lineLength += 1;
                NewChar?.Invoke(str);
            } while (keyPressed.Key != ConsoleKey.Escape);
        }
    }

    class ChangeModule
    {
        public void OnNewChar(string str)
        {
            PrintSymbol('*');
        }
        public EventListener OnSaved = str =>
        {
            if (str.Equals("OK")) PrintSymbol('+');
            else PrintSymbol('x');
        };
        
        private static void PrintSymbol(char symb)
        {
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            Console.CursorLeft = 0;
            Console.CursorTop = 1;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"[{symb}]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorLeft = left;
            Console.CursorTop = top;
        }
    }

    class LengthModule
    {
        public EventListener OnNewChar = str =>
        {
            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            Console.CursorLeft = 45;
            Console.CursorTop = 1;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Symbols ({str.Length})");
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorLeft = left;
            Console.CursorTop = top;
            
        };
    }

    class SaveModule
    {
        public event EventListener SaveText;
        public void Save()
        {
            // Code saved text will be here
            SaveText?.Invoke("OK");
        }
    }
    class SavedTimeModule
    {
        public EventListener OnSaved = str =>
        {
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            Console.CursorLeft = 5;
            Console.CursorTop = 1;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Last save : {DateTime.Now.ToShortTimeString()}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorLeft = left;
            Console.CursorTop = top;

        };
    }
    class SaveCounter
    {
        private static int counter = 0;
        public static EventListener OnSaved = str =>
        {
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            Console.CursorLeft = 23;
            Console.CursorTop = 1;
            counter++;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($" | Saved {counter} times | ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorLeft = left;
            Console.CursorTop = top;
        };
    }
    class CheckerModule
    {
        private String[] words = new String[] { "bomb", "terrorist", "attack", "nigger" };
        private int bombCounter;

        public void OnNewChar(string str)
        {
            str = str.ToLower();
            bombCounter = 0;


            foreach (String it in words)
            {
                int index = -1;
                do
                {
                    index = str.IndexOf(it, index + 1);
                    if (index != -1)
                    {
                        bombCounter++;
                    }
                } while (index >= 0);
            }

            if (bombCounter > 0)
            {
                int left = Console.CursorLeft;
                int top = Console.CursorTop;
                Console.CursorLeft = 90;
                Console.CursorTop = 1;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Restricted words: ({bombCounter})");
                Console.ForegroundColor = ConsoleColor.White;
                Console.CursorLeft = left;
                Console.CursorTop = top;
            }
        }
    }

    class CounterStrings
    {
        public EventListener Counter = str =>
        {
            MatchCollection matches = Regex.Matches(str, "\n");
            Int32 lines = matches.Count + 1;
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            Console.CursorLeft = 45;
            Console.CursorTop = 0;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Strings: ({lines})");
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorLeft = left;
            Console.CursorTop = top;
        };
    }

    class CounterWords
    {
        public void Counter(String str)
        {
            int counter = 0;
            var strTemp = str.Split(new char[] {' ', ',', '.'});
            foreach (var word in strTemp)
            {
                if(!String.IsNullOrEmpty(word) && word != "," && word != ".")
                {
                    counter++;
                }
            }
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            Console.CursorLeft = 65;
            Console.CursorTop = 1;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Words : [{counter}]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorLeft = left;
            Console.CursorTop = top;
        }
    }
}
