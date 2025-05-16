using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string inputFile = "input.txt";
        string outputFile = "output.txt";

        // Проверка существования файла
        if (!File.Exists(inputFile))
        {
            Console.WriteLine("Файл 'input.txt' не найден!");
            Console.WriteLine("Создайте файл 'input.txt' в папке с программой.");
            Console.WriteLine("Файл должен содержать матрицу смежности по шаблону:");
            Console.WriteLine("Пример:");
            Console.WriteLine("3");
            Console.WriteLine("0 1 1");
            Console.WriteLine("1 0 1");
            Console.WriteLine("1 1 0");
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
            return;
        }

        try
        {
            // Считываем строки из файла input.txt
            string[] lines = File.ReadAllLines(inputFile);

            // Первая строка — количество вершин
            int n = int.Parse(lines[0]);

            // Создаём матрицу смежности
            int[,] matrix = new int[n, n];

            // Заполняем матрицу из файла
            for (int i = 0; i < n; i++)
            {
                string[] row = lines[i + 1].Split(' ');
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = int.Parse(row[j]);
                }
            }

            // Проверка на симметричность (матрица неориентированного графа)
            bool isUndirected = true;

            for (int i = 0; i < n && isUndirected; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] != matrix[j, i] || (i == j && matrix[i, j] != 0))
                    {
                        isUndirected = false;
                        break;
                    }
                }
            }

            // Записываем результат в output.txt
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                writer.WriteLine(isUndirected ? "YES" : "NO");

                if (isUndirected)
                {
                    for (int i = 0; i < n; i++)
                    {
                        int degree = 0;
                        for (int j = 0; j < n; j++)
                        {
                            degree += matrix[i, j];
                        }
                        writer.Write(degree + " ");
                    }
                }
            }

            Console.WriteLine("Проверка завершена. Результат записан в 'output.txt'.");
        }
        catch (Exception ex)
        {
            // Выводим сообщение об ошибке, если данные в файле некорректны
            Console.WriteLine("Ошибка при обработке файла:");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine();
        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}
