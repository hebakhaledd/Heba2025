public class Item
{
    public string Name { get; }
    public PetStat AffectedStat { get; }
    public int EffectAmount { get; }
    public int DurationInSeconds { get; }

    public Item(string name, PetStat affectedStat, int effectAmount, int durationInSeconds)
    {
        Name = name;
        AffectedStat = affectedStat;
        EffectAmount = effectAmount;
        DurationInSeconds = durationInSeconds;
    }

    public async void ApplyTo(Pet pet)
    {
        Console.WriteLine($"Using {Name} on {pet.Name}...");
        await Task.Delay(DurationInSeconds * 1000);
        pet.IncreaseStat(AffectedStat, EffectAmount);
        Console.WriteLine($"{pet.Name}'s {AffectedStat} increased by {EffectAmount}.");
    }
}
