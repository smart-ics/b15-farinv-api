using Farinv.Domain.SalesContext.AntrianFeature;

namespace Farinv.Infrastructure.SalesContext.AntrianFeature;

public record AntrianViewDto(string AntrianId, int NoAntrian, int AntrianStatus,
    string RegId, string PasienId, string PasienName,
    string ReffId, string ReffDesc, DateTime AntrianDate, string SquenceTag,
    string AntrianDescription, string StartTime, string EndTime)
{
    public AntrianView ToView()
    {
        var result = new AntrianView(
            AntrianId, NoAntrian, AntrianStatus, RegId, PasienId, PasienName, 
            ReffId, ReffDesc, AntrianDate, SquenceTag, AntrianDescription, 
            TimeOnly.Parse(StartTime), TimeOnly.Parse(EndTime)
            );
        return result;
    }
}
