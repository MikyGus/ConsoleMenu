using ConsoleMenu.Program.Playground;
using System.Diagnostics;

//Trace.Listeners.Add(new TextWriterTraceListener(
//    File.CreateText(Path.Combine(Environment.GetFolderPath(
//        Environment.SpecialFolder.DesktopDirectory), "log.txt"))));
Trace.Listeners.Add(new TextWriterTraceListener(
    File.CreateText(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"))));
Trace.AutoFlush = true;

Console.WriteLine("******** Console Menu ***********");
Console.WriteLine("\n\n\n");
Debug.WriteLine("Application started");

//All.Run();
//SimpleMenu.Render_SimpleMenu_Horizontal();
//SimpleMenu.Render_SimpleMenu_Vertical();

//AddChildren.Run();
//RemoveChildren.Run();

//PositionOffsetOfFirstChild.Run();
//SetRenderer.Run();

//SetAction.Run();
//HideUnhide.Run();

//var textInputted = new TextInput(new Vector2(0, 4), 10);
//Console.SetCursorPosition(0, 10);
//Console.WriteLine(textInputted.Text);

Evolution.Run();
Debug.WriteLine("Application ended");
Console.WriteLine("\n\n\n\n\n\n\n\n");