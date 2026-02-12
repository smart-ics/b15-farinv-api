using Farinv.Domain.SalesContext.AntrianFeature;
using Farinv.Domain.Shared.Helpers.CommonValueObjects;

namespace Farinv.Infrastructure.SalesContext.AntrianFeature;

public record AntrianDto(
    string AntrianId, DateTime AntrianDate, string SequenceTag, 
    int NoAntrian, int AntrianStatus, string PersonName,
    string CrtUser, DateTime CrtDate, string UpdUser,
    DateTime UpdDate, string VodUser, DateTime VodDate) : IAntrianKey
{
    public static AntrianDto FromModel(AntrianModel model)
    {
        var result = new AntrianDto(
            model.AntrianId,
            model.AntrianDate.ToDateTime(TimeOnly.MinValue),
            model.SequenceTag,
            model.NoAntrian,
            (int)model.AntrianStatus,
            model.PersonName,
            model.AuditTrail.Created.UserId, model.AuditTrail.Created.Timestamp,
            model.AuditTrail.Modified.UserId, model.AuditTrail.Modified.Timestamp,
            model.AuditTrail.Voided.UserId, model.AuditTrail.Voided.Timestamp);
        return result;
    }

    public AntrianModel ToModel()
    {
        var crt = new AuditInfoType(CrtUser, CrtDate);
        var upd = new AuditInfoType(UpdUser, UpdDate);
        var vod = new AuditInfoType(VodUser, VodDate);
        var auditTrail = new AuditTrailType(crt, upd, vod);

        var result = new AntrianModel(
            AntrianId, 
            DateOnly.FromDateTime(AntrianDate),
            SequenceTag,
            NoAntrian,
            (AntrianStatusEnum)AntrianStatus,
            PersonName,
            auditTrail);
        return result;
    }
}
