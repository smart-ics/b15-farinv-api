using Farinv.Application.InventoryContext.MutasiFeature;
using Farinv.Domain.InventoryContext.MutasiFeature;
using Farinv.Domain.InventoryContext.StokFeature;
using Nuna.Lib.PatternHelper;
using Nuna.Lib.ValidationHelper;

namespace Farinv.Infrastructure.InventoryContext.MutasiFeature;

public class OrderMutasiRepo : IOrderMutasiRepo
{
    private readonly IOrderMutasiDal _orderMutasiDal;
    private readonly IOrderMutasiBrgDal _orderMutasiBrgDal;

    public OrderMutasiRepo(IOrderMutasiDal orderMutasiDal,
        IOrderMutasiBrgDal orderMutasiBrgDal)
    {
        _orderMutasiDal = orderMutasiDal;
        _orderMutasiBrgDal = orderMutasiBrgDal;
    }

    public void DeleteEntity(IOrderMutasiKey key)
    {
        _orderMutasiBrgDal.Delete(key);
        _orderMutasiDal.Delete(key);
    }

    public IEnumerable<OrderMutasiHeaderView> ListData(Periode filter)
    {
        var listDto = _orderMutasiDal.ListData(filter);
        var result = listDto.Select(x => new OrderMutasiHeaderView(x.OrderMutasiId, x.OrderMutasiDate, 
            x.State, new LayananReff(x.LayananOrderId, x.LayananOrderName)));
        return result;
    }

    public MayBe<OrderMutasiModel> LoadEntity(IOrderMutasiKey key)
    {
        var orderMutasi = _orderMutasiDal.GetData(key);
        var listItemDto = _orderMutasiBrgDal.ListData(key)?.ToList() ?? [];
        var listItem = listItemDto.Select(x => x.ToModel());
        var model = orderMutasi?.ToModel(listItem);
        return MayBe.From(model!);
    }

    public void SaveChanges(OrderMutasiModel model)
    {
        var dto = OrderMutasiDto.FromModel(model);
        LoadEntity(model)
            .Match(
                onSome: _ => _orderMutasiDal.Update(dto),
                onNone: () => _orderMutasiDal.Insert(dto)
            );

        var listBrgDto = model.ListItem
            .Select(x => OrderMutasiItemDto.FromModel(model.OrderMutasiId, x));
        _orderMutasiBrgDal.Delete(model);
        _orderMutasiBrgDal.Insert(listBrgDto);
    }
}
