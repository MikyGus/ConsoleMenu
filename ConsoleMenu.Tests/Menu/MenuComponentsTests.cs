using ConsoleMenu.Library.Components;
using ConsoleMenu.Library.Menu;
using FluentAssertions;

namespace ConsoleMenu.Tests.Menu;
public class MenuComponentsTests
{
    private IMenuItem _sut;
    public MenuComponentsTests()
    {
        _sut = new MenuItem("TEST");
    }

    [Fact]
    public void MenuItem_AddComponent_ShouldHaveComponent()
    {
        // Arrange
        int value = 1;
        ValueComponent<int> component = new(value);
        // Act
        _sut.AddComponent(component);
        // Assert
        IEnumerable<IValueComponent<int>> result = _sut.GetComponents<IValueComponent<int>>();
        _ = result.Should().HaveCount(1)
            .And.AllBeAssignableTo<ValueComponent<int>>();
        _ = result.ToList()[0].Value.Should().Be(value);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(10)]
    public void MenuItem_AddComponents_ShouldHaveCorrectCount(int iterations)
    {
        // Arrange
        string value = "1";
        // Act
        for (int i = 0; i < iterations; i++)
        {
            ValueComponent<string> component = new(value);
            _sut.AddComponent(component);
        }
        // Assert
        IEnumerable<IValueComponent<string>> result = _sut.GetComponents<IValueComponent<string>>();
        _ = result.Should().HaveCount(iterations)
            .And.AllBeAssignableTo<ValueComponent<string>>();
        _ = result.ToList()[0].Value.Should().Be(value);
    }

    [Fact]
    public void MenuItem_RemoveComponent_ShouldSpecifiedComponentRemoved()
    {
        // Arrange
        int valueToRemove = 15;

        for (int i = 0, iterations = 10; i < iterations; i++)
        {
            ValueComponent<int> component = new(i + 10);
            _sut.AddComponent(component);
        }
        // Act
        ValueComponent<int>? componentToRemove = _sut.GetComponents<ValueComponent<int>>()
            .FirstOrDefault(x => x.Value == valueToRemove);
        _sut.RemoveComponent(componentToRemove);
        // Assert
        IEnumerable<IValueComponent<int>> result = _sut.GetComponents<IValueComponent<int>>();
        _ = result.Should().HaveCount(9)
            .And.AllBeAssignableTo<ValueComponent<int>>();

        IValueComponent<int>? noValue5 = _sut.GetComponents<IValueComponent<int>>()
            .FirstOrDefault(x => x.Value == valueToRemove);
        _ = noValue5.Should().BeNull();
    }
}