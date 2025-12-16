namespace Farinv.Domain.InventoryContext.StokFeature;

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

public interface IStokBukuKey
{
    string StokBukuId { get;  }
}
