using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.RenderComposites;

namespace ConsoleMenu.Library.Menu;
internal interface IMenuItem
{
    public Vector2 Position { get; set; }
    void Render();
    Vector2 AreaNeeded();
    void AddRenderComposite(int priority, IRenderComposite render);
    void RemoveRenderComposite(IRenderComposite render);
    IEnumerable<RenderComposite> GetRenderComposites();
}
