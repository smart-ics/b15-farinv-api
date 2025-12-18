using Farinv.Domain.InventoryContext.StokFeature;
using Farinv.Infrastructure.Helpers;
using Farinv.Infrastructure.InventoryContext.StokFeature;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;

namespace Farinv.Test.InventoryContext.StokFeature;

public class StokBukuMapDalTest
{
    private readonly StokBukuMapDal _sut = new(ConnStringHelper.GetTestEnv());

    private static IEnumerable<StokBukuMapDto> FakerList()
        => new List<StokBukuMapDto>
        {
            new StokBukuMapDto(
                StokLayerId: "A",
                StokBukuId: "B"
            ),
            new StokBukuMapDto(
                StokLayerId: "A",
                StokBukuId: "C"
            )
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
        var actual = _sut.ListData(FakerKey());
        actual.Should().BeEquivalentTo(FakerList());
    }
}