using Farinv.Domain.SalesContext.AntrianFeature;

namespace Farinv.Domain.SalesContext.PenjualanFeature;

public class PenjualanModel : IPenjualanKey
{
    #region CREATE
    public PenjualanModel(string penjualanId, DateTime penjualanDate,
        RegReff reg)
    {
        PenjualanId = penjualanId;
        PenjualanDate = penjualanDate;
        Reg = reg;
    }

    public static PenjualanModel Default => new("-", 
        new DateTime(3000, 1, 1), RegType.Default.ToReff());
    #endregion

    #region PROPERTIES
    public string PenjualanId { get; init; }
    public DateTime PenjualanDate { get; init; }

    public RegReff Reg { get; private set; }
    #endregion

    #region BEHAVIOUR
    public PenjualanReff ToReff() => new(PenjualanId, PenjualanDate, Reg);
    #endregion
}

public interface IPenjualanKey
{
    string PenjualanId { get; }
}

public record PenjualanReff(string PenjualanId, DateTime PenjualanDate, RegReff Reg);