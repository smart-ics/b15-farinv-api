using System.Data;
using System.Data.SqlClient;
using Dapper;
using Farinv.Domain.InventoryContext.StokFeature;
using Farinv.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;

namespace Farinv.Infrastructure.InventoryContext.StokFeature;

public interface IStokBukuDal:
    IInsertBulk<StokBukuDto>,
    IDelete<IStokKey>,
    IListData<StokBukuDto, IStokKey>
{
}

public class StokBukuDal : IStokBukuDal
{
    private readonly DatabaseOptions _opt;
    public StokBukuDal(IOptions<DatabaseOptions> opt)
    {
        _opt = opt.Value;
    }
    
    public void Insert(IEnumerable<StokBukuDto> listModel)
    {
        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        using var bcp = new SqlBulkCopy(conn);
        conn.Open();
        bcp.AddMap("StokBukuId", "StokBukuId");
        bcp.AddMap("StokLayerId", "StokLayerId");
        bcp.AddMap("NoUrut", "NoUrut");
        bcp.AddMap("BrgId", "BrgId");
        bcp.AddMap("LayananId", "LayananId");
        bcp.AddMap("TrsReffId", "TrsReffId");
        bcp.AddMap("TrsReffDate", "TrsReffDate");
        bcp.AddMap("PurchaseId", "PurchaseId");
        bcp.AddMap("ReceiveId", "ReceiveId");
        bcp.AddMap("ExpDate", "ExpDate");
        bcp.AddMap("BatchNo", "BatchNo");
        bcp.AddMap("UseCase", "UseCase");
        bcp.AddMap("QtyIn", "QtyIn");
        bcp.AddMap("QtyOut", "QtyOut");
        bcp.AddMap("Hpp", "Hpp");
        bcp.AddMap("EntryDate", "EntryDate");
        var fetched = listModel.ToList();
        bcp.BatchSize = fetched.Count;
        bcp.DestinationTableName = "FARIN_StokBuku";
        bcp.WriteToServer(fetched.AsDataTable());
    }
    
    public void Delete(IStokKey key)
    {
        const string sql = """
            DELETE FROM
                FARIN_StokBuku
            WHERE
                BrgId = @BrgId AND LayananId = @LayananId
            """;
        var dp = new DynamicParameters();
        dp.AddParam("@BrgId", key.BrgId, SqlDbType.VarChar);
        dp.AddParam("@LayananId", key.LayananId, SqlDbType.VarChar);
        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);    
    }
    
    public IEnumerable<StokBukuDto> ListData(IStokKey filter)
    {
        const string sql = """
            SELECT 
                aa.StokBukuId, aa.StokLayerId, aa.NoUrut,
                aa.BrgId, aa.LayananId,
                aa.TrsReffId, aa.TrsReffDate,
                aa.PurchaseId, aa.ReceiveId, aa.ExpDate, aa.BatchNo,
                aa.UseCase, aa.QtyIn, aa.QtyOut, aa.Hpp, aa.EntryDate,
                ISNULL(bb.fs_nm_barang, '') BrgName,
                ISNULL(cc.fs_nm_layanan, '') LayananName
            FROM 
                FARIN_StokBuku aa
                LEFT JOIN tb_barang bb ON aa.BrgId = bb.fs_kd_barang
                LEFT JOIN ta_layanan cc ON aa.LayananId = cc.fs_kd_layanan
            WHERE
                aa.BrgId = @BrgId AND aa.LayananId = @LayananId
            """;
        var dp = new DynamicParameters();
        dp.AddParam("@BrgId", filter.BrgId, SqlDbType.VarChar);
        dp.AddParam("@LayananId", filter.LayananId, SqlDbType.VarChar);
        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        return conn.Read<StokBukuDto>(sql, dp);
    }
}