using Dapper;
using Farinv.Domain.SalesContext.AntrianFeature;
using Farinv.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.ValidationHelper;
using System.Data;
using System.Data.SqlClient;

namespace Farinv.Infrastructure.SalesContext.AntrianFeature;

public interface IAntrialDal :
    IInsert<AntrianDto>,
    IUpdate<AntrianDto>,
    IDelete<IAntrianKey>,
    IGetData<AntrianDto, IAntrianKey>,
    IListData<AntrianDto, Periode>
{
}

public class AntrianDal : IAntrialDal
{
    private readonly DatabaseOptions _opt;

    public AntrianDal(IOptions<DatabaseOptions> opt)
    {
        _opt = opt.Value;
    }

    public void Insert(AntrianDto dto)
    {
        const string sql = """
           INSERT INTO FARIN_Antrian(
               AntrianId, AntrianDate, SequenceTag, 
               NoAntrian, AntrianStatus, PersonName,
               CrtUser, CrtDate, UpdUser, UpdDate, VodUser, VodDate)
           VALUES (
               @AntrianId, @AntrianDate, @SequenceTag, 
               @NoAntrian, @AntrianStatus, @PersonName,
               @CrtUser, @CrtDate, @UpdUser, @UpdDate, @VodUser, @VodDate)
           """;

        var dp = new DynamicParameters();
        dp.AddParam("@AntrianId", dto.AntrianId, SqlDbType.VarChar);
        dp.AddParam("@AntrianDate", dto.AntrianDate, SqlDbType.DateTime);
        dp.AddParam("@SequenceTag", dto.SequenceTag, SqlDbType.VarChar);
        dp.AddParam("@NoAntrian", dto.NoAntrian, SqlDbType.Int);
        dp.AddParam("@AntrianStatus", dto.AntrianStatus, SqlDbType.Int);
        dp.AddParam("@PersonName", dto.PersonName, SqlDbType.VarChar);

        dp.AddParam("@CrtUser", dto.CrtUser, SqlDbType.VarChar);
        dp.AddParam("@CrtDate", dto.CrtDate, SqlDbType.DateTime);
        dp.AddParam("@UpdUser", dto.UpdUser, SqlDbType.VarChar);
        dp.AddParam("@UpdDate", dto.UpdDate, SqlDbType.DateTime);
        dp.AddParam("@VodUser", dto.VodUser, SqlDbType.VarChar);
        dp.AddParam("@VodDate", dto.VodDate, SqlDbType.DateTime);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);
    }

    public void Update(AntrianDto dto)
    {
        const string sql = """
           UPDATE 
                FARIN_Antrian
           SET
                AntrianDate = @AntrianDate, 
                SequenceTag = @SequenceTag,
                NoAntrian = @NoAntrian, 
                AntrianStatus = @AntrianStatus,
                PersonName = @PersonName,
                CrtUser = @CrtUser,
                CrtDate = @CrtDate,
                UpdUser = @UpdUser,
                UpdDate = @UpdDate,
                VodUser = @VodUser,
                VodDate = @VodDate
           WHERE
                AntrianId = @AntrianId
           """;

        var dp = new DynamicParameters();
        dp.AddParam("@AntrianId", dto.AntrianId, SqlDbType.VarChar);
        dp.AddParam("@AntrianDate", dto.AntrianDate, SqlDbType.DateTime);
        dp.AddParam("@SequenceTag", dto.SequenceTag, SqlDbType.VarChar);
        dp.AddParam("@NoAntrian", dto.NoAntrian, SqlDbType.Int);
        dp.AddParam("@AntrianStatus", dto.AntrianStatus, SqlDbType.Int);
        dp.AddParam("@PersonName", dto.PersonName, SqlDbType.VarChar);

        dp.AddParam("@CrtUser", dto.CrtUser, SqlDbType.VarChar);
        dp.AddParam("@CrtDate", dto.CrtDate, SqlDbType.DateTime);
        dp.AddParam("@UpdUser", dto.UpdUser, SqlDbType.VarChar);
        dp.AddParam("@UpdDate", dto.UpdDate, SqlDbType.DateTime);
        dp.AddParam("@VodUser", dto.VodUser, SqlDbType.VarChar);
        dp.AddParam("@VodDate", dto.VodDate, SqlDbType.DateTime);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);
    }

    public void Delete(IAntrianKey key)
    {
        const string sql = """
           DELETE FROM 
                FARIN_Antrian
           WHERE
                AntrianId = @AntrianId
           """;

        var dp = new DynamicParameters();
        dp.AddParam("@AntrianId", key.AntrianId, SqlDbType.VarChar);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);
    }

    public AntrianDto GetData(IAntrianKey key)
    {
        const string sql = """
           SELECT
                AntrianId, AntrianDate, SequenceTag, 
                NoAntrian, AntrianStatus, PersonName,
                CrtUser, CrtDate, UpdUser, UpdDate, VodUser, VodDate
           FROM
                FARIN_Antrian
           WHERE
                AntrianId = @AntrianId
           """;

        var dp = new DynamicParameters();
        dp.AddParam("@AntrianId", key.AntrianId, SqlDbType.VarChar);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        return conn.ReadSingle<AntrianDto>(sql, dp);
    }

    public IEnumerable<AntrianDto> ListData(Periode filter)
    {
        const string sql = """
           SELECT
                AntrianId, AntrianDate, SequenceTag, 
                NoAntrian, AntrianStatus, PersonName,
                CrtUser, CrtDate, UpdUser, UpdDate, VodUser, VodDate
           FROM
                FARIN_Antrian
           WHERE
                AntrianDate BETWEEN @Tgl1 AND @Tgl2
           """;

        var dp = new DynamicParameters();
        dp.AddParam("@Tgl1", filter.Tgl1, SqlDbType.DateTime);
        dp.AddParam("@Tgl2", filter.Tgl2, SqlDbType.DateTime);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        return conn.Read<AntrianDto>(sql, dp);
    }
    
}
