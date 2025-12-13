using Farinv.Domain.BrgContext.BrgFeature;
using Farinv.Domain.InventoryContext.StokFeature;

namespace Farinv.Infrastructure.InventoryContext.StokFeature;

public record StokDto(
    string StokId, DateTime StokDate,
    string BrgId, string LayananId,
    string PurchaseId, string ReceiveId,
    DateOnly ExpDate,
    int QtyIn, int QtySisa,
    decimal Hpp, string Satuan,
    string BrgName, string LayananName
    )
{
    public static StokDto FromModel(StokModel model)
    {
        var result = new StokDto(
            model.StokId, model.StokDate,
            model.BrgId, model.LayananId,
            model.PurchaseId, model.ReceiveId,
            model.ExpDate,
            model.QtyIn, model.QtySisa,
            model.Hpp, model.Satuan,
            model.Brg.BrgName, model.Layanan.LayananName);
        return result;
    }

    public StokModel ToModel(IEnumerable<StokMutasiType> listMutasi)
    {
        var brg = new BrgReff(BrgId, BrgName);
        var layanan = new LayananReff(LayananId, LayananName);
        var result = new StokModel(
            StokId, StokDate,
            BrgId, LayananId,
            brg, layanan,
            PurchaseId, ReceiveId,
            ExpDate,
            QtyIn, QtySisa,
            Hpp, Satuan,
            listMutasi);
        return result;
    }
}

public record StokMutasiDto(
    string StokId,
    int NoUrut, string MutasiId, DateTime MutasiDate,
    string JenisMutasi, int QtyIn, int QtyOut,
    DateTime EntryDate)
{
    public static StokMutasiDto FromModel(string stokId, StokMutasiType model)
    {
        var result = new StokMutasiDto(
            stokId,
            model.NoUrut, model.MutasiId, model.MutasiDate,
            model.JenisMutasi, model.QtyIn, model.QtyOut,
            model.EntryDate);
        return result;
    }

    public StokMutasiType ToModel()
    {
        var result = new StokMutasiType(
            NoUrut, MutasiId, MutasiDate,
            JenisMutasi, QtyIn, QtyOut,
            EntryDate);
        return result;
    }
}