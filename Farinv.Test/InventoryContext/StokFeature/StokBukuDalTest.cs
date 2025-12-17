using Farinv.Domain.InventoryContext.StokFeature;
using Farinv.Infrastructure.Helpers;
using Farinv.Infrastructure.InventoryContext.StokFeature;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;

namespace Farinv.Test.InventoryContext.StokFeature;

public class StokBukuDalTest
{
    private readonly StokBukuDal _sut = new(ConnStringHelper.GetTestEnv());
    private readonly DateTime _reffdate = new DateTime(2025,12,16,14,48,50);
    private readonly DateTime _expDate = new DateTime(2026, 12, 31);
    private readonly DateTime _entryDate = new DateTime(2025, 12, 16, 14, 53,00);

    private IEnumerable<StokBukuDto> FakerList()
        => new List<StokBukuDto>
        {
            new StokBukuDto(
                StokBukuId: "A",
                StokLayerId: "B",
                NoUrut: 1,
                BrgId: "C",
                LayananId: "D",
                TrsReffId: "E",
                TrsReffDate: _reffdate,
                PurchaseId: "F",
                ReceiveId: "G",
                ExpDate: _expDate,
                BatchNo: "H",
                UseCase: "I",
                QtyIn: 1,
                QtyOut: 0,
                Hpp: 1000,
                EntryDate: _entryDate,
                BrgName: "J",
                LayananName: "K"
            ),
            new StokBukuDto(
                StokBukuId: "L",
                StokLayerId: "M",
                NoUrut: 2,
                BrgId: "C",
                LayananId: "D",
                TrsReffId: "N",
                TrsReffDate: _reffdate,
                PurchaseId: "O",
                ReceiveId: "P",
                ExpDate: _expDate,
                BatchNo: "Q",
                UseCase: "R",
                QtyIn: 0,
                QtyOut: 1,
                Hpp: 1000,
                EntryDate: _entryDate,
                BrgName: "S",
                LayananName: "T"
            )
        };

    private static IStokKey FakerKey()
        => StokModel.Key("C", "D");

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
        actual.Should().BeEquivalentTo(FakerList(),
            opt => opt.Excluding(x => x.BrgName)
                .Excluding(x => x.LayananName));
    }
}