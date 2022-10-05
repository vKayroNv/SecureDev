using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataToSettings
{
    internal class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            Console.Title = Properties.Settings.Default.ApplicationNameDebug;
#else
            Console.Title = Properties.Settings.Default.ApplicationName;
#endif

            if (string.IsNullOrEmpty(Properties.Settings.Default.Name) || Properties.Settings.Default.Age == 0)
            {
                Console.Write("Введите имя: ");
                Properties.Settings.Default.Name = Console.ReadLine();

                while (true)
                {
                    Console.Write("Введите возраст: ");
                    if (byte.TryParse(Console.ReadLine(), out byte age))
                    {
                        Properties.Settings.Default.Age = age;
                        break;
                    }
                    else
                    {
                        Console.CursorTop--;
                        Console.CursorLeft = 0;
                        Console.Write(new string(' ', Console.BufferWidth - 1));
                        Console.CursorLeft = 0;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Имя: {Properties.Settings.Default.Name}");
            Console.WriteLine($"Возраст: {Properties.Settings.Default.Age}");

            Console.ReadKey(true);
        }
    }
}
