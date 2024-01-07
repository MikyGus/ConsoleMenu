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
    public ContentOrientation ContentOrientation { get; set; } = ContentOrientation.Vetical;

    public ChildrenManager(IMenuItem owner) => Owner = owner;

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
    public IChildItem GetSelectedChild()
        => HaveChildren() == false ? throw new InvalidOperationException() : _children[CurrentSelection];

    public bool HaveChildren() => _children.Any();

    public int CurrentSelection { get; private set; } = 0;

    public bool DecrementSelection()
    {
        if (CurrentSelection > 0)
        {
            RenderSelection(_children[CurrentSelection]?.Item, false);
            CurrentSelection--;
            RenderSelection(_children[CurrentSelection]?.Item, true);
            return true;
        }
        return false;
    }

    public bool IncrementSelection()
    {
        if (CurrentSelection < _children.Count - 1)
        {
            RenderSelection(_children[CurrentSelection]?.Item, false);
            CurrentSelection++;
            RenderSelection(_children[CurrentSelection]?.Item, true);
            return true;
        }
        return false;
    }

    private void RenderSelection(IMenuItem menuItem, bool isSelected)
    {
        if (menuItem is null)
        {
            throw new ArgumentNullException(nameof(menuItem), $"{nameof(menuItem)} may not be null!");
        }

        menuItem.ContentRenderer.IsSelected = isSelected;
        menuItem.Render();
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


    public void Render()
    {
        Vector2 position = PositionOfFirstChild.Duplicate();
        int index = 0;
        foreach (IMenuItem menuItem in GetChildren().Select(m => m.Item))
        {
            menuItem.ContentRenderer.IsSelected = Owner is null
                ? CurrentSelection == index
                : CurrentSelection == index && Owner.ContentRenderer.IsSelected;
            menuItem.Position = position.Duplicate();
            menuItem.Render();
            position = NextChildPosition(position, menuItem.AreaNeeded());
            index++;
        }
    }

    private Vector2 NextChildPosition(Vector2 position, Vector2 areaNeeded)
    {
        Vector2 offset = OffsetToNextChild();
        if (ContentOrientation == ContentOrientation.Vetical)
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