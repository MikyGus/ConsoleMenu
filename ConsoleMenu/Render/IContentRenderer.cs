namespace ConsoleMenu.Render;
public interface IContentRenderer
{
    Vector2 AreaNeeded(IMenuItem menuItem);
    void Render(IMenuItem menuItem);
}