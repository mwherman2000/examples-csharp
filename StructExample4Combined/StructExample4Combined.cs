using System;
using System.Numerics;
using Neo.SmartContract.Framework;

namespace StructExample
{
    public static class NeoTrace
    {
        public static void Trace(params object[] args)
        {
            Neo.SmartContract.Framework.Services.Neo.Runtime.Notify(args);
        }
    }

    public static class NeoEntityModel
    {
        public enum EntityState
        {
            NULL,
            INIT,
            SET,
            PUTTED,
            GETTED,
            MISSING,
            TOMBSTONED
        }

        public static readonly byte[] NullHash = "".ToScriptHash();
    }

    public class Point
    {
        private int _x;
        private int _y;
        private NeoEntityModel.EntityState _state;
        private byte[] _extension;

        // class name and property names
        private const string _className = "Point";
        private const string _sX = "X";
        private const string _sY = "Y";
        private const string _sSTA = "_STA";
        private const string _sEXT = "_EXT";
        private static readonly byte[] _bX = Helper.AsByteArray(_sX);
        private static readonly byte[] _bY = Helper.AsByteArray(_sY);
        private static readonly byte[] _bSTA = Helper.AsByteArray(_sSTA);
        private static readonly byte[] _bEXT = Helper.AsByteArray(_sEXT);

        //public int X
        //{
        //    get { return _x; }
        //    set { _x = value; }
        //}
        //public int Y
        //{
        //    get { return _y; }
        //    set { _y = value; }
        //}

        //public int X { get => _x; set => _x = value; }
        //public int Y { get => _y; set => _y = value; }

        public static void SetX(Point p, int value) { p._x = value; p._state = NeoEntityModel.EntityState.SET; }
        public static void SetY(Point p, int value) { p._y = value; p._state = NeoEntityModel.EntityState.SET; }
        public static void SetXY(Point p, int xvalue, int yvalue) { p._x = xvalue; p._y = yvalue; p._state = NeoEntityModel.EntityState.SET; }
        public static int GetX(Point p) { return p._x; }
        public static int GetY(Point p) { return p._y; }
        public static void SetExtension(Point p, byte[] value) { p._extension = value; }
        public static byte[] GetExtension(Point p) { return p._extension; }

        // internal 
        private const string _classKeyTag = "/#" + _className + ".";
        private static readonly byte[] _bclassKeyTag = Helper.AsByteArray(_classKeyTag);

        private Point()
        {
        }

        private static Point _Initialize(Point p)
        {
            p._x = 0;
            p._y = 0;
            p._state = NeoEntityModel.EntityState.NULL;
            p._extension = NeoEntityModel.NullHash;
            Log("Initialize(p)", p);
            return p;
        }

        public static void Log(string label, Point p)
        {
            NeoTrace.Trace(label, p._x, p._y, p._state, p._extension);
        }

        public static Point New()
        {
            Point p = new Point();
            _Initialize(p);
            Log("New()", p);
            return p;
        }

        public static Point Null()
        {
            Point p = new Point();
            _Initialize(p);
            Log("Null()", p);
            return p;
        }

        public static Point Missing()
        {
            Point p = new Point();
            p._x = 0;
            p._y = 0;
            p._state = NeoEntityModel.EntityState.MISSING;
            p._extension = NeoEntityModel.NullHash;
            Log("Missing(x,y)", p);
            return p;
        }

        public static Point Tombstone()
        {
            Point p = new Point();
            p._x = 0;
            p._y = 0;
            p._state = NeoEntityModel.EntityState.TOMBSTONED;
            p._extension = NeoEntityModel.NullHash;
            Log("Buried(x,y)", p);
            return p;
        }

        public static Point New(int x, int y)
        {
            Point p = new Point();
            p._x = x;
            p._y = y;
            p._state = NeoEntityModel.EntityState.INIT;
            p._extension = NeoEntityModel.NullHash;
            Log("New(x,y)", p);
            return p;
        }

        public static bool IsNull(Point p)
        {
            return (p._state == NeoEntityModel.EntityState.NULL);
        }

        public static bool IsMissing(Point p)
        {
            return (p._state == NeoEntityModel.EntityState.MISSING);
        }

