using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Extensions;
internal static class Vector2LargestExtension
{
    /// <summary>
    /// Returns the largest X and the largest Y of the two Vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns>Returns the largest X and the largest Y in a Vector2</returns>
    internal static Vector2 Largest(this Vector2 v1, Vector2 v2)
        => new(v1.X > v2.X ? v1.X : v2.X, v1.Y > v2.Y ? v1.Y : v2.Y);
}