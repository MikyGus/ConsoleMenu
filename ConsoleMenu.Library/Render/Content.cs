using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Render;
internal class Content : IContent
{
    private Action<IMenuItem> _action;
    private Func<IMenuItem, Vector2> _areaNeeded;
    private bool _isCurrentlyVisible;

    public bool IsSelected { get; set; }
    public bool IsMarked { get; set; }
    public required string Title { get; set; }
    public required IMenuItem Owner { get; set; }

    public Content()
    {
        IContentRenderer contentRenderer = new DefaultContentRender();
        SetRenderer(contentRenderer.Render, contentRenderer.AreaNeeded);
        _isCurrentlyVisible = false;
    }

    public void SetRenderer(Action<IMenuItem> action, Func<IMenuItem, Vector2> areaNeeded)
    {
        _action = action;
        _areaNeeded = areaNeeded;
    }

    public Vector2 AreaNeeded() => _areaNeeded?.Invoke(Owner);
    public void Render()
    {
        RenderContent();
        _isCurrentlyVisible = true;
    }

    public void EraseContent()
    {
        if (_isCurrentlyVisible)
        {
            EraseContent(Owner.Position.Duplicate());
        }
        _isCurrentlyVisible = false;
    }

    private void RenderContent()
    {
        ConsoleColor tempBgColor = Console.BackgroundColor;
        ConsoleColor tempFgColor = Console.ForegroundColor;

        _action?.Invoke(Owner);

        Console.BackgroundColor = tempBgColor;
        Console.ForegroundColor = tempFgColor;
    }

    private void EraseContent(Vector2 position)
    {
        Vector2 area = AreaNeeded();
        String eraseString = new(' ', area.X);
        for (int y = 0; y < area.Y; y++)
        {
            Console.SetCursorPosition(position.X, position.Y + y);
            Console.Write(eraseString);
        }
    }
}