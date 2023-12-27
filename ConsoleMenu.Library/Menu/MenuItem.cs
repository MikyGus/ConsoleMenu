using ConsoleMenu.Library.Extensions;
using ConsoleMenu.Library.Managers;
using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.PerformAction;
using ConsoleMenu.Library.Render.Contents;

namespace ConsoleMenu.Library.Menu;
public class MenuItem : IMenuItem
{
    private IContentRender _contentRender;
    private readonly IChildrenManager _childrenManager;
    private readonly Func<ConsoleKeyInfo, IMenuItem, bool> _onKeyPressed;

    public Vector2 Position { get; set; }

    public MenuItem(string title)
    {
        _contentRender = new BasicContentRender(title);
        _childrenManager = new ChildrenManager(this);
        _onKeyPressed = ActionToPerform.MoveSelection;
    }

    public Vector2 AreaNeeded()
    {
        var contentArea = _contentRender.AreaNeeded();
        var childrenArea = _childrenManager.AreaNeeded();
        return contentArea.MaxAdd_Vertical(childrenArea);
    }

    public void SetRender(IContentRender contentRender) => _contentRender = contentRender;

    public void Render()
    {
        _contentRender.Render(Position);
        var areaNeeded = _contentRender.AreaNeeded();
        _childrenManager.PositionOfFirstChild = new Vector2(Position.X, Position.Y + areaNeeded.Y);
        _childrenManager.Render();
    }

    public IContentRender ContentRenderer => _contentRender;

    public IChildrenManager Children => _childrenManager;

    public void PerformAction(ConsoleKeyInfo key)
    {
        if (_onKeyPressed?.Invoke(key, this) ?? true)
            if (Children.HaveChildren())
                Children.GetSelectedChild().Item.PerformAction(key);
    }
}
