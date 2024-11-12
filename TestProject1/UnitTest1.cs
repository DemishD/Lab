using Moq;
using Lab2;
using Lab2.Controllers;
using Lab2.Repositories;
using Lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.TestProject1
{
    public class UnitTest1
    {
        private readonly string _filePath;

        public UnitTest1()
        {
            _filePath = Path.GetTempFileName();
        }

        [Fact]
        public void LoadDataFromFile_ShouldLoadCorrectly()
        {
           
            var data = new[]
            {
            "1 = Item 1",
            "2 = Item 2"
        };
            File.WriteAllLines(_filePath, data);
            var repository = new Repository(_filePath);

            var items = repository.Items;

            Assert.Equal(2, items.Count);
            Assert.Equal(1, items[0].Id);
            Assert.Equal("Item 1", items[0].Value);
        }

        [Fact]
        public void SaveDataToFile_ShouldSaveCorrectly()
        {
            var repository = new Repository(_filePath);
            var newItem = new Item { Id = 3, Value = "New Item" };
            repository.Items.Add(newItem);

            repository.SaveDataToFile();

            var lines = File.ReadAllLines(_filePath);
            Assert.Contains("3 = New Item", lines);
        }

        [Fact]
        public void UpdateData_ShouldUpdateCorrectly()
        {
            var repository = new Repository(_filePath);
            repository.Items.Add(new Item { Id = 1, Value = "Old Value" });

            repository.Items[0].Value = "Updated Value";
            repository.SaveDataToFile();

            var updatedItem = repository.Items[0];
            Assert.Equal("Updated Value", updatedItem.Value);
        }

        ~UnitTest1()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }
    }
}