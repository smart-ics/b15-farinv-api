using Farinv.Application.SalesContext.AntrianFeature;
using Farinv.Domain.SalesContext.AntrianFeature;
using Nuna.Lib.PatternHelper;
using Nuna.Lib.TransactionHelper;
using Nuna.Lib.ValidationHelper;

namespace Farinv.Infrastructure.SalesContext.AntrianFeature;

public class AntrianRepo : IAntrianRepo
{
    private readonly IAntrianDal _antrianDal;
    private readonly IAntrianEntryDal _antrianEntryDal;

    public AntrianRepo(IAntrianDal antrianDal,
        IAntrianEntryDal antrianEntryDal)
    {
        _antrianDal = antrianDal;
        _antrianEntryDal = antrianEntryDal;
    }

    public void SaveChanges(AntrianModel model)
    {
        using var trans = TransHelper.NewScope();

        LoadEntity(model)
            .Match(
                onSome: _ => _antrianDal.Update(AntrianDto.FromModel(model)),
                onNone: () => _antrianDal.Insert(AntrianDto.FromModel(model))
            );
        _antrianEntryDal.Delete(model);
        _antrianEntryDal.Insert(ToDtoList(model));

        trans.Complete();
    }

    public MayBe<AntrianModel> LoadEntity(IAntrianKey key)
    {
        var hdr = _antrianDal.GetData(key);
        var listDtl = _antrianEntryDal.ListData(key)?.ToList() ?? [];
        var listDtlType = listDtl.Select(x => x.ToModel());
        var model = hdr?.ToModel(listDtlType);
        return MayBe.From(model!);
    }

    public void DeleteEntity(IAntrianKey key)
    {
        _antrianDal.Delete(key);
        _antrianEntryDal.Delete(key);
    }

    public IEnumerable<AntrianHeaderView> ListData(DateOnly filter)
    {
        var periode = new Periode(filter.ToDateTime(TimeOnly.MinValue));
        var listDto = _antrianDal.ListData(periode)?.ToList() ?? [];
        var result = listDto.Select(x => new AntrianHeaderView(x.AntrianId, x.AntrianDescription,
            DateOnly.FromDateTime(x.AntrianDate), TimeOnly.Parse(x.StartTime), x.ServicePoint));
        return result;
    }

    public IEnumerable<AntrianView> ListData(DateTime dateTime)
    {
        var listDto = _antrianDal.ListData(dateTime)?.ToList() ?? [];
        var result = listDto.Select(x => x.ToView());
        return result;
    }

    #region HELPER
    private static IEnumerable<AntrianEntryDto> ToDtoList(AntrianModel model)
    {
        return model.ListEntry
            .Select(x => AntrianEntryDto.FromModel(model.AntrianId, x));
    }
    #endregion
}
