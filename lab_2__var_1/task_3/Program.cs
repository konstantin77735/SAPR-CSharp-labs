using System;
using System.IO;
using System.Collections.Generic;

class Graph
{
    private int[,] matrix; // Матрица смежности (вес ребра, 0 = нет ребра)
    private int vertices;  // Количество вершин
    private int[,] coordinates; // Координаты вершин (x, y)


    // Конструктор графа
    public Graph(int v)
    {
        vertices = v; // Устанавливаем количество вершин
        matrix = new int[v, v]; // Инициализируем матрицу смежности размером v x v
        coordinates = new int[v, 2]; // Инициализируем массив для координат вершин (x, y)
    }

    // Метод для считывания графа из файла
    public void LoadFromFile(string filename)
    {
        var lines = File.ReadAllLines(filename); // Считываем все строки из файла
        vertices = int.Parse(lines[0]); // Первая строка — количество вершин
        matrix = new int[vertices, vertices]; // Инициализируем матрицу смежности
        coordinates = new int[vertices, 2]; // Инициализируем массив для координат вершин

        // Считываем координаты вершин (каждая вершина имеет два значения — x и y)
        for (int i = 0; i < vertices; i++)
        {
            var coords = lines[i + 1].Split(); // Разделяем строку на координаты x и y
            coordinates[i, 0] = int.Parse(coords[0]); // x-координата
            coordinates[i, 1] = int.Parse(coords[1]); // y-координата
        }

        // Считываем матрицу смежности
        for (int i = 0; i < vertices; i++)
        {
            var row = lines[i + vertices + 1].Split(); // Разделяем строку на элементы
            for (int j = 0; j < vertices; j++)
                matrix[i, j] = int.Parse(row[j]); // Заполняем элементы матрицы смежности
        }
    }

