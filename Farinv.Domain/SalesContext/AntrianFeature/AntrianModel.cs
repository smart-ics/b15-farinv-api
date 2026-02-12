using Ardalis.GuardClauses;
using Farinv.Domain.Shared.Helpers.CommonValueObjects;

namespace Farinv.Domain.SalesContext.AntrianFeature;

public class AntrianModel: IAntrianKey
{
    #region CREATION
    public AntrianModel(string antrianId, DateOnly antrianDate,
        string sequenceTag, int noAntrian, AntrianStatusEnum antrianStatus, 
        string personName, AuditTrailType auditTrail)
    {
        AntrianId = antrianId;
        AntrianDate = antrianDate;
        SequenceTag = sequenceTag;
        NoAntrian = noAntrian;
        AntrianStatus = antrianStatus;
        PersonName = personName;
        AuditTrail = auditTrail;
    }

    public static IAntrianKey Key(string id)
    {
        var result = new AntrianModel(id, DateOnly.FromDateTime(DateTime.Now),
            "-", 0, AntrianStatusEnum.Open, "-", AuditTrailType.Default);
        return result; 
    }

    public static AntrianModel Default => new("-", DateOnly.FromDateTime(DateTime.Now),
            "-", 0, AntrianStatusEnum.Open, "-", AuditTrailType.Default);

    public static AntrianModel Create(string antrianId, DateOnly antrianDate, 
        string sequenceTag, int noAntrian, string userId)
    {
        Guard.Against.NegativeOrZero(noAntrian, nameof(noAntrian));

        return new AntrianModel(antrianId, antrianDate, 
            sequenceTag, noAntrian, AntrianStatusEnum.Taken, "-",
            AuditTrailType.Create(userId, DateTime.Now));
    }
    #endregion

    #region PROPERTIES
    public string AntrianId { get; init; }
    public DateOnly AntrianDate { get; init; }

    public string SequenceTag { get; init; }
    public int NoAntrian { get; private set; }
    public AntrianStatusEnum AntrianStatus { get; private set; }
    public string PersonName { get; private set; }

    public AuditTrailType AuditTrail { get; init; }
    #endregion

    #region BEHAVIOR
    public void Assign(string personName, string userId)
    {
        Guard.Against.NullOrWhiteSpace(personName, nameof(personName));
        EnsureStatus(AntrianStatusEnum.Taken);

        AuditTrail.Modif(userId, DateTime.Now);
        AntrianStatus = AntrianStatusEnum.Assigned;
        PersonName = personName;
    }

    public void Prepare(string userId)
    {
        EnsureStatus(AntrianStatusEnum.Assigned);
        AuditTrail.Modif(userId, DateTime.Now);
        AntrianStatus = AntrianStatusEnum.Prepared;
    }

    public void Deliver(string userId)
    {
        EnsureStatus(AntrianStatusEnum.Prepared);
        AuditTrail.Modif(userId, DateTime.Now);
        AntrianStatus = AntrianStatusEnum.Delivered;
    }

    public void Cancel(string userId)
    {
        EnsureStatus(AntrianStatusEnum.Prepared);
        AuditTrail.Batal(userId, DateTime.Now);
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