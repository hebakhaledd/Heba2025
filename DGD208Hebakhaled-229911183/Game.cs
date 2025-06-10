public class Game
{
    private List<Pet> adoptedPets = new();
    private bool isRunning = true;

    public void Start()
    {
        Console.WriteLine("Welcome to the Console Pet Simulator!");

        while (isRunning)
        {
            ShowMainMenu();
            string input = Console.ReadLine();
            switch (input)
            {
                case "1": AdoptPet(); break;
                case "2": ShowStats(); break;
                case "3": UseItem(); break;
                case "4": ShowCreatorInfo(); break;
                case "0": ExitGame(); break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    private void ShowMainMenu()
    {
        Console.WriteLine("\n--- Main Menu ---");
        Console.WriteLine("1. Adopt a Pet");
        Console.WriteLine("2. View Pet Stats");
        Console.WriteLine("3. Use Item on Pet");
        Console.WriteLine("4. Show Creator Info");
        Console.WriteLine("0. Exit");
        Console.Write("Enter your choice: ");
    }

    private void AdoptPet()
    {
        Console.WriteLine("Enter the pet's name:");
        string name = Console.ReadLine();

        Console.WriteLine("Select a pet type:");
        foreach (var type in Enum.GetValues(typeof(PetType)))
            Console.WriteLine($"{(int)type} - {type}");

        if (int.TryParse(Console.ReadLine(), out int choice) &&
            Enum.IsDefined(typeof(PetType), choice))
        {
            Pet pet = new Pet(name, (PetType)choice);
            pet.PetDied += HandlePetDeath;
            adoptedPets.Add(pet);
            Console.WriteLine($"{name} the {(PetType)choice} has been adopted!");
        }
        else
        {
            Console.WriteLine("Invalid pet type selected.");
        }
    }

    private void ShowStats()
    {
        if (!adoptedPets.Any())
        {
            Console.WriteLine("No pets have been adopted yet.");
            return;
        }

        foreach (var pet in adoptedPets)
        {
            Console.WriteLine($"{pet.Name} [{pet.Type}] - Hunger: {pet.Hunger}, Sleep: {pet.Sleep}, Fun: {pet.Fun}");
        }
    }

    private void UseItem()
    {
        if (!adoptedPets.Any())
        {
            Console.WriteLine("No pets available.");
            return;
        }

        Console.WriteLine("Choose a pet:");
        for (int i = 0; i < adoptedPets.Count; i++)
            Console.WriteLine($"{i + 1}. {adoptedPets[i].Name}");

        if (int.TryParse(Console.ReadLine(), out int petIndex) && petIndex - 1 < adoptedPets.Count)
        {
            Pet selectedPet = adoptedPets[petIndex - 1];
            Item item = ItemDatabase.GetRandomItem();
            selectedPet.UseItem(item);
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }

    private void ExitGame()
    {
        isRunning = false;
        foreach (var pet in adoptedPets)
        {
            pet.Stop();
        }
        Console.WriteLine("Thank you for playing!");
    }

    private void ShowCreatorInfo()
    {
        Console.WriteLine("Created by: Hebakhaled - Student Number: 229911183");
    }

    private void HandlePetDeath(object sender, StatEventArgs e)
    {
        Console.WriteLine($"!!! {e.Message}");
        if (sender is Pet deadPet)
        {
            deadPet.Stop();
            adoptedPets.Remove(deadPet);
        }
    }
}
