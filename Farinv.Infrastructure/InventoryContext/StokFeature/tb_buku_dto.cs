// resharper disable inconsistentnaming
namespace Farinv.Infrastructure.InventoryContext.StokFeature;

public record tb_buku_dto(
    string fs_kd_trs,
    string fs_kd_barang,
    string fs_kd_layanan,
    string fs_kd_mutasi,
    string fd_tgl_jam_mutasi,
    string fd_tgl_mutasi,
    string fs_jam_mutasi,

    string fs_kd_po,
    string fs_kd_do,
    string fd_tgl_ed,
    string fs_no_batch,

    int fn_stok_in,
    int fn_stok_out,
    decimal fn_hpp,

    string fs_kd_jenis_mutasi,
    string fs_kd_satuan,
    string fs_nm_barang,
    string fs_nm_layanan);
