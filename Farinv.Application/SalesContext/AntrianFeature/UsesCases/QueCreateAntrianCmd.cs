using Ardalis.GuardClauses;
using Farinv.Domain.SalesContext.AntrianFeature;
using Farinv.Domain.Shared.Helpers;
using MediatR;
using Nuna.Lib.ValidationHelper;

namespace Farinv.Application.SalesContext.AntrianFeature.UsesCases;

public record QueCreateAntrianCmd(int ServicePoint, string TglYmd, string JamMulai)
    : IRequest<AntrianCreateResponse>;

public record AntrianCreateResponse(int NoAntrian);

public class AntrianCreateHandler : IRequestHandler<QueCreateAntrianCmd, AntrianCreateResponse>
{
    private readonly IAntrianRepo _antrianRepo;

    public AntrianCreateHandler(IAntrianRepo antrianRepo)
    {
        _antrianRepo = antrianRepo;
    }

    public Task<AntrianCreateResponse> Handle(QueCreateAntrianCmd request, CancellationToken cancellationToken)
    {
        // GUARD
        Guard.Against.NegativeOrZero(request.ServicePoint);
        Guard.Against.InvalidDateFormat(request.TglYmd, nameof(request.TglYmd));
        Guard.Against.InvalidTimeFormat(request.JamMulai, nameof(request.JamMulai));

        // BUILD
        var antrian = LoadOrCreateAntrian(request);
        var noAntrian = GenNextNoAntrian(antrian);
        antrian.AddEntry(noAntrian);

        //  WRITE
        _antrianRepo.SaveChanges(antrian);
        return Task.FromResult(new AntrianCreateResponse(noAntrian));
    }

    #region PRIVATE-HELPERS
    private AntrianModel LoadOrCreateAntrian(QueCreateAntrianCmd request)
    {
        var date = request.TglYmd.ToDate(DateFormatEnum.YMD);
        var listAntrian = _antrianRepo.ListData(DateOnly.FromDateTime(date)) ?? [];

        var time = TimeOnly.Parse(request.JamMulai);
        var antrianView = listAntrian
            .FirstOrDefault(x => x.ServicePoint == request.ServicePoint && x.StartTime == time);
        if (antrianView is not null)
        {
            var key = AntrianModel.Key(antrianView.AntrianId);
            var antrianMaybe = _antrianRepo.LoadEntity(antrianView);
            return antrianMaybe.Value;
        }

        var antrian = CreateAntrian(request);
        return antrian;
    }

    private static AntrianModel CreateAntrian(QueCreateAntrianCmd request)
    {
        var date = request.TglYmd.ToDate(DateFormatEnum.YMD);
        var antrian = AntrianModel.Create(
            DateOnly.FromDateTime(date), TimeOnly.Parse(request.JamMulai), 
            TimeOnly.MaxValue, request.ServicePoint, $"Antrian Apotek {request.ServicePoint}");

        return antrian;
    }

    private static int GenNextNoAntrian(AntrianModel antrian)
    {
        var baseNumber = antrian.ServicePoint * 1000;
        if (!antrian.ListEntry.Any())
            return baseNumber + 1;

        return antrian.ListEntry.Max(x => x.NoAntrian) + 1;
    }
    #endregion
}