using System;

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

            editModule.NewChar += changeModule.OnNewChar;
            editModule.NewChar += lengthModule.OnNewChar;
            editModule.NewChar += restrictedModule.OnNewChar;
            editModule.NewChar += CounterWords.Counter;

            saveModule.SaveText += changeModule.OnSaved;
            saveModule.SaveText += savedTimeModule.OnSaved;
            saveModule.SaveText += SaveCounter.OnSaved;
            editModule.Type();
        }
    }


    class EditModule
    {
        public event EventListener NewChar = null!;
        Program pr = new Program();
        public void Type()
        {
            String str = String.Empty;
            ConsoleKeyInfo keyPressed;
            Console.Clear();
            Console.WriteLine("Start typing... (ESC - exit; F2 - save)\n");
            do
            {
                // str[0] = Convert.ToChar(pr.stringCounter);
                keyPressed = Console.ReadKey();

                if (keyPressed.Key == ConsoleKey.Enter)
                {
                    Console.Write((char)10);
                    str += '\n';
                    pr.counterStrings++;
                    int left = Console.CursorLeft;
                    int top = Console.CursorTop;
                    Console.CursorLeft = 80;
                    Console.CursorTop = 1;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"Strings : [{pr.counterStrings}]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.CursorLeft = left;
                    Console.CursorTop = top;
                }
                else if (keyPressed.Key == ConsoleKey.F2)
                {
                    Program.saveModule.Save();
                    continue;
                }
                else str += keyPressed.KeyChar;

                NewChar?.Invoke(str);
                // if (NewChar != null) NewChar(str);
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
            if (str.Length % 40 == 0)
            {
                Console.Write((char)10);
                str += '\n';
            }
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
            //index = str.IndexOf("bomb", index + 1);


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
        private int stringCounter = 0;
        public EventListener counter = str =>
        {
        };
    }

    class CounterWords
    {
        private static String[] strMass;
        private static int counter = 0;
        public static void Counter(String str)
        {
            strMass = str.Split(new char[] { ' ', ',' });
            counter = strMass.Length;
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
