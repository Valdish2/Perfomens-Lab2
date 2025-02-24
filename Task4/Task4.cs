using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Проверяем, передан ли путь к файлу
        if (args.Length < 1)
        {
            Console.WriteLine("Использование: Task4.exe <путь_к_файлу>");
            return;
        }

        string filePath = args[0];

        // Проверяем существование файла
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Ошибка: Указанный файл не найден.");
            return;
        }

        try
        {
            // Читаем числа из файла
            string[] lines = File.ReadAllLines(filePath);
            int[] nums = Array.ConvertAll(lines, int.Parse);

            // Вычисляем минимальное количество шагов
            int moves = MinMoves(nums);

            Console.WriteLine(moves);
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: Файл должен содержать только целые числа, каждое с новой строки.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static int MinMoves(int[] nums)
    {
        Array.Sort(nums);
        int median = nums[nums.Length / 2];
        int moves = 0;

        foreach (int num in nums)
        {
            moves += Math.Abs(num - median);
        }

        return moves;
    }
}
