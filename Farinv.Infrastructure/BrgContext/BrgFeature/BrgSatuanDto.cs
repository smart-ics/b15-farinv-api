using Farinv.Domain.BrgContext.BrgFeature;

namespace Farinv.Infrastructure.BrgContext.BrgFeature;

public record BrgSatuanDto(
    string fs_kd_barang,
    string fs_kd_satuan,
    decimal fn_nilai
)
{
    public static BrgSatuanDto FromModel(string brgId, BrgSatuanType model)
    {
        // Karena BrgSatuanType hanya menyimpan satu satuan utama, kita ambil dari DosisSatuan
        return new BrgSatuanDto(
            fs_kd_barang: brgId,
            fs_kd_satuan: model.DosisSatuan.SatuanId,
            fn_nilai: model.Dosis
        );
    }

    public BrgSatuanType ToModel(IEnumerable<BrgSatuanKonversiType> konversi)
    {
        var satuan = new SatuanReff(fs_kd_satuan, "-"); // Nama satuan bisa diambil dari repo
        return BrgSatuanType.Create(fs_kd_barang, fn_nilai, satuan, konversi);
    }
}