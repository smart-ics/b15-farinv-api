using Farinv.Domain.InventoryContext.StokFeature;

namespace Farinv.Infrastructure.InventoryContext.StokFeature;

public record StokBukuDto(
    string StokBukuId,
    string StokLayerId,
    int NoUrut,
    string BrgId,
    string LayananId,
    string TrsReffId,
    DateTime TrsReffDate,
    string PurchaseId,
    string ReceiveId,
    DateTime ExpDate,
    string BatchNo,
    string UseCase,
    int QtyIn,
    int QtyOut,
    decimal Hpp,
    DateTime EntryDate,
    string BrgName,
    string LayananName)
{
    public static StokBukuDto FromModel(StokModel header, StokLayerModel detilLv1, StokBukuType model)
    {
        var result = new StokBukuDto(
            model.StokBukuId,
            detilLv1.StokLayerId,
            model.NoUrut,
            header.Brg.BrgId,
            header.Layanan.LayananId,
            model.TrsReff.ReffId,
            model.TrsReff.ReffDate,
            detilLv1.StokLot.PurchaseId,
            detilLv1.StokLot.ReceiveId,
            detilLv1.StokLot.ExpDate.ToDateTime(TimeOnly.MinValue),
            detilLv1.StokLot.BatchNo,
            model.UseCase,
            model.QtyIn,
            model.QtyOut,
            detilLv1.Hpp,
            model.EntryDate, 
            header.Brg.BrgName,
            header.Layanan.LayananName);
        return result;
    }

    public StokBukuType ToModel()
    {
        var trsReff = new TrsReffType(TrsReffId, TrsReffDate);
        var result = new StokBukuType(StokBukuId, NoUrut, trsReff, UseCase, QtyIn, QtyOut, EntryDate);
        return result;
    }
}