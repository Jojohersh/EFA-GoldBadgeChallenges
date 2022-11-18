
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
                    DisplayTotalCost();
                    break;
                case "2":
                    DisplayOutingOfEventType();
                    break;
                case "3":
                    AddNewOuting();
                    break;
                case "4":
                    UpdateOuting();
                    break;
                case "5":
                    DeleteOuting();
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
    public void DisplayTotalCost() {
        decimal totalCostOfAllOutings = 0;
        List<Outing> outings = outingsRepo.GetAllOutings();
        foreach (var outing in outings) {
            totalCostOfAllOutings += outing.TotalCost;
        }
        System.Console.WriteLine($"-------------------------------------");
        System.Console.WriteLine($"Total Cost of all outings: {totalCostOfAllOutings}\n\n");
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
    public EventType PromptForEventType()
    {
        bool validChoice = false;

        while (validChoice == false)
        {
            System.Console.WriteLine("Please select an event type below");
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
            if (validIntInput && Enum.IsDefined(typeof(EventType), --menuChoice))
            {
                validChoice = true;
                EventType selectedType = (EventType)menuChoice;
                return selectedType;
            }
        }
        return (EventType)(-1);
    }
    public void DisplayOutingOfEventType()
    {
        List<Outing> outings = outingsRepo.GetAllOutings();
        EventType selectedType = PromptForEventType();
        List<Outing> outingsOfType = outings.Where(outing => outing.EventType == selectedType).ToList();
        Console.Clear();
        DisplayOutingSection(selectedType, outingsOfType);
    }
    public void AddNewOuting()
    {
        System.Console.WriteLine("\n------Entered-Outing-Creator------\n");
        Outing newOuting = PromptForOutingCreation();
        bool added = outingsRepo.AddOuting(newOuting);
        if (added)
        {
            System.Console.WriteLine("\nSuccessfully added new outing\n");
        }
        else
        {
            System.Console.WriteLine("\nFailure adding new outing\n");
        }

    }
    public Outing PromptForOutingCreation()
    {
        EventType selectedType = PromptForEventType();
        // exit if the returned event type is not defined
        if (!Enum.IsDefined(typeof(EventType), selectedType))
        {
            System.Console.WriteLine("Error processing eventtype...\n[Press any key to continue]");
            System.Console.ReadKey();
        }
        bool validInt = false;
        int numAttendees = -1;
        while (!validInt)
        {
            System.Console.WriteLine("How many people attended the event?");
            validInt = int.TryParse(System.Console.ReadLine(), out numAttendees);
            if (!validInt || numAttendees <= 0)
            {
                System.Console.WriteLine("Please enter an integer of 1 or greater\n");
                validInt = false;
            }
        }
        decimal eventCost = 0m;
        bool validDecimal = false;
        while (!validDecimal)
        {
            System.Console.WriteLine("What was the total cost of the event?");
            validDecimal = decimal.TryParse(System.Console.ReadLine(), out eventCost);
            if (!validDecimal || eventCost < 0)
            {
                System.Console.WriteLine("Please enter a valid positive decimal number\n");
                validDecimal = false;
            }
        }
        DateOnly date = new DateOnly();
        bool validDate = false;
        while (!validDate)
        {
            System.Console.WriteLine("When did the event occur? (MM/DD/YYYY Format)");
            validDate = DateOnly.TryParse(Console.ReadLine(), out date);
            if (!validDate)
            {
                System.Console.WriteLine("Please enter a valid date in MM/DD/YYYY format\n");
            }
        }

        Outing newOuting = new Outing(selectedType, numAttendees, date, eventCost);
        return newOuting;
    }
    public void UpdateOuting()
    {
        DisplayAllOutings();
        bool validID = false;
        int inputID = -1;
        while (!validID)
        {
            System.Console.WriteLine("Please enter the ID of the Outing you would like to update");
            validID = int.TryParse(Console.ReadLine(), out inputID);
            Outing foundOuting = outingsRepo.GetOutingByID(inputID);
            if (!validID || foundOuting == null)
            {
                System.Console.WriteLine("Please input a valid ID\n");
                validID = false;
            }
            else
            {
                System.Console.WriteLine("Beginning update process for selected Outing:");
                System.Console.WriteLine(foundOuting);
            }
        }
        Outing updatedOuting = PromptForOutingCreation();
        bool succeeded = outingsRepo.UpdateOutingByID(inputID, updatedOuting);
        if (succeeded) {
            System.Console.WriteLine($"Successfully updated Outing of ID:{inputID}");
        } else {
            System.Console.WriteLine($"Failure updating Outing of ID:{inputID}");
        }
    }
    public void DeleteOuting() {
        DisplayAllOutings();
        bool validID = false;
        int inputID = -1;
        while (!validID)
        {
            System.Console.WriteLine("Please enter the ID of the Outing you would like to delete");
            validID = int.TryParse(Console.ReadLine(), out inputID);
            Outing foundOuting = outingsRepo.GetOutingByID(inputID);
            if (!validID || foundOuting == null)
            {
                System.Console.WriteLine("Please input a valid ID\n");
                validID = false;
            }
            else
            {
                System.Console.WriteLine("Beginning delete process for selected Outing:");
                System.Console.WriteLine(foundOuting);
            }
        }
        bool deleted = outingsRepo.DeleteOutingByID(inputID);
        if (deleted) {
            System.Console.WriteLine($"Successfully deleted Outing of ID:{inputID}\n");
        } else {
            System.Console.WriteLine($"Failure deleting Outing of ID:{inputID}\n");
        }
    }
}
