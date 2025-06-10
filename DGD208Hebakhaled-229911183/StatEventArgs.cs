public class StatEventArgs : EventArgs
{
    public string PetName { get; }
    public string Message { get; }

    public StatEventArgs(string petName, string message)
    {
        PetName = petName;
        Message = $"{petName} {message}";
    }
}
