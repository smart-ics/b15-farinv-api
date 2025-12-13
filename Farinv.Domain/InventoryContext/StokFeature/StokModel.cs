using Farinv.Domain.BrgContext.BrgFeature;

namespace Farinv.Domain.InventoryContext.StokFeature;

public class StokModel : IStokKey, IBrgLayananKey
{
    private readonly List<StokMutasiType> _listMutasi;

    public StokModel(string stokId, DateTime stokDate, 
        string brgId, string layananId,
        BrgReff brg, LayananReff layanan, string purchaseId, string receiveId, 
        DateOnly expDate, int qtyIn, int qtySisa, decimal hpp, string satuan, 
        IEnumerable<StokMutasiType> listMutasi)
    {
        StokId = stokId;
        StokDate = stokDate;
        BrgId = brgId;
        LayananId = layananId;
        Brg = brg;
        Layanan = layanan;
        
        PurchaseId = purchaseId;
        ReceiveId = receiveId;
        ExpDate = expDate;
        QtyIn = qtyIn;
        QtySisa = qtySisa;
        Hpp = hpp;
        Satuan = satuan;
        _listMutasi = listMutasi?.ToList() ?? [];
    }

    public static StokModel Default => new StokModel("-", new DateTime(3000, 1, 1), 
        "-", "-", BrgObatType.Default.ToReff(), LayananType.Default.ToReff(),
        "-", "-", DateOnly.FromDateTime(new DateTime(3000, 1, 1)), 0, 0, 0, "", []);
    public static IStokKey Key(string id) => new StokModel(id, new DateTime(3000, 1, 1), 
        "-", "-", BrgObatType.Default.ToReff(), LayananType.Default.ToReff(),
        "-", "-", DateOnly.FromDateTime(new DateTime(3000, 1, 1)), 0, 0, 0, "", []);

    public static StokModel Create(IBrg brg, LayananType layanan,
        DateTime mutasiDate, string purchaseId, string receiveId, DateOnly expDate,
        int qtyIn, decimal hpp)
    {
        var satuan = brg.ListSatuan.FirstOrDefault(x => x.Konversi == 1)
            ?.Satuan.SatuanName ?? "";
        var mutasi = new StokMutasiType(0, "-", mutasiDate, "", qtyIn, 0, DateTime.Now);
        return new StokModel("-", DateTime.Now,
            brg.BrgId, layanan.LayananId,
            brg.ToReff(), layanan.ToReff(),
            purchaseId, receiveId, expDate, 
            qtyIn, qtyIn, hpp, satuan, [mutasi]);
    }
    
    #region PROPERTIES
    public string StokId { get; init; }
    public DateTime StokDate { get; init; }
    
    public string BrgId { get; }
    public string LayananId { get; }
    public BrgReff Brg { get; init; }
    public LayananReff Layanan { get; init; }
    
    public string PurchaseId { get; init; }
    public string ReceiveId { get; init; }
    public DateOnly ExpDate { get; init; }
    
    public int QtyIn { get; init; }
    public int QtySisa { get; private set; }

    public decimal Hpp { get; init; }
    public string Satuan { get; init; }
    public IEnumerable<StokMutasiType> ListMutasi => _listMutasi;
    #endregion

    public void RemoveStok(int qty, string mutasiId, DateTime mutasiDate,
        string jenisMutasi)
    {
        QtySisa -= qty;
        if (QtySisa < 0)
            throw new ArgumentException("Stok tidak mencukupi");
        var noUrut = _listMutasi.Max(x => x.NoUrut);
        noUrut++;
        var mutasi = new StokMutasiType(noUrut, mutasiId, mutasiDate, jenisMutasi, 0, qty, DateTime.Now);
        _listMutasi.Add(mutasi);
    }
}

public record StokMutasiType(int NoUrut,string MutasiId, DateTime MutasiDate, 
    string JenisMutasi, int QtyIn, int QtyOut,
    DateTime EntryDate);

public interface IStokKey
{
    string StokId { get; }
}