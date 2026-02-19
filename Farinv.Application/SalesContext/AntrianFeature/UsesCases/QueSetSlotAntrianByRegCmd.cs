using Ardalis.GuardClauses;
using Farinv.Domain.SalesContext.AntrianFeature;
using MediatR;

namespace Farinv.Application.SalesContext.AntrianFeature.UsesCases;

public record QueSetSlotAntrianByRegCmd(int ServicePoint, string RegId, int NoAntrian)
    : IRequest<AntrianSetSlotByRegResponse>, IRegKey;

public record AntrianSetSlotByRegResponse(int NoAntrian);

public class AntrianCreateByRegHandler : IRequestHandler<QueSetSlotAntrianByRegCmd, AntrianSetSlotByRegResponse>
{
    private readonly IAntrianRepo _antrianRepo;
    //private readonly IGetRegService _getRegService;

    public AntrianCreateByRegHandler(IAntrianRepo antrianRepo)
    {
        _antrianRepo = antrianRepo;
        //_getRegService = getRegService;
    }

    public Task<AntrianSetSlotByRegResponse> Handle(QueSetSlotAntrianByRegCmd request, 
        CancellationToken cancellationToken)
    {
        // GUARD
        Guard.Against.NullOrWhiteSpace(request.RegId);
        Guard.Against.NegativeOrZero(request.NoAntrian);
        Guard.Against.NegativeOrZero(request.ServicePoint);

        // BUILD
        var reg = LoadReg(request);
        var antrian = LoadOrCreateAntrian(request);
        antrian.AddEntry(request.NoAntrian, reg.ToReff());

        throw new NotImplementedException();
    }

    private RegType LoadReg(QueSetSlotAntrianByRegCmd request)
    {
        throw new NotImplementedException();
    }

    private AntrianModel LoadOrCreateAntrian(QueSetSlotAntrianByRegCmd request)
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

    private static AntrianModel CreateAntrian(QueSetSlotAntrianByRegCmd request, DateOnly date)
    {
        var antrian = AntrianModel.Create(
            date,  request.ServicePoint, $"Antrian Apotek {request.ServicePoint}");

        return antrian;
    }
}