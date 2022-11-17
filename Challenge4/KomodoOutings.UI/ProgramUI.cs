
public class ProgramUI
{
    private readonly OutingRepository outingsRepo = new OutingRepository();
    private bool IsRunning = false;
    public void Run()
    {
        outingsRepo.SeedDB();
        IsRunning = true;
        System.Console.WriteLine("Hello world");
        System.Console.WriteLine("All outings...\n\n");
        DisplayAllOutings();
    }
    public void DisplayAllOutings()
    {
        List<Outing> outings = outingsRepo.GetAllOutings();

        //extract all name strings for EventType options
        String[] types = Enum.GetNames(typeof(EventType));
        //index for navigating types array
        int typeIndex = 0;
        //start at first Event type
        string currentEventType = types[typeIndex];
        //initialize string to be printed each section and total cost
        string sectionContent = "";
        decimal sectionCost = 0m;

        //loop through all outings
        foreach (var outing in outings)
        {
            //at the first instance of a new category type
            if (currentEventType != outing.EventType.ToString())
            {
                //print current Headerstring with CategoryTotal
                System.Console.WriteLine($"\n{currentEventType} ------- Total Cost: {sectionCost.ToString("C2")}------\n");
                //print content for the section
                System.Console.WriteLine(sectionContent);
                //move to next type in array of type names
                typeIndex++;
                currentEventType = types[typeIndex];
                //reset category total
                sectionCost = 0;
                //reset output string
                sectionContent = "";
                //move to next EventType name string in string[] types
            }
            //add currentOuting value and string to outputString and CategoryTotal
            sectionContent += outing.ToString();
            sectionCost += outing.TotalCost;
            System.Console.WriteLine(outing.ID + outing.EventType);
        }
    }
}
