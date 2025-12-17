using Farinv.Domain.Shared.Helpers;

namespace Farinv.Domain.InventoryContext.StokFeature;

public class StokLayerModel : IStokLayerKey
{
    private readonly List<StokBukuType> _listMovement;

    public StokLayerModel(string stokLayerId, 
        TrsReffType trsReffIn, StokLotType stokLot, 
        int qty, int qtySisa, decimal hpp,
        IEnumerable<StokBukuType> listBuku)
    {
        StokLayerId = stokLayerId;
        TrsReffIn = trsReffIn;
        StokLot = stokLot;
        QtyIn = qty;
        QtySisa = qtySisa;
        Hpp = hpp;
        _listMovement = listBuku?.ToList() ?? [];
        ModelState = ModelStateEnum.Unchange;
    }
    public static IStokLayerKey Key(string id)
        => new StokLayerModel(id, TrsReffType.Default, StokLotType.Default, 0, 0, 0, []);
    public static StokLayerModel Default
        => new StokLayerModel("-", TrsReffType.Default, StokLotType.Default, 0, 0, 0, []);

    public static StokLayerModel Create(TrsReffType trsReffIn,
        StokLotType stokLot, int qty, decimal hpp, string useCase)
    {
        var stokLayerId = Ulid.NewUlid().ToString();
        var newBuku = StokBukuType.Masuk(0, qty, trsReffIn, useCase);
        var newLayer = new StokLayerModel(stokLayerId, trsReffIn, stokLot, 
            qty, qty, hpp, [newBuku]);
        newLayer.ModelState = ModelStateEnum.Added;
        return newLayer;
    }
    public string StokLayerId { get; init; }
    public TrsReffType TrsReffIn { get; init; }
    public StokLotType StokLot { get; init; }
    public int QtyIn { get; init; }
    public int QtySisa { get; private set; }
    public decimal Hpp { get; init; }
    public IEnumerable<StokBukuType> ListBuku => _listMovement;
    public ModelStateEnum ModelState { get; private set; }
    
    public void RemoveStok(int qty, TrsReffType trsReff, string jenisMutasi)
    {
        if (qty > QtySisa)
            throw new ArgumentException("Qty tidak boleh melebihi Qty Stok");
        var newNoUrut = _listMovement.Max(x => x.NoUrut);
        newNoUrut++;
        var newBuku = StokBukuType.Keluar(newNoUrut, qty, trsReff, jenisMutasi);
        _listMovement.Add(newBuku);
        QtySisa = _listMovement.Sum(x => x.QtyIn - x.QtyOut);
        ModelState = ModelStateEnum.Updated;
    }
}

public interface IStokLayerKey
{
    string StokLayerId { get; }
}

