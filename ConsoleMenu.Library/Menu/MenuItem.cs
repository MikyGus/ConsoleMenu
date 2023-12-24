using ConsoleMenu.Library.Managers;
using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.Render.Contents;

namespace ConsoleMenu.Library.Menu;
public class MenuItem : IMenuItem
{
    private IContentRender _contentRender;
    private IChildrenManager _childrenManager;
    public Vector2 Position { get; set; }

    public MenuItem(string title)
    {
        _contentRender = new BasicContentRender(title);
        _childrenManager = new ChildrenManager(this);
    }

    public Vector2 AreaNeeded() => _contentRender.AreaNeeded();

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
}