    // Метод для сохранения графа в файл
    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename)) // Открываем файл для записи
        {
            writer.WriteLine(vertices); // Записываем количество вершин
                                        // Записываем координаты всех вершин
            for (int i = 0; i < vertices; i++)
                writer.WriteLine($"{coordinates[i, 0]} {coordinates[i, 1]}"); // Координаты вершины

            // Записываем матрицу смежности
            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < vertices; j++)
                    writer.Write(matrix[i, j] + (j < vertices - 1 ? " " : "")); // Записываем элементы строки
                writer.WriteLine(); // Переход на новую строку после каждой строки матрицы
            }
        }
    }

    // Метод для вывода графа на экран (матрица смежности и координаты вершин)
    public void Display()
    {
        Console.WriteLine("Матрица смежности:");

        // Проходим по всем строкам матрицы смежности
        for (int i = 0; i < vertices; i++)
        {
            // Проходим по всем столбцам текущей строки матрицы
            for (int j = 0; j < vertices; j++)
                Console.Write(matrix[i, j] + " "); // Выводим значение элемента матрицы

            Console.WriteLine(); // Перенос строки после вывода всех элементов строки
        }

        Console.WriteLine("\nКоординаты вершин:");

        // Выводим координаты всех вершин
        for (int i = 0; i < vertices; i++)
            Console.WriteLine($"Вершина {i}: ({coordinates[i, 0]}, {coordinates[i, 1]})"); // Выводим x и y для каждой вершины
    }


    // Метод для добавления вершины
    public void AddVertex(int x, int y)
    {
        // Создаем новую матрицу смежности размером (vertices + 1) x (vertices + 1)
        var newMatrix = new int[vertices + 1, vertices + 1];
        // Копируем данные из старой матрицы в новую
        for (int i = 0; i < vertices; i++)
            for (int j = 0; j < vertices; j++)
                newMatrix[i, j] = matrix[i, j];

        // Создаем новый массив координат с увеличенным размером
        var newCoords = new int[vertices + 1, 2];
        // Копируем существующие координаты в новый массив
        for (int i = 0; i < vertices; i++)
        {
            newCoords[i, 0] = coordinates[i, 0]; //  копируем X-координату вершины i
            newCoords[i, 1] = coordinates[i, 1]; // копируем Y-координату вершины i
        }
        // Добавляем новые координаты для новой вершины
        newCoords[vertices, 0] = x;
        newCoords[vertices, 1] = y;

        // Увеличиваем количество вершин
        vertices++;
        // Обновляем матрицу смежности и координаты
        matrix = newMatrix;
        coordinates = newCoords;
    }

    // Метод для добавления ребра
    public void AddEdge(int u, int v, int weight)
    {
        // Устанавливаем вес ребра между вершинами u и v
        matrix[u, v] = weight;
        matrix[v, u] = weight; // Для неориентированного графа
    }

    // Метод для удаления вершины
    public void RemoveVertex(int v)
    {
        // Создаем новую матрицу смежности размером (vertices - 1) x (vertices - 1)
        var newMatrix = new int[vertices - 1, vertices - 1];
        // Создаем новый массив координат с уменьшенным размером
        var newCoords = new int[vertices - 1, 2];
        int row = 0, col;

        // Копируем данные в новую матрицу, пропуская удаляемую вершину
        for (int i = 0; i < vertices; i++)
        {
            if (i == v) continue; // Пропускаем строку удаляемой вершины

            col = 0; //на каждой итерации начинаем с 1-ой позиции

            // проходимся по столбцам текущей строки i
            for (int j = 0; j < vertices; j++)
            { //если это столбец удалямой вершины, то пропускаем его
                if (j == v) continue;

                //иначе копируем его в новую матрицу
                newMatrix[row, col] = matrix[i, j]; // Что это делает?
                col++; //увеличиваем, т.к. перешли на следующий столбец
            }
            // Копируем координаты оставшихся вершин
            newCoords[row, 0] = coordinates[i, 0]; // записывает X-координату текущей вершины в новый массив
            newCoords[row, 1] = coordinates[i, 1]; // записывает Y-координату текущей вершины в новый массив.
            row++; // ереходит к следующей строке в новом массиве, чтобы следующая вершина записалась на следующую строку
        }

        // Уменьшаем количество вершин
        vertices--;
        // Обновляем матрицу смежности и координаты
        matrix = newMatrix;
        coordinates = newCoords;
    }


    // Удаление ребра
    public void RemoveEdge(int u, int v)
    {
        // удаляем ребро u->v И ТАКЖЕ v->u
        matrix[u, v] = 0;
        matrix[v, u] = 0;
    }

    // Обход в ширину (BFS)
    public void BFS(int start)
    {
        bool[] visited = new bool[vertices]; // Массив для отслеживания посещённых вершин
        Queue<int> queue = new Queue<int>(); // Очередь для хранения вершин, которые нужно посетить
        visited[start] = true; // Отмечаем стартовую вершину как посещённую
        queue.Enqueue(start); // Добавляем стартовую вершину в очередь

        Console.WriteLine("Обход в ширину:");
        // Пока очередь не пуста, продолжаем обход
        while (queue.Count > 0)
        {
            int v = queue.Dequeue(); // Извлекаем вершину из очереди
            Console.Write(v + " "); // Выводим вершину
                                    // Перебираем все соседние вершины
            for (int i = 0; i < vertices; i++)
            {
                if (matrix[v, i] != 0 && !visited[i]) // Если есть ребро и вершина ещё не посещена
                {
                    visited[i] = true; // Отмечаем вершину как посещённую
                    queue.Enqueue(i); // Добавляем вершину в очередь для дальнейшего посещения
                }
            }
        }
        Console.WriteLine();
    }

    // Обход в глубину (DFS)
    public void DFS(int start)
    {
        bool[] visited = new bool[vertices]; // Массив для отслеживания посещённых вершин
        Console.WriteLine("Обход в глубину:");
        DFSUtil(start, visited); // Рекурсивный обход в глубину
        Console.WriteLine();
    }

    // Вспомогательный метод для обхода в глубину
    private void DFSUtil(int v, bool[] visited)
    {
        visited[v] = true; // Отмечаем вершину как посещённую
        Console.Write(v + " "); // Выводим вершину
        
        // Перебираем все соседние вершины
        for (int i = 0; i < vertices; i++)
            if (matrix[v, i] != 0 && !visited[i]) // Если есть ребро и вершина ещё не посещена
                DFSUtil(i, visited); // Рекурсивно вызываем DFS для соседней вершины
    }

    // Алгоритм Дейкстры для нахождения кратчайших путей от стартовой вершины
    public void Dijkstra(int start)
    {
        int[] dist = new int[vertices]; // Массив для хранения кратчайших расстояний от стартовой вершины
        bool[] visited = new bool[vertices]; // Массив для отслеживания посещённых вершин
        
        //цикл по вершинам
        for (int i = 0; i < vertices; i++)
            dist[i] = int.MaxValue; // Инициализируем все расстояния как бесконечность

        dist[start] = 0; // Расстояние от стартовой вершины до самой себя = 0
        for (int count = 0; count < vertices - 1; count++)
        {
            int u = MinDistance(dist, visited); // Находим вершину с минимальным расстоянием, которая ещё не была посещена
            visited[u] = true; // Отмечаем вершину как посещённую
            for (int v = 0; v < vertices; v++)
                if (!visited[v] && matrix[u, v] != 0 && dist[u] != int.MaxValue && dist[u] + matrix[u, v] < dist[v])
                    dist[v] = dist[u] + matrix[u, v]; // Обновляем расстояние до вершины v, если нашли более короткий путь
        }

        // Выводим кратчайшие расстояния до всех вершин
        Console.WriteLine("Кратчайшие пути (Дейкстра):");
        for (int i = 0; i < vertices; i++)
            Console.WriteLine($"До вершины {i}: {dist[i]}");
    }

    // Метод для нахождения вершины с минимальным расстоянием, которая ещё не была посещена
    private int MinDistance(int[] dist, bool[] visited)
    {
        int min = int.MaxValue, minIndex = -1;
        for (int v = 0; v < vertices; v++)
            if (!visited[v] && dist[v] <= min) // Ищем минимальное расстояние среди непосещённых вершин
            {
                min = dist[v];
                minIndex = v;
            }
        return minIndex; // Возвращаем индекс вершины с минимальным расстоянием
    }

    // Алгоритм Краскала для нахождения минимального остовного дерева
    public void Kruskal()
    {
        int[] parent = new int[vertices]; // Массив для хранения родителей вершин (для поиска и объединения)
        for (int i = 0; i < vertices; i++)
            parent[i] = i; // Инициализируем, что каждая вершина является родителем самой себя

        List<int[]> edges = new List<int[]>(); // Список рёбер [u, v, w] (где u и v — вершины, а w — вес ребра)
        for (int i = 0; i < vertices; i++)
            for (int j = i + 1; j < vertices; j++)
                if (matrix[i, j] != 0) // Если существует ребро между вершинами i и j
                    edges.Add(new int[] { i, j, matrix[i, j] }); // Добавляем ребро в список

        edges.Sort((a, b) => a[2].CompareTo(b[2])); // Сортируем рёбра по весу

        Console.WriteLine("Минимальное остовное дерево (Краскал):");
        foreach (var edge in edges)
        {
            int u = Find(parent, edge[0]); // Находим родителя вершины u
            int v = Find(parent, edge[1]); // Находим родителя вершины v
            if (u != v) // Если вершины принадлежат разным компонентам связности
            {
                Console.WriteLine($"Ребро {edge[0]}-{edge[1]}: вес {edge[2]}"); // Выводим ребро
                Union(parent, u, v); // Объединяем компоненты связности
            }
        }
    }



    class Program
    {
        static void Main()
        {
            Graph graph = null;
            while (true)
            {
                Console.WriteLine("\n1. Загрузить граф из файла");
                Console.WriteLine("2. Сохранить граф в файл");
                Console.WriteLine("3. Показать граф");
                Console.WriteLine("4. Добавить вершину");
                Console.WriteLine("5. Добавить ребро");
                Console.WriteLine("6. Удалить вершину");
                Console.WriteLine("7. Удалить ребро");
                Console.WriteLine("8. Обход в ширину (BFS)");
                Console.WriteLine("9. Обход в глубину (DFS)");
                Console.WriteLine("10. Алгоритм Дейкстры");
                Console.WriteLine("11. Алгоритм Краскала");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                int choice = int.Parse(Console.ReadLine());
                if (choice == 0) break;

                switch (choice)
                {
                    case 1:
                        Console.Write("Введите имя файла: ");
                        string filename = Console.ReadLine();
                        graph = new Graph(0);
                        graph.LoadFromFile(filename);
                        break;

                    case 2:
                        if (graph != null)
                        {
                            Console.Write("Введите имя файла для сохранения: ");
                            graph.SaveToFile(Console.ReadLine());
                        }
                        else
                            Console.WriteLine("Граф не загружен!");
                        break;

                    case 3:
                        if (graph != null) graph.Display();
                        else Console.WriteLine("Граф не загружен!");
                        break;

                    case 4:
                        if (graph != null)
                        {
                            Console.Write("Введите координаты (x y): ");
                            var coords = Console.ReadLine().Split();
                            int x = int.Parse(coords[0]), y = int.Parse(coords[1]);
                            graph.AddVertex(x, y);
                        }
                        else
                            Console.WriteLine("Граф не загружен!");
                        break;

                    case 5:
                        if (graph != null)
                        {
                            Console.Write("Введите вершины и вес (u v w): ");
                            var edge = Console.ReadLine().Split();
                            int u = int.Parse(edge[0]), v = int.Parse(edge[1]), w = int.Parse(edge[2]);
                            graph.AddEdge(u, v, w);
                        }
                        else
                            Console.WriteLine("Граф не загружен!");
                        break;

                    case 6:
                        if (graph != null)
                        {
                            Console.Write("Введите вершину для удаления: ");
                            int v = int.Parse(Console.ReadLine());
                            graph.RemoveVertex(v);
                        }
                        else
                            Console.WriteLine("Граф не загружен!");
                        break;

                    case 7:
                        if (graph != null)
                        {
                            Console.Write("Введите ребро для удаления (u v): ");
                            var edge = Console.ReadLine().Split();
                            int u = int.Parse(edge[0]), v = int.Parse(edge[1]);
                            graph.RemoveEdge(u, v);
                        }
                        else
                            Console.WriteLine("Граф не загружен!");
                        break;

                    case 8:
                        if (graph != null)
                        {
                            Console.Write("Введите начальную вершину: ");
                            int start = int.Parse(Console.ReadLine());
                            graph.BFS(start);
                        }
                        else
                            Console.WriteLine("Граф не загружен!");
                        break;

                    case 9:
                        if (graph != null)
                        {
                            Console.Write("Введите начальную вершину: ");
                            int start = int.Parse(Console.ReadLine());
                            graph.DFS(start);
                        }
                        else
                            Console.WriteLine("Граф не загружен!");
                        break;

                    case 10:
                        if (graph != null)
                        {
                            Console.Write("Введите начальную вершину: ");
                            int start = int.Parse(Console.ReadLine());
                            graph.Dijkstra(start);
                        }
                        else
                            Console.WriteLine("Граф не загружен!");
                        break;

                    case 11:
                        if (graph != null)
                            graph.Kruskal();
                        else
                            Console.WriteLine("Граф не загружен!");
                        break;
                }
            }
        }
    }
}