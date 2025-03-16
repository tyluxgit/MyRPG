namespace Engine.EventArgs;

public class GameMessageEventArgs(string message) : System.EventArgs
{
    public string Message { get; private set; } = message;
}