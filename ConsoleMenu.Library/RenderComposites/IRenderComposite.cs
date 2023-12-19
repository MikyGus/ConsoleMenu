using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.RenderComposites;
internal interface IRenderComposite
{
    Vector2 ContentPositionOffset { get; }
    void Render(Vector2 position);
    Vector2 AreaNeeded();
}
