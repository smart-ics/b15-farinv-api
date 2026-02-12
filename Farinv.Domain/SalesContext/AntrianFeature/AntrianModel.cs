using Ardalis.GuardClauses;

namespace Farinv.Domain.SalesContext.AntrianFeature;

public class AntrianModel: IAntrianKey
{
    #region CREATION
    public AntrianModel(
        string antrianId, DateOnly antrianDate, string sequenceTag, 
        int noAntrian, AntrianStatusEnum antrianStatus, string personName, 
        DateTime takenAt, DateTime assignedAt, DateTime preparedAt, 
        DateTime deliveredAt, DateTime cancelAt)
    {
        AntrianId = antrianId;
        AntrianDate = antrianDate;
        SequenceTag = sequenceTag;
        NoAntrian = noAntrian;
        AntrianStatus = antrianStatus;
        PersonName = personName;
        TakenAt = takenAt;
        AssignedAt = assignedAt;
        PreparedAt = preparedAt;
        DeliveredAt = deliveredAt;
        CancelAt = cancelAt;
    }

    public static IAntrianKey Key(string id)
    {
        var result = new AntrianModel(
            id, DateOnly.FromDateTime(DateTime.Now), "-", 
            0, AntrianStatusEnum.Open, "-", 
            new DateTime(3000, 1, 1), new DateTime(3000, 1, 1), new DateTime(3000, 1, 1), 
            new DateTime(3000, 1, 1), new DateTime(3000, 1, 1));
        return result; 
    }

    public static AntrianModel Default => new(
            "-", DateOnly.FromDateTime(DateTime.Now),"-", 
            0, AntrianStatusEnum.Open, "-",
            new DateTime(3000, 1, 1), new DateTime(3000, 1, 1), new DateTime(3000, 1, 1),
            new DateTime(3000, 1, 1), new DateTime(3000, 1, 1));

    public static AntrianModel Create(string antrianId, DateOnly antrianDate, 
        string sequenceTag, int noAntrian)
    {
        Guard.Against.NegativeOrZero(noAntrian, nameof(noAntrian));
        Guard.Against.NullOrWhiteSpace(sequenceTag, nameof(sequenceTag));

        return new AntrianModel(antrianId, antrianDate, 
            sequenceTag, noAntrian, AntrianStatusEnum.Taken, "-",
            DateTime.Now, new DateTime(3000, 1, 1), new DateTime(3000, 1, 1),
            new DateTime(3000, 1, 1), new DateTime(3000, 1, 1));
    }
    #endregion

    #region PROPERTIES
    public string AntrianId { get; init; }
    public DateOnly AntrianDate { get; init; }

    public string SequenceTag { get; init; }
    public int NoAntrian { get; private set; }
    public AntrianStatusEnum AntrianStatus { get; private set; }
    public string PersonName { get; private set; }

    public DateTime TakenAt { get; init; }
    public DateTime AssignedAt { get; private set; }
    public DateTime PreparedAt { get; private set; }
    public DateTime DeliveredAt { get; private set; }
    public DateTime CancelAt { get; private set; }
    #endregion

    #region BEHAVIOR
    public void Assign(string personName)
    {
        Guard.Against.NullOrWhiteSpace(personName, nameof(personName));
        EnsureStatus(AntrianStatusEnum.Taken);

        AntrianStatus = AntrianStatusEnum.Assigned;
        AssignedAt = DateTime.Now;
        PersonName = personName;
    }

    public void Prepare()
    {
        EnsureStatus(AntrianStatusEnum.Assigned);
        PreparedAt = DateTime.Now;
        AntrianStatus = AntrianStatusEnum.Prepared;
    }

    public void Deliver()
    {
        EnsureStatus(AntrianStatusEnum.Prepared);
        DeliveredAt = DateTime.Now;
        AntrianStatus = AntrianStatusEnum.Delivered;
    }

    public void Cancel()
    {
        EnsureStatus(AntrianStatusEnum.Prepared);
        CancelAt = DateTime.Now;
        AntrianStatus = AntrianStatusEnum.Cancelled;
    }

    private void EnsureStatus(AntrianStatusEnum expected)
    {
        if (IsFinalState())
            throw new InvalidOperationException($"Status Antrian ({AntrianStatus}) cannot be change");

        if (AntrianStatus != expected)
            throw new InvalidOperationException($"Status Antrian must be {expected}");
    }

    private bool IsFinalState() =>
        AntrianStatus == AntrianStatusEnum.Delivered || AntrianStatus == AntrianStatusEnum.Cancelled;
    #endregion
}

public interface IAntrianKey
{
    string AntrianId { get; }
}