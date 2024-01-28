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
        switch (key.Key, menuItem.OrientationOfChildren)
        {
            case (ConsoleKey.UpArrow, Orientation.Vertical):
                return menuItem.Children.Selection.Decrement();
            case (ConsoleKey.DownArrow, Orientation.Vertical):
                return menuItem.Children.Selection.Increment();
            case (ConsoleKey.RightArrow, Orientation.Horizontal):
                return menuItem.Children.Selection.Increment();
            case (ConsoleKey.LeftArrow, Orientation.Horizontal):
                return menuItem.Children.Selection.Decrement();
        }
        menuItem.PerformAction(key);
        return false;
    }
}