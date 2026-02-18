using Farinv.Domain.SalesContext.AntrianFeature;
using System.Globalization;

namespace Farinv.Infrastructure.SalesContext.AntrianFeature;

public record AntrianDto(string AntrianId, DateTime AntrianDate,
    string StartTime, string EndTime, int ServicePoint, string SequenceTag, string AntrianDescription) : IAntrianKey
{
    public static AntrianDto FromModel(AntrianModel model) {
        var result = new AntrianDto(
            model.AntrianId,
            model.AntrianDate.ToDateTime(TimeOnly.MinValue),
            model.StartTime.ToString("HH:mm", CultureInfo.InvariantCulture),
            model.EndTime.ToString("HH:mm", CultureInfo.InvariantCulture),
            model.ServicePoint,
            model.SequenceTag,
            model.AntrianDescription);
        return result;
    }

    public AntrianModel ToModel(IEnumerable<AntrianEntryModel> listEntry)
    {
        var result = new AntrianModel(
            AntrianId,
            DateOnly.FromDateTime(AntrianDate),
            TimeOnly.Parse(StartTime),
            TimeOnly.Parse(EndTime),
            ServicePoint,
            AntrianDescription,
            listEntry);
        return result;
    }
}
