namespace Farinv.Domain.InventoryContext.StokFeature;

public class StokLayerModel : IStokLayerKey
{
    private readonly List<StokBukuType> _listMovement;

    public StokLayerModel(string stokLayerId, StokLotType stokLot, 
        int qty, int qtySisa, decimal hpp,
        IEnumerable<StokBukuType> listMovement)
    {
        StokLayerId = stokLayerId;
        StokLot = stokLot;
        QtyIn = qty;
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
    public int QtyIn { get; init; }
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

public interface IStokLayerKey
{
    string StokLayerId { get; }
}

