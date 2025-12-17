using Farinv.Domain.InventoryContext.StokFeature;
using Farinv.Infrastructure.Helpers;
using Farinv.Infrastructure.InventoryContext.StokFeature;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;

namespace Farinv.Test.InventoryContext.StokFeature;

public class StokBukuDalTest
{
    private readonly StokBukuDal _sut = new(ConnStringHelper.GetTestEnv());
    private readonly DateTime _reffdate = new DateTime(2025, 12, 16, 14, 48, 50);
    private readonly DateTime _expDate = new DateTime(2026, 12, 31);
    private readonly DateTime _entryDate = new DateTime(2025, 12, 16, 14, 53, 00);

    private IEnumerable<StokBukuDto> FakerList()
        => new List<StokBukuDto>
        {
            new StokBukuDto(
                StokLayerId: "layer1",
                NoUrut: 1,
                fs_kd_trs: "buku1",
                fs_kd_barang: "brg1",
                fs_kd_layanan: "lay1",
                fs_kd_mutasi: "mut1",
                fd_tgl_jam_mutasi: "2025-12-16 14:48:50",
                fs_kd_po: "po1",
                fs_kd_do: "do1",
                fd_tgl_ed: "2026-12-31",
                fs_no_batch: "batch1",
                fn_stok_in: 10,
                fn_stok_out: 0,
                fn_hpp: 1000,
                fs_kd_jenis_mutasi: "masuk",
                fs_kd_satuan: "buah",
                fs_nm_barang: "Obat A",
                fs_nm_layanan: "Rawat Inap"
            ),
            new StokBukuDto(
                StokLayerId: "layer2",
                NoUrut: 2,
                fs_kd_trs: "buku2",
                fs_kd_barang: "brg1", 
                fs_kd_layanan: "lay1", 
                fs_kd_mutasi: "mut2",
                fd_tgl_jam_mutasi: "2025-12-16 14:53:00",
                fs_kd_po: "po2",
                fs_kd_do: "do2",
                fd_tgl_ed: "2027-06-30",
                fs_no_batch: "batch2",
                fn_stok_in: 0,
                fn_stok_out: 5,
                fn_hpp: 2000,
                fs_kd_jenis_mutasi: "keluar",
                fs_kd_satuan: "kotak",
                fs_nm_barang: "Obat B",
                fs_nm_layanan: "Rawat Jalan"
            )
        };

    private static IStokKey FakerKey()
        => StokModel.Key("brg1", "lay1");

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
        _sut.Insert(FakerList());
        _sut.Delete(FakerKey());
    }

    [Fact]
    public void ListDataTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(FakerList());

        var actual = _sut.ListData(FakerKey());

        actual.Should().HaveCount(2);
        actual.Select(x => x.fs_kd_barang).All(x => x == "brg1").Should().BeTrue();
        actual.Select(x => x.fs_kd_layanan).All(x => x == "lay1").Should().BeTrue();
    }
}