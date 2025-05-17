using System;
using System.IO;

class Program
{
    // Основной метод
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Выберите способ заполнения массива:");
            Console.WriteLine("1 - С клавиатуры");
            Console.WriteLine("2 - Случайным образом");
            Console.WriteLine("3 - Из файла");
            int choice = int.Parse(Console.ReadLine());

            int[] array = new int[0];

            if (choice == 1) array = FillArrayFromKeyboard();
            else if (choice == 2) array = FillArrayRandom();
            else if (choice == 3) array = FillArrayFromFile();

            Console.WriteLine("Исходный массив:");
            PrintArray(array);

            Console.WriteLine("Выберите метод сортировки:");
            Console.WriteLine("1 - Пузырьковая сортировка");
            Console.WriteLine("2 - Сортировка выбором");
            Console.WriteLine("3 - Быстрая сортировка");
            int sortChoice = int.Parse(Console.ReadLine());

            Console.WriteLine("Сортировать по возрастанию (1) или убыванию (2)?");
            bool ascending = Console.ReadLine() == "1";

            int[] sortedArray = (int[])array.Clone();

            if (sortChoice == 1) BubbleSort(sortedArray, ascending);
            else if (sortChoice == 2) SelectionSort(sortedArray, ascending);
            else if (sortChoice == 3) QuickSort(sortedArray, 0, sortedArray.Length - 1, ascending);

            Console.WriteLine("Отсортированный массив:");
            PrintArray(sortedArray);

            Console.WriteLine("Сохранить в файл? (y/n)");
            if (Console.ReadLine().ToLower() == "y") SaveToFile(sortedArray);

            Console.WriteLine("Нажмите любую клавишу для нового выполнения...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    // Заполнение массива с клавиатуры
    static int[] FillArrayFromKeyboard()
    {
        Console.Write("Введите количество элементов: ");
        int n = int.Parse(Console.ReadLine());
        int[] arr = new int[n];
        for (int i = 0; i < n; i++)
        {
            Console.Write("Элемент {0}: ", i + 1);
            arr[i] = int.Parse(Console.ReadLine());
        }
        return arr;
    }

    // Заполнение массива случайными числами
    static int[] FillArrayRandom()
    {
        Random rand = new Random();
        Console.Write("Введите количество элементов: ");
        int n = int.Parse(Console.ReadLine());
        int[] arr = new int[n];
        for (int i = 0; i < n; i++) arr[i] = rand.Next(1, 101);
        return arr;
    }

    // Заполнение массива из файла
    static int[] FillArrayFromFile()
    {
        Console.Write("Введите путь к файлу: ");
        string path = Console.ReadLine();
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            int[] arr = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++) arr[i] = int.Parse(lines[i]);
            return arr;
        }
        Console.WriteLine("Файл не найден!");
        return new int[0];
    }

    // Печать массива
    static void PrintArray(int[] arr)
    {
        foreach (int item in arr) Console.Write(item + " ");
        Console.WriteLine();
    }

    // Сохранение массива в файл
    static void SaveToFile(int[] arr)
    {
        Console.Write("Введите путь для сохранения: ");
        string path = Console.ReadLine();
        File.WriteAllLines(path, Array.ConvertAll(arr, x => x.ToString()));
        Console.WriteLine("Массив сохранён.");
    }

    // Пузырьковая сортировка
    static void BubbleSort(int[] arr, bool ascending)
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int j = 0; j < arr.Length - i - 1; j++)
            {
                if ((ascending && arr[j] > arr[j + 1]) || (!ascending && arr[j] < arr[j + 1]))
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    // Сортировка выбором
    static void SelectionSort(int[] arr, bool ascending)
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            int index = i;
            for (int j = i + 1; j < arr.Length; j++)
            {
                if ((ascending && arr[j] < arr[index]) || (!ascending && arr[j] > arr[index]))
                {
                    index = j;
                }
            }
            int temp = arr[i];
            arr[i] = arr[index];
            arr[index] = temp;
        }
    }

    // Быстрая сортировка
    static void QuickSort(int[] arr, int left, int right, bool ascending)
    {
        if (left < right)
        {
            int pivotIndex = Partition(arr, left, right, ascending);
            QuickSort(arr, left, pivotIndex - 1, ascending);
            QuickSort(arr, pivotIndex + 1, right, ascending);
        }
    }

    static int Partition(int[] arr, int left, int right, bool ascending)
    {
        int pivot = arr[right];
        int i = left;
        for (int j = left; j < right; j++)
        {
            if ((ascending && arr[j] <= pivot) || (!ascending && arr[j] >= pivot))
            {
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
                i++;
            }
        }
        int temp2 = arr[i];
        arr[i] = arr[right];
        arr[right] = temp2;
        return i;
    }
}
