using ConsoleMenu.Library.Abstracts;
using ConsoleMenu.Library.Managers;
using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.Render;

namespace ConsoleMenu.Library.Menu;
public interface IMenuItem : IRenderContent
{
    IMenuItem Parent { get; set; }
    Vector2 Position { get; set; }
    /// <summary>
    /// Pushes the pressed key down all selected children.
    /// </summary>
    /// <param name="key"></param>
    /// <returns>Returns a bool stating if the key were used for an action or not (true=used)</returns>
    bool KeyPressed(ConsoleKeyInfo key);

    /// <summary>
    /// Perform acton specified in OnAction
    /// </summary>
    bool PerformAction(ConsoleKeyInfo key);

    /// <summary>
    /// Specify what to happen when user presses Enter-key with this MenuItem selected.
    /// </summary>
    event Func<IMenuItem, ConsoleKeyInfo, bool> OnAction;

    /// <summary>
    /// Sets the renderer of the content of the item. 
    /// </summary>
    /// <typeparam name="T">ContentRenderer</typeparam>
    void SetRenderer<T>() where T : ContentRender, new();

    IContentRender ContentRenderer { get; }
    IChildrenManager Children { get; }
}
