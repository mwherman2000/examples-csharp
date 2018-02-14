using Neo.SmartContract.Framework;

namespace StructExample
{
    public class StructExample2 : SmartContract
    {
        public static Point2 Add(Point2 a, Point2 b)
        {
            return new Point2
            {
                X = a.X + b.X,
                Y = a.Y + b.Y
            };
        }

        public static Point2 Main()
        {
            Point2 p1 = new Point2
            {
                X = 1,
                Y = 2
            };
            Point2 p2 = new Point2
            {
                X = 2,
                Y = 1
            };

            Point2[] array = new[]
            {
                p1, p2
            };

            Point2 p3 = Add(array[0], array[1]);

            Point2.Put("p1", p1);
            Point2.Put("p2", p2);
            Point2.Put("p3", p3);

            Point2 p1get = Point2.Get("p1");
            Point2 p2get = Point2.Get("p2");
            Point2 p3get = Point2.Get("p3");

            return p3get;
        }
    }
}
