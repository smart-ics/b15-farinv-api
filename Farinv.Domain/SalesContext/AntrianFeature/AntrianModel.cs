namespace Farinv.Domain.SalesContext.AntrianFeature;

public class AntrianModel: IAntrianKey
{
    private readonly List<AntrianEntryModel> _listEntry;

    #region CREATION
    public AntrianModel(
        string antrianId, DateOnly antrianDate, TimeOnly startTime, TimeOnly endTime,
        int servicePoint, string antrianDesc, IEnumerable<AntrianEntryModel> listEntry)
    {
        AntrianId = antrianId;
        AntrianDate = antrianDate;
        StartTime = startTime;
        EndTime = endTime;
        ServicePoint = servicePoint;
        AntrianDescription = antrianDesc;
        SequenceTag = GenSequenceTag(antrianDate, startTime, servicePoint);

        _listEntry = [.. listEntry];
    }

    public static IAntrianKey Key(string id)
    {
        var result = new AntrianModel(id, DateOnly.FromDateTime(DateTime.Now),
            TimeOnly.MinValue, TimeOnly.MinValue, 0, "-", []);
        return result;
    }

    public static AntrianModel Default => new("-", DateOnly.MinValue,
        TimeOnly.MinValue, TimeOnly.MinValue, 0, "-", []);
    #endregion

    #region PROPERTIES
    public string AntrianId { get; init; }
    public DateOnly AntrianDate { get; init; }
    public TimeOnly StartTime { get; init; }
    public TimeOnly EndTime { get; init; }

    public int ServicePoint { get; init; }
    public string SequenceTag { get; init; }
    public string AntrianDescription { get; init; }

    public IReadOnlyCollection<AntrianEntryModel> ListEntry => _listEntry.AsReadOnly();
    #endregion

    #region BEHAVIOR
    public void AddEntry(int noAntrian)
    {
        EnsureSlotAvailable(noAntrian);
        var entry = AntrianEntryModel.Create(noAntrian, RegType.Default.ToReff(), "-", "-");
        _listEntry.Add(entry);
    }

    public void AddEntry(int noAntrian, RegReff reg, string reffId, string reffDesc)
    {
        EnsureSlotAvailable(noAntrian);
        var entry = AntrianEntryModel.Create(noAntrian, reg, reffId, reffDesc);
        _listEntry.Add(entry);
    }

    public void RemoveEntry(int noAntrian)
    {
        var itemToRemove = ListEntry.FirstOrDefault(x => x.NoAntrian == noAntrian) 
            ?? throw new InvalidOperationException("NoAntrian not found.");
        if (itemToRemove.AntrianStatus != AntrianStatusEnum.Taken)
            throw new InvalidOperationException("Only Taken slot can be removed.");

        _listEntry.Remove(itemToRemove);
    }

    public void AssignSlot(int noAntrian, RegReff reg)
    {
        var entry = GetEntry(noAntrian);
        entry.Assign(reg);
    }

    public void PrepareSlot(int noAntrian)
    {
        var entry = GetEntry(noAntrian);
        entry.Prepare();
    }

    public void DeliverSlot(int noAntrian)
    {
        var entry = GetEntry(noAntrian);
        entry.Deliver();
    }

    public void CancelSlot(int noAntrian)
    {
        var entry = GetEntry(noAntrian);
        entry.Cancel();
    }

    public void UpdateSlotReff(int noAntrian, string reffId, string reffDesc)
    {
        var entry = GetEntry(noAntrian);

        if (entry.AntrianStatus != AntrianStatusEnum.Taken)
            throw new InvalidOperationException("Reff can only be set when slot is Taken.");

        entry.SetReff(reffId, reffDesc);
    }

    private AntrianEntryModel GetEntry(int noAntrian)
    {
        var entry = _listEntry.FirstOrDefault(x => x.NoAntrian == noAntrian);
        return entry is null ? throw new InvalidOperationException($"Slot {noAntrian} not found.") : entry;
    }

    private static string GenSequenceTag(DateOnly tglAntrian, TimeOnly jamMulai, int servicePoint)
    {
        var sequenceTag = $"AA{tglAntrian:yyMMdd}{jamMulai:HHmm}_{servicePoint}";
        return sequenceTag;
    }

    private void EnsureSlotAvailable(int noAntrian)
    {
        if (_listEntry.Any(x => x.NoAntrian == noAntrian))
            throw new InvalidOperationException($"Slot {noAntrian} already taken.");
    }
    #endregion
}

public interface IAntrianKey
{
    string AntrianId { get; }
}

public record AntrianHeaderView(
    string AntrianId,
    string AntrianDescription,
    DateOnly AntrianDate,
    TimeOnly StartTime,
    int ServicePoint) : IAntrianKey;


public record AntrianView(string AntrianId, int NoAntrian, int AntrianStatus, 
    string RegId, string PasienId, string PasienName,
    string ReffId, string ReffDesc, DateTime AntrianDate, string SquenceTag, 
    string AntrianDescription, TimeOnly StartTime, TimeOnly EndTime);