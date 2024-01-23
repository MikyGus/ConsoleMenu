using ConsoleMenu.Library.Abstracts;

namespace ConsoleMenu.Library.FormInput;
public interface IFormInput : IRenderContent
{
    string GetUserInput();
}