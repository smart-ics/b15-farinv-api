using Ardalis.GuardClauses;
using Farinv.Domain.BrgContext.KlasifikasiFeature;

namespace Farinv.Domain.BrgContext.BrgFeature;

public record SatuanType : ISatuanKey
{
    #region CREATION
    public SatuanType(string satuanId, string satuanName, bool isSatuanRacik)
    {
        SatuanId = satuanId;
        SatuanName = satuanName;
        IsSatuanRacik = isSatuanRacik;
    }

    public static SatuanType Create(string satuanId, string satuanName, bool isSatuanRacik)
    {
        Guard.Against.NullOrWhiteSpace(satuanId, nameof(satuanId));
        Guard.Against.NullOrWhiteSpace(satuanName, nameof(satuanName));
        return new SatuanType(satuanId, satuanName, isSatuanRacik);
    }

    public static SatuanType Default => new("-", "-", false);

    public static ISatuanKey Key(string id) => Default with { SatuanId = id };
    #endregion

    #region PROPERTIES
    public string SatuanId { get; init; }
    public string SatuanName { get; init; }
    public bool IsSatuanRacik { get; init; }
    #endregion

    #region BEHAVIOUR
    public SatuanReff ToReff() => new(SatuanId, SatuanName);
    #endregion
}

public interface ISatuanKey
{
    string SatuanId { get; }
}

public record SatuanReff(string SatuanId, string SatuanName);