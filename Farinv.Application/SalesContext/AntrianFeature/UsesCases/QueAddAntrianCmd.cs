using Ardalis.GuardClauses;
using Farinv.Domain.SalesContext.AntrianFeature;
using MediatR;

namespace Farinv.Application.SalesContext.AntrianFeature.UsesCases;

public record QueAddAntrianCmd(int ServicePoint, int NoAntrian) : IRequest;

public class AddAntrianHandler : IRequestHandler<QueAddAntrianCmd>
{
    private readonly IAntrianRepo _antrianRepo;

    public AddAntrianHandler(IAntrianRepo antrianRepo)
    {
        _antrianRepo = antrianRepo;
    }

    public Task Handle(QueAddAntrianCmd request, CancellationToken cancellationToken)
    {
        // GUARD
        Guard.Against.NegativeOrZero(request.ServicePoint);
        Guard.Against.NegativeOrZero(request.NoAntrian);

        // BUILD
        var antrian = LoadOrCreateAntrian(request);
        antrian.AddEntry(request.NoAntrian);

        //  WRITE
        _antrianRepo.SaveChanges(antrian);
        return Task.CompletedTask;
    }

    #region PRIVATE-HELPERS
    private AntrianModel LoadOrCreateAntrian(QueAddAntrianCmd request)
    {
        var date = DateOnly.FromDateTime(DateTime.Now);
        var listAntrian = _antrianRepo.ListData(date) ?? [];

        var antrianView = listAntrian
            .FirstOrDefault(x => x.ServicePoint == request.ServicePoint && x.AntrianDate == date);
        if (antrianView is not null)
        {
            var key = AntrianModel.Key(antrianView.AntrianId);
            var antrianMaybe = _antrianRepo.LoadEntity(antrianView);
            return antrianMaybe.Value;
        }

        var antrian = CreateAntrian(request, date);
        return antrian;
    }

    private static AntrianModel CreateAntrian(QueAddAntrianCmd request, DateOnly date)
    {
        var antrian = AntrianModel.Create(
            date, request.ServicePoint, $"Antrian Apotek {request.ServicePoint}");

        return antrian;
    }
    #endregion
}