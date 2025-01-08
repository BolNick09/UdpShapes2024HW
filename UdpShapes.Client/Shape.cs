global using System.Windows.Media;

namespace UdpShapes.Client;

public class Shape 
{
    public static Shape Square { get; } = new Shape (0, "Квадрат", DrawSquare);
    public static Shape Circle { get; } = new Shape (1, "Круг", DrawCircle);
    public static Shape Triangle { get; } = new Shape (2, "Треугольник", DrawTriangle);
    public static Shape Diamond { get; } = new Shape (3, "Ромб", DrawDiamond);
    public static Shape Ellipse { get; } = new Shape(4, "Эллипс", DrawCustomEllipse);
    public static Shape Rectangle { get; } = new Shape(5, "Прямоугольник", DrawCustomRectangle);

    public static IReadOnlyList<Shape> All { get; } = [
        Square, Circle, Triangle, Diamond, Ellipse, Rectangle
    ];
    public static IReadOnlyDictionary<int, Shape> IdToShape { get; }
        = All.ToSortedList (shape => shape.Id, shape => shape);


    public int Id { get; }
    public string Name { get; }
    public Action<DrawingContext, Brush, Point, int> Draw { get; }

    public Shape (int id, string name, Action<DrawingContext, Brush, Point, int> draw) {
        this.Id = id;
        this.Name = name;
        this.Draw = draw;
    }

    public override string ToString () => Name;

    private static void DrawSquare (DrawingContext d, Brush brush, Point pos, int size) =>
        d.DrawRectangle (brush, null, new Rect (pos.X, pos.Y, size * 50, size * 50));
    private static void DrawCircle (DrawingContext d, Brush brush, Point pos, int size) =>
        d.DrawEllipse (brush, null, new Point (pos.X + 25, pos.Y + 25), size * 25, size * 25);
    private static void DrawTriangle (DrawingContext d, Brush brush, Point pos, int size) =>
        d.DrawGeometry (brush, null, new PathGeometry ([
            new PathFigure (new Point ((pos.X + 25) * size, (pos.Y + 0) * size), [
                new LineSegment (new Point ((pos.X + 50) * size, (pos.Y + 50) * size), isStroked: false),
                new LineSegment (new Point (pos.X * size, (pos.Y + 50) * size), isStroked: false),
            ], closed: true)
        ]));
    private static void DrawDiamond (DrawingContext d, Brush brush, Point pos, int size) =>
        d.DrawGeometry (brush, null, new PathGeometry ([
            new PathFigure (new Point ((pos.X + 25) * size, (pos.Y + 0) * size), [
                new LineSegment (new Point ((pos.X + 50) * size, (pos.Y + 25) * size), isStroked: false),
                new LineSegment (new Point ((pos.X + 25) * size, (pos.Y + 50) * size), isStroked: false),
                new LineSegment (new Point ((pos.X + 0) * size, (pos.Y + 25) * size), isStroked: false),
            ], closed: true)
        ]));
    private static void DrawCustomEllipse(DrawingContext d, Brush brush, Point pos, int size) =>
        d.DrawEllipse(brush, null, new Point(pos.X + 25, pos.Y + 25), 50 * size, 25 * size);
    private static void DrawCustomRectangle(DrawingContext d, Brush brush, Point pos, int size) =>
        d.DrawRectangle(brush, null, new Rect(pos.X, pos.Y, 100 * size, 50 * size));
}
