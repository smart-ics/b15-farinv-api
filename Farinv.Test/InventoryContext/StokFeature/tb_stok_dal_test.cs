using Farinv.Domain.BrgContext.BrgFeature;
using Farinv.Domain.InventoryContext.StokFeature;
using Farinv.Infrastructure.Helpers;
using Farinv.Infrastructure.InventoryContext.StokFeature;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;

//  resharper disable inconsistentnaming
namespace Farinv.Test.InventoryContext.StokFeature;

public class tb_stok_dalTest
{
    private readonly tb_stok_dal _sut = new(ConnStringHelper.GetTestEnv());

    private static tb_stok_dto Faker()
        => new tb_stok_dto("A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P");

    private static IBrgKey FakerBrgKey()
        => BrgObatType.Default with { BrgId = "B" };

    private static ILayananKey FakerLayananKey()
        => LayananType.Default with { LayananId = "C" };

    [Fact]
    public void InsertTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(Faker());
    }
    
    [Fact]
    public void UpdateTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Update(Faker());
    }

    [Fact]
    public void DeleteTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Delete("A");
    }

    [Fact]
    public void GetDataTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(Faker());
        var actual = _sut.GetData("A");
        actual.Should().BeEquivalentTo(Faker());
    }
    
    [Fact]
    public void ListDataTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(Faker());
        var actual = _sut.ListData(FakerBrgKey(), FakerLayananKey());
        actual.Should().ContainEquivalentOf(Faker());
    }
}