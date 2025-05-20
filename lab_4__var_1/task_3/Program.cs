using System;
using System.Collections.Generic;
using System.Text;

// Основной класс программы
class Program
{
    // Точка входа в программу
    static void Main()
    {
        Console.WriteLine("Введите строку для кодирования алгоритмом Хаффмана:");
        string input = Console.ReadLine();

        // Проверяем, что строка не пустая
        if (!string.IsNullOrEmpty(input))
        {
            // Создаем объект для кодирования
            HuffmanCoding huffman = new HuffmanCoding();
            string encoded = huffman.Encode(input);
            Console.WriteLine("Закодированная строка: " + encoded);
        }
        else
        {
            Console.WriteLine("Строка не должна быть пустой!");
        }

        // Программа не закрывается, ожидая нажатия клавиши
        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}

// Класс для узла дерева Хаффмана
class HuffmanNode
{
    public char Symbol;
    public int Frequency;
    public HuffmanNode Left;
    public HuffmanNode Right;
}

// Класс для реализации кодирования по Хаффману
class HuffmanCoding
{
    // Метод для кодирования строки
    public string Encode(string input)
    {
        // Создаем словарь для подсчета частоты символов
        Dictionary<char, int> frequencyTable = new Dictionary<char, int>();

        // Подсчитываем частоту символов
        foreach (char c in input)
        {
            if (frequencyTable.ContainsKey(c))
                frequencyTable[c]++;
            else
                frequencyTable[c] = 1;
        }

        // Формируем очередь с приоритетом для построения дерева
        List<HuffmanNode> nodes = new List<HuffmanNode>();
        foreach (KeyValuePair<char, int> pair in frequencyTable)
        {
            nodes.Add(new HuffmanNode { Symbol = pair.Key, Frequency = pair.Value });
        }

        // Построение дерева
        while (nodes.Count > 1)
        {
            // Сортируем узлы по частоте
            nodes.Sort((a, b) => a.Frequency - b.Frequency);

            // Извлекаем два узла с наименьшей частотой
            HuffmanNode left = nodes[0];
            HuffmanNode right = nodes[1];

            // Создаем новый родительский узел
            HuffmanNode parent = new HuffmanNode
            {
                Symbol = '*',
                Frequency = left.Frequency + right.Frequency,
                Left = left,
                Right = right
            };

            // Удаляем использованные узлы и добавляем родительский
            nodes.Remove(left);
            nodes.Remove(right);
            nodes.Add(parent);
        }

        // Получаем корень дерева
        HuffmanNode root = nodes[0];

        // Генерируем кодовые слова для каждого символа
        Dictionary<char, string> codes = new Dictionary<char, string>();
        GenerateCodes(root, "", codes);

        // Кодируем строку
        StringBuilder encodedString = new StringBuilder();
        foreach (char c in input)
        {
            encodedString.Append(codes[c]);
        }

        return encodedString.ToString();
    }

    // Рекурсивный метод для генерации кодов символов
    private void GenerateCodes(HuffmanNode node, string code, Dictionary<char, string> codes)
    {
        // Если узел не пустой
        if (node != null)
        {
            // Если это листовой узел, сохраняем код
            if (node.Left == null && node.Right == null)
            {
                codes[node.Symbol] = code;
            }
            else
            {
                // Рекурсивно обходим левую и правую ветви
                GenerateCodes(node.Left, code + "0", codes);
                GenerateCodes(node.Right, code + "1", codes);
            }
        }
    }
}
