using ConsoleMenu.Library.Extensions;
using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Managers;
public class ChildrenManager : IChildrenManager
{
    private List<IChildItem> _children = new();
    private Vector2 _positionOfFirstChild = Vector2.ZERO;
    private bool _isCurrentlyVisible = false;

    public IMenuItem Owner { get; init; }
    internal Vector2 PositionOfFirstChild { get => _positionOfFirstChild; set => _positionOfFirstChild = value + PositionOffsetOfFirstChild; }
    public Vector2 PositionOffsetOfFirstChild { get; set; } = Vector2.RIGHT;
    public int PositionOffsetToNextChild { get; set; } = 1;
    public bool IsVisible { get; set; } = true;
    public bool MayCollapse { get; set; } = true;
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
    public void Remove(int itemIndex) => _children.RemoveAt(itemIndex);

    public IEnumerable<IChildItem> GetChildren() => _children;
    public IChildItem GetChild(int index)
        => index < 0 || index >= _children.Count
            ? throw new ArgumentOutOfRangeException(nameof(index))
            : _children[index];


    private Vector2 OffsetToNextChild()
        => Owner.OrientationOfChildren == Orientation.Vertical
        ? new(0, PositionOffsetToNextChild)
        : new(PositionOffsetToNextChild, 0);

    public Vector2 AreaNeeded()
    {
        if ((_isCurrentlyVisible == false || IsVisible == false) && MayCollapse)
        {
            return Vector2.ZERO;
        }

        switch (Owner.OrientationOfChildren)
        {
            case Orientation.Vertical:
                if (_children.Any())
                {
                    return _children
                        .Select(c => c.Item.AreaNeeded().Largest(OffsetToNextChild()))
                        .Aggregate((a1, a2) => a1.MaxAdd_Vertical(a2)) + PositionOffsetOfFirstChild;
                }

                break;
            case Orientation.Horizontal:
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


    public void Render()
    {
        if (IsVisible == false)
        {
            return;
        }
        _isCurrentlyVisible = true;

        Vector2 position = PositionOfFirstChild;
        int index = 0;
        IEnumerable<IMenuItem> children = GetChildren().Select(m => m.Item);
        foreach (IMenuItem menuItem in children)
        {
            menuItem.Content.IsSelected = Owner is null
                ? Selection.CurrentIndex == index
                : Selection.CurrentIndex == index && Owner.Content.IsSelected;
            menuItem.Position = position;
            menuItem.Render();
            position = NextChildPosition(position, menuItem.AreaNeeded());
            index++;
        }
    }

    public void EraseContent()
    {
        if (_isCurrentlyVisible)
        {
            IEnumerable<IMenuItem> children = GetChildren().Select(m => m.Item);
            foreach (IMenuItem menuItem in children)
            {
                menuItem.EraseContent();
            }
            _isCurrentlyVisible = false;
        }
    }

    private Vector2 NextChildPosition(Vector2 position, Vector2 areaNeeded)
    {
        if (areaNeeded == Vector2.ZERO)
        {
            return position;
        }
        Vector2 offset = OffsetToNextChild();
        switch (Owner.OrientationOfChildren)
        {
            case Orientation.Vertical:
                position.Y += areaNeeded.Y > offset.Y ? areaNeeded.Y : offset.Y;
                break;
            case Orientation.Horizontal:
                position.X += areaNeeded.X > offset.X ? areaNeeded.X : offset.X;
                break;
        }
        return position;
    }
}