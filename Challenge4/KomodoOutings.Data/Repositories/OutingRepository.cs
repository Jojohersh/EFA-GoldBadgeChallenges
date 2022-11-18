public class OutingRepository
{
    private readonly List<Outing> _outingsDB = new List<Outing>();
    private int _indexor = 0;
    public OutingRepository()
    {
        _indexor = 0;
    }
    //Create
    public bool AddOuting(Outing outing)
    {
        if (outing == null)
        {
            return false;
        }
        outing.ID = _indexor;
        _indexor++;
        _outingsDB.Add(outing);
        return true;
    }
    //Read
    public List<Outing> GetAllOutings() {
        return _outingsDB.OrderBy(outing => outing.EventType).ThenBy(outing => outing.Date).ToList();
    }
    public Outing GetOutingByID(int ID) {
        return _outingsDB.FirstOrDefault(outing => outing.ID == ID);
    }
    //Update
    public bool UpdateOutingByID(int ID, Outing updatedOuting) {
        Outing retrievedOuting = GetOutingByID(ID);
        if (retrievedOuting == null) {
            return false;
        }
        retrievedOuting.TotalCost = updatedOuting.TotalCost;
        retrievedOuting.Date = updatedOuting.Date;
        retrievedOuting.NumAttendees = updatedOuting.NumAttendees;
        retrievedOuting.EventType = updatedOuting.EventType;
        return true;
    }
    //Destroy
    public bool DeleteOutingByID(int ID) {
        Outing outing = GetOutingByID(ID);
        if (outing == null) {
            return false;
        }
        _outingsDB.Remove(outing);
        return true;
    }

    public void SeedDB() {
        Outing outing1 = new Outing(
            EventType.Golf,
            12,
            new DateOnly(2017,9,10),
            3456.12m
        );
        Outing outing2 = new Outing(
            EventType.Golf,
            14,
            new DateOnly(2000,1,3),
            896.43m
        );
        Outing outing3 = new Outing(
            EventType.Concert,
            450,
            new DateOnly(345,5,4),
            34.3m
        );
        Outing outing4 = new Outing(
            EventType.Bowling,
            98,
            new DateOnly(2006,12,9),
            592.52m
        );
        AddOuting(outing1);
        AddOuting(outing2);
        AddOuting(outing3);
        AddOuting(outing4);
    }
}