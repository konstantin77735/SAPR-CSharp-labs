using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<string> tokens; // список лексем
    static int pos = 0;         // текущая позиция

    static void Main()
    {
        try
        {
            // Чтение лексем из файла
            tokens = new List<string>(File.ReadAllLines("вход.txt"));

            Console.WriteLine("Начат синтаксический анализ...");
            Console.WriteLine("Лексемы: " + string.Join(" ", tokens.ToArray()));
            Console.WriteLine();

            S(); // стартовый символ грамматики

            if (pos < tokens.Count)
                throw new Exception("Лишние лексемы после конца программы");

            Console.WriteLine("\nПрограмма корректна");
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nОШИБКА: " + ex.Message);
        }

        // Ожидание, чтобы окно не закрылось
        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    // S → a := F ;
    static void S()
    {
        Console.WriteLine("S → a := F ;");
        Match("a");
        Match(":=");
        F();
        Match(";");
    }

    // F → F + T | T
    static void F()
    {
        Console.WriteLine("F → T {+ T}*");
        T();
        while (MatchOptional("+"))
        {
            Console.WriteLine("  нашли '+' → продолжаем разбор T");
            T();
        }
    }

    // T → T : E | E
    static void T()
    {
        Console.WriteLine("T → E {: E}*");
        E();
        while (MatchOptional(":"))
        {
            Console.WriteLine("  нашли ':' → продолжаем разбор E");
            E();
        }
    }

    // E → ( F ) | - ( F ) | a
    static void E()
    {
        if (MatchOptional("("))
        {
            Console.WriteLine("E → ( F )");
            F();
            Match(")");
        }
        else if (MatchOptional("-"))
        {
            Console.WriteLine("E → - ( F )");
            Match("(");
            F();
            Match(")");
        }
        else
        {
            Console.WriteLine("E → a");
            Match("a");
        }
    }

    // Сопоставление обязательной лексемы
    static void Match(string expected)
    {
        if (pos >= tokens.Count)
            throw new Exception("Ожидалась '" + expected + "', но достигнут конец ввода");

        if (tokens[pos] != expected)
            throw new Exception("Ожидалась '" + expected + "', но получено '" + tokens[pos] + "' на позиции " + (pos + 1));

        Console.WriteLine("  приняли '" + tokens[pos] + "'");
        pos++;
    }

    // Попытка принять необязательную лексему (например, в цикле)
    static bool MatchOptional(string token)
    {
        if (pos < tokens.Count && tokens[pos] == token)
        {
            Console.WriteLine("  приняли '" + tokens[pos] + "'");
            pos++;
            return true;
        }
        return false;
    }
}
