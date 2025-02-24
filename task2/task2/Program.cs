using System;
using System.IO;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        
        string[] path = new string[2];
        Console.Write("Напишите путь к первому файлу: ");
        path[0] = Console.ReadLine();
        Console.Write("Напишите путь ко второму файлу: ");
        path[1] = Console.ReadLine();

        if (path.Length < 2)
        {
            Console.WriteLine("Необходимо указать два файла: файл с координатами и радиусом окружности и файл с координатами точек.");
            return;
        }

        // Считывание данных из первого файла (координаты и радиус окружности)
        string[] circleData = File.ReadAllLines(path[0]);
        float centerX = float.Parse(circleData[0].Split()[0]);
        float centerY = float.Parse(circleData[0].Split()[1]);
        float radius = float.Parse(circleData[1]);

        // Считывание данных из второго файла (координаты точек)
        string[] pointsData = File.ReadAllLines(path[1]);
        foreach (var pointLine in pointsData)
        {
            float pointX = float.Parse(pointLine.Split()[0]);
            float pointY = float.Parse(pointLine.Split()[1]);

            int position = GetPointPosition(centerX, centerY, radius, pointX, pointY);
            Console.WriteLine(position);
        }
       
    }

    static int GetPointPosition(float centerX, float centerY, float radius, float pointX, float pointY)
    {
        // Расстояние от точки до центра окружности
        float distance = (float)Math.Sqrt(Math.Pow(pointX - centerX, 2) + Math.Pow(pointY - centerY, 2));

        // Сравнение расстояния с радиусом окружности
        if (Math.Abs(distance - radius) < 1e-6)
        {
            return 0; // Точка лежит на окружности
        }
        else if (distance < radius)
        {
            return 1; // Точка внутри окружности
        }
        else
        {
            return 2; // Точка снаружи окружности
        }
    }
}
