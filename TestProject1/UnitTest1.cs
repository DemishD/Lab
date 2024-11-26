using Moq;
using Xunit;
using Lab2;
using Lab2.Controllers;
using Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Lab2.Data;

namespace Lab2.TestProject1
{
    public class UnitTest1
    {
        public class ItemControllerTests
        {
            private readonly Mock<IItemsData> _mockItemsData;
            private readonly ItemController _controller;

            public ItemControllerTests()
            {
                _mockItemsData = new Mock<IItemsData>();
                _controller = new ItemController(_mockItemsData.Object);
            }

            [Fact]
            public void CreateItem()
            {
                var newItem = new Item { ID = "1", Value = "TestValue" };
                _mockItemsData.Setup(x => x.CreateItem(newItem)).Returns(newItem);

                var result = _controller.CreateItem(newItem);

                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(newItem, okResult.Value);
            }

            [Fact]
            public void GetAllItems()
            {
                var items = new List<Item>
            {
                new Item { ID = "1", Value = "Test1" },
                new Item { ID = "2", Value = "Test2" }
            };
                _mockItemsData.Setup(x => x.GetAllItems()).Returns(items);

                var result = _controller.GetAllItems();

                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnedItems = Assert.IsAssignableFrom<IEnumerable<Item>>(okResult.Value);
                Assert.Equal(items, returnedItems);
            }

            [Fact]
            public void GetItemById()
            {
                string id = "1";
                _mockItemsData.Setup(x => x.GetItemById(id)).Returns((Item?)null);

                var result = _controller.GetItemById(id);

                Assert.IsType<NotFoundResult>(result);
            }

            [Fact]
            public void DeleteItem()
            {
                string id = "1";
                _mockItemsData.Setup(x => x.DeleteItemById(id)).Returns(true);

                var result = _controller.DeleteItem(id);

                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.True((bool)okResult.Value);
            }
        }
    }
}