using Farinv.Domain.BrgContext.BrgFeature;
using Farinv.Domain.Shared.Helpers;

namespace Farinv.Domain.InventoryContext.MutasiFeature;

public class OrderMutasiItemModel
{
    #region CREATION
    public OrderMutasiItemModel(BrgReff brg, decimal qty, SatuanType satuan)
    {
        Brg = brg;
        Qty = qty;
        Satuan = satuan;
    }
    #endregion

    #region PROPERTIES
    public int NoUrut { get; internal set; }
    public BrgReff Brg { get; private set; }
    public decimal Qty { get; private set; }
    public SatuanType Satuan { get; private set; }
    #endregion

    #region BEHAVIOUR
    internal void AddQty(decimal qty)
    {
        if (qty <= 0)
            throw new DomainException("Qty harus lebih dari 0");

        Qty += qty;
    }

    internal void SetQty(decimal qty)
    {
        if (qty <= 0)
            throw new DomainException("Qty harus lebih dari 0");

        Qty = qty;
    }
    internal void SetNoUrut(int noUrut)
    {
        if (noUrut <= 0)
            throw new DomainException("NoUrut invalid");

        NoUrut = noUrut;
    }
    #endregion
}
