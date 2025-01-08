namespace UdpShapes.Client;

public class LoginVM 
{
    // Без уведомления об изменении, потому что данные меняет только пользователь своими действиями.
    // Если программа не меняет данные, то так можно.
    public string Name { get; set; } = "";

    public IReadOnlyList<NamedColor> NamedColors => NamedColor.All;
    public NamedColor? NamedColor { get; set; } = null;
    public IReadOnlyList<Shape> Shapes => Shape.All;
    public int Size { get; set; } = 1;
    public Shape? Shape { get; set; } = null;
}