        public static bool IsExtended(Point p)
        {
            return (p._extension != NeoEntityModel.NullHash);
        }

        public static Point Bury(byte[] key)
        {
            if (key.Length == 0) return Null(); // TODO - create NeoEntityModel.EntityState.BADKEY?

            byte[] _bkeyTag = Helper.Concat(key, _bclassKeyTag);

            Point p;
            /*STA*/ byte[] bsta = Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(_bkeyTag, _bSTA));
            NeoTrace.Trace("Bury(byte[]).bs", bsta.Length, bsta);
            if (bsta.Length == 0)
            {
                p = Point.Missing();
            }
            else // not MISSING - bury it
            {
                p = Point.Tombstone(); // TODO - should Bury() preserve the exist field values or re-initialize them? Preserve is cheaper but not as private
                /*STA*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(_bkeyTag, _bSTA), (int)p._state);
                /*EXT*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(_bkeyTag, _bEXT), p._extension);
                /*FLD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(_bkeyTag, _bX), p._x);
                /*FLD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(_bkeyTag, _bY), p._y);
            }
            Log("Bury(byte[]).p", p);
            return p;
        }

        public static Point Bury(string key)
        {
            if (key.Length == 0) return Null(); // TODO - create NeoEntityModel.EntityState.BADKEY?

            string _skeyTag = key + _classKeyTag;

            Point p;
            /*STA*/ byte[] bsta = Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, _skeyTag + _sSTA);
            NeoTrace.Trace("Bury(byte[]).bs", bsta.Length, bsta);
            if (bsta.Length == 0)
            {
                p = Point.Missing();
            }
            else // not MISSING - bury it
            {
                p = Point.Tombstone(); // TODO - should Bury() preserve the exist field values or re-initialize them? Preserve is cheaper but not as private
                /*STA*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, _skeyTag + _sSTA, (int)p._state);
                /*EXT*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, _skeyTag + _sEXT, p._extension);
                /*FLD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, _skeyTag + _sX, p._x);
                /*FLD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, _skeyTag + _sY, p._y);
            }
            Log("Bury(string).p", p);
            return p;
        }

        public static bool Put(byte[] key, Point p)
        {
            if (key.Length == 0) return false;

            byte[] _bkeyTag = Helper.Concat(key, _bclassKeyTag);

            p._state = NeoEntityModel.EntityState.PUTTED;
            /*STA*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(_bkeyTag, _bSTA), (int)p._state);
            /*EXT*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(_bkeyTag, _bEXT), p._extension);
            /*FLD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(_bkeyTag, _bX), p._x);
            /*FLD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(_bkeyTag, _bY), p._y);
          return true;
        }

        public static bool Put(string key, Point p)
        {
            if (key.Length == 0) return false;

            string _skeyTag = key + _classKeyTag;

            p._state = NeoEntityModel.EntityState.PUTTED;
            /*STA*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, _skeyTag + _sSTA, (int)p._state);
            /*EXT*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, _skeyTag + _sEXT, p._extension);
            /*FLD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, _skeyTag + _sX, p._x);
            /*FLD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, _skeyTag + _sY, p._y);
            return true;
        }

        public static Point Get(byte[] key)
        {
            if (key.Length == 0) return Null();

            byte[] _bkeyTag = Helper.Concat(key, _bclassKeyTag);

            Point p;
            /*STA*/ byte[] bsta = Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(_bkeyTag, _bSTA));
            NeoTrace.Trace("Get(byte[]).bs", bsta.Length, bsta);
            if (bsta.Length == 0)
            {
                p = Point.Missing();
            }
            else // not MISSING
            {
                /*EXT*/ byte[] bext = Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(_bkeyTag, _bEXT));
                int ista = (int)bsta.AsBigInteger();
                NeoEntityModel.EntityState sta = (NeoEntityModel.EntityState)ista;
                if (sta == NeoEntityModel.EntityState.TOMBSTONED)
                {
                    p = Point.Tombstone();
                    p._extension = bext; // TODO: does a Tomestone bury all of its extensions?
                }
                else // not MISSING && not TOMBSTONED
                {
                    p = new Point();
                    /*FLD*/ int x = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(_bkeyTag, _bX)).AsBigInteger();
                    /*FLD*/ int y = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(_bkeyTag, _bY)).AsBigInteger();
                    p._x = x;
                    p._y = y;
                    p._state = sta;
                    p._state = NeoEntityModel.EntityState.GETTED; /* OVERRIDE */
                    p._extension = bext;
                }
            }
            Log("Get(byte[]).p", p);
            return p;
        }

        public static Point Get(string key)
        {
            if (key.Length == 0) return Null();

            string _skeyTag = key + _classKeyTag;

            Point p;
            /*STA*/ byte[] bsta = Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, _skeyTag + _sSTA);
            NeoTrace.Trace("Get(string).bs", bsta.Length, bsta);
            if (bsta.Length == 0)
            {
                p = Point.Missing();
            }
            else // not MISSING
            {
                /*EXT*/ byte[] bext = Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, _skeyTag + _sEXT);
                int ista = (int)bsta.AsBigInteger();
                NeoEntityModel.EntityState sta = (NeoEntityModel.EntityState)ista;
                if (sta == NeoEntityModel.EntityState.TOMBSTONED)
                {
                    p = Point.Tombstone();
                    p._extension = bext; // TODO: does a Tomestone bury all of its extensions?
                }
                else // not MISSING && not TOMBSTONED
                {
                    p = new Point();
                    /*FLD*/ int x = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, _skeyTag + _sX).AsBigInteger();
                    /*FLD*/ int y = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, _skeyTag + _sY).AsBigInteger();
                    p._x = x;
                    p._y = y;
                    p._state = sta;
                    p._state = NeoEntityModel.EntityState.GETTED; /* OVERRIDE */
                    p._extension = bext;
                }
            }
            Log("Get(string).p", p);
            return p;
        }
    }

    public class StructExample3Combined : SmartContract
    {
        private static Point Add(Point a, Point b)
        {
            Point p = Point.New();
            Point.SetXY(p, Point.GetX(a) + Point.GetX(b), Point.GetY(a) + Point.GetY(b));
            return p;
        }

        public static Point Main()
        {
            NeoTrace.Trace("NullHash", NeoEntityModel.NullHash);

            Point p0 = Point.New();
            Point.Log("p0", p0);
            Point.SetX(p0, 7);
            Point.SetY(p0, 8);
            Point.Log("p0", p0);
            Point.SetXY(p0, 9, 10);
            Point.Log("p0", p0);

            Point p1 = Point.New();
            Point.SetXY(p1, 2, 4);
            Point.Log("p1", p1);

            Point p2 = Point.New();
            Point.SetXY(p2, 15, 16);
            Point.Log("p2", p2);

            Point[] line1 = new[]
            {
                p1, p2
            };
            NeoTrace.Trace("line1", line1);

            Point p3 = Add(line1[0], line1[1]);
            Point.Log("p3", p3);

            Point.Put("p1", p1);
            Point.Put("p2", p2);
            Point.Put("p3", p3);

            Point p1get = Point.Get("p1");
            Point.Log("p1get", p1get);
            Point p2get = Point.Get("p2");
            Point.Log("p2get", p2get);
            Point p3get = Point.Get("p3");
            Point.Log("p3get", p3get);

            NeoTrace.Trace("Empty key test...");
            Point nullkeyp = Point.Get("");
            Point.Log("nullkey", nullkeyp);
            NeoTrace.Trace("nullkeyp null?", Point.IsNull(nullkeyp));
            NeoTrace.Trace("nullkeyp missing?", Point.IsMissing(nullkeyp));
            NeoTrace.Trace("nullkeyp extended?", Point.IsExtended(nullkeyp));

            NeoTrace.Trace("Missing key test...");
            Point missingp = Point.Get("missingp");
            Point.Log("missingp", missingp);
            NeoTrace.Trace("missingp null?", Point.IsNull(missingp));
            NeoTrace.Trace("missingp missing?", Point.IsMissing(missingp));
            NeoTrace.Trace("missingp extended?", Point.IsExtended(missingp));

            return Point.Null();
        }
    }
}
