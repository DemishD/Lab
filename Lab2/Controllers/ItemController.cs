using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lab2.Models;
using Lab2.Data;

namespace Lab2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemsData _itemsData;

        public ItemController(IItemsData itemsData)
        {
            _itemsData = itemsData;
        }

        [HttpPut("update")]
        public IActionResult CreateItem(Item item)
        {
            return Ok(_itemsData.CreateItem(item));
        }

        [HttpGet("get")]
        public IActionResult GetAllItems()
        {
            return Ok(_itemsData.GetAllItems());
        }
        [HttpGet("get/{id}")]
        public IActionResult GetItemById(string id)
        {
            var item = _itemsData.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteItem(string id)
        {
            return Ok(_itemsData.DeleteItemById(id));
        }

    }
}
