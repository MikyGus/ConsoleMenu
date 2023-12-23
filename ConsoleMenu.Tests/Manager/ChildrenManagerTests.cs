using ConsoleMenu.Library.Managers;
using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;
using FakeItEasy;
using FluentAssertions;

namespace ConsoleMenu.Tests.Manager;
public class ChildrenManagerTests
{
    IChildrenManager _sut;
    public ChildrenManagerTests()
    {
        _sut = new ChildrenManager()
        {
            PositionOfFirstChild = Vector2.ZERO,
            PositionOffsetToNextChild = 1
        };
    }

    [Theory]
    [InlineData(0, 1, new int[] { 0, 0 })]
    [InlineData(1, 1, new int[] { 8, 1 })]
    [InlineData(2, 1, new int[] { 8, 2 })]
    [InlineData(2, 5, new int[] { 8, 10 })]
    [InlineData(3, 5, new int[] { 8, 15 })]
    public void AreaNeeded_WithChildrensAreaNeededVerticalOrientation_ReturnsTotalAreaNeeded(
    int menuItemsChildrenCount,
    int offsetToNextChild,
    int[] expectedTotalAreaNeeded)
    {
        // Arrange
        var expected = new Vector2(expectedTotalAreaNeeded[0], expectedTotalAreaNeeded[1]);
        var menuItems = A.CollectionOfFake<IMenuItem>(menuItemsChildrenCount);

        foreach (var child in menuItems)
        {
            A.CallTo(() => child.AreaNeeded()).Returns(new Vector2(8,1));
            _sut.Add(1, child);
        }
        _sut.PositionOffsetToNextChild = offsetToNextChild;
        // Act
        var result = _sut.AreaNeeded();
        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(0,1, new int[] { 0, 0 })]
    [InlineData(1,1, new int[] { 8, 1 })]
    [InlineData(1,10, new int[] { 10, 1 })]
    [InlineData(2,1, new int[] { 16, 1 })]
    [InlineData(2,10, new int[] { 20, 1 })]
    [InlineData(3,10, new int[] { 30, 1 })]
    public void AreaNeeded_WithChildrensAreaNeededHorizontalOrientation_ReturnsTotalAreaNeeded(
    int menuItemsChildrenCount,
    int offsetToNextChild,
    int[] expectedTotalAreaNeeded)
    {
        // Arrange
        var expected = new Vector2(expectedTotalAreaNeeded[0], expectedTotalAreaNeeded[1]);
        var menuItems = A.CollectionOfFake<IMenuItem>(menuItemsChildrenCount);

        foreach (var child in menuItems)
        {
            A.CallTo(() => child.AreaNeeded()).Returns(new Vector2(8, 1));
            _sut.Add(1, child);
        }
        _sut.PositionOffsetToNextChild = offsetToNextChild;
        _sut.ContentOrientation = ContentOrientation.Horizontal;
        // Act
        var result = _sut.AreaNeeded();
        // Assert
        result.Should().Be(expected);
    }
}
