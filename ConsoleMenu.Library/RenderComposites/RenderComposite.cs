using ConsoleMenu.Library.Menu;

namespace ConsoleMenu.Library.RenderComposites;
internal class RenderComposite
{
    public IMenuItem Parent { get; }
    public IRenderComposite Render {  get; }
    public int RenderPriority { get; set; }
    public RenderComposite(IMenuItem parent, IRenderComposite render, int renderPriority)
    {
        Parent = parent;
        Render = render;
        RenderPriority = renderPriority;
    }
}
