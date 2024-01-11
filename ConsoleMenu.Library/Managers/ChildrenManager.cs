using ConsoleMenu.Library.Extensions;
using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Managers;
public class ChildrenManager : IChildrenManager
{
    private List<IChildItem> _children = new();
    private Vector2 _positionOfFirstChild = Vector2.ZERO;

    public IMenuItem Owner { get; init; }
    internal Vector2 PositionOfFirstChild { get => _positionOfFirstChild; set => _positionOfFirstChild = value + PositionOffsetOfFirstChild; }
    public Vector2 PositionOffsetOfFirstChild { get; set; } = Vector2.RIGHT;
    public int PositionOffsetToNextChild { get; set; } = 1;
    public ContentOrientation Orientation { get; set; } = ContentOrientation.Vetical;
    public bool IsVisible { get; set; } = true;
    public ISelectionManager Selection { get; init; }

    public ChildrenManager(IMenuItem owner)
    {
        Selection = new SelectionManager(this);
        Owner = owner;
    }

    public void Add(int positionInList, IMenuItem item)
    {
        item.Parent = Owner;
        _children.Add(new ChildItem(item, positionInList));
        _children = _children.OrderBy(c => c.Priority).ToList();
    }

    public void Remove(IMenuItem item)
    {
        IChildItem findItemToRemove = _children.Where(x => x.Item == item).FirstOrDefault() ?? throw new ArgumentException();
        _children.Remove(findItemToRemove);
    }
    public IEnumerable<IChildItem> GetChildren() => _children;
    public IChildItem GetChild(int index)
        => index < 0 || index >= _children.Count
            ? throw new ArgumentOutOfRangeException(nameof(index))
            : _children[index];

    public bool HaveChildren() => _children.Any();


    private Vector2 OffsetToNextChild()
        => Orientation == ContentOrientation.Vetical
        ? new(0, PositionOffsetToNextChild)
        : new(PositionOffsetToNextChild, 0);

    public Vector2 AreaNeeded()
    {
        switch (Orientation)
        {
            case ContentOrientation.Vetical:
                if (_children.Any())
                {
                    return _children
                        .Select(c => c.Item.AreaNeeded().Largest(OffsetToNextChild()))
                        .Aggregate((a1, a2) => a1.MaxAdd_Vertical(a2)) + PositionOffsetOfFirstChild;
                }

                break;
            case ContentOrientation.Horizontal:
                if (_children.Any())
                {
                    return _children
                        .Select(c => c.Item.AreaNeeded().Largest(OffsetToNextChild()))
                        .Aggregate((a1, a2) => a1.AddMax_Horizontal(a2)) + PositionOffsetOfFirstChild;
                }

                break;
        }
        return Vector2.ZERO;
    }


    public void Render(bool hideChildren = false)
    {
        hideChildren = hideChildren || IsVisible == false;

        Vector2 position = PositionOfFirstChild.Duplicate();
        int index = 0;
        foreach (IMenuItem menuItem in GetChildren().Select(m => m.Item))
        {
            menuItem.Content.IsSelected = Owner is null
                ? Selection.CurrentIndex == index
                : Selection.CurrentIndex == index && Owner.Content.IsSelected;
            menuItem.Position = position.Duplicate();
            menuItem.Render(hideChildren);
            position = NextChildPosition(position, menuItem.AreaNeeded());
            index++;
        }
    }

    private Vector2 NextChildPosition(Vector2 position, Vector2 areaNeeded)
    {
        Vector2 offset = OffsetToNextChild();
        if (Orientation == ContentOrientation.Vetical)
        {
            position.Y += areaNeeded.Y > offset.Y ? areaNeeded.Y : offset.Y;
        }
        else
        {
            position.X += areaNeeded.X > offset.X ? areaNeeded.X : offset.X;
        }

        return position;
    }
}