using Ardalis.GuardClauses;

namespace Farinv.Domain.BrgContext.BrgFeature;

public record BrgType : IBrgKey
{
    #region CREATION
    public BrgType(
        string brgId,
        string brgName,
        bool isAktif,
        BrgKlasifikasiType klasifikasi,
        BrgStandardType standart,
        BrgSatuanType satuan)
    {
        BrgId = brgId;
        BrgName = brgName;
        IsAktif = isAktif;
        Klasifikasi = klasifikasi;
        Standart = standart;
        Satuan = satuan;
    }

    public static BrgType Create(
        string brgId,
        string brgName,
        bool isAktif,
        BrgKlasifikasiType klasifikasi,
        BrgStandardType standart,
        BrgSatuanType satuan)
    {
        Guard.Against.NullOrWhiteSpace(brgId, nameof(brgId));
        Guard.Against.NullOrWhiteSpace(brgName, nameof(brgName));
        Guard.Against.Null(klasifikasi, nameof(klasifikasi));
        Guard.Against.Null(standart, nameof(standart));
        Guard.Against.Null(satuan, nameof(satuan));

        return new BrgType(brgId, brgName, isAktif, klasifikasi, standart, satuan);
    }

    public static BrgType Default => new(
        brgId: "-",
        brgName: "-",
        isAktif: false,
        klasifikasi: BrgKlasifikasiType.Default,
        standart: BrgStandardType.Default,
        satuan: BrgSatuanType.Default);

    public static IBrgKey Key(string id) => Default with { BrgId = id };
    #endregion

    #region PROPERTIES
    public string BrgId { get; init; }
    public string BrgName { get; init; }
    public bool IsAktif { get; init; }

    public BrgKlasifikasiType Klasifikasi { get; init; }
    public BrgStandardType Standart { get; init; }
    public BrgSatuanType Satuan { get; init; }
    #endregion

    #region BEHAVIOUR
    public BrgReff ToReff() => new(BrgId, BrgName);
    #endregion
}

public interface IBrgKey
{
    string BrgId { get; }
}

public record BrgReff(string BrgId, string BrgName);