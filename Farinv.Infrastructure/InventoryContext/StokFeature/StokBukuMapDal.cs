using System.Data;
using System.Data.SqlClient;
using Dapper;
using Farinv.Domain.InventoryContext.StokFeature;
using Farinv.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;

namespace Farinv.Infrastructure.InventoryContext.StokFeature;

public interface IStokBukuMapDal:
    IInsertBulk<StokBukuMapDto>,
    IDelete<IStokLayerKey>,
    IListData<StokBukuMapDto, IStokLayerKey>
{
}

public class StokBukuMapDal : IStokBukuMapDal
{
    private readonly DatabaseOptions _opt;
    public StokBukuMapDal(IOptions<DatabaseOptions> opt)
    {
        _opt = opt.Value;
    }
    
    public void Insert(IEnumerable<StokBukuMapDto> listModel)
    {
        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        using var bcp = new SqlBulkCopy(conn);
        conn.Open();
        bcp.AddMap("StokLayerId", "StokLayerId");
        bcp.AddMap("StokBukuId", "StokBukuId");
        var fetched = listModel.ToList();
        bcp.BatchSize = fetched.Count;
        bcp.DestinationTableName = "FARIN_StokBukuMap";
        bcp.WriteToServer(fetched.AsDataTable());
    }
    
    public void Delete(IStokLayerKey key)
    {
        const string sql = """
                           DELETE FROM
                               FARIN_StokBukuMap
                           WHERE
                               StokLayerId = @StokLayerId
                           """;
        var dp = new DynamicParameters();
        dp.AddParam("@StokLayerId", key.StokLayerId, SqlDbType.VarChar);
        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);    
    }
    
    public IEnumerable<StokBukuMapDto> ListData(IStokLayerKey filter)
    {
        const string sql = """
                           SELECT 
                               StokLayerId, StokBukuId
                           FROM 
                               FARIN_StokBukuMap
                           WHERE
                               StokLayerId = @StokLayerId
                           """;
        var dp = new DynamicParameters();
        dp.AddParam("@StokLayerId", filter.StokLayerId, SqlDbType.VarChar);
        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        return conn.Read<StokBukuMapDto>(sql, dp);
    }
}