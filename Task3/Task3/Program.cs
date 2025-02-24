using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Проверяем, переданы ли аргументы
            if (args.Length < 2)
            {
                Console.WriteLine("Использование: Task3.exe <путь_к_tests.json> <путь_к_values.json>");
                return;
            }

            string testsPath = args[0];
            string valuesPath = args[1];

            // Проверяем существование файлов
            if (!File.Exists(testsPath) || !File.Exists(valuesPath))
            {
                Console.WriteLine("Ошибка: Один или оба файла не существуют.");
                return;
            }

            try
            {
                // Читаем JSON-файлы
                string testsJson = File.ReadAllText(testsPath);
                string valuesJson = File.ReadAllText(valuesPath);

                // Десериализуем JSON в объекты
                var testsData = JsonConvert.DeserializeObject<RootObject>(testsJson);
                var valuesData = JsonConvert.DeserializeObject<ValueRoot>(valuesJson);

                // Заполняем значения
                if (testsData != null && valuesData != null)
                {
                    FillValues(testsData, valuesData);
                }

                // Преобразуем обратно в JSON и записываем в report.json
                string reportJson = JsonConvert.SerializeObject(testsData, Formatting.Indented);
                File.WriteAllText("report.json", reportJson);

                Console.WriteLine("Файл report.json успешно создан.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void FillValues(RootObject tests, ValueRoot values)
        {
            var valuesDict = new Dictionary<int, string>();
            foreach (var result in values.values)
            {
                valuesDict[result.id] = result.value;
            }

            // Универсальный рекурсивный метод для обновления значений
            void UpdateValues(Test test)
            {
                if (valuesDict.ContainsKey(test.id))
                {
                    test.value = valuesDict[test.id];
                }

                if (test.values != null)
                {
                    foreach (var nested in test.values)
                    {
                        UpdateValues(nested);
                    }
                }
            }

            if (tests.tests != null)
            {
                foreach (var test in tests.tests)
                {
                    UpdateValues(test);
                }
            }
        }
    }

    // Описание классов для десериализации JSON
    class RootObject
    {
        public List<Test> tests { get; set; }
    }

    class Test
    {
        public int id { get; set; }
        public string title { get; set; }
        public string value { get; set; }
        public List<Test> values { get; set; }
    }

    class ValueRoot
    {
        public List<ValueItem> values { get; set; }
    }

    class ValueItem
    {
        public int id { get; set; }
        public string value { get; set; }
    }
}
