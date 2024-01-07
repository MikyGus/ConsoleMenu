using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;
using FluentAssertions;

namespace ConsoleMenu.Tests.Menu;
public class MenuItemTests
{
    private IMenuItem _sut;
    public MenuItemTests()
    {
        _sut = new MenuItem("TEST");
    }

    [Fact]
    public void MenuItem_AddChild_ChildAddedWithWrapper()
    {
        // Arrange

        // Act
        _sut.Children.Add(1, new MenuItem("TEST2"));
        // Assert
        IEnumerable<IChildItem> result = _sut.Children.GetChildren();
        result.Should().HaveCount(1);
        result.Should().BeAssignableTo<IEnumerable<IChildItem>>();
    }

    [Fact]
    public void MenuItem_RemoveChild_ChildShouldBeRemoved()
    {
        // Arrange
        var menuItem = new MenuItem("TEST2");
        _sut.Children.Add(1, menuItem);
        // Act
        _sut.Children.Remove(menuItem);
        // Assert
        IEnumerable<IChildItem> result = _sut.Children.GetChildren();
        result.Should().BeEmpty();
        result.Should().BeAssignableTo<IEnumerable<IChildItem>>();
    }
}