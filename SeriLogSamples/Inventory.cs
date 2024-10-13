using Serilog;

namespace SeriLogSamples
{
    internal class Inventory
    {
        private readonly Dictionary<string, Item> _items;
        private readonly string _userName;
        public Inventory(string myName)
        {
            _items = new Dictionary<string, Item>();
            _userName = myName;
        }

        public void Run()
        {
            string command;
            do
            {
                Console.WriteLine("Enter a command (add, update, remove, view, exit): ");
                command = Console.ReadLine()?.ToLower();
                switch (command)
                {
                    case "add":
                        AddItem();
                        break;
                    case "update":
                        UpdateItem();
                        break;
                    case "remove":
                        RemoveItem();
                        break;
                    case "view":
                        ViewItems();
                        break;
                    case "exit":
                        Log.Information("Exiting the application.");
                        break;
                    default:
                        Log.Warning("Invalid command entered: {Command}", command);
                        Console.WriteLine("Invalid command! Please try again.");
                        break;
                }
            } while (command != "exit");
        }

        private void AddItem()
        {
            Console.Write("Enter item name: ");
            string name = Console.ReadLine();
            Console.Write("Enter quantity: ");
            int quantity = int.Parse(Console.ReadLine() ?? "0");

            if (_items.ContainsKey(name))
            {
                Log.Warning("{UserName} attempted to add an item that already exists: {ItemName}.", _userName, name);
                Console.WriteLine("Item already exists. Consider updating its quantity.");
            }
            else
            {
                _items[name] = new Item(name, quantity);
                Log.Information("{UserName} added item: {ItemName} with quantity: {Quantity}", _userName,name, quantity);
                Console.WriteLine("Item added successfully.");
            }
        }

        private void UpdateItem()
        {
            Console.Write("Enter item name to update: ");
            string name = Console.ReadLine();
            if (_items.ContainsKey(name))
            {
                Console.Write("Enter new quantity: ");
                int newQuantity = int.Parse(Console.ReadLine() ?? "0");
                _items[name].Quantity = newQuantity;
                Log.Information("{UserName} updated item: {ItemName} to new quantity: {NewQuantity}", _userName, name, newQuantity);
                Console.WriteLine("Item updated successfully.");
            }
            else
            {
                Log.Warning("{UserName} attempted to update a non-existing item: {ItemName}", _userName, name);
                Console.WriteLine("Item does not exist.");
            }
        }

        private void RemoveItem()
        {
            Console.Write("Enter item name to remove: ");
            string name = Console.ReadLine();
            if (_items.Remove(name))
            {
                Log.Information("{UserName} removed item: {ItemName}", _userName, name);
                Console.WriteLine("Item removed successfully.");
            }
            else
            {
                Log.Warning("{UserName} attempted to remove a non-existing item: {ItemName}", _userName, name);
                Console.WriteLine("Item does not exist.");
            }
        }


        private void ViewItems()
        {
            Console.WriteLine("Current Inventory:");
            foreach (var item in _items.Values)
            {
                Log.Information("Item: {ItemName}, Quantity: {Quantity}", item.Name, item.Quantity);
                Console.WriteLine($"- {item.Name}: {item.Quantity}");
            }
        }
    }
}

