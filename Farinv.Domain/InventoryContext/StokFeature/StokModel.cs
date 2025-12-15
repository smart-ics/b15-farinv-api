using Farinv.Domain.BrgContext.BrgFeature;

namespace Farinv.Domain.InventoryContext.StokFeature;

public class StokModel : IStokKey
{
    #region CREATION
    private readonly List<StokLayerModel> _listLayer;
    public StokModel(string brgId, string layananId, BrgReff brg, 
        LayananReff layanan, int qty, string satuan,
        IEnumerable<StokLayerModel> listLayer)
    {
        BrgId = brgId;
        LayananId = layananId;
        Brg = brg;
        Layanan = layanan;
        Qty = qty;
        Satuan = satuan;
        _listLayer = listLayer?.ToList() ?? [];
    }
    public static StokModel Default => new StokModel("-", "-", BrgObatType.Default.ToReff(), 
        LayananType.Default.ToReff(), 0, "", []);
    public static IStokKey Key(string brgId, string layananId) 
        => new StokModel(brgId, layananId, BrgObatType.Default.ToReff(), 
        LayananType.Default.ToReff(), 0, "", []);
    #endregion
    
    #region PROPERTIES
    public string BrgId { get; }
    public string LayananId { get; }
    public BrgReff Brg { get; init; }
    public LayananReff Layanan { get; init; }
    public int Qty { get; private set; }
    public string Satuan { get; init; }
    public IEnumerable<StokLayerModel> ListLayer => _listLayer;
    #endregion

    public void AddStok(int qty, StokLotType stokLot, decimal hpp, 
        string trsReffId, DateTime trsReffDate, string jenisMutasi)
    {
        var newLayer = StokLayerModel.Create(qty, hpp, stokLot, 
            trsReffId, trsReffDate, jenisMutasi);
        _listLayer.Add(newLayer);
        UpdateQty();
    }
    public void RemoveStok(int qty, string trsReffId, DateTime trsReffDate, string jenisMutasi)
    {
        if (qty > Qty)
            throw new ArgumentException("Qty tidak boleh melebihi Qty Stok");

        var qtyToBeRemoved = qty;
        foreach (var item in _listLayer.OrderBy(x => x.StokLot.ExpDate))
        {
            var qtyRemove = Math.Min(qtyToBeRemoved, item.QtySisa);
            item.RemoveStok(qtyRemove, trsReffId, trsReffDate, jenisMutasi);
            qtyToBeRemoved -= qtyRemove;
            if (qtyToBeRemoved == 0)
                break;
        }
        if (qtyToBeRemoved > 0)
            throw new ArgumentException("Qty tidak boleh melebihi Qty Stok");
        
        UpdateQty();
    }
    
    private void UpdateQty() => Qty = _listLayer.Sum(x => x.QtySisa);
}

public class StokLayerModel : IStokLayerKey
{
    private readonly List<StokBukuType> _listMovement;

    public StokLayerModel(string stokLayerId, StokLotType stokLot, 
        int qty, int qtySisa, decimal hpp,
        IEnumerable<StokBukuType> listMovement)
    {
        StokLayerId = stokLayerId;
        StokLot = stokLot;
        Qty = qty;
        QtySisa = qtySisa;
        Hpp = hpp;
        _listMovement = listMovement?.ToList() ?? [];
    }

    public static StokLayerModel Create(int qty, decimal hpp, 
        StokLotType stokLot, string trsReffId, DateTime trsReffDate, string jenisMutasi)
    {
        var stokLayerId = Ulid.NewUlid().ToString();
        var newBuku = StokBukuType.Masuk(0, qty, trsReffId, trsReffDate, jenisMutasi);
        var newLayer = new StokLayerModel(stokLayerId, stokLot, qty, qty, hpp, [newBuku]);
        return newLayer;
    }
    public string StokLayerId { get; init; }
    public StokLotType StokLot { get; init; }
    public int Qty { get; init; }
    public int QtySisa { get; private set; }
    public decimal Hpp { get; init; }
    public IEnumerable<StokBukuType> ListMovement => _listMovement;
    
    public void RemoveStok(int qty, string trsReffId, DateTime trsReffDate, string jenisMutasi)
    {
        if (qty > QtySisa)
            throw new ArgumentException("Qty tidak boleh melebihi Qty Stok");
        var newNoUrut = _listMovement.Max(x => x.NoUrut);
        newNoUrut++;
        var newBuku = StokBukuType.Keluar(newNoUrut, qty, trsReffId, trsReffDate, jenisMutasi);
        _listMovement.Add(newBuku);
        QtySisa = _listMovement.Sum(x => x.QtyIn - x.QtyOut);
    }
}

public record StokBukuType(string StokBukuId,
    int NoUrut, string TrsReffId, DateTime TrsReffDate,
    string JenisMutasi, int QtyIn, int QtyOut,
    DateTime EntryDate) : IStokBukuKey
{
    public static StokBukuType Default => new StokBukuType("-", 0, "-", new DateTime(3000,1,1), 
        "-", 0, 0, new DateTime(3000,1,1));

    public static StokBukuType Masuk(int noUrut, int qty, string trsReffId, 
        DateTime trsReffDate, string jenisMutasi)
    {
        var newStokBukuId = Ulid.NewUlid().ToString();
        var result = new StokBukuType(newStokBukuId, noUrut, trsReffId, trsReffDate, 
            jenisMutasi, qty, 0, DateTime.Now);
        return result;
    }

    public static StokBukuType Keluar(int noUrut, int qty, string trsReffId,
        DateTime trsReffDate, string jenisMutasi)
    {
        var newStokBukuId = Ulid.NewUlid().ToString();
        var result = new StokBukuType(newStokBukuId, noUrut, trsReffId, trsReffDate, 
            jenisMutasi, 0, qty, DateTime.Now);
        return result;
    }
};

public interface IStokKey : IBrgKey, ILayananKey
{
}

public interface IStokLayerKey
{
    string StokLayerId { get; }
}

public interface IStokBukuKey
{
    string StokBukuId { get;  }
}
public record StokLotType(string PurchaseId, string ReceiveId, DateOnly ExpDate)
{
    public static StokLotType Default = new("-", "-", new DateOnly(3000, 1, 1));
}