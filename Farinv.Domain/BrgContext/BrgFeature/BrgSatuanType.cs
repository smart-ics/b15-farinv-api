using Ardalis.GuardClauses;

namespace Farinv.Domain.BrgContext.BrgFeature;

public record BrgSatuanType
{
    #region CREATION
    public BrgSatuanType(
        string brgId,
        decimal dosis,
        SatuanReff dosisSatuan,
        IEnumerable<BrgSatuanKonversiType> konversi)
    {
        BrgId = brgId;
        Dosis = dosis;
        DosisSatuan = dosisSatuan;
        Konversi = konversi?.ToList() ?? [];
    }

    public static BrgSatuanType Create(
        string brgId,
        decimal dosis,
        SatuanReff dosisSatuan,
        IEnumerable<BrgSatuanKonversiType> konversi)
    {
        Guard.Against.NullOrWhiteSpace(brgId, nameof(brgId));
        Guard.Against.Null(dosisSatuan, nameof(dosisSatuan));

        return new BrgSatuanType(brgId, dosis, dosisSatuan, konversi);
    }

    public static BrgSatuanType Default => new(
        brgId: "-",
        dosis: 0,
        dosisSatuan: SatuanType.Default.ToReff(),
        konversi: []);
    #endregion

    #region PROPERTIES
    public string BrgId { get; init; }
    public decimal Dosis { get; init; }
    public SatuanReff DosisSatuan { get; init; }
    public IEnumerable<BrgSatuanKonversiType> Konversi { get; init; }
    #endregion
}