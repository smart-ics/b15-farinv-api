using Ardalis.GuardClauses;
using Farinv.Domain.BrgContext.BrgFeature;
using Farinv.Domain.InventoryContext.StokFeature;
using Farinv.Domain.Shared.Helpers;
using Farinv.Domain.Shared.Helpers.CommonValueObjects;

namespace Farinv.Domain.InventoryContext.MutasiFeature;

public class OrderMutasiModel : IOrderMutasiKey
{
    private readonly List<OrderMutasiBrgModel> _listBrg;

    #region CREATION
    public OrderMutasiModel(string orderMutasiId, DateTime orderMutasiDate, OrderMutasiStateEnum state,
        LayananReff layananOrder, LayananReff layananTujuan, string orderNote, AuditTrailType auditTrail, 
        IEnumerable<OrderMutasiBrgModel> listBrg)
    {
        OrderMutasiId = orderMutasiId;
        OrderMutasiDate = orderMutasiDate;
        State = state;
        LayananOrder = layananOrder;
        LayananTujuan = layananTujuan;
        OrderNote = orderNote;
        AuditTrail = auditTrail;
        _listBrg = listBrg?.ToList() ?? [];
    }

    public static OrderMutasiModel Default() 
        => new("-", new DateTime(300, 1, 1), OrderMutasiStateEnum.Draft,
            LayananType.Default.ToReff(), LayananType.Default.ToReff(), "-", AuditTrailType.Default, []);

    public static IOrderMutasiKey Key(string id)
        => new OrderMutasiModel(id, new DateTime(300, 1, 1), OrderMutasiStateEnum.Draft,
            LayananType.Default.ToReff(), LayananType.Default.ToReff(), "-", AuditTrailType.Default, []);

    public static OrderMutasiModel Create(
        LayananType layananOrder,
        LayananType layananTujuan,
        string orderNote,
        string userId,
        IEnumerable<OrderMutasiBrgModel> listBrg)
    {
        Guard.Against.Null(layananOrder, nameof(layananOrder));
        Guard.Against.Null(layananTujuan, nameof(layananTujuan));
        Guard.Against.Null(listBrg, nameof(listBrg));

        var id = Ulid.NewUlid().ToString();
        var auditTrail = AuditTrailType.Create(userId, DateTime.Now);

        var result = new OrderMutasiModel(id, auditTrail.Created.Timestamp, OrderMutasiStateEnum.Draft,
            layananOrder.ToReff(), layananTujuan.ToReff(), orderNote, auditTrail, listBrg);
        return result;
    }
    #endregion

    #region PROPERTIES
    public string OrderMutasiId { get; init; }
    public DateTime OrderMutasiDate { get; init; }
    public OrderMutasiStateEnum State { get; private set; }

    public LayananReff LayananOrder { get; init; }
    public LayananReff LayananTujuan { get; init; }

    public string OrderNote { get; private set; }
    public AuditTrailType AuditTrail { get; init; }

    public IReadOnlyCollection<OrderMutasiBrgModel> ListBrg => _listBrg;

    #endregion

    #region BEHAVIOR
    public void AddItem(OrderMutasiBrgModel item)
    {
        GuardDraft();
        if (item.Qty <= 0)
            throw new DomainException($"Qty invalid");

        var existing = _listBrg.FirstOrDefault(x => x.Brg.BrgId == item.Brg.BrgId);
        if (existing is not null)
        {
            existing.AddQty(item.Qty);
            return;
        }

        _listBrg.Add(item);
    }

    public void RemoveItem(IBrgKey key)
    {
        GuardDraft();
        _listBrg.RemoveAll(x => x.Brg.BrgId == key.BrgId);
    }

    public void Submit(string note)
    {
        GuardDraft();
        GuardHasItem();

        OrderNote = note ?? "-";
        State = OrderMutasiStateEnum.Submitted;
    }

    public void Approve()
    {
        GuardStatus(OrderMutasiStateEnum.Submitted);
        State = OrderMutasiStateEnum.Approved;
    }

    public void Reject()
    {
        GuardStatus(OrderMutasiStateEnum.Submitted);
        State = OrderMutasiStateEnum.Rejected;
    }

    public void Complete()
    {
        GuardStatus(OrderMutasiStateEnum.Approved);
        State = OrderMutasiStateEnum.Completed;
    }

    #endregion

    #region GUARD
    private void GuardDraft()
    {
        GuardStatus(OrderMutasiStateEnum.Draft);
    }

    private void GuardHasItem()
    {
        if (_listBrg.Count == 0)
            throw new DomainException("Order Mutasi harus memiliki minimal 1 item");
    }

    private void GuardStatus(params OrderMutasiStateEnum[] allowed)
    {
        if (!allowed.Contains(State))
            throw new DomainException($"Status tidak valid: {State}");
    }
    #endregion
}

public interface IOrderMutasiKey
{
    string OrderMutasiId { get; }
}

public record OrderMutasiHeaderView(
    string OrderMutasiId,
    DateTime OrderMutasiDate,
    OrderMutasiStateEnum State,
    LayananReff LayananOrder) : IOrderMutasiKey;