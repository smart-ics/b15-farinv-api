using Farinv.Domain.InventoryContext.StokFeature;

public record StokLayerDto(
    string StokLayerId,
    string BrgId,
    string LayananId,
    string TrsReffInId,
    DateTime TrsReffInDate,
    string PurchaseId,
    string ReceiveId,
    DateTime ExpDate,
    string BatchNo,
    int QtyIn,
    int QtySisa,
    decimal Hpp,
    string BrgName,
    string LayananName)
{
    public static StokLayerDto FromModel(StokModel header, StokLayerModel model)
    {
        var result = new StokLayerDto(
            model.StokLayerId,
            header.BrgId,
            header.LayananId,
            model.TrsReffIn.ReffId,
            model.TrsReffIn.ReffDate,
            model.StokLot.PurchaseId,
            model.StokLot.ReceiveId,
            model.StokLot.ExpDate.ToDateTime(TimeOnly.MinValue),
            model.StokLot.BatchNo,
            model.QtyIn,
            model.QtySisa,
            model.Hpp,
            header.Brg.BrgName,
            header.Layanan.LayananName);
        return result;
    }

    public StokLayerModel ToModel(IEnumerable<StokBukuType> listBuku)
    {
        var trsReffIn = new TrsReffType(TrsReffInId, TrsReffInDate);
        var stokLot = new StokLotType(PurchaseId, ReceiveId, DateOnly.FromDateTime(ExpDate), BatchNo);
        var result = new StokLayerModel(StokLayerId, trsReffIn, stokLot, QtyIn, QtySisa, Hpp, listBuku);
        return result;
    }
}