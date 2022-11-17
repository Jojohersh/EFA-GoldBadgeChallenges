

public class UnitTest1
{
    [Fact]
    public void AddOutingToDB()
    {
        //create outing DB
        OutingRepository outingRepo = new OutingRepository();
        //create a new outing
        Outing outing = new Outing(
            EventType.Golf,
            100,
            new DateTime(2022,10,24),
            1002m
        );

        //add outing to db
        bool added = outingRepo.AddOuting(outing);
        //assert
        Assert.True(added);
    }
    [Fact]
    public void GetAllOutingsFromDBTest_ShouldBeOrderedByTypeThenDate() {
        //arrange
        Outing outing1 = new Outing(
            EventType.Bowling,
            20,
            new DateTime(2022,09,1),
            285.04m
        );
        Outing outing2 = new Outing(
            EventType.AmusementPark,
            100,
            new DateTime(2022, 09, 2),
            29.4m
        );
        Outing outing3 = new Outing(
            EventType.Bowling,
            300,
            new DateTime(2022,08,12),
            3000000.1m
        );

        OutingRepository outingRepo = new OutingRepository();
        //act
        outingRepo.AddOuting(outing1);
        outingRepo.AddOuting(outing2);
        outingRepo.AddOuting(outing3);

        List<Outing> outings = outingRepo.GetAllOutings();
        //assert
        Assert.Equal(EventType.AmusementPark,outings.ElementAt(0).EventType);
        Assert.Equal(new DateTime(2022,08,12), outings.ElementAt(1).Date);
        Assert.Equal(new DateTime(2022,09,1), outings.ElementAt(2).Date);
    }
    [Fact]
    public void GetOneOutingByID_IDShouldBeZero() {
        //arrange
        Outing outing = new Outing(
            EventType.Bowling,
            100,
            new DateTime(2022,1,1),
            1400.3m
        );
        OutingRepository outingRepo = new OutingRepository();
        //act
        bool added = outingRepo.AddOuting(outing);

        //assemble
        Assert.Equal(0, outingRepo.GetOutingByID(0).ID);
        Assert.Null(outingRepo.GetOutingByID(1));
    }
    [Fact]
    public void UpdateOutingByID_CheckingAllFields() {
        //assemble
        Outing outing = new Outing(
            EventType.AmusementPark,
            10,
            new DateTime(2022,10,20),
            100.10m
        );
        OutingRepository db = new OutingRepository();
        Outing newOuting = new Outing(
            EventType.Bowling,
            9,
            new DateTime(1942,3,4),
            20_000.56m
        );
        //act
        db.AddOuting(outing);
        db.UpdateOutingByID(0,newOuting);
        Outing updatedOuting = db.GetOutingByID(0);
        
        //assert

        Assert.Equal(EventType.Bowling,updatedOuting.EventType);
        Assert.Equal(9,updatedOuting.NumAttendees);
        Assert.Equal(new DateTime(1942,3,4), updatedOuting.Date);
        Assert.Equal(20_000.56m, updatedOuting.TotalCost);
    }
    [Fact]
    public void DeleteOutingByID_ShouldRemoveOuting() {
        Outing outing = new Outing(
            EventType.Concert,
            300,
            new DateTime(2010,9,4),
            40_000.45m
        );
        OutingRepository db = new OutingRepository();
        //act
        db.AddOuting(outing);
        bool removed = db.DeleteOutingByID(0);
        //assert
        Assert.True(removed);
        Assert.Null(db.GetOutingByID(0));
    }
}