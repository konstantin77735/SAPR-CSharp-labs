using System;
using System.Collections.Generic;

class Program
{
    // Главный метод
    static void Main()
    {
        // Вывод инструкции
        Console.WriteLine("Введите выражение в обратной польской записи (например: ab*c+):");

        // Чтение входной строки
        string input = Console.ReadLine();

        // Стек для хранения промежуточных выражений
        Stack<string> stack = new Stack<string>();

        // Проход по каждому символу во входной строке
        foreach (char ch in input)
        {
            // Если символ — операнд (буква), просто добавляем в стек
            if (Char.IsLetterOrDigit(ch))
            {
                stack.Push(ch.ToString());
            }
            else // Иначе — оператор
            {
                // Извлекаем два последних выражения из стека
                string b = stack.Pop();
                string a = stack.Pop();

                // Объединяем их в инфиксную форму и добавляем в стек
                string expr = "(" + a + ch + b + ")";
                stack.Push(expr);
            }
        }

        // Результат — верхушка стека
        Console.WriteLine("Результат: " + stack.Peek());

        // Ожидание клавиши перед закрытием
        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}
