namespace ConsoleMenu.Library.Managers;

internal static class NodeHelpers
{
    public static (bool show, bool returnWithoutRenderNode) NodeVisibility(bool showNode, bool isCurrentlyVisible, bool isVisible)
    {
        showNode = showNode && isVisible;
        bool returnWithoutRenderNode = showNode == false && isCurrentlyVisible == false;
        return (showNode, returnWithoutRenderNode);
    }
}