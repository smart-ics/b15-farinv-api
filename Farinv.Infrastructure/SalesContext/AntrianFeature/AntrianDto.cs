//using Farinv.Domain.SalesContext.AntrianFeature;

//namespace Farinv.Infrastructure.SalesContext.AntrianFeature;

//public record AntrianDto(
//    string AntrianId, DateTime AntrianDate, string SequenceTag, 
//    int NoAntrian, int AntrianStatus, string PersonName,
//    DateTime TakenAt, DateTime AssignedAt, DateTime PreparedAt, 
//    DateTime DeliveredAt, DateTime CancelAt) : IAntrianKey
//{
//    public static AntrianDto FromModel(AntrianModel model)
//    {
//        var result = new AntrianDto(
//            model.AntrianId,
//            model.AntrianDate.ToDateTime(TimeOnly.MinValue),
//            model.SequenceTag,
//            model.NoAntrian,
//            (int)model.AntrianStatus,
//            model.PersonName,
//            model.TakenAt,
//            model.AssignedAt,
//            model.PreparedAt,
//            model.DeliveredAt,
//            model.CancelAt);
//        return result;
//    }

//    public AntrianModel ToModel()
//    {
//        var result = new AntrianModel(
//            AntrianId, 
//            DateOnly.FromDateTime(AntrianDate),
//            SequenceTag,
//            NoAntrian,
//            (AntrianStatusEnum)AntrianStatus,
//            PersonName,
//            TakenAt,
//            AssignedAt,
//            PreparedAt,
//            DeliveredAt,
//            CancelAt);
//        return result;
//    }
//}
