using ConsoleMenu.Library.Extensions;
using ConsoleMenu.Library.Managers;
using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.PerformAction;
using ConsoleMenu.Library.Render;

namespace ConsoleMenu.Library.Menu;
public class MenuItem : IMenuItem
{
    private readonly string _title;
    private IContentRender _contentRender;
    private readonly ChildrenManager _childrenManager;
    private Func<IMenuItem, ConsoleKeyInfo, bool> _onAction;

    public Vector2 Position { get; set; }
    public IMenuItem Parent { get; set; }

    public MenuItem(string title)
    {
        _title = title;
        Parent = null;
        Position = Vector2.ZERO;
        _contentRender = new DefaultContentRender() { Content = _title };
        _childrenManager = new ChildrenManager(this);
    }

    public Vector2 AreaNeeded()
    {
        var contentArea = _contentRender.AreaNeeded();
        var childrenArea = _childrenManager.AreaNeeded();
        return contentArea.MaxAdd_Vertical(childrenArea);
    }

    public void SetRenderer<T>() where T : ContentRender, new()
        => _contentRender = new T() { Content = _title };

    public void Render()
    {
        _contentRender.Render(Position);
        var areaNeeded = _contentRender.AreaNeeded();
        _childrenManager.PositionOfFirstChild = new Vector2(Position.X, Position.Y + areaNeeded.Y);
        _childrenManager.Render();
    }

    public IContentRender ContentRenderer => _contentRender;

    public IChildrenManager Children => _childrenManager;

    public bool KeyPressed(ConsoleKeyInfo key)
    {
        if (Children.HaveChildren())
        {
            if (Children.GetSelectedChild().Item.KeyPressed(key))
                return true;
        }
        return ActionToPerform.MoveSelection(key, this);
    }

    public bool PerformAction(ConsoleKeyInfo key) => _onAction?.Invoke(this, key) ?? false;
    public void SetAction(Func<IMenuItem, ConsoleKeyInfo, bool> action) => _onAction = action;
}