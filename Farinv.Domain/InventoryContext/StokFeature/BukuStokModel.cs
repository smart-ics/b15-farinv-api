using Farinv.Domain.BrgContext.BrgFeature;

namespace Farinv.Domain.InventoryContext.StokFeature;

public class BukuStokModel
{
    public string BukuStokId { get; init; }
    public string StockDate { get; init; }
    public BrgReff Brg { get; init; }
}