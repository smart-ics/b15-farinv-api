using Farinv.Domain.InventoryContext.StokFeature;
using Farinv.Domain.Shared.Helpers;
using Nuna.Lib.ValidationHelper;

// resharper disable inconsistentnaming
namespace Farinv.Infrastructure.InventoryContext.StokFeature;

public record StokBukuDto(
    string StokLayerId,
    int NoUrut,

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
            layer.StokLayerId,
            buku.NoUrut,
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
        return result;
    }

    public (StokBukuType buku, string stokLayerId) ToModel()
    {
        var reffDate = fd_tgl_jam_mutasi.ToDate();
        var trsReff = new TrsReffType(fs_kd_mutasi, reffDate);

        var buku = new StokBukuType(
            fs_kd_trs,
            NoUrut,
            trsReff,
            fs_kd_jenis_mutasi,
            fn_stok_in,
            fn_stok_out,
            reffDate,
            ModelStateEnum.Unchange
        );

        return (buku, StokLayerId);
    }

}
