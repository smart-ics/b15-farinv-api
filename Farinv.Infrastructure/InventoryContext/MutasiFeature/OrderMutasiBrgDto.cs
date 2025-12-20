namespace Farinv.Infrastructure.InventoryContext.MutasiFeature;

public record OrderMutasiBrgDto(
    int NoUrut,
    string BrgId,
    string BrgName,
    decimal Qty,
    string SatuanId,
    string SatuanName);