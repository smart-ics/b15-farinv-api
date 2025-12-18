using Farinv.Domain.InventoryContext.StokFeature;
using Farinv.Infrastructure.Helpers;
using Farinv.Infrastructure.InventoryContext.StokFeature;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;

// resharper disable inconsistentnaming
namespace Farinv.Test.InventoryContext.StokFeature;

public class tb_buku_dalTest
{
    private readonly tb_buku_dal _sut = new(ConnStringHelper.GetTestEnv());
    private readonly StokBukuMapDal _stokBukuMapDal = new(ConnStringHelper.GetTestEnv());
    private static IEnumerable<tb_buku_dto> FakerList()
        => new List<tb_buku_dto>
        {
            new tb_buku_dto("A1", "B1", "C1", "D1", "E1", "F1", "G1", 1000, 0, 320,
                "H1", "I1", "J1", "K1", "L1", "M1", "N1", "O1", "P1"),
            new tb_buku_dto("A2", "B2", "C2", "D2", "E2", "F2", "G2", 1001, 0, 321,
                "H2", "I2", "J2", "K2", "L2", "M2", "N2", "O2", "P2")
        };

    private static IStokLayerKey FakerKey()
        => StokLayerModel.Key("A");

    [Fact]
    public void InsertTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(FakerList());
    }
    
    [Fact]
    public void DeleteTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Delete(FakerKey());
    }

    [Fact]
    public void ListDataTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(FakerList());
        _stokBukuMapDal.Insert(new List<StokBukuMapDto>
        {
            new StokBukuMapDto("A", "A1"),
            new StokBukuMapDto("A", "A2")
        });
        var actual = _sut.ListData([FakerKey()]);
        actual.Should().BeEquivalentTo(FakerList());
    }
}