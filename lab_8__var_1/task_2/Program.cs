using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<string> tokens; // список лексем
    static int pos = 0;         // текущая позиция

    // Для генерации и оптимизации кода
    static List<string> objectCode = new List<string>();

    static void Main()
    {
        try
        {
            tokens = new List<string>(File.ReadAllLines("вход.txt"));

            Console.WriteLine("Начат синтаксический анализ...");
            Console.WriteLine("Лексемы: " + string.Join(" ", tokens.ToArray()) + "\n");

            S(); // стартовый символ грамматики

            if (pos < tokens.Count)
                throw new Exception("Лишние лексемы после конца программы");

            Console.WriteLine("\nПрограмма корректна");

            Console.WriteLine("\n[Генерация объектного кода]");
            foreach (var line in objectCode)
                Console.WriteLine(line);

            Console.WriteLine("\n[Оптимизация кода]");
            var optimized = OptimizeCode(objectCode);
            foreach (var line in optimized)
                Console.WriteLine(line);
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nОШИБКА: " + ex.Message);
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    // S → a := F ;
    static void S()
    {
        Match("a");
        Match(":=");
        string result = F();
        Match(";");
        objectCode.Add($"STORE {result}"); // Сохраняем результат в переменную a
    }

    // F → F + T | T
    static string F()
    {
        string left = T();
        while (MatchOptional("+"))
        {
            string right = T();
            string temp = NewTemp();
            objectCode.Add($"ADD {left} {right} -> {temp}");
            left = temp;
        }
        return left;
    }

    // T → T : E | E
    static string T()
    {
        string left = E();
        while (MatchOptional(":"))
        {
            string right = E();
            string temp = NewTemp();
            objectCode.Add($"CONCAT {left} {right} -> {temp}"); // Используем CONCAT вместо :
            left = temp;
        }
        return left;
    }

    // E → ( F ) | - ( F ) | a
    static string E()
    {
        if (MatchOptional("("))
        {
            string val = F();
            Match(")");
            return val;
        }
        else if (MatchOptional("-"))
        {
            Match("(");
            string val = F();
            Match(")");
            string temp = NewTemp();
            objectCode.Add($"NEG {val} -> {temp}");
            return temp;
        }
        else
        {
            Match("a");
            return "a";
        }
    }

    // --- Генерация временных переменных ---
    static int tempCount = 0;
    static string NewTemp()
    {
        tempCount++;
        return $"t{tempCount}";
    }

    // --- Оптимизация кода ---
    static List<string> OptimizeCode(List<string> code)
    {
        List<string> optimized = new List<string>();

        foreach (string line in code)
        {
            if (line.StartsWith("ADD"))
            {
                var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (int.TryParse(parts[1], out int x) && int.TryParse(parts[2], out int y))
                {
                    int sum = x + y;
                    optimized.Add($"LOAD {sum} -> {parts[4]}");
                    continue;
                }
            }
            else if (line.StartsWith("NEG"))
            {
                var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (int.TryParse(parts[1], out int val))
                {
                    int neg = -val;
                    optimized.Add($"LOAD {neg} -> {parts[3]}");
                    continue;
                }
            }
            optimized.Add(line);
        }

        return optimized;
    }

    // --- Сопоставление обязательной и необязательной лексемы ---
    static void Match(string expected)
    {
        if (pos >= tokens.Count)
            throw new Exception("Ожидалась '" + expected + "', но достигнут конец ввода");

        if (tokens[pos] != expected)
            throw new Exception("Ожидалась '" + expected + "', но получено '" + tokens[pos] + "' на позиции " + (pos + 1));

        pos++;
    }

    static bool MatchOptional(string token)
    {
        if (pos < tokens.Count && tokens[pos] == token)
        {
            pos++;
            return true;
        }
        return false;
    }
}
