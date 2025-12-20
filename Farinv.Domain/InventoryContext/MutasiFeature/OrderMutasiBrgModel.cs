using Farinv.Domain.BrgContext.BrgFeature;
using Farinv.Domain.Shared.Helpers;

namespace Farinv.Domain.InventoryContext.MutasiFeature;

public class OrderMutasiBrgModel
{
    #region CREATION
    public OrderMutasiBrgModel(int noUrut, BrgReff brg, decimal qty, SatuanType satuan)
    {
        NoUrut = noUrut;
        Brg = brg;
        Qty = qty;
        Satuan = satuan;
    }
    #endregion

    #region PROPERTIES
    public int NoUrut { get; private set; }
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
    #endregion
}
