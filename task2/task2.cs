using System;
using System.Globalization;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string[] path = new string[2];

        // Используем аргументы командной строки, если они переданы
        if (args.Length >= 2)
        {
            path[0] = args[0];
            path[1] = args[1];
        }
        else
        {
            Console.Write("Напишите путь к первому файлу: ");
            path[0] = Console.ReadLine();
            Console.Write("Напишите путь ко второму файлу: ");
            path[1] = Console.ReadLine();
        }

        // Проверяем существование файлов
        if (!File.Exists(path[0]) || !File.Exists(path[1]))
        {
            Console.WriteLine("Ошибка: Один или оба указанных файла не существуют.");
            return;
        }

        try
        {
            // Читаем данные из первого файла (координаты центра окружности и радиус)
            string[] circleData = File.ReadAllLines(path[0]);

            if (circleData.Length < 2)
            {
                Console.WriteLine("Ошибка: Некорректный формат первого файла.");
                return;
            }

            string[] centerCoords = circleData[0].Split();
            if (centerCoords.Length < 2 || !TryParseFloat(centerCoords[0], out float centerX) || !TryParseFloat(centerCoords[1], out float centerY))
            {
                Console.WriteLine("Ошибка: Некорректные координаты центра окружности.");
                return;
            }

            if (!TryParseFloat(circleData[1], out float radius) || radius <= 0)
            {
                Console.WriteLine("Ошибка: Радиус должен быть положительным числом.");
                return;
            }

            // Читаем данные из второго файла (координаты точек)
            string[] pointsData = File.ReadAllLines(path[1]);
            foreach (var pointLine in pointsData)
            {
                string[] pointCoords = pointLine.Split();
                if (pointCoords.Length < 2 || !TryParseFloat(pointCoords[0], out float pointX) || !TryParseFloat(pointCoords[1], out float pointY))
                {
                    Console.WriteLine("Ошибка: Некорректный формат координат точки.");
                    continue; // Пропускаем некорректные строки
                }

                int position = GetPointPosition(centerX, centerY, radius, pointX, pointY);
                Console.WriteLine(position);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static int GetPointPosition(float centerX, float centerY, float radius, float pointX, float pointY)
    {
        float distance = (float)Math.Sqrt(Math.Pow(pointX - centerX, 2) + Math.Pow(pointY - centerY, 2));

        if (Math.Abs(distance - radius) < 1e-6) return 0; // Точка на окружности
        else if (distance < radius) return 1; // Точка внутри окружности
        else return 2; // Точка снаружи окружности
    }

    static bool TryParseFloat(string input, out float result)
    {
        // Пробуем сначала использовать текущую культуру (может быть ',' или '.')
        if (float.TryParse(input, NumberStyles.Float, CultureInfo.CurrentCulture, out result))
            return true;

        // Пробуем с точкой как десятичным разделителем (нужен для корректной работы в разных локалях)
        if (float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
            return true;

        return false;
    }
}
