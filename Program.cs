using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NewTry
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(190, 50);
            bool programIsOn = true;
            string path = @"dataBase.txt";
            Information info = new Information();
            info.Test(path);
            Repository dataBase = new Repository(path);
            PacMan egg = new PacMan();

            while (programIsOn)
            {
                Console.SetCursorPosition(0, 1);
                info.Intro();
                switch (Console.ReadLine().ToLower())
                {
                    case "ввод":
                        dataBase.AddWorkerToBase();
                        File.Delete(path);
                        dataBase.Save(path);
                        break;

                    case "вывод":
                        Console.Clear();
                        dataBase.GetAllWorkers();
                        break;

                    case "выход":
                        programIsOn = false;
                        break;

                    case "сортировка":
                        Console.Clear();
                        Console.WriteLine("Дата ввода, Фамилия, Имя, Отчество, Возраст, Рост, Дата рождения, Место рождения");
                        Console.Write("Выберите метод сортировки: ");
                        switch (Console.ReadLine().ToLower())
                        {
                            case "дата ввода":
                                dataBase.SortedByInsertionData();
                                break;
                            case "фамилия":
                                dataBase.SortedByLastName();
                                break;
                            case "имя":
                                dataBase.SortedByFirstName();
                                break;
                            case "отчетсво":
                                dataBase.SortedByPatronymic();
                                break;
                            case "возраст":
                                dataBase.SortedByAge();
                                break;
                            case "рост":
                                dataBase.SorterByHeight();
                                break;
                            case "дата рождения":
                                dataBase.SortedByBirthday();
                                break;
                            case "место рождения":
                                dataBase.SortedByBirthplace();
                                break;
                                default:
                                Console.WriteLine("Неверный ввод! Возврат в главное меню");
                                Console.ReadKey();
                                break;
                        }
                        break;

                    case "диапазон":
                        Console.WriteLine("Диапазон в работе");
                        dataBase.Import(dataBase.TestDate(), dataBase.TestDate());
                        Console.ReadKey();
                        break;

                    case "удалить":
                        dataBase.Delete(dataBase.Integer());
                        File.Delete(path);
                        dataBase.Save(path);
                        break;

                    case "рабочий":
                        dataBase.WorkerById(int.Parse(Console.ReadLine()));
                        Console.ReadKey();
                        break;

                    case "яйцо":
                        egg.Main();
                        Console.SetWindowSize(190, 50);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "помощь":
                        info.Help();
                        Console.ReadLine();
                        break;

                    default:
                        Console.WriteLine("Неправильный Ввод!");
                        break;
                }

                Console.Clear();
            }




        }
    }
}
