using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.FormInput;
public class TextInput : IFormInput
{
    private Vector2 _position;
    private readonly int _maxLengthOfText;

    public string Text { get; private set; }

    public TextInput(Vector2 position, int maxLengthOfText)
    {
        _position = position;
        _maxLengthOfText = maxLengthOfText;
    }

    public string GetUserInput()
    {
        Render();
        Console.SetCursorPosition(_position.X + 1, _position.Y + 1);
        ReadInput();
        EraseContent();
        return Text ?? throw new ArgumentNullException();
    }

    public void Render() => RenderBorder();

    public void EraseContent()
    {
        string blankRow = new(' ', _maxLengthOfText + 2);
        for (int i = 0, rows = AreaNeeded().Y; i < rows; i++)
        {
            Console.SetCursorPosition(_position.X, _position.Y + i);
            Console.Write(blankRow);
        }
    }

    public Vector2 AreaNeeded() => new(_maxLengthOfText + 2, 3);

    private void ReadInput()
    {
        char[] chars = new char[_maxLengthOfText];
        int charIndex = 0;
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Backspace)
            {
                int positionX = _position.X + charIndex < _position.X + 1 ? _position.X + 1 : _position.X + charIndex;
                Console.SetCursorPosition(positionX, _position.Y + 1);
                if (charIndex > 0)
                {
                    Console.Write(' ');
                    Console.SetCursorPosition(positionX, _position.Y + 1);
                    chars[charIndex - 1] = ' ';
                    charIndex--;
                }
            }
            else if (charIndex < _maxLengthOfText)
            {
                Console.Write(key.KeyChar);
                chars[charIndex] = key.KeyChar;
                charIndex++;
            }
        } while (key.Key is not ConsoleKey.Enter and not ConsoleKey.Escape);
        Text = new string(chars).Trim('\0', ' ', '\r');

    }

    private void RenderBorder()
    {
        string topBottomRow = new('X', _maxLengthOfText + 2);
        string middleRows = $"X{new string(' ', _maxLengthOfText)}X";
        Console.SetCursorPosition(_position.X, _position.Y);
        Console.Write(topBottomRow);
        Console.SetCursorPosition(_position.X, _position.Y + 1);
        Console.Write(middleRows);
        Console.SetCursorPosition(_position.X, _position.Y + 2);
        Console.Write(topBottomRow);
    }
}