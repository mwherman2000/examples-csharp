using System;
using System.Numerics;
using Neo.SmartContract.Framework;
//using Neo.SmartContract.Framework.Services.Neo; // use fully-qualified references to resolve
using Neo.SmartContract.Framework.Services.System;

// Reference: https://docs.microsoft.com/en-us/dotnet/csharp/codedoc
// Reference: https://marketplace.visualstudio.com/items?itemName=sergeb.GhostDoc
// Reference: https://github.com/EWSoftware/SHFB // Sandcastle github
// Reference: http://ewsoftware.github.io/SHFB/html/b772e00e-1705-4062-adb6-774826ce6700.htm // Sandcastle installation instructions
// Reference: https://github.com/EWSoftware/SHFB/releases // Sandcastle installation
// Reference: https://msdn.microsoft.com/en-us/library/ms669985.aspx // HTML Workshop
// Reference: https://marketplace.visualstudio.com/items?itemName=k--kato.docomment // XML Commentor add-on
// Reference: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/doc-compiler-option // project.xml gen flag

/*
     MIT License

     Copyright (c) 2018 Michael Herman (mwherman@parallelspace.net)

     Permission is hereby granted, free of charge, to any person obtaining a copy
     of this software and associated documentation files (the "Software"), to deal
     in the Software without restriction, including without limitation the rights
     to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
     copies of the Software, and to permit persons to whom the Software is
     furnished to do so, subject to the following conditions:
     The above copyright notice and this permission notice shall be included in all
     copies or substantial portions of the Software.
     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
     LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
     OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
     SOFTWARE.
  */

namespace NeoPersistableClass
{
    /// <summary>
    /// NPCdApp - NEO Persistable Class (NPC) Framework Version 0.1 Reference Implementation
    /// 
    /// Author: Michael Herman
    ///         @mwherman2000
    ///         mwherman@parallelspace.net
    ///         
    /// The NEO Persistable Class (NPC) framework enables efficient object-oriented 
    /// smart contract development using C#.NEO, Visual Studio and the NEO Developer Tools 
    /// to build distributed apps (dApps) on the NEO Blockchain.
    /// 
    /// NPC Levels Supported
    /// ----------------------------------------------------------
    ///	NPC Level 1 Basic
    /// NPC Level 2 State-managed
    /// NPC Level 3 Persistable
    /// NPC Level 4 Collectable
    /// NPC Level 5 Extendible(future work)
    /// NPC Level 6 Authorized(future work)
    /// ----------------------------------------------------------
    /// Main() Parameters: string operation, object[]args
    ///  
    /// Neo-gui Contract Parameter Type: 0710 (string, array)
    /// 
    /// Operation Parameter Values
    /// ----------------------------------------------------------
    /// testall [nIterations] - NPC Level 1-2-3-4 test cases: Run all the tests sequentially. Log the results.
    /// test1   [no args]     - Dump miscellaneous variables to the log
    /// test2   [no args]     - NPC Level 1 test cases: Create 3 Points and a line. Add two Points. Log the results.
    /// test3   [no args]     - NPC Level 2/3 test cases: Create 3 Points and test NPC Level 3 entity persistence. Log the results.
    /// tets4   [no args]     - NPC Level 2/3 test cases: Test IsNull(), IsMissing() and IsExtended(). Log the results.
    /// test5   [no args]     - NPC Level 4 test cases: Test NeoStorageKeys. Log the results.
    /// test6   [nIterations] - NPC Level 4 test cases: Test NeoStorageKeys. Log the results.
    /// </summary>

    /// <summary>
    /// NCPdApp class
    /// Contains the Main() entry point for this dApp smart contract
    /// as well as one function for each test case (in addition to some helper methods)
    /// </summary>
    /// <seealso cref="Neo.SmartContract.Framework.SmartContract" />
    public class NPCdApp : SmartContract
    {
        // Test data: WIF from the NEO privatenet Python environment
        private const string WIF2 = "KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr";
        private const string WIF2AccountAddress = "AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y";
        private const string WIF2AccountPublicKey = "031a6c6fbbdf02ca351745fa86b9ba5a9452d785ac4f7fc2b7548ca2a46c4fcf4a";
        private const string WIF2AccountPrivateKeyHex = "1dd37fba80fec4e6a6f13fd708d8dcb3b29def768017052f6c930fa1c5d90bbb";
        private static readonly byte[] WIF2AccountAddressScriptHash = WIF2AccountAddress.ToScriptHash();

        /// <summary>
        /// Main entry point for the NPCaPP dApp
        /// </summary>
        /// <param name="operation">operation</param>
        /// <param name="args">arguments</param>
        /// <returns>object</returns>
        public static object Main(string operation, params object[] args)
        {
            string msg = "success";
            NeoTrace.Trace("===============================================");
            NeoTrace.Trace("NPCdApp - NEO Persistable Class (NPC) Framework");
            NeoTrace.Trace("NPCdApp - Version 0.1 Reference Implementation");
            NeoTrace.Trace("----------------------------------------------");
            NeoTrace.Trace("operation", operation, args);
            NeoTrace.Trace("===============================================");

            if (operation == "testall" || operation.Length == 0)
            {
                msg = test1(args);
                msg = test2(args);
                msg = test3(args);
                msg = test4(args);
                msg = test5(args);
                msg = test6(args);
            }
            else if (operation == "test1")
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

