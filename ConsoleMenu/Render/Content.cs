namespace ConsoleMenu.Render;
internal class Content : IContent
{
    private Action<IMenuItem> _action;
    private Func<IMenuItem, Vector2> _areaNeeded;

    public bool IsSelected { get; set; }
    public bool IsMarked { get; set; }
    public required string Title { get; set; }
    public required IMenuItem Owner { get; set; }
    internal bool IsCurrentlyVisible { get; private set; }


    public Content()
    {
        IContentRenderer contentRenderer = new DefaultContentRender();
        SetRenderer(contentRenderer.Render, contentRenderer.AreaNeeded);
        IsCurrentlyVisible = false;
    }

    public void SetRenderer(Action<IMenuItem> action, Func<IMenuItem, Vector2> areaNeeded)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _areaNeeded = areaNeeded ?? throw new ArgumentNullException(nameof(areaNeeded));
    }

    public Vector2 AreaNeeded() => _areaNeeded.Invoke(Owner);
    public void Render()
    {
        RenderContent();
        IsCurrentlyVisible = true;
    }

    public void EraseContent()
    {
        if (IsCurrentlyVisible)
        {
            EraseContent(Owner.Position);
        }
        IsCurrentlyVisible = false;
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
        string eraseString = new(' ', area.X);
        for (int y = 0; y < area.Y; y++)
        {
            Console.SetCursorPosition(position.X, position.Y + y);
            Console.Write(eraseString);
        }
    }
}