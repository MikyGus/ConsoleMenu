using ConsoleMenu.Components;
using ConsoleMenu.Managers;
using ConsoleMenu.Menu;

namespace ConsoleMenu;
public partial class MenuItem : IMenuItemChildren
{
    private readonly ChildrenManager _childrenManager;

    public Orientation OrientationOfChildren => _childrenManager.OrientationOfChildren;
    public bool IsChildrenVisible => _childrenManager.IsVisible;
    public IMenuItem this[int i] => _childrenManager.GetChild(i);
    public IMenuItem this[string s] => _childrenManager.GetChildren().FirstOrDefault(x => x.Title == s);
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
    public Vector2 PositionOffsetOfFirstChild => _childrenManager.PositionOffsetOfFirstChild;
    public int PositionOffsetToNextChild => _childrenManager.PositionOffsetToNextChild;
}