using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.RenderComposites;
internal interface IRenderComposite
{
    void Render(Vector2 position);
    Vector2 AreaNeeded();
}
