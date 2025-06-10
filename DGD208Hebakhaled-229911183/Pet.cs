public class Pet
{
    public string Name { get; }
    public PetType Type { get; }
    public int Hunger { get; private set; } = 50;
    public int Sleep { get; private set; } = 50;
    public int Fun { get; private set; } = 50;

    public event EventHandler<StatEventArgs> PetDied;

    private CancellationTokenSource cancellationTokenSource = new();

    public Pet(string name, PetType type)
    {
        Name = name;
        Type = type;
        StartStatDecay();
    }

    private async void StartStatDecay()
    {
        var token = cancellationTokenSource.Token;
        try
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(3000, token);
                DecreaseStats();

                if (Hunger <= 0 || Sleep <= 0 || Fun <= 0)
                {
                    PetDied?.Invoke(this, new StatEventArgs(Name, "has died."));
                    break;
                }
            }
        }
        catch (TaskCanceledException) { }
    }

    private void DecreaseStats()
    {
        Hunger = Math.Max(0, Hunger - 1);
        Sleep = Math.Max(0, Sleep - 1);
        Fun = Math.Max(0, Fun - 1);
    }

    public void IncreaseStat(PetStat stat, int amount)
    {
        switch (stat)
        {
            case PetStat.Hunger: Hunger = Math.Min(100, Hunger + amount); break;
            case PetStat.Sleep: Sleep = Math.Min(100, Sleep + amount); break;
            case PetStat.Fun: Fun = Math.Min(100, Fun + amount); break;
        }
    }

    public void Stop()
    {
        cancellationTokenSource.Cancel();
    }

    public void UseItem(Item item)
    {
        item.ApplyTo(this);
    }
}
