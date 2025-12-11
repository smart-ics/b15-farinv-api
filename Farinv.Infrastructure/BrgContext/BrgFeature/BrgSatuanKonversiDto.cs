using Farinv.Domain.BrgContext.BrgFeature;

namespace Farinv.Infrastructure.BrgContext.BrgFeature;

public record BrgSatuanKonversiDto(
    string fs_kd_barang,
    string fs_kd_satuan,
    int fn_nilai
)
{
    public static BrgSatuanKonversiDto FromModel(string brgId, BrgSatuanKonversiType model)
    {
        return new BrgSatuanKonversiDto(
            fs_kd_barang: brgId,
            fs_kd_satuan: model.SatuanKonversi.SatuanId,
            fn_nilai: model.NilaiKonversi
        );
    }

    public BrgSatuanKonversiType ToModel()
    {
        var satuan = new SatuanReff(fs_kd_satuan, "-"); // Nama satuan bisa diambil dari repo
        var satuanKonversi = new SatuanReff(fs_kd_satuan, "-"); // Ini sama, perlu mapping dari repo
        // Kita asumsikan satuan dan satuan konversi adalah sama untuk sementara.
        // Jika berbeda, perlu dua kolom di tabel (fs_kd_satuan_dasar, fs_kd_satuan_konversi)
        return BrgSatuanKonversiType.Create(fs_kd_barang, satuan, fn_nilai, satuanKonversi);
    }
}