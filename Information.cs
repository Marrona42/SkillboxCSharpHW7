using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NewTry
{
    internal class Information
    {
        public void Test(string path)
        {
            if (File.Exists(path))
            {
                Console.WriteLine("Файл загружен");
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
            else
            {
                File.AppendAllText(path, "ID#Дата Добавления#Фамилия#Имя#Отчество#Дата рождения#Возраст#Рост#Место Рождения");
                Console.WriteLine("Файл создан");
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
        }

        public void Intro()
        {
            Console.WriteLine($"Введите одну из следующих комманд:");
            Console.WriteLine("Ввод - для ввода нового пользователя");
            Console.WriteLine("Вывод - для вывода базы данных на консколь");
            Console.WriteLine("Сортировка - для выбора метода сортировки");
            Console.WriteLine("Диапазон - для вывода данных в определенном временном диапазоне");
            Console.WriteLine("Удалить - для удаление выбранной записи по ID");
            Console.WriteLine("Рабочий - вывод рабочего по ID");
            Console.WriteLine("Выход - для выхода из программы");
            Console.WriteLine("Помощь - для информации");
        }

        public void Help()
        {
            Console.Clear();
            string[] lines = File.ReadAllLines("help.txt");
            foreach (var line in lines)
                Console.WriteLine(line);
        }

    }
}
