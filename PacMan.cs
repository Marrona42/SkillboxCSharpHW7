using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace NewTry
{
    internal class PacMan
    {
        public void Main()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.SetWindowSize(100, 30);

            char[,] map = ReadMap("map.txt"); //Чтение карты для игры с файла игры
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false); //Считывание кнопки

            Task.Run(() =>
            {
                while (true)
                {
                    pressedKey = Console.ReadKey();
                }
            });

            int pacmanX = 1;
            int pacmanY = 1;
            int score = 0;
            int enemyX = 29;
            int enemyY = 3;
            int enemy2X = 7;
            int enemy2Y = 7;

            Random rand = new Random();

            while (true) //Бесконечный цикл для того чтобы игра не закрывалась
            {
                Console.SetCursorPosition(0, 0); //Очистка консоли для новой отрисовки

                int keyRandom = rand.Next(0, 4);
                int keyRandom2 = rand.Next(0, 4);

                HandleInput(pressedKey, ref pacmanX, ref pacmanY, map, ref score);
                AutoInput(keyRandom, ref enemyX, ref enemyY, map);
                AutoInput(keyRandom2, ref enemy2X, ref enemy2Y, map);


                Console.ForegroundColor = ConsoleColor.Blue; // Отрисовка цвета стен
                DrawMap(map); //отрисовка карты

                Console.ForegroundColor = ConsoleColor.Yellow; //Перекраска пакмана
                Console.SetCursorPosition(pacmanX, pacmanY); //Изменение позиции курсора для отрисовки пакмана
                Console.Write("@"); //Отрисовка Пакмана

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(enemyX, enemyY);
                Console.Write("*");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(enemy2X, enemy2Y);
                Console.Write("*");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(34, 0);
                Console.WriteLine($"Score: {score}");
                Console.SetCursorPosition(34, 1);
                Console.Write("Наберите 40 очков");
                Console.SetCursorPosition(34, 3);
                Console.WriteLine($"Рандом: {keyRandom}");
                Console.SetCursorPosition(34, 4);
                Console.WriteLine($"Рандом: {keyRandom2}");


                Thread.Sleep(200);

                if (score == 40)
                {
                    Console.Clear();
                    Console.WriteLine("Вы победили!");
                    Console.ReadKey();
                    break;
                }

                if (map[enemyX, enemyY] == map[pacmanX, pacmanY] || map[enemy2X, enemy2Y] == map[pacmanX, pacmanY])
                {
                    Console.Clear();
                    Console.WriteLine("Вы Проиграли!");
                    Console.ReadKey();
                    break;
                }
            }
        }

        private static char[,] ReadMap(string path) //Получение карты в Массив
        {
            string[] file = File.ReadAllLines("map.txt"); //Чтение файла в массив

            char[,] map = new char[GetMaxLenghtOfLine(file), file.Length]; //Получение рамера массива

            for (int x = 0; x < map.GetLength(0); x++) //Цикл для того, чтобы поместить каждый элемент файла в элемент массива
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] = file[y][x];

            return map; //Возвращаем отрисованную карту
        }

        private static void DrawMap(char[,] map) //Рисование карты
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.Write("\n");
            }
        }

        private static void HandleInput(ConsoleKeyInfo pressedKey, ref int pacmanX, ref int pacmanY, char[,] map, ref int score)
        {
            int[] direction = GetDirrection(pressedKey);

            int nextPacmanPositionX = pacmanX + direction[0];
            int nextPacmanPositionY = pacmanY + direction[1];

            char nextCell = map[nextPacmanPositionX, nextPacmanPositionY];

            if (nextCell == ' ' || nextCell == '.')
            {
                pacmanX = nextPacmanPositionX;
                pacmanY = nextPacmanPositionY;

                if (nextCell == '.')
                {
                    score++;
                    map[nextPacmanPositionX, nextPacmanPositionY] = ' ';
                }
            }
        }

        private static void AutoInput(int keyRandom, ref int enemyX, ref int enemyY, char[,] map)
        {
            int[] errection = { 0, 0 };

            switch (keyRandom)
            {
                case 0:
                    errection[1] = -1;
                    break;
                case 1:
                    errection[1] = 1;
                    break;
                case 2:
                    errection[0] = -1;
                    break;
                case 3:
                    errection[0] = 1;
                    break;
            }

            int nextEnemyPositionX = enemyX + errection[0];
            int nextEnemyPositionY = enemyY + errection[1];

            char enemyCell = map[nextEnemyPositionX, nextEnemyPositionY];

            if (enemyCell == ' ' || enemyCell == '.')
            {
                enemyX = nextEnemyPositionX;
                enemyY = nextEnemyPositionY;
            }
        }

        private static int[] GetDirrection(ConsoleKeyInfo pressedKey)
        {
            int[] dirrection = { 0, 0 };

            if (pressedKey.Key == ConsoleKey.UpArrow)
                dirrection[1] = -1;
            else if (pressedKey.Key == ConsoleKey.DownArrow)
                dirrection[1] = 1;
            else if (pressedKey.Key == ConsoleKey.LeftArrow)
                dirrection[0] = -1;
            else if (pressedKey.Key == ConsoleKey.RightArrow)
                dirrection[0] = 1;

            return dirrection;
        }

        private static int GetMaxLenghtOfLine(string[] lines) //Получение масимального размера карты файла
        {
            int maxLength = lines[0].Length;

            foreach (var line in lines)
                if (line.Length > maxLength)
                    maxLength = line.Length;

            return maxLength;
        }
    }
}
