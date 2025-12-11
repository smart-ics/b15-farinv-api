using Dapper;
using Farinv.Domain.BrgContext.BrgFeature;
using Farinv.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using System.Data;
using System.Data.SqlClient;

namespace Farinv.Infrastructure.BrgContext.BrgFeature;

public interface IBrgSatuanDal :
    IInsert<BrgSatuanDto>,
    IUpdate<BrgSatuanDto>,
    IDelete<IBrgKey>,
    IGetData<BrgSatuanDto, IBrgKey>,
    IListData<BrgSatuanDto>
{
}

public class BrgSatuanDal : IBrgSatuanDal
{
    private readonly DatabaseOptions _opt;

    public BrgSatuanDal(IOptions<DatabaseOptions> opt)
    {
        _opt = opt.Value;
    }

    public void Insert(BrgSatuanDto dto)
    {
        const string sql = """
            INSERT INTO tb_barang_satuan (
                fs_kd_barang,
                fs_kd_satuan,
                fn_nilai
            ) VALUES (
                @fs_kd_barang,
                @fs_kd_satuan,
                @fn_nilai
            )
            """;

        var dp = new DynamicParameters();
        dp.AddParam("@fs_kd_barang", dto.fs_kd_barang, SqlDbType.VarChar);
        dp.AddParam("@fs_kd_satuan", dto.fs_kd_satuan, SqlDbType.VarChar);
        dp.AddParam("@fn_nilai", dto.fn_nilai, SqlDbType.Decimal);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);
    }

    public void Update(BrgSatuanDto dto)
    {
        const string sql = """
            UPDATE tb_barang_satuan
            SET
                fs_kd_satuan = @fs_kd_satuan,
                fn_nilai = @fn_nilai
            WHERE
                fs_kd_barang = @fs_kd_barang
            """;

        var dp = new DynamicParameters();
        dp.AddParam("@fs_kd_barang", dto.fs_kd_barang, SqlDbType.VarChar);
        dp.AddParam("@fs_kd_satuan", dto.fs_kd_satuan, SqlDbType.VarChar);
        dp.AddParam("@fn_nilai", dto.fn_nilai, SqlDbType.Decimal);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);
    }

    public void Delete(IBrgKey key)
    {
        const string sql = """
            DELETE FROM tb_barang_satuan
            WHERE fs_kd_barang = @fs_kd_barang
            """;

        var dp = new DynamicParameters();
        dp.AddParam("@fs_kd_barang", key.BrgId, SqlDbType.VarChar);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);
    }

    public BrgSatuanDto GetData(IBrgKey key)
    {
        const string sql = """
            SELECT
                fs_kd_barang,
                fs_kd_satuan,
                fn_nilai
            FROM tb_barang_satuan
            WHERE fs_kd_barang = @fs_kd_barang
            """;

        var dp = new DynamicParameters();
        dp.AddParam("@fs_kd_barang", key.BrgId, SqlDbType.VarChar);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        return conn.ReadSingle<BrgSatuanDto>(sql, dp);
    }

    public IEnumerable<BrgSatuanDto> ListData()
    {
        const string sql = """
            SELECT
                fs_kd_barang,
                fs_kd_satuan,
                fn_nilai
            FROM tb_barang_satuan
            """;

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        return conn.Read<BrgSatuanDto>(sql);
    }
}