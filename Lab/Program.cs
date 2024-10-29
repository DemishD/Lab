using System;
using System.Collections.Generic;
using System.IO;

namespace Lab
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filePath = "data.txt";

            Dictionary<string, string> data = LoadData(filePath);

            Console.WriteLine("Текущее состояние данных:");
            DisplayData(data);

            Console.Write("\nВведите ID для обновления: ");
            string id = Console.ReadLine();

            if (data.ContainsKey(id))
            {
                Console.Write("Введите новое значение: ");
                string newValue = Console.ReadLine();

                data[id] = newValue;

                SaveData(filePath, data);

                Console.WriteLine($"\nОбновлено: ID = {id}, Новое значение = {newValue}");
            }
            else
            {
                Console.WriteLine("ID не найден.");
            }
        }

        // Метод для загрузки данных из файла в словарь
        public static Dictionary<string, string> LoadData(string filePath)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        string id = parts[0].Trim();
                        string value = parts[1].Trim();
                        data[id] = value;
                    }
                }
            }

            return data;
        }

        // Метод для сохранения данных обратно в файл
        public static void SaveData(string filePath, Dictionary<string, string> data)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var entry in data)
                {
                    writer.WriteLine($"{entry.Key} = {entry.Value}");
                }
            }
        }

        // Метод для отображения данных в консоли
        public static void DisplayData(Dictionary<string, string> data)
        {
            foreach (var entry in data)
            {
                Console.WriteLine($"{entry.Key} = {entry.Value}");
            }
        }
    }
}
