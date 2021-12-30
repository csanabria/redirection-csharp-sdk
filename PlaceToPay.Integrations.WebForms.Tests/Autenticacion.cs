using PlacetoPay.Integrations.Library.CSharp.Carrier;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AuthenticationRedirection
{
    ////class Authentication
    ////{
    //    /**
    //    * Example of use, in your code you can ignore this function
    //    */
    //    //static void Main(string[] args)
    //    //{
    //    //    String login = "usuarioprueba";
    //    //    String tranKey = "ABCD1234";

    //    //    Authentication auth = new Authentication(login, tranKey);

    //    //    // Example of the values to use. YOU NEED TO CHANGE FOR YOUR OWN LOGIN AND TRANKEY
    //    //    System.Console.WriteLine("Login: " + auth.getLogin());
    //    //    System.Console.WriteLine("TranKey: " + auth.getTranKey());
    //    //    System.Console.WriteLine("Seed: " + auth.getSeed());
    //    //    System.Console.WriteLine("Nonce: " + auth.getNonce());

    //    //    // This is just a quick test, IGNORE IT
    //    //    String nonce = "WmEyvut9GgvcMWrV";
    //    //    String seed = "2016-08-30T16:21:35+00:00";
    //    //    auth.setNonce(nonce).setSeed(seed);
    //    //    if (auth.getTranKey().Equals("i/RFwSHAh8d7YgtO3HME5kCnYy8=") && auth.getNonce().Equals("V21FeXZ1dDlHZ3ZjTVdyVg=="))
    //    //    {
    //    //        System.Console.WriteLine("--- Quick test passed ---");
    //    //    }
    //    //    else
    //    //    {
    //    //        System.Console.WriteLine("--- Quick test FAILING ---");
    //    //    }

    //    //    Console.ReadLine();
    //    //}

    //    String login;
    //    String tranKey;
    //    String seed;
    //    String nonce;

    //    public Authentication(String login, String tranKey)
    //    {
    //        this.login = login;
    //        this.tranKey = tranKey;
    //        Generate();
    //    }

    //    /**
    //     * Invoque this function each time that you use this class to generate a WS call
    //     * this will save the need to construct a new class every time
    //     */
    //    public Authentication Generate()
    //    {
    //        nonce = (new Random()).GetHashCode().ToString();
    //        seed = (DateTime.Now).ToString("yyyy-MM-ddTHH:mm:sszzz");
    //        return this;
    //    }

    //    public String getLogin()
    //    {
    //        return this.login;
    //    }

    //    public String getTranKey()
    //    {
    //        return Base64(ToSha1(nonce + seed + tranKey));
    //    }

    //    public String getSeed()
    //    {
    //        return this.seed;
    //    }

    //    public String getNonce()
    //    {
    //        return Base64(nonce);
    //    }

    //    public Authentication setNonce(String nonce)
    //    {
    //        this.nonce = nonce;
    //        return this;
    //    }

    //    public Authentication setSeed(String seed)
    //    {
    //        this.seed = seed;
    //        return this;
    //    }

    //    static public String Base64(byte[] input)
    //    {
    //        return System.Convert.ToBase64String(input);
    //    }

    //    static public String Base64(String input)
    //    {
    //        if (input != null)
    //        {
    //            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input));
    //        }
    //        return "";
    //    }

    //    static public byte[] ToSha1(String text)
    //    {
    //        System.Security.Cryptography.SHA1 hashString = new System.Security.Cryptography.SHA1CryptoServiceProvider();
    //        return hashString.ComputeHash(ToStream(text));
    //    }

    //    static public Stream ToStream(String text)
    //    {
    //        MemoryStream stream = new MemoryStream();
    //        StreamWriter sw = new StreamWriter(stream);
    //        sw.Write(text);
    //        sw.Flush();
    //        stream.Position = 0;
    //        return stream;
    //    }

    //}
}