using Farinv.Domain.BrgContext.StandardFeature;
using Farinv.Infrastructure.BrgContext.StandardFeature;
using FluentAssertions;
using Moq;

namespace Farinv.Test.BrgContext.StandardFeature;

public class FornasRepoTests
{
    private readonly Mock<IFornasDal> _fornasDalMock;
    private readonly FornasRepo _repository;

    public FornasRepoTests()
    {
        _fornasDalMock = new Mock<IFornasDal>();
        _repository = new FornasRepo(_fornasDalMock.Object);
    }

    [Fact]
    public void UT1_GivenExistingEntity_WhenSaveChanges_ThenUpdateIsCalled()
    {
        // Arrange
        var existingModel = CreateTestModel();
        _fornasDalMock
            .Setup(x => x.GetData(It.IsAny<IFornasKey>()))
            .Returns(CreateTestDto());

        // Act
        _repository.SaveChanges(existingModel);

        // Assert
        _fornasDalMock.Verify(x => x.Update(It.IsAny<FornasDto>()), Times.Once);
        _fornasDalMock.Verify(x => x.Insert(It.IsAny<FornasDto>()), Times.Never);
    }

    [Fact]
    public void UT2_GivenNewEntity_WhenSaveChanges_ThenInsertIsCalled()
    {
        // Arrange
        var newModel = CreateTestModel();
        var key = CreateTestKey();
        _fornasDalMock
            .Setup(x => x.GetData(key))
            .Returns((FornasDto)null!);

        // Act
        _repository.SaveChanges(newModel);

        // Assert
        _fornasDalMock.Verify(x => x.Insert(It.IsAny<FornasDto>()), Times.Once);
        _fornasDalMock.Verify(x => x.Update(It.IsAny<FornasDto>()), Times.Never);
    }

    [Fact]
    public void UT3_GivenExistingEntity_WhenLoadEntity_ThenEntityIsReturned()
    {
        // Arrange
        var expectedDto = CreateTestDto();
        var key = CreateTestKey();
        _fornasDalMock
            .Setup(x => x.GetData(key))
            .Returns(expectedDto);

        // Act
        var result = _repository.LoadEntity(key);

        // Assert
        result.HasValue.Should().BeTrue();
        result.Match(
            onSome: model => model.Should().NotBeNull(),
            onNone: () => Assert.Fail("Expected Some but got None"));
    }

    [Fact]
    public void UT4_GivenNonExistingEntity_WhenLoadEntity_ThenNoneIsReturned()
    {
        // Arrange
        var key = CreateTestKey();
        _fornasDalMock
            .Setup(x => x.GetData(key))
            .Returns((FornasDto)null!);

        // Act
        var result = _repository.LoadEntity(key);

        // Assert
        result.HasValue.Should().BeFalse();
        result.Match(
            onSome: model => Assert.Fail("Expected None but got Some"),
            onNone: () => { });
    }

    [Fact]
    public void UT5_GivenKey_WhenDeleteEntity_ThenDeleteIsCalled()
    {
        // Arrange
        var key = CreateTestKey();

        // Act
        _repository.DeleteEntity(key);

        // Assert
        _fornasDalMock.Verify(x => x.Delete(key), Times.Once);
    }

    [Fact]
    public void UT6_GivenEmptyList_WhenListData_ThenEmptyListIsReturned()
    {
        // Arrange
        _fornasDalMock
            .Setup(x => x.ListData())
            .Returns((IEnumerable<FornasDto>)null!);

        // Act
        var result = _repository.ListData();

        // Assert
        var fornasTypes = result.ToList();
        fornasTypes.Should().NotBeNull();
        fornasTypes.Should().BeEmpty();
    }

    [Fact]
    public void UT7_GivenListWithItems_WhenListData_ThenListWithModelsIsReturned()
    {
        // Arrange
        var dtos = new List<FornasDto> { CreateTestDto(), CreateTestDto() };
        _fornasDalMock
            .Setup(x => x.ListData())
            .Returns(dtos);

        // Act
        var result = _repository.ListData();

        // Assert
        var fornasTypes = result.ToList();
        fornasTypes.Should().NotBeNull();
        fornasTypes.Count.Should().Be(2);
    }

    private static FornasType CreateTestModel()
        => FornasType.Default;

    private static FornasDto CreateTestDto()
        => FornasDto.FromModel(FornasType.Default);

    private static IFornasKey CreateTestKey()
        => FornasType.Key("F0001");
}
