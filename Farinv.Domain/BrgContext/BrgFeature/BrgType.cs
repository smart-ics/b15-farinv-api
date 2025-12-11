using Ardalis.GuardClauses;
using Farinv.Domain.BrgContext.KlasifikasiFeature;
using Farinv.Domain.BrgContext.StandardFeature;

namespace Farinv.Domain.BrgContext.BrgFeature;

public record BrgType : IBrgKey
{
    #region CREATION
    public BrgType(
        string brgId,
        string brgName,
        bool isAktif,
        BrgSatuanType satuan)
    {
        BrgId = brgId;
        BrgName = brgName;
        IsAktif = isAktif;
        Satuan = satuan;
    }

    public static BrgType Create(
        string brgId,
        string brgName,
        bool isAktif,
        BrgSatuanType satuan)
    {
        Guard.Against.NullOrWhiteSpace(brgId, nameof(brgId));
        Guard.Against.NullOrWhiteSpace(brgName, nameof(brgName));
        Guard.Against.Null(satuan, nameof(satuan));

        return new BrgType(brgId, brgName, isAktif, satuan);
    }

    public static BrgType Default => new(
        brgId: "-",
        brgName: "-",
        isAktif: false,
        satuan: BrgSatuanType.Default);

    public static IBrgKey Key(string id) => Default with { BrgId = id };
    #endregion

    #region PROPERTIES
    public string BrgId { get; init; }
    public string BrgName { get; init; }
    public bool IsAktif { get; init; }
    public string KetBarang { get; init; }
    public GroupRekDkType GroupRekDk { get; init; }

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

public interface IKlasifikasiUmum
{
    GolonganType Golongan { get; }
    GroupObatDkType GroupObatDk { get; }
    KelompokType Kelompok { get; }
    SifatType Sifat { get; }
    BentukType Bentuk { get; }
}

public interface IKlasifikasiObat
{
    GenerikType Generik { get; }
    KelasTerapiType KelasTerapi { get; }
    GolTerapiType GolTerapi { get; }
    OriginalType Original { get; }
}

public interface IPabrik
{
    PabrikType Pabrik { get; }
}