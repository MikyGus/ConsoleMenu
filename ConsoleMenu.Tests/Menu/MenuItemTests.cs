using ConsoleMenu.Library.Menu;
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
        _sut.AddChild("TEST2");
        // Assert
        IEnumerable<IMenuItem> result = _sut.GetChildren();
        result.Should().HaveCount(1);
        result.Should().BeAssignableTo<IEnumerable<IMenuItem>>();
    }

    [Fact]
    public void MenuItem_RemoveChildWithIndex_ChildShouldBeRemoved()
    {
        // Arrange
        _sut.AddChild("TEST2");
        // Act
        _sut.RemoveChild(0);
        // Assert
        IEnumerable<IMenuItem> result = _sut.GetChildren();
        result.Should().BeEmpty();
        result.Should().BeAssignableTo<IEnumerable<IMenuItem>>();
    }

    [Fact]
    public void MenuItem_RemoveChildWithReferenceToObject_ChildShouldBeRemoved()
    {
        // Arrange
        _sut.AddChild("TEST2");
        var itemToRemove = _sut[0];
        // Act
        _sut.RemoveChild(itemToRemove);
        // Assert
        IEnumerable<IMenuItem> result = _sut.GetChildren();
        result.Should().BeEmpty();
        result.Should().BeAssignableTo<IEnumerable<IMenuItem>>();
    }
}