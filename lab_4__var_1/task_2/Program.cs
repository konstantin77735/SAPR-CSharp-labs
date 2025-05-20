using System;
using System.Collections.Generic;

class Program
{
    // Класс для представления процесса
    class Process
    {
        public int Start { get; set; }
        public int Finish { get; set; }
        public int Index { get; set; }

        public Process(int index, int start, int finish)
        {
            Index = index;
            Start = start;
            Finish = finish;
        }
    }

    static void Main()
    {
        Console.WriteLine("Задача выбора процессов:\n");
        Console.WriteLine("Цель: выбрать максимально возможное количество процессов, которые не пересекаются по времени.");
        Console.WriteLine("Введите количество процессов: ");
        int processCount = int.Parse(Console.ReadLine());

        List<Process> processes = new List<Process>();

        // Ввод данных от пользователя
        for (int i = 0; i < processCount; i++)
        {
            Console.WriteLine("Введите начальное и конечное время процесса {0} (через пробел):", i + 1);
            string[] input = Console.ReadLine().Split(' ');
            int start = int.Parse(input[0]);
            int finish = int.Parse(input[1]);
            processes.Add(new Process(i + 1, start, finish));
        }

        // Выводим все процессы
        Console.WriteLine("\nВсе процессы:");
        foreach (Process p in processes)
        {
            Console.WriteLine("Процесс {0}: Начало: {1}, Окончание: {2}", p.Index, p.Start, p.Finish);
        }

        Console.WriteLine("\nСортируем процессы по времени окончания для выбора...");
        processes.Sort((x, y) => x.Finish.CompareTo(y.Finish));

        // Список для хранения выбранных процессов
        List<Process> selectedProcesses = new List<Process>();

        // Выбираем первый процесс
        selectedProcesses.Add(processes[0]);
        int lastFinishTime = processes[0].Finish;

        // Проходим по оставшимся процессам
        for (int i = 1; i < processes.Count; i++)
        {
            // Если текущий процесс не перекрывается с последним выбранным
            if (processes[i].Start >= lastFinishTime)
            {
                // Добавляем процесс в список выбранных
                selectedProcesses.Add(processes[i]);
                lastFinishTime = processes[i].Finish;
            }
        }

        // Выводим выбранные процессы с пояснением
        Console.WriteLine("\nВыбранные процессы:");
        foreach (Process p in selectedProcesses)
        {
            Console.WriteLine("Процесс {0}: Начало: {1}, Окончание: {2}", p.Index, p.Start, p.Finish);
        }

        Console.WriteLine("\nПочему выбраны именно эти процессы?");
        Console.WriteLine("Сначала отсортировали все процессы по времени окончания. Затем последовательно выбирали процессы, которые начинаются после окончания последнего выбранного процесса. Так мы минимизируем пересечения и максимизируем количество выбранных процессов.");
        Console.WriteLine("Пример: Если Процесс 1 заканчивается раньше других, он выбирается первым. Затем выбирается следующий, который начинается после его окончания и так далее.");

        // Ожидание ввода для предотвращения закрытия приложения
        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}