        /// <summary>
        /// Dump miscellaneous variables to the log
        /// </summary>
        /// <param name="args">arguments</param>
        /// <returns>string</returns>
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

        /// <summary>
        /// NPC Level 1 test case: Create 3 Points and a line. Add two Points. Log the results
        /// </summary>
        /// <param name="args">arguments</param>
        /// <returns>string</returns>
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

        /// <summary>
        /// NPC Level 1 test cases: Create 3 Points and a line. Add two Points. Log the results
        /// </summary>
        /// <param name="args">arguments</param>
        /// <returns>string</returns>
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

        /// <summary>
        /// NPC Level 2/3 test cases: Test IsNull(), IsMissing() and IsExtended(). Log the results
        /// </summary>
        /// <param name="args">arguments</param>
        /// <returns>string</returns>
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

        /// <summary>
        /// NPC Level 4 test cases: Test NeoStorageKeys. Log the results
        /// </summary>
        /// <param name="args">arguments</param>
        /// <returns>string</returns>
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

        /// <summary>
        /// NPC Level 4 test cases: Test NeoStorageKeys. Log the results. Number is iterations is taken from <c>args[0]</c>
        /// </summary>
        /// <param name="args">arguments</param>
        /// <returns>string</returns>
        public static string test6(object[] args)
        {
            string msg = "success";
            int maxIterations = 10;
            if (args.Length > 0)
            {
                maxIterations = (int)((byte[])args[0]).AsBigInteger();
            }
            if (maxIterations <= 0) maxIterations = 10;
            if (maxIterations > 20) maxIterations = 10;
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

        /// <summary>
        /// Add two points arithmetically (helper function)
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns>Point</returns>
        private static Point Add(Point a, Point b)

        {
            Point p = Point.New();
            Point.Set(p, Point.GetX(a) + Point.GetX(b), Point.GetY(a) + Point.GetY(b));
            return p;
        }

        /// <summary>
        /// Gets the invoking userScriptHash (helper function)
        /// </summary>
        /// <returns>userScriptHash</returns>
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

    /// <summary>
    /// Neotrace class
    /// Wrapper for fully-qualified method <c>Neo.SmartContract.Framework.Services.Neo.Runtime.Notify(args)</c>
    /// </summary>
     public class NeoTrace
    {
        /// <summary>
        /// Traces the specified arguments.
        /// </summary>
        /// <param name="args">arguments</param>
        /// <returns>void</returns>
        public static void Trace(params object[] args)
        {
            Neo.SmartContract.Framework.Services.Neo.Runtime.Notify(args);
        }
    }

    /// <summary>
    /// NeoEntityModel class
    /// Utility class for EntityState enum (NPC Level 1-6) as well as helper functions and extensions
    /// like <c>AsBigInteger</c> and <c>BytesToEntityState</c>
    /// </summary>
    public static class NeoEntityModel
    {
        /// <summary>
        /// 
        /// </summary>
        public enum EntityState
        {
            /// <summary>
            /// Null entity state
            /// </summary>
            NULL,
            /// <summary>
            /// Initialized entity state
            /// </summary>
            INIT,
            /// <summary>
            /// Set entity state
            /// </summary>
            SET,
            /// <summary>
            /// Putted entity state
            /// </summary>
            PUTTED,
            /// <summary>
            /// Getted entity state
            /// </summary>
            GETTED,
            /// <summary>
            /// Missing entity state
            /// </summary>
            MISSING,
            /// <summary>
            /// Tombstoned entity state
            /// </summary>
            TOMBSTONED,
            /// <summary>
            /// Notauthorized entity state (Future work)
            /// </summary>
            NOTAUTHORIZED /* Future work */
        }

        /// <summary>
        /// Convert EntityState state to BigInteger
        /// </summary>
        /// <param name="state">state</param>
        /// <returns>BigInteger</returns>
        public static BigInteger AsBigInteger(this EntityState state)
        {
            int istate = (int)state;
            BigInteger bis = istate;
            return bis;
        }

        /// <summary>
        /// Convert state byte[] to EntityState state
        /// </summary>
        /// <param name="bsta">bsta</param>
        /// <returns>EntityState</returns>
        public static EntityState BytesToEntityState(byte[] bsta)
        {
            int ista = (int)bsta.AsBigInteger();
            NeoEntityModel.EntityState sta = (NeoEntityModel.EntityState)ista;
            return sta;
        }

        /// <summary>
        /// Null Script Hash
        /// </summary>
        public static readonly byte[] NullScriptHash = "".ToScriptHash();
        /// <summary>
        /// Null Byte Array
        /// </summary>
        public static readonly byte[] NullByteArray = "".AsByteArray();
    }

    /// <summary>
    /// NeoVersionedAppUser class
    /// Class used to support the efficient creation and management of <c>NeoStorageKeys</c>
    /// </summary>
    public class NeoVersionedAppUser
    {
        /// <summary>
        /// Application name
        /// </summary>
        private byte[] _app;
        /// <summary>
        /// Major version number of the application
        /// </summary>
        private int _major;
        /// <summary>
        /// Minor version number of the application
        /// </summary>
        private int _minor;
        /// <summary>
        /// Build number of the application
        /// </summary>
        private int _build;
        //private int _revision;
        /// <summary>
        /// User Script Hash
        /// </summary>
        private byte[] _userScriptHash;
        /// <summary>
        /// Entity state
        /// </summary>
        private NeoEntityModel.EntityState _state;

        /// <summary>
        /// Sets the name of the application.
        /// </summary>
        /// <param name="vau">vau</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetAppName(NeoVersionedAppUser vau, byte[] value) { vau._app = value; vau._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets the application name as byte array.
        /// </summary>
        /// <param name="vau">vau</param>
        /// <returns>string</returns>
        public static byte[] GetAppNameAsByteArray(NeoVersionedAppUser vau) { return vau._app; }
        /// <summary>
        /// Sets the name of the application.
        /// </summary>
        /// <param name="vau">vau</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetAppName(NeoVersionedAppUser vau, string value) { vau._app = value.AsByteArray(); vau._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets the application name as string.
        /// </summary>
        /// <param name="vau">vau</param>
        /// <returns>string</returns>
        public static string GetAppNameAsString(NeoVersionedAppUser vau) { return vau._app.AsString(); }
        /// <summary>
        /// Sets the major version number of the application
        /// </summary>
        /// <param name="vau">vau</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetMajor(NeoVersionedAppUser vau, int value) { vau._major = value; vau._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets the major version number of the application
        /// </summary>
        /// <param name="vau">vau</param>
        /// <returns>int</returns>
        public static int GetMajor(NeoVersionedAppUser vau) { return vau._major; }
        /// <summary>
        /// Sets the minor version number of the application
        /// </summary>
        /// <param name="vau">vau</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetMinor(NeoVersionedAppUser vau, int value) { vau._minor = value; vau._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets the minor version number of the application
        /// </summary>
        /// <param name="vau">vau</param>
        /// <returns>int</returns>
        public static int GetMinor(NeoVersionedAppUser vau) { return vau._minor; }
        /// <summary>
        /// Sets the build number
        /// </summary>
        /// <param name="vau">vau</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetBuild(NeoVersionedAppUser vau, int value) { vau._build = value; vau._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets the build number
        /// </summary>
        /// <param name="vau">vau</param>
        /// <returns>int</returns>
        public static int GetBuild(NeoVersionedAppUser vau) { return vau._build; }
        //public static void SetRevision(NeoVersionedAppUser vau, int value) { vau._revision = value; vau._state = NeoEntityModel.EntityState.SET; }
        //public static int GetRevision(NeoVersionedAppUser vau) { return vau._revision; }
        /// <summary>
        /// Sets the userScriptHash.
        /// </summary>
        /// <param name="vau">vau</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetUserScriptHash(NeoVersionedAppUser vau, byte[] value) { vau._userScriptHash = value; vau._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets the userScriptHash.
        /// </summary>
        /// <param name="vau">vau</param>
        /// <returns>userScriptHash</returns>
        public static byte[] GetUserScriptHash(NeoVersionedAppUser vau) { return vau._userScriptHash; }
        /// <summary>
        /// Sets the specified versioned app user.
        /// </summary>
        /// <param name="vau">vau</param>
        /// <param name="app">application</param>
        /// <param name="major">major</param>
        /// <param name="minor">minor</param>
        /// <param name="build">build</param>
        /// <param name="userScriptHash">userScriptHash</param>
        /// <returns>void</returns>
        public static void Set(NeoVersionedAppUser vau, byte[] app, int major, int minor, int build, /*int revision,*/ byte[] userScriptHash)
        {
            vau._app = app; vau._major = major; vau._minor = minor; vau._build = build; /*vau._revision = revision;*/
            vau._userScriptHash = userScriptHash; vau._state = NeoEntityModel.EntityState.SET;
        }
        /// <summary>
        /// Sets the specified version app user.
        /// </summary>
        /// <param name="vau">vau</param>
        /// <param name="app">application</param>
        /// <param name="major">major</param>
        /// <param name="minor">minor</param>
        /// <param name="build">build</param>
        /// <param name="userScriptHash">userScriptHash</param>
        /// <returns>void</returns>
        public static void Set(NeoVersionedAppUser vau, string app, int major, int minor, int build, /*int revision,*/ byte[] userScriptHash)
        {
            vau._app = app.AsByteArray(); vau._major = major; vau._minor = minor; vau._build = build; /*vau._revision = revision;*/
            vau._userScriptHash = userScriptHash;  vau._state = NeoEntityModel.EntityState.SET;
        }

        // Factory methods

        /// <summary>
        /// Prevents a default instance of the <see cref="NeoVersionedAppUser"/> class from being created.
        /// </summary>
        private NeoVersionedAppUser()
        {
        }

        /// <summary>
        /// Initializes the specified vau.
        /// </summary>
        /// <param name="vau">vau</param>
        /// <returns>NeoVersionedAppUser</returns>
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

        /// <summary>
        /// News this instance.
        /// </summary>
        /// <returns>NeoVersionedAppUser</returns>
        public static NeoVersionedAppUser New()
        {
            NeoVersionedAppUser vau = new NeoVersionedAppUser();
            _Initialize(vau);
            LogExt("New().vau", vau);
            return vau;
        }

        /// <summary>
        /// News the specified application.
        /// </summary>
        /// <param name="app">application</param>
        /// <param name="major">major</param>
        /// <param name="minor">minor</param>
        /// <param name="build">build</param>
        /// <param name="userScriptHash">userScriptHash</param>
        /// <returns>NeoVersionedAppUser</returns>
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

        /// <summary>
        /// News the specified application.
        /// </summary>
        /// <param name="app">application</param>
        /// <param name="major">major</param>
        /// <param name="minor">minor</param>
        /// <param name="build">build</param>
        /// <param name="userScriptHash">userScriptHash</param>
        /// <returns>NeoVersionedAppUser</returns>
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

        /// <summary>
        /// Create a new entity representing a Null entity
        /// </summary>
        /// <returns>NeoVersionedAppUser</returns>
        public static NeoVersionedAppUser Null()
        {
            NeoVersionedAppUser vau = new NeoVersionedAppUser();
            _Initialize(vau);
            LogExt("Null().vau", vau);
            return vau;
        }

        // EntityState wrapper methods

        /// <summary>
        /// Test whether the specified NeoVersionedAppUser is Null.
        /// </summary>
        /// <param name="vau">vau</param>
        /// <returns>
        ///   <c>true</c> if the specified vau is null; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNull(NeoVersionedAppUser vau)
        {
            return (vau._state == NeoEntityModel.EntityState.NULL);
        }

        // Log/trace methods
        /// <summary>
        /// Logs the specified label.
        /// </summary>
        /// <param name="label">label</param>
        /// <param name="vau">vau</param>
        /// <returns>void</returns>
        public static void Log(string label, NeoVersionedAppUser vau)
        {
            NeoTrace.Trace(label, vau._app, vau._major, vau._minor, vau._build, /*vau._revision,*/ vau._userScriptHash);
        }

        /// <summary>
        /// Logs the ext.
        /// </summary>
        /// <param name="label">label</param>
        /// <param name="vau">vau</param>
        /// <returns>void</returns>
        public static void LogExt(string label, NeoVersionedAppUser vau)
        {
            NeoTrace.Trace(label, vau._app, vau._major, vau._minor, vau._build, /*vau._revision,*/ vau._userScriptHash, vau._state); // long values, state, extension last
        }
    }

    /// <summary>
    /// NeoStorageKey class
    /// Used to manage NeoStorageKeys (NSKs) and the serialization of NSKs into
    /// NeoStorageKey Object Notation (NSKON)
    /// </summary>
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

        /// <summary>
        /// Application name
        /// </summary>
        private byte[] _app;
        /// <summary>
        /// Major version of the application
        /// </summary>
        private int _major;
        /// <summary>
        /// Minor version of the application
        /// </summary>
        private int _minor;
        /// <summary>
        /// Build number of the application
        /// </summary>
        private int _build;
        //private int _revision;
        /// <summary>
        /// User script hash
        /// </summary>
        private byte[] _userScriptHash;
        /// <summary>
        /// Class name
        /// </summary>
        private byte[] _className;
        /// <summary>
        /// Index into a collection
        /// </summary>
        private int _index;
        /// <summary>
        /// Entity field name
        /// </summary>
        private string _fieldName;
        /// <summary>
        /// Entity state
        /// </summary>
        private NeoEntityModel.EntityState _state;

        /// <summary>
        /// Sets the name of the application.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetAppName(NeoStorageKey nsk, byte[] value) { nsk._app = value; nsk._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets the application name as byte array.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <returns>app</returns>
        public static byte[] GetAppNameAsByteArray(NeoStorageKey nsk) { return nsk._app; }
        /// <summary>
        /// Sets the name of the application.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetAppName(NeoStorageKey nsk, string value) { nsk._app = value.AsByteArray(); nsk._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets the application name as string.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <returns>string</returns>
        public static string GetAppNameAsString(NeoStorageKey nsk) { return nsk._app.AsString(); }
        /// <summary>
        /// Sets the major version number of an application.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetMajor(NeoStorageKey nsk, int value) { nsk._major = value; nsk._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets the major version number of an application.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <returns>int</returns>
        public static int GetMajor(NeoStorageKey nsk) { return nsk._major; }
        /// <summary>
        /// Sets the minor version number of an application
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetMinor(NeoStorageKey nsk, int value) { nsk._minor = value; nsk._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets the minor version number of an application.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <returns>int</returns>
        public static int GetMinor(NeoStorageKey nsk) { return nsk._minor; }
        /// <summary>
        /// Sets the build number of an application
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetBuild(NeoStorageKey nsk, int value) { nsk._build = value; nsk._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets the build number of an application
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <returns>int</returns>
        public static int GetBuild(NeoStorageKey nsk) { return nsk._build; }
        //public static void SetRevision(NeoStorageKey nsk, int value) { nsk._revision = value; nsk._state = NeoEntityModel.EntityState.SET; }
        //public static int GetRevision(NeoStorageKey nsk) { return nsk._revision; }
        /// <summary>
        /// Sets the userScriptHash of a NEO Storage Key.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetUserScriptHash(NeoStorageKey nsk, byte[] value) { nsk._userScriptHash = value; nsk._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets the userScriptHash of a NEO Storage Key.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <returns>userScriptHash</returns>
        public static byte[] GetUserScriptHash(NeoStorageKey nsk) { return nsk._userScriptHash; }
        /// <summary>
        /// Sets the class name of a NEO Storage Key
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetClassName(NeoStorageKey nsk, byte[] value) { nsk._className = value; nsk._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets the class name of a NEO Storage Key as byte array.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <returns>className</returns>
        public static byte[] GetClassNameAsByteArray(NeoStorageKey nsk) { return nsk._className; }
        /// <summary>
        /// Sets the class name of the NEO Storage Key
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetClassName(NeoStorageKey nsk, string value) { nsk._className = value.AsByteArray(); nsk._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets the class name of a NEO Storage Key as string.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <returns>string</returns>
        public static string GetClassNameAsString(NeoStorageKey nsk) { return nsk._className.AsString(); }
        /// <summary>
        /// Sets the index of a NEO Storage Key
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetIndex(NeoStorageKey nsk, int value) { nsk._index = value; nsk._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets the index of a NEO Storage Key
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <returns>int</returns>
        public static int GetIndex(NeoStorageKey nsk) { return nsk._index; }
        /// <summary>
        /// Sets the class field name of the NEO Storage Key.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetFieldName(NeoStorageKey nsk, string value) { nsk._fieldName = value; nsk._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets the field name of the NEO Storage Key.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <returns>string</returns>
        public static string GetFieldName(NeoStorageKey nsk) { return nsk._fieldName; }
        /// <summary>
        /// Sets the specified NEO Storage Key field values.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <param name="app">application</param>
        /// <param name="major">major</param>
        /// <param name="minor">minor</param>
        /// <param name="build">build</param>
        /// <param name="userScriptHash">userScriptHash</param>
        /// <param name="className">class name</param>
        /// <param name="index">index</param>
        /// <param name="fieldName">field name</param>
        /// <returns>void</returns>
        public static void Set(NeoStorageKey nsk, byte[] app, int major, int minor, int build, /*int revision,*/ byte[] userScriptHash, byte[] className, int index, string fieldName)
        {
            nsk._app = app; nsk._major = major; nsk._minor = minor; nsk._build = build; /*nsk._revision = revision*/;
            nsk._userScriptHash = userScriptHash; nsk._className = className; nsk._index = index; nsk._fieldName = fieldName;
            nsk._state = NeoEntityModel.EntityState.SET;
        }
        /// <summary>
        /// Sets the specified NEO Storage Key field values.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <param name="app">application</param>
        /// <param name="major">major</param>
        /// <param name="minor">minor</param>
        /// <param name="build">build</param>
        /// <param name="userScriptHash">userScriptHash</param>
        /// <param name="className">class name</param>
        /// <param name="index">index</param>
        /// <param name="fieldName">field name</param>
        /// <returns>void</returns>
        public static void Set(NeoStorageKey nsk, string app, int major, int minor, int build, /*int revision,*/ byte[] userScriptHash, string className, int index, string fieldName)
        {
            nsk._app = app.AsByteArray(); nsk._major = major; nsk._minor = minor; nsk._build = build; /*nsk._revision = revision*/;
            nsk._userScriptHash = userScriptHash; nsk._className = className.AsByteArray(); nsk._index = index; nsk._fieldName = fieldName;
            nsk._state = NeoEntityModel.EntityState.SET;
        }
        /// <summary>
        /// Sets the specified NEO Storage Key field values.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <param name="vau">vau</param>
        /// <param name="userScriptHash">userScriptHash</param>
        /// <param name="className">class name</param>
        /// <param name="index">index</param>
        /// <param name="fieldName">field name</param>
        /// <returns>void</returns>
        public static void Set(NeoStorageKey nsk, NeoVersionedAppUser vau, byte[] userScriptHash, byte[] className, int index, string fieldName)
        {
            nsk._major = NeoVersionedAppUser.GetMajor(vau); nsk._minor = NeoVersionedAppUser.GetMinor(vau); nsk._build = NeoVersionedAppUser.GetBuild(vau); /*nsk._revision = NeoVersionedAppUser.GetRevision(vau);*/
            nsk._userScriptHash = NeoVersionedAppUser.GetUserScriptHash(vau);
            nsk._className = className; nsk._index = index; nsk._fieldName = fieldName;
            nsk._state = NeoEntityModel.EntityState.SET;
        }
        /// <summary>
        /// Sets the specified NEO Storage Key field values.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <param name="vau">vau</param>
        /// <param name="userScriptHash">userScriptHash</param>
        /// <param name="className">class name</param>
        /// <param name="index">index</param>
        /// <param name="fieldName">field name</param>
        /// <returns>void</returns>
        public static void Set(NeoStorageKey nsk, NeoVersionedAppUser vau, byte[] userScriptHash, string className, int index, string fieldName)
        {
            nsk._major = NeoVersionedAppUser.GetMajor(vau); nsk._minor = NeoVersionedAppUser.GetMinor(vau); nsk._build = NeoVersionedAppUser.GetBuild(vau); /*nsk._revision = NeoVersionedAppUser.GetRevision(vau);*/
            nsk._userScriptHash = NeoVersionedAppUser.GetUserScriptHash(vau);
            nsk._className = className.AsByteArray(); nsk._index = index; nsk._fieldName = fieldName;
            nsk._state = NeoEntityModel.EntityState.SET;
        }

        // Factory methods
        /// <summary>
        /// Prevents a default instance of the <see cref="NeoStorageKey"/> class from being created.
        /// </summary>
        private NeoStorageKey()
        {
        }

        /// <summary>
        /// Initializes the specified NSK.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <returns>NeoStorageKey</returns>
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

        /// <summary>
        /// News this instance.
        /// </summary>
        /// <returns>NeoStorageKey</returns>
        public static NeoStorageKey New()
        {
            NeoStorageKey nsk = new NeoStorageKey();
            _Initialize(nsk);
            LogExt("New().nsk", nsk);
            return nsk;
        }

        /// <summary>
        /// News the specified application.
        /// </summary>
        /// <param name="app">application</param>
        /// <param name="major">major</param>
        /// <param name="minor">minor</param>
        /// <param name="build">build</param>
        /// <param name="userScriptHash">userScriptHash</param>
        /// <param name="className">class name</param>
        /// <param name="index">index</param>
        /// <param name="fieldName">field name</param>
        /// <returns>NeoStorageKey</returns>
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

        /// <summary>
        /// News the specified application.
        /// </summary>
        /// <param name="app">application</param>
        /// <param name="major">major</param>
        /// <param name="minor">minor</param>
        /// <param name="build">build</param>
        /// <param name="userScriptHash">userScriptHash</param>
        /// <param name="className">class name</param>
        /// <param name="index">index</param>
        /// <param name="fieldName">field name</param>
        /// <returns>NeoStorageKey</returns>
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

        /// <summary>
        /// News the specified vau.
        /// </summary>
        /// <param name="vau">vau</param>
        /// <param name="className">class name</param>
        /// <returns>NeoStorageKey</returns>
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

        /// <summary>
        /// News the specified vau.
        /// </summary>
        /// <param name="vau">vau</param>
        /// <param name="className">class name</param>
        /// <returns>NeoStorageKey</returns>
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

        /// <summary>
        /// Create a new entity representing a Null entity
        /// </summary>
        /// <returns>NeoStorageKey</returns>
        public static NeoStorageKey Null()
        {
            NeoStorageKey nsk = new NeoStorageKey();
            _Initialize(nsk);
            LogExt("Null().nsk", nsk);
            return nsk;
        }

        // EntityState wrapper methods

        /// <summary>
        /// Test whether the specified NeoStorageKey is Null.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <returns>
        ///   <c>true</c> if the specified NSK is null; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNull(NeoStorageKey nsk)
        {
            return (nsk._state == NeoEntityModel.EntityState.NULL);
        }

        // Log/trace methods

        /// <summary>
        /// Logs the specified label.
        /// </summary>
        /// <param name="label">label</param>
        /// <param name="nsk">NSK</param>
        /// <returns>void</returns>
        public static void Log(string label, NeoStorageKey nsk)
        {
            NeoTrace.Trace(label, nsk._app, nsk._major, nsk._minor, nsk._build, /*nsk._revision,*/ nsk._className, nsk._index, nsk._fieldName, nsk._userScriptHash);
        }

        /// <summary>
        /// Logs the ext.
        /// </summary>
        /// <param name="label">label</param>
        /// <param name="nsk">NSK</param>
        /// <returns>void</returns>
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

        /// <summary>
        /// Compute a NEO Storage Key.
        /// </summary>
        /// <param name="nsk">NSK</param>
        /// <param name="index">index</param>
        /// <param name="fieldName">field name</param>
        /// <returns>bNeoStorageKey</returns>
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

    /// <summary>
    /// Point class
    /// Reference implementation of a NPC class consisting of a pair of x and y coordinates
    /// </summary>
    /// <seealso cref="NeoPersistableClass.NeoTrace" />
    public class Point : NeoTrace
    {
        /// <summary>
        /// Core fields (NPC Level all)
        /// </summary>

        /// <summary>
        /// X coordinate
        /// </summary>
        private BigInteger _x;
        /// <summary>
        /// Y coordinate
        /// </summary>
        private BigInteger _y;
        /// <summary>
        /// Entity state
        /// </summary>
        private NeoEntityModel.EntityState _state;
        /// <summary>
        /// Extension script hash
        /// </summary>
        private byte[] _extension;

        // Accessors (NPC Level all)

        /// <summary>
        /// Gets X coordinate
        /// </summary>
        /// <param name="p">p</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetX(Point p, BigInteger value) { p._x = value; p._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets X coordinate
        /// </summary>
        /// <param name="p">p</param>
        /// <returns>BigInteger</returns>
        public static BigInteger GetX(Point p) { return p._x; }
        /// <summary>
        /// Sets Y coordinate
        /// </summary>
        /// <param name="p">p</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetY(Point p, BigInteger value) { p._y = value; p._state = NeoEntityModel.EntityState.SET; }
        /// <summary>
        /// Gets Y coordinate
        /// </summary>
        /// <param name="p">p</param>
        /// <returns>BigInteger</returns>
        public static BigInteger GetY(Point p) { return p._y; }
        /// <summary>
        /// Sets Extension script hash
        /// </summary>
        /// <param name="p">p</param>
        /// <param name="value">value</param>
        /// <returns>void</returns>
        public static void SetExtension(Point p, byte[] value) { p._extension = value; }
        /// <summary>
        /// Gets Extension script hash.
        /// </summary>
        /// <param name="p">p</param>
        /// <returns>extension</returns>
        public static byte[] GetExtension(Point p) { return p._extension; }
        /// <summary>
        /// Sets the specified Point field values
        /// </summary>
        /// <param name="p">p</param>
        /// <param name="xvalue">xvalue</param>
        /// <param name="yvalue">yvalue</param>
        /// <returns>void</returns>
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

        // Metadata fields (NPC Level all)        

        private const string _className = "Point";
        private const string _sX = "X";
        private const string _sY = "Y";
        private const string _sSTA = "_STA";
        private const string _sEXT = "_EXT";
        private static readonly byte[] _bX = Helper.AsByteArray(_sX);
        private static readonly byte[] _bY = Helper.AsByteArray(_sY);
        private static readonly byte[] _bSTA = Helper.AsByteArray(_sSTA);
        private static readonly byte[] _bEXT = Helper.AsByteArray(_sEXT);

        // System fields (NPC Level all)
        
        private const string _classKeyTag = "/#" + _className + ".";
        private static readonly byte[] _bclassKeyTag = Helper.AsByteArray(_classKeyTag);

        // Factory methods (NPC Level all)

        /// <summary>
        /// Prevents a default instance of the <see cref="Point"/> class from being created.
        /// </summary>
        private Point()
        {
        }

        /// <summary>
        /// Initializes the specified Point field values
        /// </summary>
        /// <param name="p">p</param>
        /// <returns>Point</returns>
        private static Point _Initialize(Point p)
        {
            p._x = 0;
            p._y = 0;
            p._state = NeoEntityModel.EntityState.NULL;
            p._extension = NeoEntityModel.NullScriptHash;
            LogExt("_Initialize(p).p", p);
            return p;
        }

        /// <summary>
        /// Create a new entity initialized with "zero" values
        /// </summary>
        /// <returns>
        /// Point
        /// </returns>
        public static Point New()
        {
            Point p = new Point();
            _Initialize(p);
            LogExt("New().p", p);
            return p;
        }

        /// <summary>
        /// Create a new entity initialized with (x,y) values
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>
        /// Point
        /// </returns>
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

        /// <summary>
        /// Create a new entity representing a Null entity
        /// </summary>
        /// <returns>
        /// Point
        /// </returns>
        public static Point Null()
        {
            Point p = new Point();
            _Initialize(p);
            LogExt("Null().p", p);
            return p;
        }

        // EntityState wrapper methods (NPC Level all)

        /// <summary>
        /// Test whether the specified entity is Null.
        /// </summary>
        /// <param name="p">p</param>
        /// <returns>
        /// bool
        /// </returns>
        public static bool IsNull(Point p)
        {
            return (p._state == NeoEntityModel.EntityState.NULL);
        }

        // Logging methods (NPC Level all)

        /// <summary>
        /// Log the entity's core fields
        /// </summary>
        /// <param name="label">label</param>
        /// <param name="p">p</param>
        /// <returns>void</returns>
        public static void Log(string label, Point p)
        {
            NeoTrace.Trace(label, p._x, p._y);
        }

        /// <summary>
        /// Log the entity's core and system fields
        /// </summary>
        /// <param name="label">label</param>
        /// <param name="p">p</param>
        /// <returns>void</returns>
        public static void LogExt(string label, Point p)
        {
            NeoTrace.Trace(label, p._x, p._y, p._state, p._extension); // long values, state, extension last
        }

        // Persistable methods (NPC Level 2)

        /// <summary>
        /// Test whether the specified entity is Missing.
        /// </summary>
        /// <param name="p">p</param>
        /// <returns>
        /// bool
        /// </returns>
        public static bool IsMissing(Point p)
        {
            return (p._state == NeoEntityModel.EntityState.MISSING);
        }

        /// <summary>
        /// Create a new entity representing a Missing entity
        /// </summary>
        /// <returns>
        /// Point
        /// </returns>
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

        /// <summary>
        /// Put an entity into Storage based on a byte[] valued key (NPC Level 2)
        /// </summary>
        /// <param name="p">p</param>
        /// <param name="bkey">bkey</param>
        /// <returns>bool</returns>
        public static bool Put(Point p, byte[] bkey)
        {
            if (bkey.Length == 0) return false;

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            byte[] _bkeyTag = Helper.Concat(bkey, _bclassKeyTag);

            p._state = NeoEntityModel.EntityState.PUTTED;
            /*STA*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bSTA), p._state.AsBigInteger());
            /*EXT*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bEXT), p._extension);
            /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bX), p._x);
            /*FIELD*/ Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bY), p._y);
            LogExt("Put(bkey).p", p);
            return true;
        }

        /// <summary>
        /// Put an entity into Storage based on a string valued key (NPC Level 2)
        /// </summary>
        /// <param name="p">p</param>
        /// <param name="skey">skey</param>
        /// <returns>bool</returns>
        public static bool Put(Point p, string skey)
        {
            if (skey.Length == 0) return false;
            LogExt("Put(ks).p", p);

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            string _skeyTag = skey + _classKeyTag;
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

        /// <summary>
        /// Get an entity from Storage based on a byte[] valued key (NPC Level 2)
        /// </summary>
        /// <param name="bkey">bkey</param>
        /// <returns>Point</returns>
        public static Point Get(byte[] bkey)
        {
            if (bkey.Length == 0) return Null();

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            byte[] _bkeyTag = Helper.Concat(bkey, _bclassKeyTag);

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

        /// <summary>
        /// Get an entity from Storage based on a string valued key (NPC Level 2)
        /// </summary>
        /// <param name="skey">skey</param>
        /// <returns>Point</returns>
        public static Point Get(string skey)
        {
            if (skey.Length == 0) return Null();

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            string _skeyTag = skey + _classKeyTag;

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

        // Deletable methods (NPC Level 3)

        /// <summary>
        /// Test whether the specified entity is Buried (Tombstoned)
        /// </summary>
        /// <param name="p">p</param>
        /// <returns>
        /// bool
        /// </returns>
        public static bool IsBuried(Point p)
        {
            return (p._state == NeoEntityModel.EntityState.TOMBSTONED);
        }

        /// <summary>
        /// Create a new entity representing a Tombstoned entity (NPC Level 3)
        /// </summary>
        /// <returns>
        /// Point
        /// </returns>
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

        /// <summary>
        /// Bury an entity in Storage based on a byte[] valued key (NPC Level 3)
        /// </summary>
        /// <param name="bkey">bkey</param>
        /// <returns>Point</returns>
        public static Point Bury(byte[] bkey)
        {
            if (bkey.Length == 0) return Null(); // TODO - create NeoEntityModel.EntityState.BADKEY?

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            byte[] _bkeyTag = Helper.Concat(bkey, _bclassKeyTag);

            Point p;
            /*STA*/
            byte[] bsta = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, Helper.Concat(_bkeyTag, _bSTA));
            NeoTrace.Trace("Bury(kb).bs", bsta.Length, bsta);
            if (bsta.Length == 0)
            {
                p = Point.Missing();
            }
            else // not MISSING - bury it
            {
                p = Point.Tombstone(); // TODO - should Bury() preserve the exist field values or re-initialize them? Preserve is cheaper but not as private
                /*STA*/
                Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bSTA), p._state.AsBigInteger());
                /*EXT*/
                Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bEXT), p._extension);
                /*FIELD*/
                Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bX), p._x);
                /*FIELD*/
                Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, Helper.Concat(_bkeyTag, _bY), p._y);
            }
            LogExt("Bury(kb).p", p);
            return p; // return Point p to signal if key is Missing or bad key
        }

