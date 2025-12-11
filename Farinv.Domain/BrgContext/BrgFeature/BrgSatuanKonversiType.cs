using Ardalis.GuardClauses;

namespace Farinv.Domain.BrgContext.BrgFeature;

public record BrgSatuanKonversiType
{
    #region CREATION
    public BrgSatuanKonversiType(
        string brgId,
        SatuanReff satuan,
        int nilaiKonversi,
        SatuanReff satuanKonversi)
    {
        BrgId = brgId;
        Satuan = satuan;
        NilaiKonversi = nilaiKonversi;
        SatuanKonversi = satuanKonversi;
    }

    public static BrgSatuanKonversiType Create(
        string brgId,
        SatuanReff satuan,
        int nilaiKonversi,
        SatuanReff satuanKonversi)
    {
        Guard.Against.NullOrWhiteSpace(brgId, nameof(brgId));
        Guard.Against.Null(satuan, nameof(satuan));
        Guard.Against.Null(satuanKonversi, nameof(satuanKonversi));

        return new BrgSatuanKonversiType(brgId, satuan, nilaiKonversi, satuanKonversi);
    }

    public static BrgSatuanKonversiType Default => new(
        brgId: "-",
        satuan: SatuanType.Default.ToReff(),
        nilaiKonversi: 1,
        satuanKonversi: SatuanType.Default.ToReff());
    #endregion

    #region PROPERTIES
    public string BrgId { get; init; }
    public SatuanReff Satuan { get; init; }
    public int NilaiKonversi { get; init; }
    public SatuanReff SatuanKonversi { get; init; }
    #endregion
}