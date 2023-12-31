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
    [InlineData(2, 2, 1, 2)]
    [InlineData(1, 1, 1, 2)]
    [InlineData(1, 2, 1, 1)]
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
    [InlineData(false, 1, new int[] { 1 }, 2)]
    [InlineData(true, 2, new int[] { 1 }, 2)]
    [InlineData(true, 1, new int[] { }, 2)]
    public void Add_AddItemToCollection_ReturnsExpectedBool(bool expectedBool, int selectionMax, int[] preInputs, int testInput)
    {
        // Arrange
        _sut.SetSelectionMinMax(0, selectionMax);
        foreach (var inputItem in preInputs)
            _sut.Add(inputItem);
        // Act
        var result = _sut.Add(testInput);
        // Assert
        result.Should().Be(expectedBool);
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

    [Theory]
    [InlineData(0, 0, new int[] { 1 }, new int[] { 1 })]
    [InlineData(1, 1, new int[] { 1 }, new int[] { 1 })]
    [InlineData(1, 0, new int[] { 1 }, new int[] { 2 })]
    public void Remove_RemoveItemsFromCollection_ReturnsExpectedCount(int expectedCount, int selectionMin, int[] inputs, int[] removeItems)
    {
        // Arrange
        _sut.SetSelectionMinMax(selectionMin, 10);
        foreach (var input in inputs)
            _sut.Add(input);
        // Act
        foreach (var remove in removeItems)
            _sut.Remove(remove);
        // Assert
        _sut.SelectedItems().Should().HaveCount(expectedCount);
    }

    [Theory]
    [InlineData(false, 0, new int[] { 1 }, 2)]
    [InlineData(true, 0, new int[] { 1 }, 1)]
    [InlineData(false, 1, new int[] { 1 }, 2)]
    [InlineData(false, 1, new int[] { 1 }, 1)]
    public void Remove_RemoveItemFromCollection_ReturnsExpectedBool(bool expectedBool, int selectionMin, int[] preInputs, int testInput)
    {
        // Arrange
        _sut.SetSelectionMinMax(selectionMin, 10);
        foreach (var inputItem in preInputs)
            _sut.Add(inputItem);
        // Act
        var result = _sut.Remove(testInput);
        // Assert
        result.Should().Be(expectedBool);
    }

    [Theory]
    [InlineData(1, 0, new int[] { 1 }, new int[] { 1 })]
    [InlineData(0, 1, new int[] { 1 }, new int[] { 1 })]
    [InlineData(0, 0, new int[] { 1 }, new int[] { 2 })]
    public void Remove_RemoveItemsFromCollection_InvokesSelectionChangedEvent(int expectedInvokedCount, int selectionMin, int[] inputs, int[] removeItems)
    {
        // Arrange
        _sut.SetSelectionMinMax(selectionMin, 10);
        foreach (var input in inputs)
            _sut.Add(input);
        var eventInvokedCount = 0;
        _sut.SelectionChanged += ((x) => eventInvokedCount++);
        // Act
        foreach (var remove in removeItems)
            _sut.Remove(remove);
        // Assert
        eventInvokedCount.Should().Be(expectedInvokedCount);
    }

    [Theory]
    [InlineData(0, 5, new int[] { 1, 2, 3 }, new[] { 1, 4 }, new int[] { 2, 3, 4 })]
    [InlineData(3, 3, new int[] { 1, 2, 3 }, new[] { 1, 4 }, new int[] { 2, 3, 4 })]
    [InlineData(0, 5, new int[] { 1, 2, 3 }, new[] { 1, 2 }, new int[] { 1, 2, 3 })]
    [InlineData(0, 5, new int[] { 1, 2, 3 }, new[] { 0, 4 }, new int[] { 1, 2, 3 })]
    [InlineData(0, 5, new int[] { 1, 2, 3 }, new[] { 2, 0 }, new int[] { 0, 1, 3 })]
    public void MoveSelection_MoveSelectionToNewIndex_ReturnsExpectedCollection(
        int selectionMin,
        int selectionMax,
        int[] preInputs,
        int[] moveItems,
        int[] expectedCollection)
    {
        // Arrange
        if (moveItems.Length != 2)
            throw new ArgumentException("May only be 2 values! From and To!");
        _sut.SetSelectionMinMax(selectionMin, selectionMax);
        foreach (var input in preInputs)
            _sut.Add(input);
        // Act
        _sut.MoveSelection(moveItems[0], moveItems[1]);
        // Assert
        _sut.SelectedItems().Should().BeEquivalentTo(expectedCollection);
    }

    [Theory]
    [InlineData(0, 5, new int[] { 1, 2, 3 }, new[] { 1, 4 }, true)]
    [InlineData(3, 3, new int[] { 1, 2, 3 }, new[] { 1, 4 }, true)]
    [InlineData(0, 5, new int[] { 1, 2, 3 }, new[] { 1, 2 }, false)]
    [InlineData(0, 5, new int[] { 1, 2, 3 }, new[] { 0, 4 }, false)]
    [InlineData(0, 5, new int[] { 1, 2, 3 }, new[] { 2, 0 }, true)]
    public void MoveSelection_MoveSelectionToNewIndex_InvokesSelectionChangedEvent(
    int selectionMin,
    int selectionMax,
    int[] preInputs,
    int[] moveItems,
    bool expectedTheEventToBeInvoked)
    {
        // Arrange
        if (moveItems.Length != 2)
            throw new ArgumentException("May only be 2 values! From and To!");
        _sut.SetSelectionMinMax(selectionMin, selectionMax);
        foreach (var input in preInputs)
            _sut.Add(input);
        var eventHaveBeenInvoked = false;
        _sut.SelectionChanged += (x => eventHaveBeenInvoked = true);
        // Act
        var result = _sut.MoveSelection(moveItems[0], moveItems[1]);
        // Assert
        eventHaveBeenInvoked.Should().Be(expectedTheEventToBeInvoked);
        result.Should().Be(expectedTheEventToBeInvoked);
    }
}
