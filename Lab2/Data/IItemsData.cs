using Lab2.Models;

namespace Lab2.Data
{
    public interface IItemsData
    {
        Item? CreateItem(Item item);
        IEnumerable<Item?>? GetAllItems();

        Item? GetItemById(string id);

        bool DeleteItemById(string id);
    }
}
