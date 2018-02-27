using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace Book1
{
    public class Contract1 : SmartContract
    {
        public struct Book
        {
            public decimal price;
            public string title;
            public string author;
        }

        public static object Main()
        {
            Book b = new Book();
            b.author = "Douglas Adams";
            b.title = "HHGTTG";
            Storage.Put(Storage.CurrentContext, "testkey", b);
            byte[] book = Storage.Get(Storage.CurrentContext, "testkey");
            return b; // or return book;
        }
    }
}
