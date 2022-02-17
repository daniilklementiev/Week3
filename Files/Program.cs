using System;
using System.IO;

namespace Files
{
    class Program
    {
        const String Filename = "file.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello Files!");
            try
            {
                using (StreamWriter writer = new StreamWriter(Filename))
                {
                    writer.WriteLine("Hello Files w/ Using() {}!");

                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.WriteLine("Done");
        }
    }
}