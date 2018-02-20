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

        public static readonly byte[] NullScriptHash = "".ToScriptHash();
        public static readonly byte[] NullByteArray = "".AsByteArray();
    }

    public class NeoVersionedAppUser
    {
        private byte[] _app;
        private int _major;
        private int _minor;
        private int _build;
        //private int _revision;
        private byte[] _userScriptHash; 
        private NeoEntityModel.EntityState _state;

        public static void SetAppName(NeoVersionedAppUser vau, byte[] value) { vau._app = value; vau._state = NeoEntityModel.EntityState.SET; }
        public static byte[] GetAppNameAsByteArray(NeoVersionedAppUser vau) { return vau._app; }
        public static void SetAppName(NeoVersionedAppUser vau, string value) { vau._app = value.AsByteArray(); vau._state = NeoEntityModel.EntityState.SET; }
        public static string GetAppNameAsString(NeoVersionedAppUser vau) { return vau._app.AsString(); }
        public static void SetMajor(NeoVersionedAppUser vau, int value) { vau._major = value; vau._state = NeoEntityModel.EntityState.SET; }
        public static int GetMajor(NeoVersionedAppUser vau) { return vau._major; }
        public static void SetMinor(NeoVersionedAppUser vau, int value) { vau._minor = value; vau._state = NeoEntityModel.EntityState.SET; }
        public static int GetMinor(NeoVersionedAppUser vau) { return vau._minor; }
        public static void SetBuild(NeoVersionedAppUser vau, int value) { vau._build = value; vau._state = NeoEntityModel.EntityState.SET; }
        public static int GetBuild(NeoVersionedAppUser vau) { return vau._build; }
        //public static void SetRevision(NeoVersionedAppUser vau, int value) { vau._revision = value; vau._state = NeoEntityModel.EntityState.SET; }
        //public static int GetRevision(NeoVersionedAppUser vau) { return vau._revision; }
        public static void SetUserScriptHash(NeoVersionedAppUser vau, byte[] value) { vau._userScriptHash = value; vau._state = NeoEntityModel.EntityState.SET; }
        public static byte[] GetUserScriptHash(NeoVersionedAppUser vau) { return vau._userScriptHash; }
        public static void Set(NeoVersionedAppUser vau, byte[] app, int major, int minor, int build, /*int revision,*/ byte[] userScriptHash)
        {
            vau._app = app; vau._major = major; vau._minor = minor; vau._build = build; /*vau._revision = revision;*/
            vau._userScriptHash = userScriptHash; vau._state = NeoEntityModel.EntityState.SET;
        }
        public static void Set(NeoVersionedAppUser vau, string app, int major, int minor, int build, /*int revision,*/ byte[] userScriptHash)
        {
            vau._app = app.AsByteArray(); vau._major = major; vau._minor = minor; vau._build = build; /*vau._revision = revision;*/
            vau._userScriptHash = userScriptHash;  vau._state = NeoEntityModel.EntityState.SET;
        }

        // Factory methods
        private NeoVersionedAppUser()
        {
        }

        private static NeoVersionedAppUser _Initialize(NeoVersionedAppUser vau)
        {
            vau._app = NeoEntityModel.NullByteArray;
            vau._major = 0;
            vau._minor = 0;
            vau._build = 0;
            //vau._revision = 0;
            vau._state = NeoEntityModel.EntityState.NULL;
            Log("_Initialize(vau).vau", vau);
            return vau;
        }

        public static NeoVersionedAppUser New()
        {
            NeoVersionedAppUser vau = new NeoVersionedAppUser();
            _Initialize(vau);
            Log("New().vau", vau);
            return vau;
        }

        public static NeoVersionedAppUser New(byte[] app, int major, int minor, int build, /*int revision,*/ byte[] userScriptHash)
        {
            NeoVersionedAppUser vau = new NeoVersionedAppUser();
            vau._app = app; ;
            vau._major = major;
            vau._minor = minor;
            vau._build = build;
            //vau._revision = revision;
            vau._userScriptHash = userScriptHash;
            vau._state = NeoEntityModel.EntityState.INIT;
            Log("New(a,m,m,b,u).vau", vau);
            return vau;
        }

        public static NeoVersionedAppUser New(string app, int major, int minor, int build, /*int revision,*/ byte[] userScriptHash)
        {
            NeoVersionedAppUser vau = new NeoVersionedAppUser();
            vau._app = app.AsByteArray();
            vau._major = major;
            vau._minor = minor;
            vau._build = build;
            //vau._revision = revision;
            vau._userScriptHash = userScriptHash;
            vau._state = NeoEntityModel.EntityState.INIT;
            Log("New(a,m,m,b,u).vau", vau);
            return vau;
        }

        public static NeoVersionedAppUser Null()
        {
            NeoVersionedAppUser vau = new NeoVersionedAppUser();
            _Initialize(vau);
            Log("Null().vau", vau);
            return vau;
        }

        // EntityState wrapper methods
        public static bool IsNull(NeoVersionedAppUser vau)
        {
            return (vau._state == NeoEntityModel.EntityState.NULL);
        }

        // Log/trace methods
        public static void Log(string label, NeoVersionedAppUser vau)
        {
            NeoTrace.Trace(label, vau._app, vau._major, vau._minor, vau._build, /*vau._revision,*/ vau._userScriptHash, vau._state);
        }
    }

    public class NeoStorageKey
    {
        //string key = "{" + String.Format("a:{0},v:{1}.{2}.{3}.{4},u:{5},e:{6},i:{7},f:{8}",
        //    app,
        //    NeoVersion.GetMajor(ver), NeoVersion.GetMinor(ver), NeoVersion.GetBuild(ver), NeoVersion.GetRevision(ver),
        //    WIF2AccountAddressScriptHash.ToString(), "Point", 10, "X") + "}";

        private byte[] _app;
        private int _major;
        private int _minor;
        private int _build;
        //private int _revision;
        private byte[] _userScriptHash;
        private byte[] _className;
        private int _index;
        private string _fieldName;
        private NeoEntityModel.EntityState _state;

        public static void SetAppName(NeoStorageKey sk, byte[] value) { sk._app = value; sk._state = NeoEntityModel.EntityState.SET; }
        public static byte[] GetAppNameAsByteArray(NeoStorageKey sk) { return sk._app; }
        public static void SetAppName(NeoStorageKey sk, string value) { sk._app = value.AsByteArray(); sk._state = NeoEntityModel.EntityState.SET; }
        public static string GetAppNameAsString(NeoStorageKey sk) { return sk._app.AsString(); }
        public static void SetMajor(NeoStorageKey sk, int value) { sk._major = value; sk._state = NeoEntityModel.EntityState.SET; }
        public static int GetMajor(NeoStorageKey sk) { return sk._major; }
        public static void SetMinor(NeoStorageKey sk, int value) { sk._minor = value; sk._state = NeoEntityModel.EntityState.SET; }
        public static int GetMinor(NeoStorageKey sk) { return sk._minor; }
        public static void SetBuild(NeoStorageKey sk, int value) { sk._build = value; sk._state = NeoEntityModel.EntityState.SET; }
        public static int GetBuild(NeoStorageKey sk) { return sk._build; }
        //public static void SetRevision(NeoStorageKey sk, int value) { sk._revision = value; sk._state = NeoEntityModel.EntityState.SET; }
        //public static int GetRevision(NeoStorageKey sk) { return sk._revision; }
        public static void SetUserScriptHash(NeoStorageKey sk, byte[] value) { sk._userScriptHash = value; sk._state = NeoEntityModel.EntityState.SET; }
        public static byte[] GetUserScriptHash(NeoStorageKey sk) { return sk._userScriptHash; }
        public static void SetClassName(NeoStorageKey sk, byte[] value) { sk._className = value; sk._state = NeoEntityModel.EntityState.SET; }
        public static byte[] GetClassNameAsByteArray(NeoStorageKey sk) { return sk._className; }
        public static void SetClassName(NeoStorageKey sk, string value) { sk._className = value.AsByteArray(); sk._state = NeoEntityModel.EntityState.SET; }
        public static string GetClassNameAsString(NeoStorageKey sk) { return sk._className.AsString(); }
        public static void SetIndex(NeoStorageKey sk, int value) { sk._index = value; sk._state = NeoEntityModel.EntityState.SET; }
        public static int GetIndex(NeoStorageKey sk) { return sk._index; }
        public static void SetFieldName(NeoStorageKey sk, string value) { sk._fieldName = value; sk._state = NeoEntityModel.EntityState.SET; }
        public static string GetFieldName(NeoStorageKey sk) { return sk._fieldName; }
        public static void Set(NeoStorageKey sk, byte[] app, int major, int minor, int build, /*int revision,*/ byte[] userScriptHash, byte[] className, int index, string fieldName)
        {
            sk._app = app; sk._major = major; sk._minor = minor; sk._build = build; /*sk._revision = revision*/;
            sk._userScriptHash = userScriptHash; sk._className = className; sk._index = index; sk._fieldName = fieldName;
            sk._state = NeoEntityModel.EntityState.SET;
        }
        public static void Set(NeoStorageKey sk, string app, int major, int minor, int build, /*int revision,*/ byte[] userScriptHash, string className, int index, string fieldName)
        {
            sk._app = app.AsByteArray(); sk._major = major; sk._minor = minor; sk._build = build; /*sk._revision = revision*/;
            sk._userScriptHash = userScriptHash; sk._className = className.AsByteArray(); sk._index = index; sk._fieldName = fieldName;
            sk._state = NeoEntityModel.EntityState.SET;
        }
        public static void Set(NeoStorageKey sk, NeoVersionedAppUser vau, byte[] userScriptHash, byte[] className, int index, string fieldName)
        {
            sk._major = NeoVersionedAppUser.GetMajor(vau); sk._minor = NeoVersionedAppUser.GetMinor(vau); sk._build = NeoVersionedAppUser.GetBuild(vau); /*sk._revision = NeoVersionedAppUser.GetRevision(vau);*/
            sk._userScriptHash = NeoVersionedAppUser.GetUserScriptHash(vau);
            sk._className = className; sk._index = index; sk._fieldName = fieldName;
            sk._state = NeoEntityModel.EntityState.SET;
        }
        public static void Set(NeoStorageKey sk, NeoVersionedAppUser vau, byte[] userScriptHash, string className, int index, string fieldName)
        {
            sk._major = NeoVersionedAppUser.GetMajor(vau); sk._minor = NeoVersionedAppUser.GetMinor(vau); sk._build = NeoVersionedAppUser.GetBuild(vau); /*sk._revision = NeoVersionedAppUser.GetRevision(vau);*/
            sk._userScriptHash = NeoVersionedAppUser.GetUserScriptHash(vau);
            sk._className = className.AsByteArray(); sk._index = index; sk._fieldName = fieldName;
            sk._state = NeoEntityModel.EntityState.SET;
        }

        // Factory methods
        private NeoStorageKey()
        {
        }

        private static NeoStorageKey _Initialize(NeoStorageKey sk)
        {
            sk._app = NeoEntityModel.NullByteArray;
            sk._major = 0;
            sk._minor = 0;
            sk._build = 0;
            //sk._revision = 0;
            sk._userScriptHash = NeoEntityModel.NullScriptHash;
            sk._className = NeoEntityModel.NullByteArray;
            sk._index = 0;
            sk._fieldName = "";
            sk._state = NeoEntityModel.EntityState.NULL;
            Log("_Initialize(sk).sk", sk);
            return sk;
        }

        public static NeoStorageKey New()
        {
            NeoStorageKey sk = new NeoStorageKey();
            _Initialize(sk);
            Log("New().sk", sk);
            return sk;
        }

        public static NeoStorageKey New(byte[] app, int major, int minor, int build, /*int revision,*/ byte[] userScriptHash, byte[] className, int index, string fieldName)
        {
            NeoStorageKey sk = new NeoStorageKey();
            sk._app = app;
            sk._major = major;
            sk._minor = minor;
            sk._build = build;
            //sk._revision = revision;
            sk._userScriptHash = userScriptHash;
            sk._className = className;
            sk._index = index;
            sk._fieldName = fieldName;
            sk._state = NeoEntityModel.EntityState.INIT;
            Log("New(ab,m,m,b,u,cb,i,f,s).sk", sk);
            return sk;
        }

        public static NeoStorageKey New(string app, int major, int minor, int build, /*int revision,*/ byte[] userScriptHash, string className, int index, string fieldName)
        {
            NeoStorageKey sk = new NeoStorageKey();
            sk._app = app.AsByteArray();
            sk._major = major;
            sk._minor = minor;
            sk._build = build;
            //sk._revision = revision;
            sk._userScriptHash = userScriptHash;
            sk._className = className.AsByteArray();
            sk._index = index;
            sk._fieldName = fieldName;
            sk._state = NeoEntityModel.EntityState.INIT;
            Log("New(as,m,m,b,u,cs,i,f,s).sk", sk);
            return sk;
        }

        public static NeoStorageKey New(NeoVersionedAppUser vau, byte[] className)
        {
            if (NeoVersionedAppUser.IsNull(vau))
            {
                return NeoStorageKey.Null();
            }

            NeoStorageKey sk = new NeoStorageKey();
            sk._app = NeoVersionedAppUser.GetAppNameAsByteArray(vau);
            sk._major = NeoVersionedAppUser.GetMajor(vau);
            sk._minor = NeoVersionedAppUser.GetMinor(vau);
            sk._build = NeoVersionedAppUser.GetBuild(vau);
            //sk._revision = NeoVersionedAppUser.GetRevision(vau);
            sk._userScriptHash = NeoVersionedAppUser.GetUserScriptHash(vau);
            sk._className = className;
            sk._index = 0;
            sk._fieldName = "";
            sk._state = NeoEntityModel.EntityState.INIT;
            Log("New(vau,cb)", sk);
            return sk;
        }

        public static NeoStorageKey New(NeoVersionedAppUser vau, string className)
        {
            if (NeoVersionedAppUser.IsNull(vau))
            {
                return NeoStorageKey.Null();
            }

            NeoStorageKey sk = new NeoStorageKey();
            sk._app = NeoVersionedAppUser.GetAppNameAsByteArray(vau);
            sk._major = NeoVersionedAppUser.GetMajor(vau);
            sk._minor = NeoVersionedAppUser.GetMinor(vau);
            sk._build = NeoVersionedAppUser.GetBuild(vau);
            //sk._revision = NeoVersionedAppUser.GetRevision(vau);
            sk._userScriptHash = NeoVersionedAppUser.GetUserScriptHash(vau);
            sk._className = className.AsByteArray();
            sk._index = 0;
            sk._fieldName = "";
            sk._state = NeoEntityModel.EntityState.INIT;
            Log("New(vau,cs).sk", sk);
            return sk;
        }

        public static NeoStorageKey Null()
        {
            NeoStorageKey sk = new NeoStorageKey();
            _Initialize(sk);
            Log("Null().sk", sk);
            return sk;
        }

        // EntityState wrapper methods
        public static bool IsNull(NeoStorageKey sk)
        {
            return (sk._state == NeoEntityModel.EntityState.NULL);
        }

        // Log/trace methods
        public static void Log(string label, NeoStorageKey sk)
        {
            NeoTrace.Trace(label, sk._app, sk._major, sk._minor, sk._build, /*sk._revision,*/ sk._userScriptHash, sk._className, sk._index, sk._fieldName, sk._state);
        }

        private static readonly byte[] _bLeftBrace = "{".AsByteArray();
        private static readonly byte[] _bRightBrace = "}".AsByteArray();
        private static readonly byte[] _bColon = ":".AsByteArray();
        private static readonly byte[] _bComma = ",".AsByteArray();
        private static readonly byte[] _ba = "a".AsByteArray();
        private static readonly byte[] _bM = "M".AsByteArray();
        private static readonly byte[] _bm = "m".AsByteArray();
        private static readonly byte[] _bb = "b".AsByteArray();
        //private static readonly byte[] _br = "r".AsByteArray();
        private static readonly byte[] _bu = "u".AsByteArray();
        private static readonly byte[] _bc = "c".AsByteArray();
        private static readonly byte[] _bi = "i".AsByteArray();
        private static readonly byte[] _bf = "f".AsByteArray();

        //* Core methods
        public static byte[] GetKey(NeoStorageKey sk, int index, byte[]fieldName)
        {
            Log("GetKey(sk,i,fb)", sk);

            byte[] bkey = Helper.Concat(_bLeftBrace, _ba).Concat(sk._app).Concat(_bComma);
            bkey = Helper.Concat(bkey, _bM).Concat(((BigInteger)(sk._major)).AsByteArray()).Concat(_bComma);
            bkey = Helper.Concat(bkey, _bm).Concat(((BigInteger)(sk._minor)).AsByteArray()).Concat(_bComma);
            bkey = Helper.Concat(bkey, _bb).Concat(((BigInteger)(sk._build)).AsByteArray()).Concat(_bComma);
            //bkey = Helper.Concat(bkey, _br).Concat(((BigInteger)(sk._revision)).AsByteArray()).Concat(_bComma);
            bkey = Helper.Concat(bkey, _bu).Concat(sk._userScriptHash).Concat(_bComma);
            bkey = Helper.Concat(bkey, _bc).Concat(sk._className).Concat(_bComma);

            bkey = Helper.Concat(bkey, _bi).Concat(((BigInteger)(index)).AsByteArray()).Concat(_bComma);
            bkey = Helper.Concat(bkey, _bf).Concat(fieldName);

            bkey = Helper.Concat(bkey, _bRightBrace);
            NeoTrace.Trace("GetKey(sk).bkey", bkey);
            return bkey;
        }
    }

    public class Point
    {
        private int _x;
        private int _y;
        private NeoEntityModel.EntityState _state;
        private byte[] _extension;

        // Accessors
        public static void SetX(Point p, int value) { p._x = value; p._state = NeoEntityModel.EntityState.SET; }
        public static int GetX(Point p) { return p._x; }
        public static void SetY(Point p, int value) { p._y = value; p._state = NeoEntityModel.EntityState.SET; }
        public static int GetY(Point p) { return p._y; }
        public static void SetExtension(Point p, byte[] value) { p._extension = value; }
        public static byte[] GetExtension(Point p) { return p._extension; }
        public static void Set(Point p, int xvalue, int yvalue) { p._x = xvalue; p._y = yvalue; p._state = NeoEntityModel.EntityState.SET; }

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

        // Internal fields
        private const string _classKeyTag = "/#" + _className + ".";
        private static readonly byte[] _bclassKeyTag = Helper.AsByteArray(_classKeyTag);

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

        // Factory methods
        private Point()
        {
        }

        private static Point _Initialize(Point p)
        {
            p._x = 0;
            p._y = 0;
            p._state = NeoEntityModel.EntityState.NULL;
            p._extension = NeoEntityModel.NullScriptHash;
            Log("_Initialize(p).p", p);
            return p;
        }

        public static Point New()
        {
            Point p = new Point();
            _Initialize(p);
            Log("New().p", p);
            return p;
        }

        public static Point New(int x, int y)
        {
            Point p = new Point();
            p._x = x;
            p._y = y;
            p._state = NeoEntityModel.EntityState.INIT;
            p._extension = NeoEntityModel.NullScriptHash;
            Log("New(x,y).p", p);
            return p;
        }

        public static Point Null()
        {
            Point p = new Point();
            _Initialize(p);
            Log("Null().p", p);
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
            NeoTrace.Trace(label, p._x, p._y, p._state, p._extension);
        }

        // Extendible class methods
        public static bool IsExtended(Point p)
        {
            return (p._extension != NeoEntityModel.NullScriptHash);
        }

        // Persistable class methods
        public static bool IsMissing(Point p)
        {
            return (p._state == NeoEntityModel.EntityState.MISSING);
        }

        public static bool IsBuried(Point p)
        {
            return (p._state == NeoEntityModel.EntityState.TOMBSTONED);
        }

        public static Point Missing()
        {
            Point p = new Point();
            p._x = 0;
            p._y = 0;
            p._state = NeoEntityModel.EntityState.MISSING;
            p._extension = NeoEntityModel.NullScriptHash;
            Log("Missing().p", p);
            return p;
        }

        public static Point Tombstone()
        {
            Point p = new Point();
            p._x = 0;
            p._y = 0;
            p._state = NeoEntityModel.EntityState.TOMBSTONED;
            p._extension = NeoEntityModel.NullScriptHash;
            Log("Tombstone().p", p);
            return p;
        }

        // Core methods
        public static Point Bury(byte[] key)
        {
            if (key.Length == 0) return Null(); // TODO - create NeoEntityModel.EntityState.BADKEY?

            var ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            byte[] _bkeyTag = Helper.Concat(key, _bclassKeyTag);

            Point p;
            /*STA*/ byte[] bsta = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, Helper.Concat(_bkeyTag, _bSTA));
            NeoTrace.Trace("Bury(kb).bs", bsta.Length, bsta);
            if (bsta.Length == 0)
            {
                p = Point.Missing();
            }
            else // not MISSING - bury it
            {
                p = Point.Tombstone(); // TODO - should Bury() preserve the exist field values or re-initialize them? Preserve is cheaper but not as private
                /*STA*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bSTA), (int)p._state);
                /*EXT*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bEXT), p._extension);
                /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bX), p._x);
                /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bY), p._y);
            }
            Log("Bury(kb).p", p);
            return p; // return Point p to signal if key is Missing or bad key
        }

        public static Point Bury(string key)
        {
            if (key.Length == 0) return Null(); // TODO - create NeoEntityModel.EntityState.BADKEY?

            var ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            string _skeyTag = key + _classKeyTag;

            Point p;
            /*STA*/ byte[] bsta = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, _skeyTag + _sSTA);
            NeoTrace.Trace("Bury(ks).bs", bsta.Length, bsta);
            if (bsta.Length == 0)
            {
                p = Point.Missing();
            }
            else // not MISSING - bury it
            {
                p = Point.Tombstone(); // TODO - should Bury() preserve the exist field values or re-initialize them? Preserve is cheaper but not as private
                /*STA*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sSTA, (int)p._state);
                /*EXT*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sEXT, p._extension);
                /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sX, p._x);
                /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sY, p._y);
            }
            Log("Bury(ks).p", p);
            return p; // return Point p to signal if key is Missing or bad key
        }

        public static Point BuryElement(NeoVersionedAppUser vau, int index)
        {
            if (NeoVersionedAppUser.IsNull(vau)) // TODO - create NeoEntityModel.EntityState.BADKEY?
            {
                return Point.Null();
            }

            var ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            NeoStorageKey sk = NeoStorageKey.New(vau, "Point");

            byte[] bkey;
            Point p;
            /*STA*/
            byte[] bsta = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, bkey = NeoStorageKey.GetKey(sk, index, _bSTA));
            NeoTrace.Trace("Bury(vau,index).bs", bsta.Length, bsta);
            if (bsta.Length == 0)
            {
                p = Point.Missing();
            }
            else // not MISSING - bury it
            {
                p = Point.Tombstone(); // TODO - should Bury() preserve the exist field values or re-initialize them? Preserve is cheaper but not as private
                /*STA*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.GetKey(sk, index, _bSTA), (int)p._state);
                /*EXT*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.GetKey(sk, index, _bEXT), p._extension);
                /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.GetKey(sk, index, _bX), p._x);
                /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.GetKey(sk, index, _bY), p._y);
            }
            Log("Bury(vau,i).p", p);
            return p;
        }

        public static bool Put(Point p, byte[] key)
        {
            if (key.Length == 0) return false;

            var ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            byte[] _bkeyTag = Helper.Concat(key, _bclassKeyTag);

            p._state = NeoEntityModel.EntityState.PUTTED;
            /*STA*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bSTA), (int)p._state);
            /*EXT*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bEXT), p._extension);
            /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bX), p._x);
            /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bY), p._y);
            Log("Put(bkey).p", p);
            return true;
        }

        public static bool Put(Point p, string key)
        {
            if (key.Length == 0) return false;

            var ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            string _skeyTag = key + _classKeyTag;

            p._state = NeoEntityModel.EntityState.PUTTED;
            /*STA*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sSTA, (int)p._state);
            /*EXT*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sEXT, p._extension);
            /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sX, p._x);
            /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sY, p._y);
            Log("Put(ks).p", p);
            return true;
        }

        public static bool PutElement(Point p, NeoVersionedAppUser vau, int index)
        {
            if (NeoVersionedAppUser.IsNull(vau)) return false;

            var ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            NeoStorageKey sk = NeoStorageKey.New(vau, "Point");

            byte[] bkey;
            p._state = NeoEntityModel.EntityState.PUTTED;
            /*STA*/Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.GetKey(sk, index, _bSTA), (int)p._state);
            /*EXT*/Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.GetKey(sk, index, _bEXT), p._extension);
            /*FIELD*/Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.GetKey(sk, index, _bX), p._x);
            /*FIELD*/Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.GetKey(sk, index, _bY), p._y);
            Log("PutElement(vau,i).p", p);
            return true;
        }

        public static Point Get(byte[] key)
        {
            if (key.Length == 0) return Null();

            var ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            byte[] _bkeyTag = Helper.Concat(key, _bclassKeyTag);

            Point p;
            /*STA*/ byte[] bsta = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, Helper.Concat(_bkeyTag, _bSTA));
            NeoTrace.Trace("Get(kb).bs", bsta.Length, bsta);
            if (bsta.Length == 0)
            {
                p = Point.Missing();
            }
            else // not MISSING
            {
                /*EXT*/ byte[] bext = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, Helper.Concat(_bkeyTag, _bEXT));
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
                    /*FIELD*/ int x = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, Helper.Concat(_bkeyTag, _bX)).AsBigInteger();
                    /*FIELD*/ int y = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, Helper.Concat(_bkeyTag, _bY)).AsBigInteger();
                    p._x = x;
                    p._y = y;
                    p._state = sta;
                    p._state = NeoEntityModel.EntityState.GETTED; /* OVERRIDE */
                    p._extension = bext;
                }
            }
            Log("Get(kb).p", p);
            return p;
        }

        public static Point Get(string key)
        {
            if (key.Length == 0) return Null();

            var ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            string _skeyTag = key + _classKeyTag;

            Point p;
            /*STA*/ byte[] bsta = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, _skeyTag + _sSTA);
            NeoTrace.Trace("Get(ks).bs", bsta.Length, bsta);
            if (bsta.Length == 0)
            {
                p = Point.Missing();
            }
            else // not MISSING
            {
                /*EXT*/ byte[] bext = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, _skeyTag + _sEXT);
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
                    /*FIELD*/ int x = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, _skeyTag + _sX).AsBigInteger();
                    /*FIELD*/ int y = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, _skeyTag + _sY).AsBigInteger();
                    p._x = x;
                    p._y = y;
                    p._state = sta;
                    p._state = NeoEntityModel.EntityState.GETTED; /* OVERRIDE */
                    p._extension = bext;
                }
            }
            Log("Get(ks).p", p);
            return p;
        }
    }

    public class StructExample3Combined : SmartContract
    {
        // WIF from the NEO privatenet Python environment
        public const string WIF2 = "KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr";
        public const string WIF2AccountAddress = "AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y";
        public const string WIF2AccountPublicKey = "031a6c6fbbdf02ca351745fa86b9ba5a9452d785ac4f7fc2b7548ca2a46c4fcf4a";
        public const string WIF2AccountPrivateKeyHex = "1dd37fba80fec4e6a6f13fd708d8dcb3b29def768017052f6c930fa1c5d90bbb";
        public static readonly byte[] WIF2AccountAddressScriptHash = WIF2AccountAddress.ToScriptHash();

        private static Point Add(Point a, Point b)

        {
            Point p = Point.New();
            Point.Set(p, Point.GetX(a) + Point.GetX(b), Point.GetY(a) + Point.GetY(b));
            return p;
        }

        public static Point Main()
        {
            NeoTrace.Trace("NullHash", NeoEntityModel.NullScriptHash);

            Point p0 = Point.New();
            Point.Log("p0", p0);
            Point.SetX(p0, 7);
            Point.SetY(p0, 8);
            Point.Log("p0", p0);
            Point.Set(p0, 9, 10);
            Point.Log("p0", p0);

            Point p1 = Point.New();
            Point.Set(p1, 2, 4);
            Point.Log("p1", p1);

            Point p2 = Point.New();
            Point.Set(p2, 15, 16);
            Point.Log("p2", p2);

            Point[] line1 = new[]
            {
                p1, p2
            };
            NeoTrace.Trace("line1", line1);

            Point p3 = Add(line1[0], line1[1]);
            Point.Log("p3", p3);

            Point.Put(p1, "p1");
            Point.Put(p2, "p2");
            Point.Put(p3, "p3");

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

            string app = "FooBar";
            byte[] user = WIF2AccountAddressScriptHash;
            NeoVersionedAppUser vau = NeoVersionedAppUser.New(app, 1, 0, 2034, user);
            Point p4 = Point.New();
            Point.Set(p4, 10, 20);
            int index = 24;
            Point.PutElement(p4, vau, index);

            string key = "test";
            Point.Put(p4, key);

            return Point.Null();
        }
    }
}
