using System;
using System.IO;

namespace Files
{
    class Program
    {
        const String Filename = "file.txt";
        
        static void Main(string[] args)
        {
            String path = @"C:\Users\user\source\repos\Week3\Files\bin\Debug\net6.0\loren.txt";
            Console.WriteLine("Hello Files!");
            #region запись в файл
            try
            {
                using (StreamWriter writer = new StreamWriter(Filename))
                {
                    writer.WriteLine("Hello Files w/ Using() {}!");

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
                    while((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
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