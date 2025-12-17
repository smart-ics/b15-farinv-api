using Farinv.Domain.InventoryContext.StokFeature;

// resharper disable inconsistentnaming
namespace Farinv.Infrastructure.InventoryContext.StokFeature;

public record StokBukuDto(
    string fs_kd_trs, 
    string fs_kd_barang,
    string fs_kd_layanan,
    string fs_kd_mutasi,
    string fd_tgl_jam_mutasi,
    
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
    string fs_nm_layanan)
{
    public static StokBukuDto FromModel(StokModel header, StokLayerModel layer, StokBukuType buku)
    {
        var result = new StokBukuDto(
            buku.StokBukuId,
            header.BrgId,
            header.LayananId,
            buku.TrsReff.ReffId,
            $"{buku.TrsReff.ReffDate:yyyy-MM-dd HH:mm:ss}",
            layer.StokLot.PurchaseId,
            layer.StokLot.ReceiveId,
            $"{layer.StokLot.ExpDate:yyyy-MM-dd}",
            layer.StokLot.BatchNo,
            buku.QtyIn,
            buku.QtyOut,
            layer.Hpp,
            buku.UseCase,
            header.Satuan,
            header.Brg.BrgName,
            header.Layanan.LayananName);
    }
    
}