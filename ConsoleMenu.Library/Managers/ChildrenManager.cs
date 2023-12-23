using ConsoleMenu.Library.Extensions;
using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Managers;
public class ChildrenManager : IChildrenManager
{
    private readonly List<IChildItem> _children = new();
    private int _selectedIndex = 0;
    public IMenuItem Parent { get; init; }
    public Vector2 PositionOfFirstChild { get; set; } = Vector2.ZERO;
    public int PositionOffsetToNextChild { get; set; } = 1;
    public ContentOrientation ContentOrientation { get; set; } = ContentOrientation.Vetical;
    public void Add(int positionInList, IMenuItem item) => _children.Add(new ChildItem(Parent, item, positionInList));
    public void Remove(IMenuItem item)
    {
        var findItemToRemove = _children.Where(x => x.Item == item).FirstOrDefault() ?? throw new ArgumentException();
        _children.Remove(findItemToRemove);
    }
    public IEnumerable<IChildItem> GetChildren() => _children.OrderBy(c => c.Priority);

    public int CurrentSelection() => _selectedIndex;
    public void DecrementSelection()
    {
        if (_selectedIndex >= 0)
        {
            _selectedIndex--;
            _children[_selectedIndex + 1]?.Item.Render();
            _children[_selectedIndex]?.Item.Render();
        }
    }

    public void IncrementSelection()
    {
        if (_selectedIndex < _children.Count - 1)
        {
            _selectedIndex++;
            _children[_selectedIndex - 1]?.Item.Render();
            _children[_selectedIndex]?.Item.Render();
        }
    }

    private Vector2 OffsetToNextChild() 
        => ContentOrientation == ContentOrientation.Vetical 
        ? new(0, PositionOffsetToNextChild) 
        : new(PositionOffsetToNextChild, 0);

    public Vector2 AreaNeeded()
    {
        switch (ContentOrientation)
        {
            case ContentOrientation.Vetical:
                if (_children.Any())
                    return _children
                        .Select(c => c.Item.AreaNeeded().Largest(OffsetToNextChild()))
                        .Aggregate((a1, a2) => a1.MaxAdd_Vertical(a2));
                break;
            case ContentOrientation.Horizontal:
                if (_children.Any())
                    return _children
                        .Select(c => c.Item.AreaNeeded().Largest(OffsetToNextChild()))
                        .Aggregate((a1, a2) => a1.AddMax_Horizontal(a2));
                break;
        }
        return Vector2.ZERO;
    }


    public void Render()
    {
        var position = PositionOfFirstChild.Duplicate();
        foreach (var menuItem in GetChildren().Select(m => m.Item))
        {
            menuItem.Position = position;
            menuItem.Render();
            position = NextChildPosition(position,menuItem.AreaNeeded());
        }
    }

    private Vector2 NextChildPosition(Vector2 position, Vector2 areaNeeded)
    {
        var offset = OffsetToNextChild();
        if (ContentOrientation == ContentOrientation.Vetical)
            position.Y += areaNeeded.Y - 1 > offset.Y ? areaNeeded.Y - 1 : offset.Y;
        else
            position.X += areaNeeded.X - 1 > offset.X ? areaNeeded.X - 1 : offset.X;
        return position;
    }
}
