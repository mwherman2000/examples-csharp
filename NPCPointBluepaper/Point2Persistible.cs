using Neo.SmartContract.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NeoPesistenceClasses2
{
    public class Point : NeoTrace /* Level 2 */
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

        // Class name and property names
        private const string _className = "Point";
        private const string _sX = "X";
        private const string _sY = "Y";
        private const string _sSTA = "_STA";
        private const string _sEXT = "_EXT";
        private static readonly byte[] _bX = Helper.AsByteArray(_sX);
        private static readonly byte[] _bY = Helper.AsByteArray(_sY);
        private static readonly byte[] _bSTA = Helper.AsByteArray(_sSTA);
        private static readonly byte[] _bEXT = Helper.AsByteArray(_sEXT);

        // Internal fields
        private const string _classKeyTag = "/#" + _className + ".";
        private static readonly byte[] _bclassKeyTag = Helper.AsByteArray(_classKeyTag);

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

        // Persistable methods
        public static bool IsMissing(Point p)
        {
            return (p._state == NeoEntityModel.EntityState.MISSING);
        }

        public static Point Missing()
        {
            Point p = new Point();
            p._x = 0;
            p._y = 0;
            p._state = NeoEntityModel.EntityState.MISSING;
            LogExt("Missing().p", p);
            return p;
        }

        public static bool Put(Point p, byte[] key)
        {
            if (key.Length == 0) return false;

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            byte[] _bkeyTag = Helper.Concat(key, _bclassKeyTag);

            p._state = NeoEntityModel.EntityState.PUTTED;
            /*STA*/
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bSTA), p._state.AsBigInteger());
            /*FIELD*/
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bX), p._x);
            /*FIELD*/
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bY), p._y);
            LogExt("Put(bkey).p", p);
            return true;
        }

        public static bool Put(Point p, string key)
        {
            if (key.Length == 0) return false;
            LogExt("Put(ks).p", p);

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            string _skeyTag = key + _classKeyTag;
            Trace("Put(ks)._skeyTag", _skeyTag);

            p._state = NeoEntityModel.EntityState.PUTTED;
            BigInteger bis = p._state.AsBigInteger();
            Trace("Put(ks).bis", bis);
            /*STA*/
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sSTA, bis);
            /*FIELD*/
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sX, p._x);
            /*FIELD*/
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sY, p._y);
            LogExt("Put(ks).p", p);
            return true;
        }

        public static Point Get(byte[] key)
        {
            if (key.Length == 0) return Null();

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            byte[] _bkeyTag = Helper.Concat(key, _bclassKeyTag);

            Point p;
            /*STA*/
            byte[] bsta = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, Helper.Concat(_bkeyTag, _bSTA));
            NeoTrace.Trace("Get(kb).bs", bsta.Length, bsta);
            if (bsta.Length == 0)
            {
                p = Point.Missing();
            }
            else // not MISSING
            {
                /*EXT*/
                byte[] bext = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, Helper.Concat(_bkeyTag, _bEXT));
                int ista = (int)bsta.AsBigInteger();
                NeoEntityModel.EntityState sta = (NeoEntityModel.EntityState)ista;
                p = new Point();
                /*FIELD*/
                BigInteger x = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, Helper.Concat(_bkeyTag, _bX)).AsBigInteger();
                /*FIELD*/
                BigInteger y = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, Helper.Concat(_bkeyTag, _bY)).AsBigInteger();
                p._x = x;
                p._y = y;
                p._state = sta;
                p._state = NeoEntityModel.EntityState.GETTED; /* OVERRIDE */
            }
            LogExt("Get(kb).p", p);
            return p;
        }

        public static Point Get(string key)
        {
            if (key.Length == 0) return Null();

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            string _skeyTag = key + _classKeyTag;

            Point p;
            /*STA*/
            byte[] bsta = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, _skeyTag + _sSTA);
            NeoTrace.Trace("Get(ks).bs", bsta.Length, bsta);
            if (bsta.Length == 0)
            {
                p = Point.Missing();
            }
            else // not MISSING
            {
                /*EXT*/
                byte[] bext = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, _skeyTag + _sEXT);
                int ista = (int)bsta.AsBigInteger();
                NeoEntityModel.EntityState sta = (NeoEntityModel.EntityState)ista;
                p = new Point();
                /*FIELD*/
                BigInteger x = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, _skeyTag + _sX).AsBigInteger();
                /*FIELD*/
                BigInteger y = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, _skeyTag + _sY).AsBigInteger();
                NeoTrace.Trace("Get(ks).x,y", x, y);
                p._x = x;
                p._y = y;
                p._state = sta;
                p._state = NeoEntityModel.EntityState.GETTED; /* OVERRIDE */
            }
            LogExt("Get(ks).p", p);
            return p;
        }
    }
}
