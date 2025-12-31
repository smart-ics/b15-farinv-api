using Farinv.Domain.InventoryContext.MutasiFeature;
using Farinv.Domain.InventoryContext.StokFeature;
using Farinv.Domain.Shared.Helpers.CommonValueObjects;

namespace Farinv.Infrastructure.InventoryContext.MutasiFeature;

public record OrderMutasiDto(
    string OrderMutasiId, DateTime OrderMutasiDate, OrderMutasiStateEnum State,

    string LayananOrderId, string LayananOrderName,
    string LayananTujuanId, string LayananTujuanName, 
    string OrderNote,

    string CrtUser, DateTime CrtDate, string UpdUser,
    DateTime UpdDate, string VodUser, DateTime VodDate)
{
    public static OrderMutasiDto FromModel(OrderMutasiModel model)
    {
        var result = new OrderMutasiDto(
            model.OrderMutasiId,
            model.OrderMutasiDate,
            model.State,
            model.LayananOrder.LayananId,
            model.LayananOrder.LayananName,
            model.LayananTujuan.LayananId,
            model.LayananTujuan.LayananName,
            model.OrderNote,
            model.AuditTrail.Created.UserId,
            model.AuditTrail.Created.Timestamp,
            model.AuditTrail.Modified.UserId,
            model.AuditTrail.Modified.Timestamp,
            model.AuditTrail.Voided.UserId,
            model.AuditTrail.Voided.Timestamp
        );
        return result;
    }

    public OrderMutasiModel ToModel(IEnumerable<OrderMutasiItemModel> listBrg)
    {
        var auditTrail = AuditTrailType.Create(CrtUser, CrtDate);
        auditTrail.Modif(UpdUser, UpdDate);
        auditTrail.Batal(VodUser, VodDate);

        var result = new OrderMutasiModel(
            OrderMutasiId,
            OrderMutasiDate,
            State,
            new LayananReff(LayananOrderId, LayananOrderName),
            new LayananReff(LayananTujuanId, LayananTujuanName),
            OrderNote,
            auditTrail,
            listBrg?.ToList() ?? []);
        return result;
    }

}