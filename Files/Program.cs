

namespace Files
{
    class Program
    {
        const String Filename = "./TextFile1.txt";
        const String CurrentDir = "./";

        private static String GetProjectRoot()
        {
            String exePath = Directory.GetCurrentDirectory();
            // Поиск корневой папки приложения - exe-файл находится в bin/debug/net6.0/.exe
            // задание: извлечь из пути часть, отвечающую за корень проекта
            // проверить работу путём вывода на экран файла-конспекта TextFile1.txt
            int binIndex = exePath.IndexOf("\\bin\\");
            if (binIndex == -1) return null;
            return exePath.Substring(0, binIndex);
        }

        private static void GetFilePath(String fileName)
        {
            String searchable = '*' + fileName + '*'; // форматирование имени для поиска по фрагменту
            String[] allFoundFiles = Directory.GetFiles(@"C:\Users\user\source\repos\Week3\", searchable, SearchOption.AllDirectories); // массив путей
            if(allFoundFiles.Length > 0) // условие если существует хоть один путь
            {
                Console.WriteLine("Результаты поиска файла с таким именем: ");
                foreach (string file in allFoundFiles)
                {
                    Console.WriteLine(file);
                }
            }
            else
            {
                Console.WriteLine("Файл с таким именем не найден");
            }
            
        }

        static void Main(string[] args)
        {
            // String filename = GetProjectRoot() + @"\sicker.txt\";

            Console.WriteLine("Поиск файла в директориях!");
            Console.Write("Введите имя файла: ");
            String searchableFile = Console.ReadLine();
            GetFilePath(searchableFile);
            Console.WriteLine("Желаете открыть файла? (y/n) : ");
            String choice = Console.ReadLine();
            if(choice == "y")
            {
                Console.WriteLine("Введите путь желаемого файла : ");
                String path = Console.ReadLine();
                try
                {
                    Console.WriteLine("Содержимое: ");
                    using (StreamReader sr = new StreamReader(path))
                    {
                        Console.WriteLine(sr.ReadToEnd());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Спасибо что доверяете нам. ");
            }
            
            
            
            
        }
        static void Main3(string[] args)
        {
            Directory.GetCurrentDirectory();
            Console.WriteLine("Поиск файлов");
            Console.Write("Введите имя файла: ");
            String name = Console.ReadLine();
            name = '*' + name + '*';

            Console.WriteLine("Результаты поиска:");
            Console.WriteLine("Найдены папки: ");
            foreach(String dir in Directory.EnumerateDirectories(CurrentDir, name)) 
            {
                Console.WriteLine("{0, 15} <DIR> {1, -5}", 
                    Directory.GetCreationTime(dir).ToShortDateString(),
                    dir);
            }
            Console.WriteLine("Найдены файлы: ");
            foreach (String dir in Directory.EnumerateFiles(CurrentDir, name))
            {
                Console.WriteLine("{0, 15} {1, 15}",
                   File.GetCreationTime(dir).ToShortDateString(),
                    dir);
            }
            Console.WriteLine("----------------");
        }
        static void Main2(string[] args)
        {
            // File 
            // Directory - стат класс для создания, перемещения, вывода списка
            Console.WriteLine("Directories");
            foreach (String dir in Directory.EnumerateDirectories(CurrentDir))
            {
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Files");
            foreach (String fileName in Directory.EnumerateFiles(CurrentDir))
            {
                Console.WriteLine("{0} {1} {2} {3}",
                File.GetCreationTime(fileName).ToShortDateString(),
                File.GetCreationTime(fileName).ToShortTimeString(),
                new FileInfo(fileName).Length,
                fileName);
            }
        }

        static void Main1(string[] args)
        {
            String path = @"C:\Users\user\source\repos\Week3\Files\bin\Debug\net6.0\loren.txt";
            Console.WriteLine("Hello Files!");
            #region запись в файл
            try
            {
                using (StreamWriter writer = new StreamWriter(Filename))
                {
                    writer.WriteLine("Hello Files w/ Using() {}!");
                    Console.WriteLine("Writing done");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            #region чтение 
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                    Console.WriteLine("Reading done");
                }
            } catch (Exception ex)
            {
                Console.WriteLine("This file couldn't be read");
                Console.WriteLine(ex.Message);
            }
            #endregion
            
            Console.WriteLine("Done");
        }
    }
}