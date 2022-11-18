
public class ProgramUI
{
    private readonly OutingRepository outingsRepo = new OutingRepository();
    public void Run()
    {
        outingsRepo.SeedDB();
        MainMenu();
    }
    public void MainMenu()
    {
        bool IsRunning = true;
        Console.Clear();
        while (IsRunning == true)
        {
            string menuChoice = "";

            System.Console.WriteLine("Komodo Insurance\n------Outing-Manager---------\n\n");
            System.Console.WriteLine("What would you like to do?");
            System.Console.WriteLine("   1. Show all outings\n   2. View one category of outings\n   3. Add a new outing\n   4. Update an existing outing\n   5. Delete an existing outing\n   6. Exit Application");
            menuChoice = Console.ReadLine();
            switch (menuChoice)
            {
                case "1":
                    DisplayAllOutings();
                    break;
                case "2":
                    PromptForSectionType(outingsRepo.GetAllOutings());
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
                    IsRunning = false;
                    break;
                default:
                    System.Console.WriteLine("invalid selection please try again!");
                    break;
            }

        }
    }
    public void DisplayAllOutings()
    {
        List<Outing> outings = outingsRepo.GetAllOutings();
        foreach (EventType value in Enum.GetValues(typeof(EventType)))
        {
            DisplayOutingSection(value, outings);
        }

    }
    public void DisplayOutingSection(EventType eventType, List<Outing> outings)
    {
        List<Outing> outingSection = outings.Where(outing => outing.EventType == eventType).ToList();
        string sectionContent = "";
        decimal sectionTotalCost = 0m;
        //assemble the section text body of the sorted outings
        //calculate the total cost of the section
        foreach (var outing in outingSection)
        {
            sectionContent += $"\n{outing.ToString()}\n";
            sectionTotalCost += outing.TotalCost;
        }
        //construct the section header with the event type and the total section cost
        string sectionHeader = $"\nEvent Type: {eventType} --------------- Total Cost: {sectionTotalCost.ToString("C2")}\n";
        //if the section doesn't have events, say so
        if (outingSection.Count == 0)
        {
            System.Console.WriteLine(sectionHeader + $"\nNo events recorded\n");
        }
        else
        {
            System.Console.WriteLine(sectionHeader + sectionContent);
        }
    }

    public void PromptForSectionType(List<Outing> outings)
    {
        bool validChoice = false;

        while (validChoice == false)
        {
            System.Console.WriteLine("What type of event would you like to view?");
            int menuNumber = 1;
            foreach (EventType type in Enum.GetValues(typeof(EventType)))
            {
                System.Console.WriteLine($"   {menuNumber}. {type}");
                menuNumber++;
            }
            // check first if an int was input
            // if a valid int is provided, see if it is defined in the EventType enum (decremented for zero based)
            int menuChoice = -1;
            bool validIntInput = int.TryParse(Console.ReadLine(), out menuChoice);
            if (validIntInput && Enum.IsDefined(typeof(EventType),--menuChoice)) {
                validChoice = true;
                EventType selectedType = (EventType) menuChoice;
                List<Outing> outingsOfType = outings.Where(outing => outing.EventType == selectedType).ToList();
                Console.Clear();
                DisplayOutingSection(selectedType, outingsOfType);
            }
        }
    }
}
