using ConsoleMenu.Abstracts;

namespace ConsoleMenu.Render;
public interface IContent : IRenderContent
{
    bool IsSelected { get; set; }
    bool IsMarked { get; set; }
    string Title { get; set; }
    IMenuItem Owner { get; set; }
    bool IsCurrentlyVisible { get; }

    void SetRenderer(Action<IMenuItem> action, Func<IMenuItem, Vector2> areaNeeded);
}