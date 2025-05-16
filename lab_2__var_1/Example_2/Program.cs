using System;

class BinaryTreeArray
{
    static int[] A = { 0, 3, 2, 4, 1, 6, 5, 8, 7, 9 }; // Массив A (индекс 0 не используется)

    // Корень
    static int Root()
    {
        return A[1];
    }

    // Левый потомок узла i
    static int LeftChild(int i)
    {
        int left = 2 * i;
        return left < A.Length ? A[left] : -1; // -1, если узла нет
    }

    // Правый потомок узла i
    static int RightChild(int i)
    {
        int right = 2 * i + 1;
        return right < A.Length ? A[right] : -1; // -1, если узла нет
    }

    // Родитель узла i
    static int Parent(int i)
    {
        int parent = i / 2;
        return parent > 0 ? A[parent] : -1; // -1, если родителя нет
    }

    static void Main()
    {
        Console.WriteLine($"Корень: {Root()}");

        // Пример для узла с индексом 2 (значение 2)
        int i = 2;
        Console.WriteLine($"Узел {A[i]}:");
        Console.WriteLine($"Левый потомок: {LeftChild(i)}");
        Console.WriteLine($"Правый потомок: {RightChild(i)}");
        Console.WriteLine($"Родитель: {Parent(i)}");

        // Пример для узла с индексом 3 (значение 4)
        i = 3;
        Console.WriteLine($"\nУзел {A[i]}:");
        Console.WriteLine($"Левый потомок: {LeftChild(i)}");
        Console.WriteLine($"Правый потомок: {RightChild(i)}");
        Console.WriteLine($"Родитель: {Parent(i)}");

        // Добавляем паузу, чтобы консоль не закрывалась
        Console.WriteLine("\nНажмите Enter, чтобы закрыть...");
        Console.ReadLine();
    }
}