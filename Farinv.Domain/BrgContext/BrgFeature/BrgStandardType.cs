using Ardalis.GuardClauses;
using Farinv.Domain.BrgContext.StandardFeature;

namespace Farinv.Domain.BrgContext.BrgFeature;

public record BrgStandardType
{
    #region CREATION
    public BrgStandardType(
        string brgId,
        KfaReff kfa,
        FornasReff fornas,
        GolTerapiType golTerapiType,
        KelasTerapiType kelasTerapiType,
        OriginalType originalType,
        IEnumerable<BrgStandardFormulariumType> listFormularium)
    {
        BrgId = brgId;
        Kfa = kfa;
        Fornas = fornas;
        GolTerapiType = golTerapiType;
        KelasTerapiType = kelasTerapiType;
        OriginalType = originalType;
        ListFormularium = listFormularium?.ToList() ?? [];
    }

    public static BrgStandardType Create(
        string brgId,
        KfaReff kfa,
        FornasReff fornas,
        GolTerapiType golTerapiType,
        KelasTerapiType kelasTerapiType,
        OriginalType originalType,
        IEnumerable<BrgStandardFormulariumType> listFormularium)
    {
        Guard.Against.NullOrWhiteSpace(brgId, nameof(brgId));
        Guard.Against.Null(kfa, nameof(kfa));
        Guard.Against.Null(fornas, nameof(fornas));
        Guard.Against.Null(golTerapiType, nameof(golTerapiType));
        Guard.Against.Null(kelasTerapiType, nameof(kelasTerapiType));
        Guard.Against.Null(originalType, nameof(originalType));

        return new BrgStandardType(brgId, kfa, fornas, golTerapiType, kelasTerapiType, originalType, listFormularium);
    }

    public static BrgStandardType Default => new(
        brgId: "-",
        kfa: KfaType.Default.ToReff(),
        fornas: FornasType.Default.ToReff(),
        golTerapiType: GolTerapiType.Default,
        kelasTerapiType: KelasTerapiType.Default,
        originalType: OriginalType.Default,
        listFormularium: []);
    #endregion

    #region PROPERTIES
    public string BrgId { get; init; }
    public KfaReff Kfa { get; init; }
    public FornasReff Fornas { get; init; }
    public GolTerapiType GolTerapiType { get; init; }
    public KelasTerapiType KelasTerapiType { get; init; }
    public OriginalType OriginalType { get; init; }
    public IEnumerable<BrgStandardFormulariumType> ListFormularium { get; init; }
    #endregion
}