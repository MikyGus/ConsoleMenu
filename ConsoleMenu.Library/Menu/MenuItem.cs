using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.Render.Contents;

namespace ConsoleMenu.Library.Menu;
public class MenuItem : IMenuItem
{
    private IContentRender _contentRender;
    private List<IChildItem> _children;
    public Vector2 Position { get; set; }

    public MenuItem(string title)
    {
        _contentRender = new BasicContentRender(title);
        _children = new List<IChildItem>();
    }

    public Vector2 AreaNeeded() => throw new NotImplementedException();
    public void SetRender(IContentRender contentRender) => _contentRender = contentRender;

    public void Render() => _contentRender.Render(Position);
    public IContentRender ContentRenderer() => _contentRender;
    public void AddChildItem(int priority, IMenuItem item) => _children.Add(new ChildItem(this, item, priority));

    public void RemoveChildItem(IMenuItem item)
    {
        var findItem = _children.Where(x => x.Item == item).FirstOrDefault();
        if (findItem is null)
            throw new ArgumentException();
        _children.Remove(findItem);
    }

    public IEnumerable<IChildItem> GetChildren() => _children;
}
