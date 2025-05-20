using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    // Структура для хранения информации о лексеме
    struct Lexeme
    {
        public string Type;
        public string Value;
        public Lexeme(string type, string value)
        {
            Type = type; Value = value;
        }
    }

    static void Main()
    {
        Console.WriteLine("Введите путь к входному файлу:");
        string path = Console.ReadLine();

        if (!File.Exists(path))
        {
            Console.WriteLine("Файл не найден.");
            Console.ReadLine(); return;
        }

        // Чтение текста из файла и удаление комментариев
        string text = File.ReadAllText(path);
        text = Regex.Replace(text, @"\(\*.*?\*\)", "", RegexOptions.Singleline); // удаляем (* комментарии *)

        // Регулярные выражения для разных типов лексем
        string id = @"[a-zA-Z_][a-zA-Z0-9_]{0,31}";
        string floatNum = @"\d+\.\d+([eE][\+\-]?\d+)?|\d+[eE][\+\-]?\d+";
        string assign = @":=";
        string number = @"\d+";
        string str = $"(\"[^\"]{{0,32}}\")";
        string op = @"[\+\-\*/]";
        string punct = @"[();]";
        string ws = @"\s+";

        // Компиляция общей регулярки
        Regex tokenRegex = new Regex(
            $"{assign}|{floatNum}|{id}|{number}|{str}|{op}|{punct}|{ws}",
            RegexOptions.Compiled);

        List<Lexeme> lexemes = new List<Lexeme>();
        MatchCollection matches = tokenRegex.Matches(text);
        int pos = 0;

        foreach (Match m in matches)
        {
            if (m.Index > pos)
            {
                Console.WriteLine("Ошибка: недопустимый символ в позиции " + pos);
                break;
            }
            string val = m.Value;
            if (Regex.IsMatch(val, ws)) { pos += val.Length; continue; } // пропускаем пробелы

            // Определение типа лексемы
            string type = Regex.IsMatch(val, assign) ? "Оператор присваивания" :
                          Regex.IsMatch(val, floatNum) ? "Число с плавающей точкой" :
                          Regex.IsMatch(val, number) ? "Целое число" :
                          Regex.IsMatch(val, id) ? "Идентификатор" :
                          Regex.IsMatch(val, str) ? "Строка" :
                          Regex.IsMatch(val, op) ? "Оператор" :
                          Regex.IsMatch(val, punct) ? "Разделитель" :
                          "Неизвестно";

            lexemes.Add(new Lexeme(type, val));
            pos += val.Length;
        }

        // Вывод результатов
        Console.WriteLine("\nТаблица лексем:");
        foreach (Lexeme l in lexemes)
            Console.WriteLine("{0,-25} {1}", l.Type, l.Value);

        Console.WriteLine("\nНажмите Enter для выхода...");
        Console.ReadLine();
    }
}
