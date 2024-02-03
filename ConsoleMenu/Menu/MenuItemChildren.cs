using ConsoleMenu.Components;
using ConsoleMenu.Managers;
using ConsoleMenu.Menu;

namespace ConsoleMenu;
public partial class MenuItem : IMenuItemChildren
{
    private readonly ChildrenManager _childrenManager;

    public Orientation OrientationOfChildren
    {
        get => _childrenManager.OrientationOfChildren;
        set => _childrenManager.OrientationOfChildren = value;
    }
    public bool IsChildrenVisible { get => _childrenManager.IsVisible; set => _childrenManager.IsVisible = value; }
    public IMenuItem this[int i] => _childrenManager.GetChild(i);
    public IMenuItem this[string s] => _childrenManager.GetChildren().FirstOrDefault(x => x.Content.Title == s);
    public void AddChild(string title, int positionInList = int.MaxValue)
    {
        IMenuItem menuItem = new MenuItem(title);
        AddChild(menuItem, positionInList);
    }

    public void AddChild<T>(T value, string title, int positionInList = int.MaxValue)
    {
        IMenuItem menuItem = new MenuItem(title);
        menuItem.AddComponent(new ValueComponent<T>(value));
        AddChild(menuItem, positionInList);
    }
    private void AddChild(IMenuItem menuItem, int positionInList)
    {
        if (positionInList != int.MaxValue)
        {
            menuItem.AddComponent(new ListPriorityComponent(positionInList));
        }
        _childrenManager.Add(menuItem);
    }

    public void RemoveChild(int i) => _childrenManager.Remove(i);
    public void RemoveChild(IMenuItem menuItem) => _childrenManager.Remove(menuItem);
    public IEnumerable<IMenuItem> GetChildren() => _childrenManager.GetChildren();
    public bool HaveChildren() => _childrenManager.GetChildren().Any();
    public Vector2 PositionOffsetOfFirstChild
    {
        get => _childrenManager.PositionOffsetOfFirstChild;
        set => _childrenManager.PositionOffsetOfFirstChild = value;
    }
    public int PositionOffsetToNextChild
    {
        get => _childrenManager.PositionOffsetToNextChild;
        set => _childrenManager.PositionOffsetToNextChild = value;
    }
}