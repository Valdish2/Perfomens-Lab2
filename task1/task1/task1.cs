using System;
using System.IO;

class CircularArray
{
    static void Main(string[] args)
    {
        // Проверяем, что ввод не null и не пустая строка
        if (args == null || args.Length < 2)
        {
            Console.WriteLine("Ошибка: необходимо передать два числа (размер массива и шаг) в аргументах командной строки.");
            return;
        }

        int n, m;

        // Пробуем распарсить аргументы
        try
        {
            n = int.Parse(args[0]);
            m = int.Parse(args[1]);
        }
        catch
        {
            Console.WriteLine("Ошибка: оба аргумента должны быть целыми числами.");
            return;
        }

        // Проверяем, что числа положительные
        if (n <= 0 || m <= 0)
        {
            Console.WriteLine("Ошибка: размер массива и шаг должны быть положительными числами.");
            return;
        }

        int[] array = new int[n];
        for (int i = 0; i < n; i++)
        {
            array[i] = i + 1;
        }

        int currentIndex = 0;
        do
        {
            Console.Write(array[currentIndex]);
            currentIndex = (currentIndex + m - 1) % array.Length;
        } while (currentIndex != 0);


        Console.ReadLine();
    }

}
