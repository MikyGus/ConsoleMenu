﻿using ConsoleMenu.Components;
using ConsoleMenu.Managers;
using FakeItEasy;
using FluentAssertions;

namespace ConsoleMenu.Tests.Manager;
public class ChildrenManagerTests
{
    ChildrenManager _sut;
    IMenuItem _menuItem;
    public ChildrenManagerTests()
    {
        _menuItem = A.Fake<IMenuItem>();
        _sut = new ChildrenManager(_menuItem)
        {
            PositionOfFirstChild = Vector2.ZERO,
            PositionOffsetToNextChild = 1
        };
        _sut.Selection = new SelectionManager(_sut);
    }

    [Theory]
    [InlineData(0, 1, new int[] { 0, 0 }, new int[] { 0, 0 })]
    [InlineData(1, 1, new int[] { 0, 0 }, new int[] { 8, 1 })]
    [InlineData(2, 1, new int[] { 0, 0 }, new int[] { 8, 2 })]
    [InlineData(2, 5, new int[] { 0, 0 }, new int[] { 8, 10 })]
    [InlineData(3, 5, new int[] { 0, 0 }, new int[] { 8, 15 })]

    [InlineData(0, 1, new int[] { 1, 0 }, new int[] { 0, 0 })]
    [InlineData(1, 1, new int[] { 1, 0 }, new int[] { 9, 1 })]
    [InlineData(2, 1, new int[] { 1, 0 }, new int[] { 9, 2 })]
    [InlineData(2, 5, new int[] { 1, 0 }, new int[] { 9, 10 })]
    [InlineData(3, 5, new int[] { 1, 0 }, new int[] { 9, 15 })]
    public void AreaNeeded_WithChildrensAreaNeededVerticalOrientation_ReturnsTotalAreaNeeded(
    int menuItemsChildrenCount,
    int offsetToNextChild,
    int[] firstChildOffset,
    int[] expectedTotalAreaNeeded)
    {
        // Arrange
        var firstChOffset = new Vector2(firstChildOffset[0], firstChildOffset[1]);
        var expected = new Vector2(expectedTotalAreaNeeded[0], expectedTotalAreaNeeded[1]);
        IList<IMenuItem> menuItems = A.CollectionOfFake<IMenuItem>(menuItemsChildrenCount);

        foreach (IMenuItem child in menuItems)
        {
            A.CallTo(() => child.AreaNeeded()).Returns(new Vector2(8, 1));
            _sut.Add(child);
        }
        _sut.PositionOffsetToNextChild = offsetToNextChild;
        _sut.PositionOffsetOfFirstChild = firstChOffset;
        _sut.Render();
        // Act
        Vector2 result = _sut.AreaNeeded();
        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(0, 1, new int[] { 0, 0 }, new int[] { 0, 0 })]
    [InlineData(1, 1, new int[] { 0, 0 }, new int[] { 8, 1 })]
    [InlineData(1, 10, new int[] { 0, 0 }, new int[] { 10, 1 })]
    [InlineData(2, 1, new int[] { 0, 0 }, new int[] { 16, 1 })]
    [InlineData(2, 10, new int[] { 0, 0 }, new int[] { 20, 1 })]
    [InlineData(3, 10, new int[] { 0, 0 }, new int[] { 30, 1 })]

    [InlineData(0, 1, new int[] { 1, 0 }, new int[] { 0, 0 })]
    [InlineData(1, 1, new int[] { 1, 0 }, new int[] { 9, 1 })]
    [InlineData(1, 10, new int[] { 1, 0 }, new int[] { 11, 1 })]
    [InlineData(2, 1, new int[] { 1, 0 }, new int[] { 17, 1 })]
    [InlineData(2, 10, new int[] { 1, 0 }, new int[] { 21, 1 })]
    [InlineData(3, 10, new int[] { 1, 0 }, new int[] { 31, 1 })]
    public void AreaNeeded_WithChildrensAreaNeededHorizontalOrientation_ReturnsTotalAreaNeeded(
    int menuItemsChildrenCount,
    int offsetToNextChild,
    int[] firstChildOffset,
    int[] expectedTotalAreaNeeded)
    {
        // Arrange
        var expected = new Vector2(expectedTotalAreaNeeded[0], expectedTotalAreaNeeded[1]);
        var firstChOffset = new Vector2(firstChildOffset[0], firstChildOffset[1]);
        IList<IMenuItem> menuItems = A.CollectionOfFake<IMenuItem>(menuItemsChildrenCount);

        foreach (IMenuItem child in menuItems)
        {
            A.CallTo(() => child.AreaNeeded()).Returns(new Vector2(8, 1));
            _sut.Add(child);
        }
        _sut.PositionOffsetToNextChild = offsetToNextChild;
        ((ChildrenManager)_sut).OrientationOfChildren = Orientation.Horizontal;
        _sut.PositionOffsetOfFirstChild = firstChOffset;
        _sut.Render();
        // Act
        Vector2 result = _sut.AreaNeeded();
        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 })]
    [InlineData(new int[] { 3, 2, 1 }, new int[] { 1, 2, 3 })]
    [InlineData(new int[] { 1, 4, 1 }, new int[] { 1, 1, 4 })]
    public void GetChildren_AddChildrenWithPositionInList_ReturnsChildrenInOrder(
        int[] positionInListInput,
        int[] expectedOrderOfChildren)
    {
        // Arrange
        IList<IMenuItem> menuItems = GenerateMenuItems().Take(positionInListInput.Length).ToList();
        for (int i = 0; i < menuItems.Count; i++)
        {
            menuItems[i].AddComponent(new ListPriorityComponent(positionInListInput[i]));
            _sut.Add(menuItems[i]);
        }
        // Act
        IEnumerable<IMenuItem> result = _sut.GetChildren();
        // Assert
        result.Select(x => x.GetComponents<ListPriorityComponent>().Select(x => x.Value).FirstOrDefault())
            .Should().BeEquivalentTo(expectedOrderOfChildren);
    }

    private IEnumerable<IMenuItem> GenerateMenuItems()
    {
        for (int i = 0; ; i++)
        {
            yield return new MenuItem($"Menu {i}");
        }
    }
}