using System.Data;
using System.Data.SqlClient;
using Dapper;
using Farinv.Domain.InventoryContext.StokFeature;
using Farinv.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;

namespace Farinv.Infrastructure.InventoryContext.StokFeature;

public interface IStokBukuDal :
    IInsertBulk<tb_buku_dto>,
    IDelete<IStokKey>,
    IListData<tb_buku_dto, IStokKey>
{
}

public class StokBukuDal : IStokBukuDal
{
    private readonly DatabaseOptions _opt;

    public StokBukuDal(IOptions<DatabaseOptions> opt)
    {
        _opt = opt.Value;
    }

    public void Insert(IEnumerable<tb_buku_dto> listModel)
    {
        var list = listModel.ToList();
        InsertTbBuku(list);
        InsertFarinStokBuku(list);
    }

    private void InsertTbBuku(List<tb_buku_dto> list)
    {
        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        using var bcp = new SqlBulkCopy(conn);
        conn.Open();

        bcp.AddMap("fs_kd_trs", "fs_kd_trs");
        bcp.AddMap("fs_kd_barang", "fs_kd_barang");
        bcp.AddMap("fs_kd_layanan", "fs_kd_layanan");
        bcp.AddMap("fs_kd_mutasi", "fs_kd_mutasi");
        bcp.AddMap("fd_tgl_jam_mutasi", "fd_tgl_jam_mutasi");
        bcp.AddMap("fd_tgl_mutasi", "fd_tgl_mutasi");
        bcp.AddMap("fs_jam_mutasi", "fs_jam_mutasi");
        bcp.AddMap("fs_kd_po", "fs_kd_po");
        bcp.AddMap("fs_kd_do", "fs_kd_do");
        bcp.AddMap("fd_tgl_ed", "fd_tgl_ed");
        bcp.AddMap("fs_no_batch", "fs_no_batch");
        bcp.AddMap("fn_stok_in", "fn_stok_in");
        bcp.AddMap("fn_stok_out", "fn_stok_out");
        bcp.AddMap("fn_hpp", "fn_hpp");
        bcp.AddMap("fs_kd_jenis_mutasi", "fs_kd_jenis_mutasi");
        bcp.AddMap("fs_kd_satuan", "fs_kd_satuan");

        bcp.BatchSize = list.Count;
        bcp.DestinationTableName = "tb_buku";
        bcp.WriteToServer(list.AsDataTable());
    }

    private void InsertFarinStokBuku(List<tb_buku_dto> list)
    {
        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        using var bcp = new SqlBulkCopy(conn);
        conn.Open();

        bcp.AddMap("StokLayerId", "StokLayerId");
        bcp.AddMap("StokBukuId", "fs_kd_trs");
        bcp.AddMap("NoUrut", "NoUrut");

        bcp.BatchSize = list.Count;
        bcp.DestinationTableName = "FARIN_StokBuku";
        bcp.WriteToServer(list.AsDataTable());
    }

    public void Delete(IStokKey key)
    {
        const string sql = """
            DELETE FROM FARIN_StokBuku
            WHERE StokBukuId IN (
                SELECT fs_kd_trs FROM tb_buku
                WHERE fs_kd_barang = @BrgId AND fs_kd_layanan = @LayananId
            );
            DELETE FROM tb_buku
            WHERE fs_kd_barang = @BrgId AND fs_kd_layanan = @LayananId;
            """;
        var dp = new DynamicParameters();
        dp.AddParam("@BrgId", key.BrgId, SqlDbType.VarChar);
        dp.AddParam("@LayananId", key.LayananId, SqlDbType.VarChar);
        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);
    }

    public IEnumerable<tb_buku_dto> ListData(IStokKey key)
    {
        const string sql = """
            SELECT 
                ISNULL(bb.StokLayerId, '') AS StokLayerId, 
                ISNULL(bb.NoUrut, 0) AS NoUrut,
                aa.fs_kd_trs, aa.fs_kd_barang, 
                aa.fs_kd_layanan, aa.fd_tgl_jam_mutasi,
                aa.fs_kd_po, aa.fs_kd_do, aa.fd_tgl_ed, aa.fs_no_batch,
                aa.fn_stok_in, aa.fn_stok_out, aa.fn_hpp, aa.fs_kd_satuan,
                ISNULL(cc.fs_nm_barang, '') AS fs_nm_barang,
                ISNULL(dd.fs_nm_layanan, '') AS fs_nm_layanan
            FROM 
                tb_buku aa
                LEFT JOIN FARIN_StokBuku bb ON aa.fs_kd_trs = bb.StokBukuId
                LEFT JOIN tb_barang cc ON aa.fs_kd_barang = cc.fs_kd_barang
                LEFT JOIN ta_layanan dd ON aa.fs_kd_layanan = dd.fs_kd_layanan
            WHERE 
                aa.fs_kd_barang = @BrgId AND aa.fs_kd_layanan = @LayananId
            """;
        var dp = new DynamicParameters();
        dp.AddParam("@BrgId", key.BrgId, SqlDbType.VarChar);
        dp.AddParam("@LayananId", key.LayananId, SqlDbType.VarChar);
        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        return conn.Read<tb_buku_dto>(sql, dp);
    }
}
