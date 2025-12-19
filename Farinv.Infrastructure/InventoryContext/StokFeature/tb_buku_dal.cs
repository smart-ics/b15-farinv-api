using System.Data;
using System.Data.SqlClient;
using Dapper;
using Farinv.Domain.BrgContext.BrgFeature;
using Farinv.Domain.InventoryContext.StokFeature;
using Farinv.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;

namespace Farinv.Infrastructure.InventoryContext.StokFeature;

// resharper disable inconsistentnaming
public interface Itb_buku_dal:
    IInsertBulk<tb_buku_dto>,
    IDelete<IStokLayerKey>,
    IListData<tb_buku_dto, IEnumerable<IStokLayerKey>>
{
    IEnumerable<tb_buku_dto> ListData<T>(T key, IEnumerable<string> listKodeDo) 
        where T: IBrgKey, ILayananKey;
}

public class tb_buku_dal : Itb_buku_dal
{
    private readonly DatabaseOptions _opt;
    public tb_buku_dal(IOptions<DatabaseOptions> opt)
    {
        _opt = opt.Value;
    }
    
    public void Insert(IEnumerable<tb_buku_dto> listModel)
    {
        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        using var bcp = new SqlBulkCopy(conn);
        conn.Open();
        bcp.AddMap("fs_kd_trs", "fs_kd_trs");
        bcp.AddMap("fs_kd_barang", "fs_kd_barang");
        bcp.AddMap("fs_kd_layanan", "fs_kd_layanan");

        bcp.AddMap("fs_kd_po", "fs_kd_po");
        bcp.AddMap("fs_kd_do", "fs_kd_do");
        bcp.AddMap("fd_tgl_ed", "fd_tgl_ed");
        bcp.AddMap("fs_no_batch", "fs_no_batch");
        
        bcp.AddMap("fn_stok_in", "fn_stok_in");
        bcp.AddMap("fn_stok_out", "fn_stok_out");
        bcp.AddMap("fn_hpp", "fn_hpp");

        bcp.AddMap("fs_kd_mutasi", "fs_kd_mutasi");
        bcp.AddMap("fd_tgl_mutasi", "fd_tgl_mutasi");
        bcp.AddMap("fs_jam_mutasi", "fs_jam_mutasi");
        bcp.AddMap("fd_tgl_jam_mutasi", "fd_tgl_jam_mutasi");
        
        bcp.AddMap("fs_kd_jenis_mutasi", "fs_kd_jenis_mutasi");
        bcp.AddMap("fs_kd_satuan", "fs_kd_satuan");
        var fetched = listModel.ToList();
        bcp.BatchSize = fetched.Count;
        bcp.DestinationTableName = "tb_buku";
        bcp.WriteToServer(fetched.AsDataTable());
    }
    
    public void Delete(IStokLayerKey stokLayerKey)
    {
        const string sql = """
            DELETE tb_buku
            FROM
                tb_buku aa
                INNER JOIN FARIN_StokLayerMap bb ON aa.fs_kd_trs = bb.StokBukuId 
            WHERE
                bb.StokLayerId = @StokLayerId
            """;
        var dp = new DynamicParameters();
        dp.AddParam("@StokLayerId", stokLayerKey.StokLayerId, SqlDbType.VarChar);
        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);    
    }
    
    public IEnumerable<tb_buku_dto> ListData(IEnumerable<IStokLayerKey> listStokLayerKey)
    {
        const string sql = """
            SELECT 
                aa.fs_kd_trs, aa.fs_kd_barang, aa.fs_kd_layanan,
                aa.fs_kd_po, aa.fs_kd_do, aa.fd_tgl_ed, aa.fs_no_batch, 
                aa.fn_stok_in, aa.fn_stok_out, aa.fn_hpp, 
                aa.fs_kd_mutasi, aa.fd_tgl_mutasi, aa.fs_jam_mutasi, aa.fd_tgl_jam_mutasi, 
                aa.fs_kd_jenis_mutasi, aa.fs_kd_satuan,
                ISNULL(cc.fs_nm_barang, '') fs_nm_barang, 
                ISNULL(dd.fs_nm_layanan, '') fs_nm_layanan,
                ISNULL(bb.StokLayerId, '') fs_stok_layer_id
            FROM 
                tb_buku aa
                LEFT JOIN FARIN_StokBukuMap bb ON aa.fs_kd_trs = bb.StokBukuId
                LEFT JOIN tb_barang cc ON aa.fs_kd_barang = cc.BrgId
                LEFT JOIN tb_layanan dd ON aa.fs_kd_layanan = dd.fs_kd_layanan
            WHERE
                bb.StokLayerId IN @listStokLayerId
            """;
        var dp = new DynamicParameters();
        dp.Add("@listStokLayerId", listStokLayerKey);
        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        return conn.Read<tb_buku_dto>(sql, dp);
    }

    public IEnumerable<tb_buku_dto> ListData<T>(T key, IEnumerable<string> listKodeDo) 
        where T : IBrgKey, ILayananKey
    {
        const string sql = """
           SELECT 
               aa.fs_kd_trs, aa.fs_kd_barang, aa.fs_kd_layanan,
               aa.fs_kd_po, aa.fs_kd_do, aa.fd_tgl_ed, aa.fs_no_batch, 
               aa.fn_stok_in, aa.fn_stok_out, aa.fn_hpp, 
               aa.fs_kd_mutasi, aa.fd_tgl_mutasi, aa.fs_jam_mutasi, aa.fd_tgl_jam_mutasi, 
               aa.fs_kd_jenis_mutasi, aa.fs_kd_satuan,
               ISNULL(cc.fs_nm_barang, '') fs_nm_barang, 
               ISNULL(dd.fs_nm_layanan, '') fs_nm_layanan,
               ISNULL(bb.StokLayerId, '') fs_stok_layer_id
           FROM 
               tb_buku aa
               LEFT JOIN FARIN_StokBukuMap bb ON aa.fs_kd_trs = bb.StokBukuId
               LEFT JOIN tb_barang cc ON aa.fs_kd_barang = cc.fs_kd_barang
               LEFT JOIN ta_layanan dd ON aa.fs_kd_layanan = dd.fs_kd_layanan
           WHERE
               aa.fs_kd_barang = @fs_kd_barang
               AND aa.fs_kd_layanan = @fs_kd_layanan
               AND aa.fs_kd_do IN @fs_kd_do
           """;
        
        var dp = new DynamicParameters();
        dp.AddParam("@fs_kd_barang", key.BrgId, SqlDbType.VarChar);
        dp.AddParam("@fs_kd_layanan", key.LayananId, SqlDbType.VarChar);
        dp.Add("@fs_kd_do", listKodeDo);
        
        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        return conn.Read<tb_buku_dto>(sql, dp);
    }
}