        /// <summary>
        /// Bury an entity in Storage based on a string valued key (NPC Level 3)
        /// </summary>
        /// <param name="skey">skey</param>
        /// <returns>Point</returns>
        public static Point Bury(string skey)
        {
            if (skey.Length == 0) return Null(); // TODO - create NeoEntityModel.EntityState.BADKEY?

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            string _skeyTag = skey + _classKeyTag;

            Point p;
            /*STA*/
            byte[] bsta = Neo.SmartContract.Framework.Services.Neo.Storage.Get(ctx, _skeyTag + _sSTA);
            NeoTrace.Trace("Bury(ks).bs", bsta.Length, bsta);
            if (bsta.Length == 0)
            {
                p = Point.Missing();
            }
            else // not MISSING - bury it
            {
                p = Point.Tombstone(); // TODO - should Bury() preserve the exist field values or re-initialize them? Preserve is cheaper but not as private
                /*STA*/
                Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sSTA, p._state.AsBigInteger());
                /*EXT*/
                Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sEXT, p._extension);
                /*FIELD*/
                Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sX, p._x);
                /*FIELD*/
                Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, _skeyTag + _sY, p._y);
            }
            LogExt("Bury(ks).p", p);
            return p; // return Point p to signal if key is Missing or bad key
        }

        /// <summary>
        /// Collectible methods (NPC Level 4)
        /// </summary>
        /// <param name="p">p</param>
        /// <param name="vau">vau</param>
        /// <param name="index">index</param>
        /// <returns>
        /// bool
        /// </returns>
        public static bool PutElement(Point p, NeoVersionedAppUser vau, int index)
        {
            if (NeoVersionedAppUser.IsNull(vau)) return false;

            Neo.SmartContract.Framework.Services.Neo.StorageContext ctx = Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext;
            NeoStorageKey nsk = NeoStorageKey.New(vau, "Point");

            byte[] bkey;
            p._state = NeoEntityModel.EntityState.PUTTED;
            /*STA*/
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bSTA), p._state.AsBigInteger());
            /*EXT*/
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bEXT), p._extension);
            /*FIELD*/
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bX), p._x);
            /*FIELD*/
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bY), p._y);
            LogExt("PutElement(vau,i).p", p);
            return true;
        }

        /// <summary>
        /// Get an element of an array of entities from Storage based on a NeoStorageKey (NPC Level 4)
        /// </summary>
        /// <param name="vau">vau</param>
        /// <param name="index">index</param>
        /// <returns>
        /// Point
        /// </returns>
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

        /// <summary>
        /// Bury an element of an array of entities in Storage based on a NeoStorageKey (NPC Level 4)
        /// </summary>
        /// <param name="vau">vau</param>
        /// <param name="index">index</param>
        /// <returns>
        /// Point
        /// </returns>
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
                /*STA*/
                Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bSTA), p._state.AsBigInteger());
                /*EXT*/
                Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bEXT), p._extension);
                /*FIELD*/
                Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bX), p._x);
                /*FIELD*/
                Neo.SmartContract.Framework.Services.Neo.Storage.Put(ctx, bkey = NeoStorageKey.StorageKey(nsk, index, _bY), p._y);
            }
            LogExt("Bury(vau,i).p", p);
            return p;
        }

        // Extensible methods (NPC Level 5 - Future Work)

        /// <summary>
        /// Test whether the specified entity has been Extended.
        /// </summary>
        /// <param name="p">p</param>
        /// <returns>
        /// bool
        /// </returns>
        public static bool IsExtended(Point p)
        {
            return (p._extension != NeoEntityModel.NullScriptHash);
        }
    }
}
