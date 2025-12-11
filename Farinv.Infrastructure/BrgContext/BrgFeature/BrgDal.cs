// using Dapper;
// using Farinv.Domain.BrgContext.BrgFeature;
// using Farinv.Infrastructure.Helpers;
// using Microsoft.Extensions.Options;
// using Nuna.Lib.DataAccessHelper;
// using System.Data;
// using System.Data.SqlClient;
//
// namespace Farinv.Infrastructure.BrgContext.BrgFeature;
//
// public interface IBrgDal :
//     IInsert<BrgDto>,
//     IUpdate<BrgDto>,
//     IDelete<IBrgKey>,
//     IGetData<BrgDto, IBrgKey>,
//     IListData<BrgDto>
// {
// }
//
// public class BrgDal : IBrgDal
// {
//     private readonly DatabaseOptions _opt;
//
//     public BrgDal(IOptions<DatabaseOptions> opt)
//     {
//         _opt = opt.Value;
//     }
//
//     public void Insert(BrgDto dto)
//     {
//         const string sql = """
//             INSERT INTO tb_barang (
//                 fs_kd_barang,
//                 fs_nm_barang,
//                 fb_aktif,
//                 fs_kd_golongan,
//                 fs_kd_gol_obat_dk,
//                 fs_kd_kelompok,
//                 fs_kd_sifat,
//                 fs_kd_bentuk,
//                 fs_kd_jenis_barang,
//                 fs_kd_pabrik,
//                 fs_kd_gol_terapi,
//                 fs_kd_kelas_terapi,
//                 fs_kd_original
//             ) VALUES (
//                 @fs_kd_barang,
//                 @fs_nm_barang,
//                 @fb_aktif,
//                 @fs_kd_golongan,
//                 @fs_kd_gol_obat_dk,
//                 @fs_kd_kelompok,
//                 @fs_kd_sifat,
//                 @fs_kd_bentuk,
//                 @fs_kd_jenis_barang,
//                 @fs_kd_pabrik,
//                 @fs_kd_gol_terapi,
//                 @fs_kd_kelas_terapi,
//                 @fs_kd_original
//             )
//             """;
//
//         var dp = new DynamicParameters();
//         dp.AddParam("@fs_kd_barang", dto.fs_kd_barang, SqlDbType.VarChar);
//         dp.AddParam("@fs_nm_barang", dto.fs_nm_barang, SqlDbType.VarChar);
//         dp.AddParam("@fb_aktif", dto.fb_aktif, SqlDbType.Bit);
//         dp.AddParam("@fs_kd_golongan", dto.fs_kd_golongan, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_gol_obat_dk", dto.fs_kd_gol_obat_dk, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_kelompok", dto.fs_kd_kelompok, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_sifat", dto.fs_kd_sifat, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_bentuk", dto.fs_kd_bentuk, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_jenis_barang", dto.fs_kd_jenis_barang, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_pabrik", dto.fs_kd_pabrik, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_gol_terapi", dto.fs_kd_gol_terapi, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_kelas_terapi", dto.fs_kd_kelas_terapi, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_original", dto.fs_kd_original, SqlDbType.VarChar);
//
//         using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
//         conn.Execute(sql, dp);
//     }
//
//     public void Update(BrgDto dto)
//     {
//         const string sql = """
//             UPDATE tb_barang
//             SET
//                 fs_nm_barang = @fs_nm_barang,
//                 fb_aktif = @fb_aktif,
//                 fs_kd_golongan = @fs_kd_golongan,
//                 fs_kd_gol_obat_dk = @fs_kd_gol_obat_dk,
//                 fs_kd_kelompok = @fs_kd_kelompok,
//                 fs_kd_sifat = @fs_kd_sifat,
//                 fs_kd_bentuk = @fs_kd_bentuk,
//                 fs_kd_jenis_barang = @fs_kd_jenis_barang,
//                 fs_kd_pabrik = @fs_kd_pabrik,
//                 fs_kd_gol_terapi = @fs_kd_gol_terapi,
//                 fs_kd_kelas_terapi = @fs_kd_kelas_terapi,
//                 fs_kd_original = @fs_kd_original
//             WHERE
//                 fs_kd_barang = @fs_kd_barang
//             """;
//
//         var dp = new DynamicParameters();
//         dp.AddParam("@fs_kd_barang", dto.fs_kd_barang, SqlDbType.VarChar);
//         dp.AddParam("@fs_nm_barang", dto.fs_nm_barang, SqlDbType.VarChar);
//         dp.AddParam("@fb_aktif", dto.fb_aktif, SqlDbType.Bit);
//         dp.AddParam("@fs_kd_golongan", dto.fs_kd_golongan, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_gol_obat_dk", dto.fs_kd_gol_obat_dk, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_kelompok", dto.fs_kd_kelompok, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_sifat", dto.fs_kd_sifat, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_bentuk", dto.fs_kd_bentuk, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_jenis_barang", dto.fs_kd_jenis_barang, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_pabrik", dto.fs_kd_pabrik, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_gol_terapi", dto.fs_kd_gol_terapi, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_kelas_terapi", dto.fs_kd_kelas_terapi, SqlDbType.VarChar);
//         dp.AddParam("@fs_kd_original", dto.fs_kd_original, SqlDbType.VarChar);
//
//         using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
//         conn.Execute(sql, dp);
//     }
//
//     public void Delete(IBrgKey key)
//     {
//         const string sql = """
//             DELETE FROM tb_barang
//             WHERE fs_kd_barang = @fs_kd_barang
//             """;
//
//         var dp = new DynamicParameters();
//         dp.AddParam("@fs_kd_barang", key.BrgId, SqlDbType.VarChar);
//
//         using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
//         conn.Execute(sql, dp);
//     }
//
//     public BrgDto GetData(IBrgKey key)
//     {
//         const string sql = """
//             SELECT
//                 fs_kd_barang,
//                 fs_nm_barang,
//                 fb_aktif,
//                 fs_kd_golongan,
//                 fs_kd_gol_obat_dk,
//                 fs_kd_kelompok,
//                 fs_kd_sifat,
//                 fs_kd_bentuk,
//                 fs_kd_jenis_barang,
//                 fs_kd_pabrik,
//                 fs_kd_gol_terapi,
//                 fs_kd_kelas_terapi,
//                 fs_kd_original
//             FROM tb_barang
//             WHERE fs_kd_barang = @fs_kd_barang
//             """;
//
//         var dp = new DynamicParameters();
//         dp.AddParam("@fs_kd_barang", key.BrgId, SqlDbType.VarChar);
//
//         using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
//         return conn.ReadSingle<BrgDto>(sql, dp);
//     }
//
//     public IEnumerable<BrgDto> ListData()
//     {
//         const string sql = """
//             SELECT
//                 fs_kd_barang,
//                 fs_nm_barang,
//                 fb_aktif,
//                 fs_kd_golongan,
//                 fs_kd_gol_obat_dk,
//                 fs_kd_kelompok,
//                 fs_kd_sifat,
//                 fs_kd_bentuk,
//                 fs_kd_jenis_barang,
//                 fs_kd_pabrik,
//                 fs_kd_gol_terapi,
//                 fs_kd_kelas_terapi,
//                 fs_kd_original
//             FROM tb_barang
//             """;
//
//         using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
//         return conn.Read<BrgDto>(sql);
//     }
// }