using ConsoleMenu.Library.Managers;
using FluentAssertions;

namespace ConsoleMenu.Tests.Manager;
public class SelectionManagerTests
{
    private ISelectionManager _sut;
    public SelectionManagerTests()
    {
        _sut = new SelectionManager();
        _sut.SetSelectionMinMax(0, 1);
    }

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(1, 2, 1)]
    [InlineData(2, 2, 1,2)]
    [InlineData(1, 1, 1,2)]
    [InlineData(1, 2, 1,1)]
    public void Add_AddItemToCollection_ReturnsExpectedCount(int expectedCount, int selectionMax, params int[] inputs)
    {
        // Arrange
        _sut.SetSelectionMinMax(0, selectionMax);
        // Act
        foreach (var inputItem in inputs)
            _sut.Add(inputItem);
        // Assert
        _sut.SelectedItems().Should().HaveCount(expectedCount);
    }

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(1, 2, 1)]
    [InlineData(2, 2, 1, 2)]
    [InlineData(1, 1, 1, 2)]
    [InlineData(1, 2, 1, 1)]
    public void Add_AddItemToCollection_InvokesSelectionChangedEvent(int expectedCount, int selectionMax, params int[] inputs)
    {
        // Arrange
        var eventInvoked = 0;
        _sut.SetSelectionMinMax(0, selectionMax);
        _sut.SelectionChanged += ((x) => eventInvoked++);
        // Act
        foreach (var inputItem in inputs)
            _sut.Add(inputItem);
        // Assert
        eventInvoked.Should().Be(expectedCount);
    }


}
