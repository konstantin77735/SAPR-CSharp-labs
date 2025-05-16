using System;

// Класс для узла дерева
class Node
{
    public int Data; // Значение узла
    public Node Left, Right; // Ссылки на левого и правого потомков

    // Конструктор узла
    public Node(int data)
    {
        Data = data;
        Left = Right = null; // Изначально потомков нет
    }
}

// Класс для бинарного дерева поиска
class BST
{
    public Node Root; // Корень дерева

    // Метод для вставки узла в дерево
    public Node Insert(Node root, int data)
    {
        // Если текущий узел пустой, создаём новый узел с заданным значением
        if (root == null)
            return new Node(data);

        // Если значение меньше текущего узла, вставляем в левое поддерево
        if (data < root.Data)
            root.Left = Insert(root.Left, data);
        // Если значение больше текущего узла, вставляем в правое поддерево
        else if (data > root.Data)
            root.Right = Insert(root.Right, data);

        // Возвращаем текущий узел
        return root;
    }

    // Прямой обход дерева (pre-order: корень, лево, право)
    public void PreOrder(Node root)
    {
        if (root == null) return; // Если узел пустой, выходим

        Console.Write(root.Data + " "); // Выводим текущее значение
        PreOrder(root.Left); // Рекурсивно обходим левое поддерево
        PreOrder(root.Right); // Рекурсивно обходим правое поддерево
    }

    // Симметричный обход дерева (in-order: лево, корень, право)
    public void InOrder(Node root)
    {
        if (root == null) return;

        InOrder(root.Left); // Сначала обходим левое поддерево
        Console.Write(root.Data + " "); // Затем выводим текущее значение
        InOrder(root.Right); // Затем обходим правое поддерево
    }

    // Обратный обход дерева (post-order: лево, право, корень)
    public void PostOrder(Node root)
    {
        if (root == null) return;

        PostOrder(root.Left); // Сначала обходим левое поддерево
        PostOrder(root.Right); // Затем обходим правое поддерево
        Console.Write(root.Data + " "); // В конце выводим текущее значение
    }

    // Метод для вычисления высоты дерева
    public int Height(Node root)
    {
        if (root == null) return 0; // Базовый случай: пустое дерево имеет высоту 0

        // Высота левого и правого поддеревьев
        int leftHeight = Height(root.Left);
        int rightHeight = Height(root.Right);

        // Высота дерева — это 1 (текущий узел) + максимальная высота поддеревьев
        return Math.Max(leftHeight, rightHeight) + 1;
    }

    // Метод для вывода пути от корня до узла с заданным значением
    public void PathToNode(Node root, int value)
    {
        Console.Write($"Путь к {value}: ");

        Node current = root; // Начинаем с корня

        // Пока текущий узел не равен искомому и не пустой
        while (current != null && current.Data != value)
        {
            Console.Write(current.Data + " -> "); // Выводим текущее значение

            // Если искомое значение меньше текущего узла, переходим влево
            if (value < current.Data)
                current = current.Left;
            // Иначе переходим вправо
            else
                current = current.Right;
        }

        // Если узел найден, выводим его значение, иначе — узел не найден
        if (current != null)
            Console.WriteLine(current.Data);
        else
            Console.WriteLine("Узел не найден");
    }
}

// Основной класс программы
class Program
{
    static void Main()
    {
        BST tree = new BST(); // Создаём новое дерево

        // Массив значений для вставки в дерево
        int[] values = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        // Вставляем каждое значение в дерево
        foreach (int value in values)
        {
            tree.Root = tree.Insert(tree.Root, value);
        }

        // Выполняем обходы дерева
        Console.WriteLine("Прямой обход:");
        tree.PreOrder(tree.Root);
        Console.WriteLine();

        Console.WriteLine("Симметричный обход:");
        tree.InOrder(tree.Root);
        Console.WriteLine();

        Console.WriteLine("Обратный обход:");
        tree.PostOrder(tree.Root);
        Console.WriteLine();

        // Выводим высоту дерева
        Console.WriteLine($"Высота дерева: {tree.Height(tree.Root)}");

        // Проверяем путь до узла с значением 8
        tree.PathToNode(tree.Root, 8);

        // Пауза, чтобы консоль не закрылась сразу
        Console.WriteLine("\nНажмите Enter, чтобы закрыть...");
        Console.ReadLine();
    }
}
