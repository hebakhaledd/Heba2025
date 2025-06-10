public static class ItemDatabase
{
    private static List<Item> items = new()
    {
        new Item("Food", PetStat.Hunger, 20, 2),
        new Item("Nap", PetStat.Sleep, 25, 3),
        new Item("Toy", PetStat.Fun, 30, 2),
    };

    private static Random random = new();

    public static Item GetRandomItem()
    {
        return items[random.Next(items.Count)];
    }

    public static List<Item> GetAllItems()
    {
        return items;
    }
}
