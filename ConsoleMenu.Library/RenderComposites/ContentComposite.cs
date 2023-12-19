using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.RenderComposites;
internal class ContentComposite : IContentComposite
{
    private readonly string _content;

    public ContentComposite(string content)
    {
        _content = content;
    }
    public Vector2 AreaNeeded() => new(_content.Length, 1);
    public void Render(Vector2 position)
    {
        Console.SetCursorPosition(position.X, position.Y);
        Console.Write(_content);
    }
}
