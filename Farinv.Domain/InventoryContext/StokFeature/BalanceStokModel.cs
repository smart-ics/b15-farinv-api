using Farinv.Domain.BrgContext.BrgFeature;

namespace Farinv.Domain.InventoryContext.StokFeature;

public class BalanceStokModel : IBrgLayananKey
{
    public BalanceStokModel(string brgId, string layananId, BrgReff brg, LayananReff layanan, int qty, string satuan)
    {
        BrgId = brgId;
        LayananId = layananId;
        Brg = brg;
        Layanan = layanan;
        Qty = qty;
        Satuan = satuan;
    }
    public string BrgId { get; }
    public string LayananId { get; }
    public BrgReff Brg { get; init; }
    public LayananReff Layanan { get; init; }
    public int Qty { get; init; }
    public string Satuan { get; init; }
}

public interface IBrgLayananKey : IBrgKey, ILayananKey
{
}