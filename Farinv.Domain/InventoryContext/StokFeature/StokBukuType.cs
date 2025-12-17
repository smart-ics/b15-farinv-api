using Farinv.Domain.Shared.Helpers;

namespace Farinv.Domain.InventoryContext.StokFeature;

public record StokBukuType : IStokBukuKey
{
    public StokBukuType(string stokBukuId, int noUrut, TrsReffType trsReff, string useCase, 
        int qtyIn, int qtyOut, DateTime entryDate, ModelStateEnum modelState)
    {
        StokBukuId = stokBukuId;
        NoUrut = noUrut;

        TrsReff = trsReff;
        UseCase = useCase;
        QtyIn = qtyIn;
        QtyOut = qtyOut;
        EntryDate = entryDate;
        ModelState = modelState;
    }
    
    public string StokBukuId { get; init;}
    public int NoUrut { get; init;}
    public TrsReffType TrsReff { get; init;}
    public string UseCase { get; init;}
    public int QtyIn { get; init;}
    public int QtyOut { get; init;}
    public DateTime EntryDate { get; init;}
    public ModelStateEnum ModelState { get; init; }

    public static StokBukuType Default => new StokBukuType("-", 0, TrsReffType.Default, 
        "-", 0, 0, new DateTime(3000,1,1), ModelStateEnum.Unchange);

    public static StokBukuType Masuk(int noUrut, int qty, 
        TrsReffType trsReff, string useCase)
    {
        var newStokBukuId = Ulid.NewUlid().ToString();
        var result = new StokBukuType(newStokBukuId, noUrut, trsReff, 
            useCase, qty, 0, DateTime.Now, ModelStateEnum.Added);
        return result;
    }

    public static StokBukuType Keluar(int noUrut, int qty, 
        TrsReffType trsReff, string useCase)
    {
        var newStokBukuId = Ulid.NewUlid().ToString();
        var result = new StokBukuType(newStokBukuId, noUrut, trsReff, 
            useCase, 0, qty, DateTime.Now, ModelStateEnum.Added);
        return result;
    }
};

public interface IStokBukuKey
{
    string StokBukuId { get;  }
}
