using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.RenderComposites;
using FluentAssertions;

namespace ConsoleMenu.Tests.Menu;
public class MenuItemTests
{
    private IMenuItem _sut;
    public MenuItemTests()
    {
        _sut = new MenuItem();
    }

    [Fact]
    public void GetRenderComposites_GetComposites_ReturnsAIEnumerable()
    {
        // Arrange

        // Act
        var result = _sut.GetRenderComposites();
        // Assert
        result.Should().BeEmpty();
        result.Should().BeAssignableTo<IEnumerable<RenderComposite>>();
    }

}
