using Farinv.Domain.SalesContext.AntrianFeature;

namespace Farinv.Infrastructure.SalesContext.AntrianFeature;

public record AntrianViewDto(string AntrianId, int NoAntrian, int AntrianStatus,
    string RegId, string PasienId, string PasienName,
    string ReffId, string ReffDesc, DateTime AntrianDate, string AntrianDescription)
{
    public AntrianView ToView()
    {
        var result = new AntrianView(
            AntrianId, NoAntrian, AntrianStatus, RegId, PasienId, PasienName, 
            ReffId, ReffDesc, AntrianDate, AntrianDescription
            );
        return result;
    }
}
