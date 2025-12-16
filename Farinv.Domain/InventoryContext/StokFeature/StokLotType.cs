namespace Farinv.Domain.InventoryContext.StokFeature;

public record StokLotType(string PurchaseId, string ReceiveId, DateOnly ExpDate)
{
    public static StokLotType Default = new("-", "-", new DateOnly(3000, 1, 1));
}