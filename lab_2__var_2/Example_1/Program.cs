using System;

// Класс для узла дерева
class Node
{
    public int Data;
    public Node Left, Right;

    public Node(int data)
    {
        Data = data;
        Left = Right = null;
    }
}

// Класс для бинарного дерева поиска
class BST
{
    public Node Root;

    // Вставка узла
    public Node Insert(Node root, int data)
    {
        if (root == null)
            return new Node(data);

        if (data < root.Data)
            root.Left = Insert(root.Left, data);
        else if (data > root.Data)
            root.Right = Insert(root.Right, data);

        return root;
    }

    // Прямой обход (pre-order: корень, лево, право)
    public void PreOrder(Node root)
    {
        if (root == null) return;
        Console.Write(root.Data + " ");
        PreOrder(root.Left);
        PreOrder(root.Right);
    }

    // Симметричный обход (in-order: лево, корень, право)
    public void InOrder(Node root)
    {
        if (root == null) return;
        InOrder(root.Left);
        Console.Write(root.Data + " ");
        InOrder(root.Right);
    }

    // Обратный обход (post-order: лево, право, корень)
    public void PostOrder(Node root)
    {
        if (root == null) return;
        PostOrder(root.Left);
        PostOrder(root.Right);
        Console.Write(root.Data + " ");
    }

    // Высота дерева
    public int Height(Node root)
    {
        if (root == null) return 0;
        int leftHeight = Height(root.Left);
        int rightHeight = Height(root.Right);
        return Math.Max(leftHeight, rightHeight) + 1;
    }

    // Проверка движения от корня к узлу
    public void PathToNode(Node root, int value)
    {
        Console.Write($"Путь к {value}: ");
        Node current = root;
        while (current != null && current.Data != value)
        {
            Console.Write(current.Data + " -> ");
            if (value < current.Data)
                current = current.Left;
            else
                current = current.Right;
        }
        if (current != null)
            Console.WriteLine(current.Data);
        else
            Console.WriteLine("Узел не найден");
    }
}

class Program
{
    static void Main()
    {
        BST tree = new BST();
        int[] values = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        // Построение дерева
        foreach (int value in values)
        {
            tree.Root = tree.Insert(tree.Root, value);
        }

        Console.WriteLine("Прямой обход:");
        tree.PreOrder(tree.Root);
        Console.WriteLine();

        Console.WriteLine("Симметричный обход:");
        tree.InOrder(tree.Root);
        Console.WriteLine();

        Console.WriteLine("Обратный обход:");
        tree.PostOrder(tree.Root);
        Console.WriteLine();

        Console.WriteLine($"Высота дерева: {tree.Height(tree.Root)}");

        // Проверка пути к узлу (например, к 8)
        tree.PathToNode(tree.Root, 8);

        // Добавляем паузу, чтобы консоль не закрывалась
        Console.WriteLine("\nНажмите Enter, чтобы закрыть...");
        Console.ReadLine();
    }
}