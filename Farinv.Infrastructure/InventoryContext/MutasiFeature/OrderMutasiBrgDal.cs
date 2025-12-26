using Dapper;
using Farinv.Domain.InventoryContext.MutasiFeature;
using Farinv.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using System.Data;
using System.Data.SqlClient;

namespace Farinv.Infrastructure.InventoryContext.MutasiFeature;

public interface IOrderMutasiBrgDal :
    IInsertBulk<OrderMutasiBrgDto>,
    IDelete<IOrderMutasiKey>,
    IListData<OrderMutasiBrgDto, IOrderMutasiKey>
{
}

public class OrderMutasiBrgDal : IOrderMutasiBrgDal
{
    private readonly DatabaseOptions _opt;

    public OrderMutasiBrgDal(IOptions<DatabaseOptions> opt)
    {
        _opt = opt.Value;
    }

    public void Insert(IEnumerable<OrderMutasiBrgDto> listModel)
    {
        var list = listModel.ToList();

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        using var bcp = new SqlBulkCopy(conn);
        conn.Open();

        bcp.AddMap("OrderMutasiId", "OrderMutasiId");
        bcp.AddMap("NoUrut", "NoUrut");
        bcp.AddMap("BrgId", "BrgId");
        bcp.AddMap("BrgName", "BrgName");
        bcp.AddMap("Qty", "Qty");
        bcp.AddMap("SatuanId", "SatuanId");
        bcp.AddMap("SatuanName", "SatuanName");

        bcp.BatchSize = list.Count;
        bcp.DestinationTableName = "FARIN_OrderMutasiBrg";
        bcp.WriteToServer(list.AsDataTable());
    }

    public void Delete(IOrderMutasiKey key)
    {
        const string sql = """
            DELETE FROM FARIN_OrderMutasiBrg
            WHERE OrderMutasiId = @OrderMutasiId;
            """;
        var dp = new DynamicParameters();
        dp.AddParam("@OrderMutasiId", key.OrderMutasiId, SqlDbType.VarChar);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);
    }

    public IEnumerable<OrderMutasiBrgDto> ListData(IOrderMutasiKey filter)
    {
        const string sql = """
            SELECT
                OrderMutasiId, NoUrut,
                BrgId, BrgName, Qty,
                SatuanId, SatuanName
            FROM
                FARIN_OrderMutasiBrg
            WHERE
               OrderMutasiId = @OrderMutasiId 
            """;

        var dp = new DynamicParameters();
        dp.AddParam("@OrderMutasiId", filter.OrderMutasiId, SqlDbType.VarChar);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        return conn.Read<OrderMutasiBrgDto>(sql, dp);
    }
}
