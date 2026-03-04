using Farinv.Domain.InventoryContext.StokFeature;
using Farinv.Domain.SalesContext.AntrianFeature;
using Farinv.Domain.SalesContext.ResepFeature;

namespace Farinv.Domain.SalesContext.PenjualanFeature;

public class PenjualanModel : IPenjualanKey
{
    public PenjualanModel(string penjualanId, RegReff reg, DokterReff dokter, LayananReff layanan, decimal subtotal,
        decimal totalEmbalase, decimal totalTax, decimal totalDiskon, decimal diskonLain, decimal biayaLain,
        decimal grandTotal)
    {
        PenjualanId = penjualanId;
        Register = reg;
        Dokter = dokter;
        Layanan = layanan;
        SubTotal = subtotal;
        TotalEmbalase = totalEmbalase;
        TotalTax = totalTax;
        TotalDiskon = totalDiskon;
        DiskonLain = diskonLain;
        BiayaLain = biayaLain;
        GrandTotal = grandTotal;
    }

    public static PenjualanModel Key(string id) => new(id, RegType.Default.ToReff(), DokterType.Default.ToReff(),
        LayananType.Default.ToReff(), 0, 0, 0, 0, 0, 0, 0);

    public string PenjualanId { get; private set; }
    public RegReff Register { get; private set; }
    public DokterReff Dokter { get; private set; }
    public LayananReff Layanan { get; private set; }
    public decimal SubTotal { get; private set; }
    public decimal TotalEmbalase { get; private set; }
    public decimal TotalTax { get; private set; }
    public decimal TotalDiskon { get; private set; }
    public decimal DiskonLain { get; private set; }
    public decimal BiayaLain { get; private set; }
    public decimal GrandTotal { get; private set; }
}