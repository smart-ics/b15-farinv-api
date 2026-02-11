using Ardalis.GuardClauses;

namespace Farinv.Domain.SalesContext.AntrianFeature;

public class AntrianModel: IAntrianKey
{
    #region CREATION
    public AntrianModel(string antrianId, DateOnly antrianDate,
        int noAntrian, AntrianStatusEnum status, string pasienName)
    {
        AntrianId = antrianId;
        AntrianDate = antrianDate;
        NoAntrian = noAntrian;
        Status = status;
        PasienName = pasienName;
    }

    public static IAntrianKey Key(string id)
    {
        var result = new AntrianModel(id, DateOnly.FromDateTime(DateTime.Now),
            0, AntrianStatusEnum.Open, "-");
        return result;
    }

    public static AntrianModel Default => new("-", DateOnly.FromDateTime(DateTime.Now),
            0, AntrianStatusEnum.Open, "-");

    public static AntrianModel Create(string antrianId, 
        DateOnly antrianDate, int noAntrian)
    {
        Guard.Against.NegativeOrZero(noAntrian, nameof(noAntrian));

        return new AntrianModel(antrianId, antrianDate, 
            noAntrian, AntrianStatusEnum.Taken, "-");
    }
    #endregion

    #region PROPERTIES
    public string AntrianId { get; init; }
    public DateOnly AntrianDate { get; init; }

    public int NoAntrian { get; private set; }
    public AntrianStatusEnum Status { get; private set; }
    public string PasienName { get; private set; }
    #endregion

    #region BEHAVIOR
    public void Assign(string pasienName)
    {
        Guard.Against.NullOrWhiteSpace(pasienName, nameof(pasienName));

        EnsureStatus(AntrianStatusEnum.Taken);
        Status = AntrianStatusEnum.Assigned;
        PasienName = pasienName;
    }

    public void Prepare()
    {
        EnsureStatus(AntrianStatusEnum.Assigned);
        Status = AntrianStatusEnum.Prepared;
    }

    public void Deliver()
    {
        EnsureStatus(AntrianStatusEnum.Prepared);
        Status = AntrianStatusEnum.Delivered;
    }

    public void Cancel()
    {
        EnsureStatus(AntrianStatusEnum.Prepared);
        Status = AntrianStatusEnum.Cancelled;
    }

    private void EnsureStatus(AntrianStatusEnum expected)
    {
        if (IsFinalState())
            throw new InvalidOperationException($"Status Antrian ({Status}) cannot be change");

        if (Status != expected)
            throw new InvalidOperationException($"Status Antrian must be {expected}");
    }

    private bool IsFinalState() => 
        Status == AntrianStatusEnum.Delivered || Status == AntrianStatusEnum.Cancelled;
    #endregion
}

public interface IAntrianKey
{
    string AntrianId { get; }
}