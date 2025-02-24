using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string basePath = "C:\\Users\\SKORPION\\Desktop\\Task4\\Task4";
        string file = Console.ReadLine();
        string fullPath = System.IO.Path.Combine(basePath, file);
        string[] lines = File.ReadAllLines(fullPath);
        int[] nums = Array.ConvertAll(lines, int.Parse);

        int moves = MinMoves(nums);

        Console.WriteLine(moves);
        Console.ReadLine();
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
