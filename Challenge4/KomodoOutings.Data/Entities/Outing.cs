public class Outing
{
    public int ID;
    public EventType EventType { get; set; }
    public int NumAttendees;
    public DateTime Date;
    public decimal CostPerPerson { 
        get {
            if (NumAttendees == 0) {
                return 0;
            }
            return TotalCost / NumAttendees;
        } 
    }
    public decimal TotalCost { get; set; }
    public Outing(EventType eventType, int numAttendees, DateTime date, decimal totalCost)
    {
        EventType = eventType;
        NumAttendees = numAttendees;
        Date = date;
        TotalCost = totalCost;
    }
    public override string ToString() {
        // tostring("C2") formats decimals to "$...X.XX" format
        return $"ID:{ID} {EventType} {Date}\nAttendees {NumAttendees}\nCost Per Person {CostPerPerson.ToString("C2")}\nTotal Cost {TotalCost.ToString("C2")}";
    }
}
