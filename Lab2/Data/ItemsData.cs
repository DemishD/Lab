using Lab2.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace Lab2.Data
{
    public class ItemsData : IItemsData
    {
        private readonly IDatabase _db;

        public ItemsData(IConnectionMultiplexer connection) 
        { 
            _db=connection.GetDatabase();
        }
        public Item? CreateItem(Item item)
        {
            if(item.ID != null)
            {
                item.ID = $"{item.ID}";
            }
            else
            {
                item.ID = $"{Guid.NewGuid().ToString()}";
            }
            _db.HashSet("itemdb", new HashEntry[]
            {
                new HashEntry(item.ID,JsonSerializer.Serialize(item))
            });
            return item;
        }

        public IEnumerable<Item?>? GetAllItems()
        {
            return Array.ConvertAll(
                _db.HashGetAll("itemdb"),
                items=> JsonSerializer.Deserialize<Item>(items.Value.ToString())
                ).ToList();
        }

        public Item? GetItemById(string id)
        {
            return JsonSerializer.Deserialize<Item?>(_db.HashGet("itemdb",$"{id}").ToString());
        }

        public bool DeleteItemById(string id)
        {
            return _db.HashDelete("itemdb",$"{id}");
        }
    }
}
