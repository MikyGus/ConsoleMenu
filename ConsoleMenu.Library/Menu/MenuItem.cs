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

    public Vector2 Position { get; set; }
    public IMenuItem Parent { get; set; }
    public MenuItem(string title)
    {
        Parent = null;
        _contentRender = new BasicContentRender(title);
        _childrenManager = new ChildrenManager(this);
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
        _childrenManager.PositionOfFirstChild = new Vector2(Position.X + 2, Position.Y + areaNeeded.Y);
        _childrenManager.Render();
    }

    public IContentRender ContentRenderer => _contentRender;

    public IChildrenManager Children => _childrenManager;

    public bool PerformAction(ConsoleKeyInfo key)
    {
        bool actionUsed = false;
        if (Children.HaveChildren())
        {
            actionUsed = Children.GetSelectedChild().Item.PerformAction(key);
            if (actionUsed)
                return true;
        }
        if (actionUsed == false)
        {
            actionUsed = ActionToPerform.MoveSelection(key, this);
        }
        return actionUsed;
    }
}
