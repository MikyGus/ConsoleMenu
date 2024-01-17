using ConsoleMenu.Library.Managers;
using ConsoleMenu.Library.Menu;

namespace ConsoleMenu.Library.PerformAction;
public class ActionToPerform
{
    /// <summary>
    /// Desides what to do with the keypresses. 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="menuItem"></param>
    /// <returns>Returns a bool stating if the key were used for an action or not (true=used). Tried to move selection, but failed returns false.</returns>
    public static bool MoveSelection(ConsoleKeyInfo key, IMenuItem menuItem)
    {
        return (key.Key, menuItem.Children.Orientation) switch
        {
            (ConsoleKey.UpArrow, ContentOrientation.Vetical) => menuItem.Children.Selection.Decrement(),
            (ConsoleKey.DownArrow, ContentOrientation.Vetical) => menuItem.Children.Selection.Increment(),
            (ConsoleKey.RightArrow, ContentOrientation.Horizontal) => menuItem.Children.Selection.Increment(),
            (ConsoleKey.LeftArrow, ContentOrientation.Horizontal) => menuItem.Children.Selection.Decrement(),
            _ => menuItem.PerformAction(key),
        };
    }
}