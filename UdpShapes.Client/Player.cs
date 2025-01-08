global using System.Windows.Media;
global using System.Windows;
using System.Numerics;

namespace UdpShapes.Client;

public record Player 
{
    public int Id { get; }

    public string Name { get; }
    public NamedColor NamedColor { get; set; }
    public Shape Shape { get; }

    public int size { get; }

    public Point Pos { get; init; }

    // универсальный конструктор
    public Player (int id, string name, NamedColor namedColor, Shape shape, int size, Point pos)
    {
        this.Id = id;
        this.Name = name;
        this.NamedColor = namedColor;
        this.Shape = shape;
        this.size = size;
        this.Pos = pos;
    }

    // создать нового игрока из формы входа
    public Player (string name, NamedColor namedColor, Shape shape, int size) : this 
    (
        Random.Shared.Next (),
        name, namedColor, shape, size,
        new Point (Random.Shared.Next (400), Random.Shared.Next (400))
    ) { }

    // преобразование типа: EnteredMessage -> Player
    public static Player FromEntered (EnteredMessage message) => new Player 
    (
        message.Id, message.Name,
        NamedColor.IdToColor[message.ColorId],
        Shape.IdToShape[message.ShapeId],
        message.Size,
        message.Pos
    );

    // преобразование типа: ExistingMessage -> Player
    public static Player FromExisting (ExistingMessage message) => new Player 
    (
        message.Id, message.Name,
        NamedColor.IdToColor[message.ColorId],
        Shape.IdToShape[message.ShapeId],
        message.Size,
        message.Pos
    );

    // возвращает копию игрока, но с изменённой позицией
    public Player Move (Point newPos) =>
        this with { Pos = newPos };

    // преобразование типа: Player -> EnteredMessage
    public EnteredMessage ToEnteredMessage () => new EnteredMessage 
    {
        Id = Id,
        Name = Name,
        ColorId = NamedColor.Id,
        ShapeId = Shape.Id,
        Size = size,
        Pos = Pos
    };

    // преобразование типа: Player -> ExistingMessage
    public ExistingMessage ToExistingMessage () => new ExistingMessage 
    {
        Id = Id,
        Name = Name,
        ColorId = NamedColor.Id,
        ShapeId = Shape.Id,
        Size = size,
        Pos = Pos
    };

    // Нарисовать игрока
    public void Draw(DrawingContext d)
    {
        Typeface typeface = new Typeface("Arial");
        Brush textBrush = NamedColor.Brush;
        FormattedText formattedText = new FormattedText(Name, System.Globalization.CultureInfo.InvariantCulture, FlowDirection.LeftToRight, typeface, 30, textBrush);
        Point textPoint = Pos;
        textPoint.Y = Pos.Y - 50 * size;

        d.DrawText(formattedText, textPoint);
        Shape.Draw(d, NamedColor.Brush, Pos, size);
    }
}
