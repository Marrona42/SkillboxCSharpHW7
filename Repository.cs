using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NewTry
{
    internal class Repository
    {
        private Worker[] workers;
        private string path;
        int index;
        string[] titles;
        public int Count { get { return this.index; } }

        public Repository(string Path)
        {
            this.path = Path; // Сохранение пути к файлу с данными
            this.index = 1; // текущая позиция для добавления сотрудника в workers
            this.titles = new string[0]; // инициализаия массива заголовков   
            this.workers = new Worker[1]; // инициализаия массива сотрудников.

            this.Load(); // Загрузка данных
        }
        private void Load()
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                titles = sr.ReadLine().Split('#');

                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('#');

                    Add(new Worker(Convert.ToString(index), args[1], args[2], args[3], args[4],
                        args[5], args[6], args[7], args[8]));
                }
            }
        }

        public void Add(Worker ConcreteWorker)
        {
            this.Resize(index >= this.workers.Length);
            this.workers[index] = ConcreteWorker;
            this.index++;
        }

        private void Resize(bool Flag)
        {
            if (Flag)
            {
                Array.Resize(ref this.workers, this.workers.Length * 3);
            }
        }

        public void Save(string Path)
        {
            string temp = String.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}#{7}#{8}",
                                            this.titles[0],
                                            this.titles[1],
                                            this.titles[2],
                                            this.titles[3],
                                            this.titles[4],
                                            this.titles[5],
                                            this.titles[6],
                                            this.titles[7],
                                            this.titles[8]
                                            );

            File.AppendAllText(Path, $"{temp}\n");

            for (int i = 0; i < this.index; i++)
            {
                temp = String.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}#{7}#{8}",
                                        this.workers[i].Identity,
                                        this.workers[i].InsertionDate,
                                        this.workers[i].LastName,
                                        this.workers[i].FirstName,
                                        this.workers[i].Patronymic,
                                        this.workers[i].Birthday,
                                        this.workers[i].Age,
                                        this.workers[i].Height,
                                        this.workers[i].BirthPlace
                                        );
                File.AppendAllText(Path, $"{temp}\n");
            }
        }

        public void GetAllWorkers()
        {
            PrintTitles();

            for (int i = 0; i < index; i++)
            {
                Console.WriteLine(this.workers[i].Print());
            }
            Console.ReadKey();
        }

        public void PrintTitles()
        {
            Console.WriteLine($"{this.titles[0],4} {this.titles[1],20} {this.titles[2],15} {this.titles[3],15} {this.titles[4],15} " +
                            $"{this.titles[5],23} {this.titles[6],11} {this.titles[7],11} {this.titles[8],21}");
        }

        public void AddWorkerToBase()
        {
            Console.Write("Введите Фамилию: ");
            string name = Console.ReadLine();
            Console.Write("Введите Имя: ");
            string surename = Console.ReadLine();
            Console.Write("Введите Отчество: ");
            string patronymic = Console.ReadLine();

            Console.Write("Введите Дату рождения: ");

            DateTime dob; // date of birth
            string input;
            do
            {
                Console.WriteLine("Введите дату рождения в формате дд.ММ.гггг (день.месяц.год):");
                input = Console.ReadLine();
            }
            while (!DateTime.TryParseExact(input, "dd.MM.yyyy", null, DateTimeStyles.None, out dob));

            DateTime nowx = new DateTime();
            nowx = DateTime.Today;
            TimeSpan cute = nowx.Subtract(dob);
            int abc = Convert.ToInt32(cute.TotalDays);
            abc = abc / 365;

            Console.Write("Введите Рост в см: ");
            string height = Convert.ToString(Integer());

            Console.Write("Введите Место рождения: ");
            string place = Console.ReadLine();

            Add(new Worker(Convert.ToString(index), Convert.ToString(DateTime.Now),
                name, surename, patronymic, Convert.ToString(dob), Convert.ToString(abc), height, place));
        }

        public int Integer()
        {
            string input;
            int a;
            do
            {
                input = Console.ReadLine();
            } while (!int.TryParse(input, out a));
            return a;
        }

        public void Delete(int indexToDelete)
        {
            int lenght = this.workers.Length;

            for (int i = 0; i < lenght; i++)
            {
                if (i == indexToDelete)
                {
                    for (int j = i; j < lenght - 1; j++)
                        this.workers[j] = this.workers[j + 1];
                }
            }
            index--;
        }

        public void Import(DateTime date1, DateTime date2)
        {
            PrintTitles();
            for (int i = 0; i < this.workers.Length; i++)
            {
                int resultX = DateTime.Compare(date1, Convert.ToDateTime(this.workers[i].InsertionDate));
                int resultY = DateTime.Compare(date2, Convert.ToDateTime(this.workers[i].InsertionDate));


                if (resultX < 0 && resultY > 0)
                {
                    Console.WriteLine(this.workers[i].Print());
                }
            }
        }

        public void WorkerById(int insert)
        {
            PrintTitles();
            for (int i = 0; i < this.workers.Length; i++)
            {
                if (Convert.ToInt32(this.workers[i].Identity) == insert)
                {
                    Console.WriteLine(this.workers[i].Print());
                }
            }
        }

        public void SorterByHeight()
        {
            PrintTitles();

            var sortHeight = from p in this.workers
                             orderby p.Height
                             select p;
            foreach (var p in sortHeight)
                Console.WriteLine($"{p.Identity,4} " + $"{p.InsertionDate,20} " + $"{p.LastName,15} " + $"{p.FirstName,15} " +
                    $"{p.Patronymic,15} " + $"{p.Birthday,23} " + $"{p.Age,11} " + $"{p.Height,11} " + $"{p.BirthPlace,21}");

            Console.ReadKey();
        }

        public void SortedByInsertionData()
        {
            PrintTitles();

            var sortHeight = from p in this.workers
                             orderby p.InsertionDate
                             select p;
            foreach (var p in sortHeight)
                Console.WriteLine($"{p.Identity,4} " + $"{p.InsertionDate,20} " + $"{p.LastName,15} " + $"{p.FirstName,15} " +
                    $"{p.Patronymic,15} " + $"{p.Birthday,23} " + $"{p.Age,11} " + $"{p.Height,11} " + $"{p.BirthPlace,21}");

            Console.ReadKey();
        }

        public void SortedByFirstName()
        {
            PrintTitles();

            var sortHeight = from p in this.workers
                             orderby p.FirstName
                             select p;
            foreach (var p in sortHeight)
                Console.WriteLine($"{p.Identity,4} " + $"{p.InsertionDate,20} " + $"{p.LastName,15} " + $"{p.FirstName,15} " +
                    $"{p.Patronymic,15} " + $"{p.Birthday,23} " + $"{p.Age,11} " + $"{p.Height,11} " + $"{p.BirthPlace,21}");

            Console.ReadKey();
        }

        public void SortedByLastName()
        {
            PrintTitles();

            var sortHeight = from p in this.workers
                             orderby p.LastName
                             select p;
            foreach (var p in sortHeight)
                Console.WriteLine($"{p.Identity,4} " + $"{p.InsertionDate,20} " + $"{p.LastName,15} " + $"{p.FirstName,15} " +
                    $"{p.Patronymic,15} " + $"{p.Birthday,23} " + $"{p.Age,11} " + $"{p.Height,11} " + $"{p.BirthPlace,21}");

            Console.ReadKey();
        }

        public void SortedByPatronymic()
        {
            PrintTitles();

            var sortHeight = from p in this.workers
                             orderby p.Patronymic
                             select p;
            foreach (var p in sortHeight)
                Console.WriteLine($"{p.Identity,4} " + $"{p.InsertionDate,20} " + $"{p.LastName,15} " + $"{p.FirstName,15} " +
                    $"{p.Patronymic,15} " + $"{p.Birthday,23} " + $"{p.Age,11} " + $"{p.Height,11} " + $"{p.BirthPlace,21}");

            Console.ReadKey();
        }

        public void SortedByAge()
        {
            PrintTitles();

            var sortHeight = from p in this.workers
                             orderby p.Age
                             select p;
            foreach (var p in sortHeight)
                Console.WriteLine($"{p.Identity,4} " + $"{p.InsertionDate,20} " + $"{p.LastName,15} " + $"{p.FirstName,15} " +
                    $"{p.Patronymic,15} " + $"{p.Birthday,23} " + $"{p.Age,11} " + $"{p.Height,11} " + $"{p.BirthPlace,21}");

            Console.ReadKey();
        }

        public void SortedByBirthday()
        {
            PrintTitles();

            var sortHeight = from p in this.workers
                             orderby p.Birthday
                             select p;
            foreach (var p in sortHeight)
                Console.WriteLine($"{p.Identity,4} " + $"{p.InsertionDate,20} " + $"{p.LastName,15} " + $"{p.FirstName,15} " +
                    $"{p.Patronymic,15} " + $"{p.Birthday,23} " + $"{p.Age,11} " + $"{p.Height,11} " + $"{p.BirthPlace,21}");

            Console.ReadKey();
        }

        public void SortedByBirthplace()
        {
            PrintTitles();

            var sortHeight = from p in this.workers
                             orderby p.BirthPlace
                             select p;
            foreach (var p in sortHeight)
                Console.WriteLine($"{p.Identity,4} " + $"{p.InsertionDate,20} " + $"{p.LastName,15} " + $"{p.FirstName,15} " +
                    $"{p.Patronymic,15} " + $"{p.Birthday,23} " + $"{p.Age,11} " + $"{p.Height,11} " + $"{p.BirthPlace,21}");

            Console.ReadKey();
        }

        public DateTime TestDate()
        {
            DateTime time;
            string input;
            do
            {
                Console.WriteLine("Введите дату дд.ММ.гггг (день.месяц.год):");
                input = Console.ReadLine();
            }
            while (!DateTime.TryParseExact(input, "dd.MM.yyyy", null, DateTimeStyles.None, out time));
            return time;
        }
    }
}
