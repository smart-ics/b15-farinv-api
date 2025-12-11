using Ardalis.GuardClauses;
using Farinv.Domain.BrgContext.KlasifikasiFeature;

namespace Farinv.Domain.BrgContext.BrgFeature;

public record BrgKlasifikasiType
{
    #region CREATION
    public BrgKlasifikasiType(
        string brgId,
        GolonganType golongan,
        GroupObatDkType groupObatDk,
        KelompokType kelompok,
        SifatType sifat,
        BentukType bentuk,
        JenisType jenis,
        PabrikReff pabrik)
    {
        BrgId = brgId;
        Golongan = golongan;
        GroupObatDk = groupObatDk;
        Kelompok = kelompok;
        Sifat = sifat;
        Bentuk = bentuk;
        Jenis = jenis;
        Pabrik = pabrik;
    }

    public static BrgKlasifikasiType Create(
        string brgId,
        GolonganType golongan,
        GroupObatDkType groupObatDk,
        KelompokType kelompok,
        SifatType sifat,
        BentukType bentuk,
        JenisType jenis,
        PabrikReff pabrik)
    {
        Guard.Against.NullOrWhiteSpace(brgId, nameof(brgId));
        Guard.Against.Null(golongan, nameof(golongan));
        Guard.Against.Null(groupObatDk, nameof(groupObatDk));
        Guard.Against.Null(kelompok, nameof(kelompok));
        Guard.Against.Null(sifat, nameof(sifat));
        Guard.Against.Null(bentuk, nameof(bentuk));
        Guard.Against.Null(jenis, nameof(jenis));
        Guard.Against.Null(pabrik, nameof(pabrik));

        return new BrgKlasifikasiType(brgId, golongan, groupObatDk, kelompok, sifat, bentuk, jenis, pabrik);
    }

    public static BrgKlasifikasiType Default => new(
        brgId: "-",
        golongan: GolonganType.Default,
        groupObatDk: GroupObatDkType.Default,
        kelompok: KelompokType.Default,
        sifat: SifatType.Default,
        bentuk: BentukType.Default,
        jenis: JenisType.Default,
        pabrik: PabrikType.Default.ToReff());
    #endregion

    #region PROPERTIES
    public string BrgId { get; init; }
    public GolonganType Golongan { get; init; }
    public GroupObatDkType GroupObatDk { get; init; }
    public KelompokType Kelompok { get; init; }
    public SifatType Sifat { get; init; }
    public BentukType Bentuk { get; init; }
    public JenisType Jenis { get; init; }
    public PabrikReff Pabrik { get; init; }
    #endregion
}