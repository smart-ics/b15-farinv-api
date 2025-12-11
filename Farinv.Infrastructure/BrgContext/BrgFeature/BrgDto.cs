using Farinv.Domain.BrgContext.BrgFeature;

namespace Farinv.Infrastructure.BrgContext.BrgFeature;

public record BrgDto(
    string fs_kd_barang,
    string fs_nm_barang,
    bool fb_aktif,
    string fs_kd_golongan,
    string fs_kd_gol_obat_dk,
    string fs_kd_kelompok,
    string fs_kd_sifat,
    string fs_kd_bentuk,
    string fs_kd_jenis_barang,
    string fs_kd_pabrik,
    string fs_kd_gol_terapi,
    string fs_kd_kelas_terapi,
    string fs_kd_original
)
{
    public static BrgDto FromModel(BrgType model)
    {
        return new BrgDto(
            fs_kd_barang: model.BrgId,
            fs_nm_barang: model.BrgName,
            fb_aktif: model.IsAktif,
            // Klasifikasi
            fs_kd_golongan: model.Klasifikasi.Golongan.GolonganId,
            fs_kd_gol_obat_dk: model.Klasifikasi.GroupObatDk.GroupObatDkId,
            fs_kd_kelompok: model.Klasifikasi.Kelompok.KelompokId,
            fs_kd_sifat: model.Klasifikasi.Sifat.SifatId,
            fs_kd_bentuk: model.Klasifikasi.Bentuk.BentukId,
            fs_kd_jenis_barang: model.Klasifikasi.Jenis.JenisId,
            fs_kd_pabrik: model.Klasifikasi.Pabrik.PabrikId,
            // Standart
            fs_kd_gol_terapi: model.Standart.GolTerapiType.GolTerapiId,
            fs_kd_kelas_terapi: model.Standart.KelasTerapiType.KelasTerapiId,
            fs_kd_original: model.Standart.OriginalType.OriginalId
        );
    }

    // ToModel akan dibuat di Repo, karena perlu menggabungkan data dari beberapa tabel.
}