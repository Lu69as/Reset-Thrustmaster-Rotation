using TextCopy;
using WindowsInput;

Console.WriteLine("Hint: If you want 300 degrees, type 'f' for f1,");
Console.WriteLine("      And to get 900 degrees type 'd' for default"); Console.WriteLine();
Console.WriteLine("Enter the amount of wheel rotation you want"); Console.WriteLine(); 
var rotation = Console.ReadLine().ToLower();
decimal wantedRotation;
if (rotation == "d")
    wantedRotation = 900;
else if (rotation == "f")
    wantedRotation = 300;
else {
    bool parsable = decimal.TryParse(rotation, out wantedRotation);
    if (!parsable || wantedRotation > 1080 || wantedRotation < 40) {
        Console.WriteLine("ERROR: Not a valid wheel rotation!"); return;
    }
}


wantedRotation = (int)(Math.Ceiling(wantedRotation / 10) * 10);
var s = new InputSimulator();
s.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.LWIN);
Thread.Sleep(300); s.Keyboard.TextEntry("cpl"); Thread.Sleep(200);
s.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
Thread.Sleep(1200); s.Keyboard.TextEntry("Thrustmaster");
s.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB, WindowsInput.Native.VirtualKeyCode.RETURN);
Thread.Sleep(500); s.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB, WindowsInput.Native.VirtualKeyCode.TAB);
s.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.CONTROL); s.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.SHIFT);
s.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RIGHT, WindowsInput.Native.VirtualKeyCode.VK_C);
s.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.CONTROL);
s.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
s.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.SHIFT);

Thread.Sleep(600);

decimal currentRotation = decimal.Parse(ClipboardService.GetText());
var change = (currentRotation > wantedRotation 
    ? currentRotation - wantedRotation 
    : wantedRotation - currentRotation) / 10;

if (currentRotation > wantedRotation) {
    for (int i = 0; i < change; i++)
        s.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.LEFT);
    Console.WriteLine("Changed rotation from " + currentRotation 
        + "Deg down to " + wantedRotation + "Deg");
}
else if (currentRotation < wantedRotation) {
    for (int i = 0; i < change; i++)
        s.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RIGHT);
    Console.WriteLine("Changed rotation from " + currentRotation 
        + "Deg up to " + wantedRotation + "Deg");
}

for (int i = 0;i < 4; i++)
    s.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
s.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
Thread.Sleep(600);
for (int i = 0; i < 3; i++)
    s.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
s.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
