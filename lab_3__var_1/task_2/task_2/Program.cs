using System;

// Класс узла дерева
public class BinaryTreeNode<T> where T : IComparable<T>
{
    public T Value;
    public BinaryTreeNode<T> Left;
    public BinaryTreeNode<T> Right;

    public BinaryTreeNode(T value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

// Класс бинарного дерева
public class BinaryTree<T> where T : IComparable<T>
{
    private BinaryTreeNode<T> _head;
    private int _count;

    // Добавление узла
    public void Add(T value)
    {
        BinaryTreeNode<T> newNode = new BinaryTreeNode<T>(value);

        if (_head == null)
        {
            _head = newNode;
        }
        else
        {
            BinaryTreeNode<T> current = _head;
            BinaryTreeNode<T> parent;

            while (true)
            {
                parent = current;
                if (value.CompareTo(current.Value) < 0)
                {
                    current = current.Left;
                    if (current == null)
                    {
                        parent.Left = newNode;
                        break;
                    }
                }
                else
                {
                    current = current.Right;
                    if (current == null)
                    {
                        parent.Right = newNode;
                        break;
                    }
                }
            }
        }

        _count++;
    }

    // Удаление узла
    public bool Remove(T value)
    {
        BinaryTreeNode<T> current, parent;
        current = FindWithParent(value, out parent);

        if (current == null)
        {
            return false;
        }

        _count--;

        // Случай 1: У узла нет правого ребенка
        if (current.Right == null)
        {
            if (parent == null)
            {
                _head = current.Left;
            }
            else
            {
                if (parent.Left == current)
                    parent.Left = current.Left;
                else
                    parent.Right = current.Left;
            }
        }
        // Случай 2: У правого ребенка нет левого ребенка
        else if (current.Right.Left == null)
        {
            current.Right.Left = current.Left;
            if (parent == null)
            {
                _head = current.Right;
            }
            else
            {
                if (parent.Left == current)
                    parent.Left = current.Right;
                else
                    parent.Right = current.Right;
            }
        }
        // Случай 3: У правого ребенка есть левый ребенок
        else
        {
            BinaryTreeNode<T> leftmost = current.Right.Left;
            BinaryTreeNode<T> leftmostParent = current.Right;

            while (leftmost.Left != null)
            {
                leftmostParent = leftmost;
                leftmost = leftmost.Left;
            }

            leftmostParent.Left = leftmost.Right;
            leftmost.Left = current.Left;
            leftmost.Right = current.Right;

            if (parent == null)
            {
                _head = leftmost;
            }
            else
            {
                if (parent.Left == current)
                    parent.Left = leftmost;
                else
                    parent.Right = leftmost;
            }
        }

        return true;
    }

    // Метод для проверки существования узла
    public bool Contains(T value)
    {
        BinaryTreeNode<T> parent;
        return FindWithParent(value, out parent) != null;
    }

    // Метод для поиска узла и его родителя
    private BinaryTreeNode<T> FindWithParent(T value, out BinaryTreeNode<T> parent)
    {
        BinaryTreeNode<T> current = _head;
        parent = null;

        while (current != null)
        {
            int result = value.CompareTo(current.Value);

            if (result < 0)
            {
                parent = current;
                current = current.Left;
            }
            else if (result > 0)
            {
                parent = current;
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        return current;
    }

    // Количество узлов в дереве
    public int Count => _count;

    // Очистка дерева
    public void Clear()
    {
        _head = null;
        _count = 0;
    }

    // Метод для завершения программы
    public void WaitForExit()
    {
        Console.WriteLine("Нажмите любую клавишу для завершения...");
        Console.ReadKey();
    }
}

// Главный класс программы
public class Program
{
    public static void Main(string[] args)
    {
        BinaryTree<int> tree = new BinaryTree<int>();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Добавить узел");
            Console.WriteLine("2 - Удалить узел");
            Console.WriteLine("3 - Проверить наличие узла");
            Console.WriteLine("4 - Показать количество узлов");
            Console.WriteLine("5 - Очистить дерево");
            Console.WriteLine("6 - Выйти");

            int choice;
            int.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1:
                    Console.Write("Введите значение для добавления: ");
                    int valueToAdd;
                    if (int.TryParse(Console.ReadLine(), out valueToAdd))
                    {
                        tree.Add(valueToAdd);
                        Console.WriteLine("Узел добавлен.");
                    }
                    break;

                case 2:
                    Console.Write("Введите значение для удаления: ");
                    int valueToRemove;
                    if (int.TryParse(Console.ReadLine(), out valueToRemove))
                    {
                        bool removed = tree.Remove(valueToRemove);
                        Console.WriteLine(removed ? "Узел удален." : "Узел не найден.");
                    }
                    break;

                case 3:
                    Console.Write("Введите значение для поиска: ");
                    int valueToFind;
                    if (int.TryParse(Console.ReadLine(), out valueToFind))
                    {
                        bool exists = tree.Contains(valueToFind);
                        Console.WriteLine(exists ? "Узел найден." : "Узел не найден.");
                    }
                    break;

                case 4:
                    Console.WriteLine("Количество узлов: " + tree.Count);
                    break;

                case 5:
                    tree.Clear();
                    Console.WriteLine("Дерево очищено.");
                    break;

                case 6:
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }

            Console.WriteLine();
        }

        tree.WaitForExit();
    }
}
