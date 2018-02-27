using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace ReturnPoint1
{
    public class NeoTrace /* Level *all* */
    {
        public static void Trace(params object[] args)
        {
            Neo.SmartContract.Framework.Services.Neo.Runtime.Notify(args);
        }
    }
    public static class NeoEntityModel /* Level 1 */
    {
        public enum EntityState
        {
            NULL,
            INIT,
            SET
        }

        public static BigInteger AsBigInteger(this EntityState state)
        {
            int istate = (int)state;
            BigInteger bis = istate;
            return bis;
        }

        public static EntityState BytesToEntityState(byte[] bsta)
        {
            int ista = (int)bsta.AsBigInteger();
            NeoEntityModel.EntityState sta = (NeoEntityModel.EntityState)ista;
            return sta;
        }
    }

    public class Point : NeoTrace /* Level 1 */
    {
        private BigInteger _x;
        private BigInteger _y;
        private NeoEntityModel.EntityState _state;

        // Accessors
        public static void SetX(Point p, BigInteger value) { p._x = value; p._state = NeoEntityModel.EntityState.SET; }
        public static BigInteger GetX(Point p) { return p._x; }
        public static void SetY(Point p, BigInteger value) { p._y = value; p._state = NeoEntityModel.EntityState.SET; }
        public static BigInteger GetY(Point p) { return p._y; }
        public static void Set(Point p, BigInteger xvalue, BigInteger yvalue) { p._x = xvalue; p._y = yvalue; p._state = NeoEntityModel.EntityState.SET; }

        // Factory methods
        private Point()
        {
        }

        private static Point _Initialize(Point p)
        {
            p._x = 0;
            p._y = 0;
            p._state = NeoEntityModel.EntityState.NULL; ;
            LogExt("_Initialize(p).p", p);
            return p;
        }

        public static Point New()
        {
            Point p = new Point();
            _Initialize(p);
            LogExt("New().p", p);
            return p;
        }

        public static Point New(int x, int y)
        {
            Point p = new Point();
            p._x = x;
            p._y = y;
            p._state = NeoEntityModel.EntityState.INIT;
            LogExt("New(x,y).p", p);
            return p;
        }

        public static Point Null()
        {
            Point p = new Point();
            _Initialize(p);
            LogExt("Null().p", p);
            return p;
        }

        // EntityState wrapper methods
        public static bool IsNull(Point p)
        {
            return (p._state == NeoEntityModel.EntityState.NULL);
        }

        // Log/trace methods
        public static void Log(string label, Point p)
        {
            NeoTrace.Trace(label, p._x, p._y);
        }

        public static void LogExt(string label, Point p)
        {
            NeoTrace.Trace(label, p._x, p._y, p._state);
        }
    }

    public class Contract1 : SmartContract
    {
        public static Point Main()
        {
            NeoTrace.Trace("================================================");
            NeoTrace.Trace("TestdApp - NEO Persistable Class (NPC) Framework");
            NeoTrace.Trace("TestdApp - Version 2.0 Reference Implementation");
            NeoTrace.Trace("================================================");

            Point p = Point.New();
            Point.Set(p, 5, 6);
            Point.LogExt("p", p);

            return p;
        }
    }
}
