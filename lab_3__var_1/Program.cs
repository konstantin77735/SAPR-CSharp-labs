using System;
using System.IO;

class Program
{
    static void Main()
    {
        int[] array = null;
        int size = 0;
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Ввод массива с клавиатуры");
            Console.WriteLine("2. Заполнение массива случайными числами");
            Console.WriteLine("3. Загрузка массива из файла");
            Console.WriteLine("4. Просмотр массива");
            Console.WriteLine("5. Сортировка по возрастанию");
            Console.WriteLine("6. Сортировка по убыванию");
            Console.WriteLine("7. Сохранение массива в файл");
            Console.WriteLine("8. Выход");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": // Ввод с клавиатуры
                    Console.Write("Введите размер массива: ");
                    size = int.Parse(Console.ReadLine());
                    array = new int[size];
                    for (int i = 0; i < size; i++)
                    {
                        Console.Write($"Элемент {i}: ");
                        array[i] = int.Parse(Console.ReadLine());
                    }
                    break;

                case "2": // Случайное заполнение
                    Console.Write("Введите размер массива: ");
                    size = int.Parse(Console.ReadLine());
                    Console.Write("Введите минимальное значение: ");
                    int min = int.Parse(Console.ReadLine());
                    Console.Write("Введите максимальное значение: ");
                    int max = int.Parse(Console.ReadLine());
                    array = new int[size];
                    Random rand = new Random();
                    for (int i = 0; i < size; i++)
                    {
                        array[i] = rand.Next(min, max + 1);
                    }
                    Console.WriteLine("Массив заполнен случайными числами.");
                    break;

                case "3": // Загрузка из файла
                    Console.Write("Введите путь к файлу: ");
                    string inputPath = Console.ReadLine();
                    try
                    {
                        string[] lines = File.ReadAllLines(inputPath);
                        size = lines.Length;
                        array = new int[size];
                        for (int i = 0; i < size; i++)
                        {
                            array[i] = int.Parse(lines[i]);
                        }
                        Console.WriteLine("Массив загружен из файла.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                    break;

                case "4": // Просмотр массива
                    if (array == null || array.Length == 0)
                    {
                        Console.WriteLine("Массив пуст.");
                    }
                    else
                    {
                        Console.WriteLine("Массив:");
                        Console.WriteLine(string.Join(" ", Array.ConvertAll(array, x => x.ToString())));
                    }
                    break;

                case "5": // Сортировка по возрастанию
                    if (array == null || array.Length == 0)
                    {
                        Console.WriteLine("Массив пуст.");
                    }
                    else
                    {
                        QuickSort(array, 0, array.Length - 1, ascending: true);
                        Console.WriteLine("Массив отсортирован по возрастанию.");
                    }
                    break;

                case "6": // Сортировка по убыванию
                    if (array == null || array.Length == 0)
                    {
                        Console.WriteLine("Массив пуст.");
                    }
                    else
                    {
                        QuickSort(array, 0, array.Length - 1, ascending: false);
                        Console.WriteLine("Массив отсортирован по убыванию.");
                    }
                    break;

                case "7": // Сохранение в файл
                    if (array == null || array.Length == 0)
                    {
                        Console.WriteLine("Массив пуст.");
                    }
                    else
                    {
                        Console.Write("Введите путь для сохранения файла: ");
                        string outputPath = Console.ReadLine();
                        try
                        {
                            File.WriteAllLines(outputPath, Array.ConvertAll(array, x => x.ToString()));
                            Console.WriteLine("Массив сохранен в файл.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                    }
                    break;

                case "8": // Выход
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    // Реализация быстрой сортировки (Quicksort)
    static void QuickSort(int[] array, int low, int high, bool ascending)
    {
        if (low < high)
        {
            int pi = Partition(array, low, high, ascending);
            QuickSort(array, low, pi - 1, ascending);
            QuickSort(array, pi + 1, high, ascending);
        }
    }

    static int Partition(int[] array, int low, int high, bool ascending)
    {
        int pivot = array[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            bool swapCondition = ascending ? array[j] <= pivot : array[j] >= pivot;
            if (swapCondition)
            {
                i++;
                Swap(array, i, j);
            }
        }
        Swap(array, i + 1, high);
        return i + 1;
    }

    static void Swap(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
}