namespace ConsoleMenu.Abstracts;
public interface IRenderContent
{
    /// <summary>
    /// Render node and children
    /// </summary>
    void Render();
    /// <summary>
    /// Erases the content from the screen
    /// </summary>
    void EraseContent();
    Vector2 AreaNeeded();
}