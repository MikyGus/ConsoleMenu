namespace ConsoleMenu.Abstracts;
public interface IVisibility
{
    bool IsVisible { get; set; }
    bool MayCollapse { get; set; }
}