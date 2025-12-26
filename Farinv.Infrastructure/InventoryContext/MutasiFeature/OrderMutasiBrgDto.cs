using Farinv.Domain.BrgContext.BrgFeature;
using Farinv.Domain.InventoryContext.MutasiFeature;

namespace Farinv.Infrastructure.InventoryContext.MutasiFeature;

public record OrderMutasiBrgDto(
    string OrderMutasiId,
    int NoUrut,
    string BrgId,
    string BrgName,
    decimal Qty,
    string SatuanId,
    string SatuanName)
{
    public static OrderMutasiBrgDto FromModel(string orderMutasiId, OrderMutasiBrgModel model)
    {
        var result = new OrderMutasiBrgDto(orderMutasiId, model.NoUrut, 
            model.Brg.BrgId, model.Brg.BrgName, model.Qty, model.Satuan.SatuanId, model.Satuan.SatuanName );
        return result;
    }

    public OrderMutasiBrgModel ToModel()
    {
        var brg = new BrgReff(BrgId, BrgName);
        var satuan = new SatuanType(SatuanId, SatuanName);
        var result = new OrderMutasiBrgModel(NoUrut, brg, Qty, satuan);
        return result;
    }
}