using Dapper;
using Farinv.Domain.InventoryContext.MutasiFeature;
using Farinv.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.ValidationHelper;
using System.Data;
using System.Data.SqlClient;

namespace Farinv.Infrastructure.InventoryContext.MutasiFeature;

public interface IOrderMutasiDal :
    IInsert<OrderMutasiDto>,
    IUpdate<OrderMutasiDto>,
    IDelete<IOrderMutasiKey>,
    IGetData<OrderMutasiDto, IOrderMutasiKey>,
    IListData<OrderMutasiDto, Periode>
{
}

public class OrderMutasiDal : IOrderMutasiDal
{
    private readonly DatabaseOptions _opt;

    public OrderMutasiDal(IOptions<DatabaseOptions> opt)
    {
        _opt = opt.Value;
    }

    public void Insert(OrderMutasiDto model)
    {
        const string sql = """
            INSERT INTO FARIN_OrderMutasi (
                OrderMutasiId, OrderMutasiDate, OrderMutasiState,
                LayananOrderId, LayananOrderName, 
                LayananTujuanId, LayananTujuanName,
                OrderNote,
                UserCreateId, TglJamCreate,
                UserModifyId, TglJamModify,
                UserVoidId, TglJamVoid
            )
            VALUES (
                @OrderMutasiId, @OrderMutasiDate, @State,
                @LayananOrderId, @LayananOrderName, 
                @LayananTujuanId, @LayananTujuanName,
                @OrderNote,
                @UserCreateId, @TglJamCreate,
                @UserModifyId, @TglJamModify,
                @UserVoidId, @TglJamVoid
            )
            """;
        var dp = new DynamicParameters();
        dp.AddParam("@OrderMutasiId", model.OrderMutasiId, SqlDbType.VarChar);
        dp.AddParam("@OrderMutasiDate", model.OrderMutasiDate, SqlDbType.DateTime);
        dp.AddParam("@State", (int)model.State, SqlDbType.Int);
        dp.AddParam("@LayananOrderId", model.LayananOrderId, SqlDbType.VarChar);
        dp.AddParam("@LayananOrderName", model.LayananOrderName, SqlDbType.VarChar);
        dp.AddParam("@LayananTujuanId", model.LayananTujuanId, SqlDbType.VarChar);
        dp.AddParam("@LayananTujuanName", model.LayananTujuanName, SqlDbType.VarChar);
        dp.AddParam("@OrderNote", model.OrderNote, SqlDbType.VarChar);
        dp.AddParam("@UserCreateId", model.UserCreateId, SqlDbType.VarChar);
        dp.AddParam("@TglJamCreate", model.TglJamCreate, SqlDbType.DateTime);
        dp.AddParam("@UserModifyId", model.UserModifyId, SqlDbType.VarChar);
        dp.AddParam("@TglJamModify", model.TglJamModify, SqlDbType.DateTime);
        dp.AddParam("@UserVoidId", model.UserVoidId, SqlDbType.VarChar);
        dp.AddParam("@TglJamVoid", model.TglJamVoid, SqlDbType.DateTime);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);
    }

    public void Update(OrderMutasiDto model)
    {
        const string sql = """
            UPDATE FARIN_OrderMutasi SET
                OrderMutasiDate = @OrderMutasiDate,
                OrderMutasiState = @State,
                LayananOrderId = @LayananOrderId,
                LayananOrderName = @LayananOrderName,
                LayananTujuanId = @LayananTujuanId,
                LayananTujuanName = @LayananTujuanName,
                OrderNote = @OrderNote,
                UserModifyId = @UserModifyId,
                TglJamModify = @TglJamModify,
                UserVoidId = @UserVoidId,
                TglJamVoid = @TglJamVoid
            WHERE 
                OrderMutasiId = @OrderMutasiId
            """;
        var dp = new DynamicParameters();
        dp.AddParam("@OrderMutasiId", model.OrderMutasiId, SqlDbType.VarChar);
        dp.AddParam("@OrderMutasiDate", model.OrderMutasiDate, SqlDbType.DateTime);
        dp.AddParam("@State", (int)model.State, SqlDbType.Int);
        dp.AddParam("@LayananOrderId", model.LayananOrderId, SqlDbType.VarChar);
        dp.AddParam("@LayananOrderName", model.LayananOrderName, SqlDbType.VarChar);
        dp.AddParam("@LayananTujuanId", model.LayananTujuanId, SqlDbType.VarChar);
        dp.AddParam("@LayananTujuanName", model.LayananTujuanName, SqlDbType.VarChar);
        dp.AddParam("@OrderNote", model.OrderNote, SqlDbType.VarChar);
        dp.AddParam("@UserModifyId", model.UserModifyId, SqlDbType.VarChar);
        dp.AddParam("@TglJamModify", model.TglJamModify, SqlDbType.DateTime);
        dp.AddParam("@UserVoidId", model.UserVoidId, SqlDbType.VarChar);
        dp.AddParam("@TglJamVoid", model.TglJamVoid, SqlDbType.DateTime);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);
    }

    public void Delete(IOrderMutasiKey key)
    {
        const string sql = """
            DELETE FROM FARIN_OrderMutasi WHERE OrderMutasiId = @OrderMutasiId;
            """;
        var dp = new DynamicParameters();
        dp.AddParam("@OrderMutasiId", key.OrderMutasiId, SqlDbType.VarChar);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);
    }

    public OrderMutasiDto GetData(IOrderMutasiKey key)
    {
        const string sql = """
            SELECT 
                OrderMutasiId, OrderMutasiDate, OrderMutasiState,
                LayananOrderId, LayananOrderName, 
                LayananTujuanId, LayananTujuanName,
                OrderNote,
                UserCreateId, TglJamCreate,
                UserModifyId, TglJamModify,
                UserVoidId, TglJamVoid
            FROM 
                FARIN_OrderMutasi
            WHERE 
                OrderMutasiId = @OrderMutasiId
            """;
        var dp = new DynamicParameters();
        dp.AddParam("@OrderMutasiId", key.OrderMutasiId, SqlDbType.VarChar);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        return conn.ReadSingle<OrderMutasiDto>(sql, dp);
    }

    public IEnumerable<OrderMutasiDto> ListData(Periode filter)
    {
        const string sql = """
            SELECT 
                OrderMutasiId, OrderMutasiDate, OrderMutasiState,
                LayananOrderId, LayananOrderName, 
                LayananTujuanId, LayananTujuanName,
                OrderNote,
                UserCreateId, TglJamCreate,
                UserModifyId, TglJamModify,
                UserVoidId, TglJamVoid
            FROM 
                FARIN_OrderMutasi
            WHERE 
                AntrianDate BETWEEN @Tgl1 AND @Tgl2
            """;
        var dp = new DynamicParameters();
        dp.AddParam("@Tgl1", filter.Tgl1, SqlDbType.DateTime);
        dp.AddParam("@Tgl2", filter.Tgl2, SqlDbType.DateTime);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        return conn.Read<OrderMutasiDto>(sql, dp);
    }

}
