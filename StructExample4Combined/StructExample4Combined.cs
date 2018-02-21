using System;
using System.Numerics;
using Neo.SmartContract.Framework;
//using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;

namespace StructExample
{
    public class NeoTrace
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
            TOMBSTONED,
            NOTAUTHORIZED
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
            LogExt("_Initialize(vau).vau", vau);
            return vau;
        }

        public static NeoVersionedAppUser New()
        {
            NeoVersionedAppUser vau = new NeoVersionedAppUser();
            _Initialize(vau);
            LogExt("New().vau", vau);
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
            LogExt("New(a,m,m,b,u).vau", vau);
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
            LogExt("New(a,m,m,b,u).vau", vau);
            return vau;
        }

        public static NeoVersionedAppUser Null()
        {
            NeoVersionedAppUser vau = new NeoVersionedAppUser();
            _Initialize(vau);
            LogExt("Null().vau", vau);
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
            NeoTrace.Trace(label, vau._app, vau._major, vau._minor, vau._build, /*vau._revision,*/ vau._userScriptHash);
        }

        public static void LogExt(string label, NeoVersionedAppUser vau)
        {
            NeoTrace.Trace(label, vau._app, vau._major, vau._minor, vau._build, /*vau._revision,*/ vau._userScriptHash, vau._state); // long values, state, extension last
        }
    }

    public class NeoStorageKey
    {
        // NSKON = NeoStorageKey Object Notation
        //string key = "{" + String.Format("a:T={0},M:T={1},M:T={2},b:T={3}r:T={4},u:T={5},c:T={6},i:T={7},f:T={8}",
        //    app,
        //    NeoVersion.GetMajor(ver), NeoVersion.GetMinor(ver), NeoVersion.GetBuild(ver), NeoVersion.GetRevision(ver),
        //    WIF2AccountAddressScriptHash.ToString(), "Point", 10, "X") + "}";
        //
        //  where T = 1-byte data type code based on https://github.com/neo-project/neo/blob/master/neo/SmartContract/ContractParameterType.cs
        //
        // Related specifications: http://bsonspec.org/faq.html
        //

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

        public static void SetAppName(NeoStorageKey nsk, byte[] value) { nsk._app = value; nsk._state = NeoEntityModel.EntityState.SET; }
        public static byte[] GetAppNameAsByteArray(NeoStorageKey nsk) { return nsk._app; }
        public static void SetAppName(NeoStorageKey nsk, string value) { nsk._app = value.AsByteArray(); nsk._state = NeoEntityModel.EntityState.SET; }
        public static string GetAppNameAsString(NeoStorageKey nsk) { return nsk._app.AsString(); }
        public static void SetMajor(NeoStorageKey nsk, int value) { nsk._major = value; nsk._state = NeoEntityModel.EntityState.SET; }
        public static int GetMajor(NeoStorageKey nsk) { return nsk._major; }
        public static void SetMinor(NeoStorageKey nsk, int value) { nsk._minor = value; nsk._state = NeoEntityModel.EntityState.SET; }
        public static int GetMinor(NeoStorageKey nsk) { return nsk._minor; }
        public static void SetBuild(NeoStorageKey nsk, int value) { nsk._build = value; nsk._state = NeoEntityModel.EntityState.SET; }
        public static int GetBuild(NeoStorageKey nsk) { return nsk._build; }
        //public static void SetRevision(NeoStorageKey nsk, int value) { nsk._revision = value; nsk._state = NeoEntityModel.EntityState.SET; }
        //public static int GetRevision(NeoStorageKey nsk) { return nsk._revision; }
        public static void SetUserScriptHash(NeoStorageKey nsk, byte[] value) { nsk._userScriptHash = value; nsk._state = NeoEntityModel.EntityState.SET; }
        public static byte[] GetUserScriptHash(NeoStorageKey nsk) { return nsk._userScriptHash; }
        public static void SetClassName(NeoStorageKey nsk, byte[] value) { nsk._className = value; nsk._state = NeoEntityModel.EntityState.SET; }
        public static byte[] GetClassNameAsByteArray(NeoStorageKey nsk) { return nsk._className; }
        public static void SetClassName(NeoStorageKey nsk, string value) { nsk._className = value.AsByteArray(); nsk._state = NeoEntityModel.EntityState.SET; }
        public static string GetClassNameAsString(NeoStorageKey nsk) { return nsk._className.AsString(); }
        public static void SetIndex(NeoStorageKey nsk, int value) { nsk._index = value; nsk._state = NeoEntityModel.EntityState.SET; }
        public static int GetIndex(NeoStorageKey nsk) { return nsk._index; }
        public static void SetFieldName(NeoStorageKey nsk, string value) { nsk._fieldName = value; nsk._state = NeoEntityModel.EntityState.SET; }
        public static string GetFieldName(NeoStorageKey nsk) { return nsk._fieldName; }
        public static void Set(NeoStorageKey nsk, byte[] app, int major, int minor, int build, /*int revision,*/ byte[] userScriptHash, byte[] className, int index, string fieldName)
        {
            nsk._app = app; nsk._major = major; nsk._minor = minor; nsk._build = build; /*nsk._revision = revision*/;
            nsk._userScriptHash = userScriptHash; nsk._className = className; nsk._index = index; nsk._fieldName = fieldName;
            nsk._state = NeoEntityModel.EntityState.SET;
        }
        public static void Set(NeoStorageKey nsk, string app, int major, int minor, int build, /*int revision,*/ byte[] userScriptHash, string className, int index, string fieldName)
        {
            nsk._app = app.AsByteArray(); nsk._major = major; nsk._minor = minor; nsk._build = build; /*nsk._revision = revision*/;
            nsk._userScriptHash = userScriptHash; nsk._className = className.AsByteArray(); nsk._index = index; nsk._fieldName = fieldName;
            nsk._state = NeoEntityModel.EntityState.SET;
        }
        public static void Set(NeoStorageKey nsk, NeoVersionedAppUser vau, byte[] userScriptHash, byte[] className, int index, string fieldName)
        {
            nsk._major = NeoVersionedAppUser.GetMajor(vau); nsk._minor = NeoVersionedAppUser.GetMinor(vau); nsk._build = NeoVersionedAppUser.GetBuild(vau); /*nsk._revision = NeoVersionedAppUser.GetRevision(vau);*/
            nsk._userScriptHash = NeoVersionedAppUser.GetUserScriptHash(vau);
            nsk._className = className; nsk._index = index; nsk._fieldName = fieldName;
            nsk._state = NeoEntityModel.EntityState.SET;
        }
        public static void Set(NeoStorageKey nsk, NeoVersionedAppUser vau, byte[] userScriptHash, string className, int index, string fieldName)
        {
            nsk._major = NeoVersionedAppUser.GetMajor(vau); nsk._minor = NeoVersionedAppUser.GetMinor(vau); nsk._build = NeoVersionedAppUser.GetBuild(vau); /*nsk._revision = NeoVersionedAppUser.GetRevision(vau);*/
            nsk._userScriptHash = NeoVersionedAppUser.GetUserScriptHash(vau);
            nsk._className = className.AsByteArray(); nsk._index = index; nsk._fieldName = fieldName;
            nsk._state = NeoEntityModel.EntityState.SET;
        }

        // Factory methods
        private NeoStorageKey()
        {
        }

        private static NeoStorageKey _Initialize(NeoStorageKey nsk)
        {
            nsk._app = NeoEntityModel.NullByteArray;
            nsk._major = 0;
            nsk._minor = 0;
            nsk._build = 0;
            //nsk._revision = 0;
            nsk._userScriptHash = NeoEntityModel.NullScriptHash;
            nsk._className = NeoEntityModel.NullByteArray;
            nsk._index = 0;
            nsk._fieldName = "";
            nsk._state = NeoEntityModel.EntityState.NULL;
            LogExt("_Initialize(nsk).nsk", nsk);
            return nsk;
        }

        public static NeoStorageKey New()
        {
            NeoStorageKey nsk = new NeoStorageKey();
            _Initialize(nsk);
            LogExt("New().nsk", nsk);
            return nsk;
        }

        public static NeoStorageKey New(byte[] app, int major, int minor, int build, /*int revision,*/ byte[] userScriptHash, byte[] className, int index, string fieldName)
        {
            NeoStorageKey nsk = new NeoStorageKey();
            nsk._app = app;
            nsk._major = major;
            nsk._minor = minor;
            nsk._build = build;
            //nsk._revision = revision;
            nsk._userScriptHash = userScriptHash;
            nsk._className = className;
            nsk._index = index;
            nsk._fieldName = fieldName;
            nsk._state = NeoEntityModel.EntityState.INIT;
            LogExt("New(ab,m,m,b,u,cb,i,f,s).nsk", nsk);
            return nsk;
        }

        public static NeoStorageKey New(string app, int major, int minor, int build, /*int revision,*/ byte[] userScriptHash, string className, int index, string fieldName)
        {
            NeoStorageKey nsk = new NeoStorageKey();
            nsk._app = app.AsByteArray();
            nsk._major = major;
            nsk._minor = minor;
            nsk._build = build;
            //nsk._revision = revision;
            nsk._userScriptHash = userScriptHash;
            nsk._className = className.AsByteArray();
            nsk._index = index;
            nsk._fieldName = fieldName;
            nsk._state = NeoEntityModel.EntityState.INIT;
            LogExt("New(as,m,m,b,u,cs,i,f,s).nsk", nsk);
            return nsk;
        }

        public static NeoStorageKey New(NeoVersionedAppUser vau, byte[] className)
        {
            if (NeoVersionedAppUser.IsNull(vau))
            {
                return NeoStorageKey.Null();
            }

            NeoStorageKey nsk = new NeoStorageKey();
            nsk._app = NeoVersionedAppUser.GetAppNameAsByteArray(vau);
            nsk._major = NeoVersionedAppUser.GetMajor(vau);
            nsk._minor = NeoVersionedAppUser.GetMinor(vau);
            nsk._build = NeoVersionedAppUser.GetBuild(vau);
            //nsk._revision = NeoVersionedAppUser.GetRevision(vau);
            nsk._userScriptHash = NeoVersionedAppUser.GetUserScriptHash(vau);
            nsk._className = className;
            nsk._index = 0;
            nsk._fieldName = "";
            nsk._state = NeoEntityModel.EntityState.INIT;
            LogExt("New(vau,cb)", nsk);
            return nsk;
        }

        public static NeoStorageKey New(NeoVersionedAppUser vau, string className)
        {
            if (NeoVersionedAppUser.IsNull(vau))
            {
                return NeoStorageKey.Null();
            }

            NeoStorageKey nsk = new NeoStorageKey();
            nsk._app = NeoVersionedAppUser.GetAppNameAsByteArray(vau);
            nsk._major = NeoVersionedAppUser.GetMajor(vau);
            nsk._minor = NeoVersionedAppUser.GetMinor(vau);
            nsk._build = NeoVersionedAppUser.GetBuild(vau);
            //nsk._revision = NeoVersionedAppUser.GetRevision(vau);
            nsk._userScriptHash = NeoVersionedAppUser.GetUserScriptHash(vau);
            nsk._className = className.AsByteArray();
            nsk._index = 0;
            nsk._fieldName = "";
            nsk._state = NeoEntityModel.EntityState.INIT;
            LogExt("New(vau,cs).nsk", nsk);
            return nsk;
        }

        public static NeoStorageKey Null()
        {
            NeoStorageKey nsk = new NeoStorageKey();
            _Initialize(nsk);
            LogExt("Null().nsk", nsk);
            return nsk;
        }

        // EntityState wrapper methods
        public static bool IsNull(NeoStorageKey nsk)
        {
            return (nsk._state == NeoEntityModel.EntityState.NULL);
        }

        // Log/trace methods
        public static void Log(string label, NeoStorageKey nsk)
        {
            NeoTrace.Trace(label, nsk._app, nsk._major, nsk._minor, nsk._build, /*nsk._revision,*/ nsk._className, nsk._index, nsk._fieldName, nsk._userScriptHash);
        }

        public static void LogExt(string label, NeoStorageKey nsk)
        {
            NeoTrace.Trace(label, nsk._app, nsk._major, nsk._minor, nsk._build, /*nsk._revision,*/ nsk._className, nsk._index, nsk._fieldName, nsk._userScriptHash, nsk._state); // long values, state, extension last
        }

        private static readonly byte[] _bLeftBrace = "{".AsByteArray();
        private static readonly byte[] _bRightBrace = "}".AsByteArray();
        private static readonly byte[] _bColon = ":".AsByteArray();
        private static readonly byte[] _bEquals = "=".AsByteArray();
        private static readonly byte[] _bSemiColon = ";".AsByteArray();
        private static readonly byte[] _ba = "a".AsByteArray(); // App name
        private static readonly byte[] _bM = "M".AsByteArray(); // App major version
        private static readonly byte[] _bm = "m".AsByteArray(); // App minor version
        private static readonly byte[] _bb = "b".AsByteArray(); // App build number
        //private static readonly byte[] _br = "r".AsByteArray(); // App revision number
        private static readonly byte[] _bu = "u".AsByteArray(); // User script hash
        private static readonly byte[] _bc = "c".AsByteArray(); // Class name
        private static readonly byte[] _bi = "i".AsByteArray(); // Index value
        private static readonly byte[] _bf = "f".AsByteArray(); // Field name

        private static readonly byte[] _bStringType = { (byte)Neo.SmartContract.ContractParameterType.String };
        private static readonly byte[] _bBigIntegerType = { (byte)Neo.SmartContract.ContractParameterType.Integer };
        private static readonly byte[] _bUserScriptHashType = { (byte)Neo.SmartContract.ContractParameterType.ByteArray };

        //* Core methods
        public static byte[] StorageKey(NeoStorageKey nsk, int index, byte[]fieldName)
        {
            LogExt("StorageKey(nsk,i,fb).nsk", nsk);

            byte[] bkey = Helper.Concat(_bLeftBrace, _ba).Concat(_bColon).Concat(_bStringType).Concat(_bEquals).Concat(nsk._app).Concat(_bSemiColon);
            bkey =               Helper.Concat(bkey, _bM).Concat(_bColon).Concat(_bBigIntegerType).Concat(_bEquals).Concat(((BigInteger)(nsk._major)).AsByteArray()).Concat(_bSemiColon);
            bkey =               Helper.Concat(bkey, _bm).Concat(_bColon).Concat(_bBigIntegerType).Concat(_bEquals).Concat(((BigInteger)(nsk._minor)).AsByteArray()).Concat(_bSemiColon);
            bkey =               Helper.Concat(bkey, _bb).Concat(_bColon).Concat(_bBigIntegerType).Concat(_bEquals).Concat(((BigInteger)(nsk._build)).AsByteArray()).Concat(_bSemiColon);
            //bkey =             Helper.Concat(bkey, _br).Concat(_bColon).Concat(_bBigIntegerType).Concat(_bEquals).Concat(((BigInteger)(nsk._revision)).AsByteArray()).Concat(_bComma);
            bkey =               Helper.Concat(bkey, _bu).Concat(_bColon).Concat(_bUserScriptHashType).Concat(_bEquals).Concat(nsk._userScriptHash).Concat(_bSemiColon);
            bkey =               Helper.Concat(bkey, _bc).Concat(_bColon).Concat(_bStringType).Concat(_bEquals).Concat(nsk._className).Concat(_bSemiColon);

            bkey =               Helper.Concat(bkey, _bi).Concat(_bColon).Concat(_bBigIntegerType).Concat(_bColon).Concat(((BigInteger)(index)).AsByteArray()).Concat(_bSemiColon);
            bkey =               Helper.Concat(bkey, _bf).Concat(_bColon).Concat(_bStringType).Concat(_bColon).Concat(fieldName);

            bkey =               Helper.Concat(bkey, _bRightBrace);
            NeoTrace.Trace("StorageKey(nsk).bkey$BSK", bkey);
            return bkey;
        }
    }

    public class Point : NeoTrace
    {
        private BigInteger _x;
        private BigInteger _y;
        private NeoEntityModel.EntityState _state;
        private byte[] _extension;

        // Accessors
        public static void SetX(Point p, BigInteger value) { p._x = value; p._state = NeoEntityModel.EntityState.SET; }
        public static BigInteger GetX(Point p) { return p._x; }
        public static void SetY(Point p, BigInteger value) { p._y = value; p._state = NeoEntityModel.EntityState.SET; }
        public static BigInteger GetY(Point p) { return p._y; }
        public static void SetExtension(Point p, byte[] value) { p._extension = value; }
        public static byte[] GetExtension(Point p) { return p._extension; }
        public static void Set(Point p, BigInteger xvalue, BigInteger yvalue) { p._x = xvalue; p._y = yvalue; p._state = NeoEntityModel.EntityState.SET; }

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
            p._extension = NeoEntityModel.NullScriptHash;
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
            NeoTrace.Trace(label, p._x, p._y, p._state, p._extension); // long values, state, extension last
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
            LogExt("Missing().p", p);
            return p;
        }

        public static Point Tombstone()
        {
            Point p = new Point();
            p._x = 0;
            p._y = 0;
            p._state = NeoEntityModel.EntityState.TOMBSTONED;
            p._extension = NeoEntityModel.NullScriptHash;
            LogExt("Tombstone().p", p);
            return p;
        }

        // Core methods
        public static Point Bury(byte[] key)
        {
            if (key.Length == 0) return Null(); // TODO - create NeoEntityModel.EntityState.BADKEY?

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
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
                /*STA*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bSTA), p._state.AsBigInteger());
                /*EXT*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bEXT), p._extension);
                /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bX), p._x);
                /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bY), p._y);
            }
            LogExt("Bury(kb).p", p);
            return p; // return Point p to signal if key is Missing or bad key
        }

        public static Point Bury(string key)
        {
            if (key.Length == 0) return Null(); // TODO - create NeoEntityModel.EntityState.BADKEY?

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
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
                /*STA*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sSTA, p._state.AsBigInteger());
                /*EXT*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sEXT, p._extension);
                /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sX, p._x);
                /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sY, p._y);
            }
            LogExt("Bury(ks).p", p);
            return p; // return Point p to signal if key is Missing or bad key
        }

        public static Point BuryElement(NeoVersionedAppUser vau, int index)
        {
            if (NeoVersionedAppUser.IsNull(vau)) // TODO - create NeoEntityModel.EntityState.BADKEY?
            {
                return Point.Null();
            }

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            NeoStorageKey nsk = NeoStorageKey.New(vau, "Point");

            byte[] bkey;
            Point p;
            /*STA*/
            byte[] bsta = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bSTA));
            NeoTrace.Trace("Bury(vau,index).bs", bsta.Length, bsta);
            if (bsta.Length == 0)
            {
                p = Point.Missing();
            }
            else // not MISSING - bury it
            {
                p = Point.Tombstone(); // TODO - should Bury() preserve the exist field values or re-initialize them? Preserve is cheaper but not as private
                /*STA*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bSTA), p._state.AsBigInteger());
                /*EXT*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bEXT), p._extension);
                /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bX), p._x);
                /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bY), p._y);
            }
            LogExt("Bury(vau,i).p", p);
            return p;
        }

        public static bool Put(Point p, byte[] key)
        {
            if (key.Length == 0) return false;

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            byte[] _bkeyTag = Helper.Concat(key, _bclassKeyTag);

            p._state = NeoEntityModel.EntityState.PUTTED;
            /*STA*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bSTA), p._state.AsBigInteger());
            /*EXT*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bEXT), p._extension);
            /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bX), p._x);
            /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bY), p._y);
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
            /*STA*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sSTA, bis);
            /*EXT*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sEXT, p._extension);
            /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sX, p._x);
            /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sY, p._y);
            LogExt("Put(ks).p", p);
            return true;
        }

        public static bool PutElement(Point p, NeoVersionedAppUser vau, int index)
        {
            if (NeoVersionedAppUser.IsNull(vau)) return false;

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            NeoStorageKey nsk = NeoStorageKey.New(vau, "Point");

            byte[] bkey;
            p._state = NeoEntityModel.EntityState.PUTTED;
            /*STA*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bSTA), p._state.AsBigInteger());
            /*EXT*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bEXT), p._extension);
            /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bX), p._x);
            /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bY), p._y);
            LogExt("PutElement(vau,i).p", p);
            return true;
        }

        public static Point Get(byte[] key)
        {
            if (key.Length == 0) return Null();

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
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
                    /*FIELD*/ BigInteger x = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, Helper.Concat(_bkeyTag, _bX)).AsBigInteger();
                    /*FIELD*/ BigInteger y = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, Helper.Concat(_bkeyTag, _bY)).AsBigInteger();
                    p._x = x;
                    p._y = y;
                    p._state = sta;
                    p._state = NeoEntityModel.EntityState.GETTED; /* OVERRIDE */
                    p._extension = bext;
                }
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
                    /*FIELD*/ BigInteger x = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, _skeyTag + _sX).AsBigInteger();
                    /*FIELD*/ BigInteger y = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, _skeyTag + _sY).AsBigInteger();
                    NeoTrace.Trace("Get(ks).x,y", x, y);
                    p._x = x;
                    p._y = y;
                    p._state = sta;
                    p._state = NeoEntityModel.EntityState.GETTED; /* OVERRIDE */
                    p._extension = bext;
                }
            }
            LogExt("Get(ks).p", p);
            return p;
        }

        public static Point GetElement(NeoVersionedAppUser vau, int index)
        {
            if (NeoVersionedAppUser.IsNull(vau)) return Null();

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            NeoStorageKey nsk = NeoStorageKey.New(vau, "Point");

            Point p;
            byte[] bkey;
            /*STA*/
            byte[] bsta = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bSTA));
            NeoTrace.Trace("Get(kb).bs", bsta.Length, bsta);
            if (bsta.Length == 0)
            {
                p = Point.Missing();
            }
            else // not MISSING
            {
                /*EXT*/
                byte[] bext = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bEXT));
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
                    /*FIELD*/
                    BigInteger x = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bX)).AsBigInteger();
                    /*FIELD*/
                    BigInteger y = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bY)).AsBigInteger();
                    p._x = x;
                    p._y = y;
                    p._state = sta;
                    p._state = NeoEntityModel.EntityState.GETTED; /* OVERRIDE */
                    p._extension = bext;
                }
            }
            LogExt("Get(kb).p", p);
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

        public static object Main(string operation, params object[] args)
        {
            string msg = "success";
            NeoTrace.Trace("========================================");
            NeoTrace.Trace("operation", operation, args);
            NeoTrace.Trace("========================================");

            if (operation == "test1")
            {
                msg = test1(args);
            }
            else if (operation == "test2")
            {
                msg = test2(args);
            }
            else if (operation == "test3")
            {
                msg = test3(args); 
            }
            else if (operation == "test4")
            {
                msg = test4(args);
            }
            else if (operation == "test5")
            {
                msg = test5(args);
            }
            else if (operation == "test6")
            {
                msg = test6(args);
            }
            else
            {
                msg = "Unknown operation code";
            }
            NeoTrace.Trace("----------------------------------------");

            return msg;
        }

        public static string test1(object[] args)
        {
            string msg = "success";

            NeoTrace.Trace("NullHash", NeoEntityModel.NullScriptHash);

            NeoTrace.Trace("NeoEntityModel.EntityState...");
            NeoEntityModel.EntityState state1 = NeoEntityModel.EntityState.MISSING;
            NeoTrace.Trace("state", state1);
            int istate = (int)state1;
            NeoTrace.Trace("state1", state1);

            BigInteger bis = state1.AsBigInteger();
            NeoTrace.Trace("bis", bis);

            byte[] bsta = { 0x4 };
            NeoTrace.Trace("bsta", bsta);
            NeoEntityModel.EntityState state2 = NeoEntityModel.BytesToEntityState(bsta);
            NeoTrace.Trace("state2", state2);

            return msg;
        }

        public static string test2(object[] args)
        {
            string msg = "success";

            NeoTrace.Trace("Make P0...");
            Point p0 = Point.New();
            Point.Log("p0", p0);
            Point.SetX(p0, 7);
            Point.SetY(p0, 8);
            Point.Log("p0", p0);
            Point.Set(p0, 9, 10);
            Point.Log("p0", p0);

            NeoTrace.Trace("Make P1...");
            Point p1 = Point.New();
            Point.Set(p1, 2, 4);
            Point.Log("p1", p1);

            NeoTrace.Trace("Make P2...");
            Point p2 = Point.New();
            Point.Set(p2, 15, 16);
            Point.Log("p2", p2);

            NeoTrace.Trace("Make line1...");
            Point[] line1 = new[]
            {
                p1, p2
            };
            NeoTrace.Trace("line1", line1, p1, p2); // TODO: neo-gui doesn't understand this: line1

            NeoTrace.Trace("Add 2 points...");
            Point p3 = Add(line1[0], line1[1]);
            Point.Log("p3", p3);

            return msg;
        }

        public static string test3(object[] args)
        {
            string msg = "success";

            NeoTrace.Trace("Make P1...");
            Point p1 = Point.New();
            Point.Set(p1, 2, 4);
            Point.Log("p1", p1);

            NeoTrace.Trace("Make P2...");
            Point p2 = Point.New();
            Point.Set(p2, 12, 14);
            Point.Log("p2", p2);

            NeoTrace.Trace("Make P3...");
            Point p3 = Point.New();
            Point.Set(p2, 22, 24);
            Point.Log("p3", p3);

            NeoTrace.Trace("Put P1...");
            Point.Put(p1, "p1");
            NeoTrace.Trace("Put P2...");
            Point.Put(p2, "p2");
            NeoTrace.Trace("Put P3...");
            Point.Put(p3, "p3");

            NeoTrace.Trace("Get P1...");
            Point p1get = Point.Get("p1");
            Point.Log("p1get", p1get);
            NeoTrace.Trace("Get P2...");
            Point p2get = Point.Get("p2");
            Point.Log("p2get", p2get);
            NeoTrace.Trace("Get P3...");
            Point p3get = Point.Get("p3");
            Point.Log("p3get", p3get);

            return msg;
        }

        public static string test4(object[] args)
        {
            string msg = "success";

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

            return msg;
        }

        public static string test5(object[] args)
        {
            string msg = "success";

            NeoTrace.Trace("Test NeoStorageKeys...");
            Point p4 = Point.New();
            Point.Set(p4, 10, 20);
            Point.Log("p4", p4);

            string app = "FooBar";
            byte[] user = WIF2AccountAddressScriptHash;
            NeoVersionedAppUser vau = NeoVersionedAppUser.New(app, 1, 0, 2034, user);
            NeoVersionedAppUser.Log("test5.vau", vau);

            int index = 24;
            NeoTrace.Trace("index", index);
            Point.PutElement(p4, vau, index);
            index = 25;
            NeoTrace.Trace("index", index);
            Point.PutElement(p4, vau, index);

            index = 24;
            Point p4get = Point.GetElement(vau, index);
            Point.LogExt("p4get", p4get);

            Point p4bury1 = Point.BuryElement(vau, index);
            Point.LogExt("p4bury1", p4bury1);
            Point p4bury2 = Point.GetElement(vau, index);
            Point.LogExt("p4bury2", p4bury2);

            return msg;
        }

        public static string test6(object[] args)
        {
            string msg = "success";
            int maxIterations = (int)((byte[])args[0]).AsBigInteger();
            if (maxIterations <= 0) maxIterations = 10;
            NeoTrace.Trace("maxIterations", maxIterations);

            byte[] callingUserScriptHash = ExecutionEngine.CallingScriptHash;
            NeoTrace.Trace("callingUserScriptHash", callingUserScriptHash);
            byte[] entryUserScriptHash = ExecutionEngine.EntryScriptHash;
            NeoTrace.Trace("entryUserScriptHash", entryUserScriptHash);
            byte[] executingUserScriptHash = ExecutionEngine.ExecutingScriptHash;
            NeoTrace.Trace("executingUserScriptHash", executingUserScriptHash);
            byte[] invokingUserScriptHash = GetInvokingUserScriptHash();
            NeoTrace.Trace("invokingUserScriptHash", invokingUserScriptHash);

            Point p4 = Point.New();
            Point.Set(p4, 10, 20);
            Point.Log("p4", p4);

            string app = "FooBar";
            NeoVersionedAppUser vau = NeoVersionedAppUser.New(app, 1, 0, 2034, invokingUserScriptHash);
            NeoVersionedAppUser.Log("test6.vau", vau);

            int iteration = 0;
            for (int index = 30; index < 40; index++)
            {
                Point.Set(p4, index, -index);
                Point.PutElement(p4, vau, index);
                iteration++;
                if (iteration > maxIterations) break;
            }

            iteration = 0;
            for (int index = 30; index < 40; index++)
            {
                Point.Set(p4, index, -index);
                Point x = Point.GetElement(vau, index);
                Point.Log("loop.x", x);
                if (Point.GetX(p4) != index || Point.GetY(p4) != -index)
                {
                    msg = ">>>>(x,y) are different";
                    NeoTrace.Trace(msg);
                    break;
                }
                iteration++;
                if (iteration > maxIterations) break;
            }

            return msg;
        }

        private static byte[] GetInvokingUserScriptHash()
        {
            byte[] userScriptHash = NeoEntityModel.NullScriptHash;

            Neo.SmartContract.Framework.Services.Neo.Transaction tx = (Neo.SmartContract.Framework.Services.Neo.Transaction)ExecutionEngine.ScriptContainer;
            Neo.SmartContract.Framework.Services.Neo.TransactionOutput[] outputs = tx.GetOutputs();
            if (outputs.Length > 0)
            {
                userScriptHash = outputs[0].ScriptHash;
            }

            return NeoEntityModel.NullScriptHash;
        }
    }
}
