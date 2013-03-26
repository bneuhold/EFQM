using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace MRI.Tools
{
    public class Encryption64Util
    {
        #region members

        private const string DEFAULT_KEY = "#kl?+@<z";

        #endregion

        public static string Encrypt(string stringToEncrypt)
        {
            string key = System.Configuration.ConfigurationManager.AppSettings.Get("CryptoKey");
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream;

            // Check whether the key is valid, otherwise make it valid
            CheckKey(ref key);

            des.Key = HashKey(key, des.KeySize / 8);
            des.IV = HashKey(key, des.KeySize / 8);
            byte[] inputBytes = Encoding.UTF8.GetBytes(stringToEncrypt);

            cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(inputBytes, 0, inputBytes.Length);
            cryptoStream.FlushFinalBlock();

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public static string Decrypt(string stringToDecrypt)
        {
            string key = System.Configuration.ConfigurationManager.AppSettings.Get("CryptoKey");
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream;

            // Check whether the key is valid, otherwise make it valid
            CheckKey(ref key);

            des.Key = HashKey(key, des.KeySize / 8);
            des.IV = HashKey(key, des.KeySize / 8);
            byte[] inputBytes = Convert.FromBase64String(stringToDecrypt);

            cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(inputBytes, 0, inputBytes.Length);
            cryptoStream.FlushFinalBlock();

            Encoding encoding = Encoding.UTF8;
            return encoding.GetString(memoryStream.ToArray());
        }

        /// <summary>
        /// Make sure the used key has a length of exact eight characters.
        /// </summary>
        /// <param name="keyToCheck">Key being checked.</param>
        private static void CheckKey(ref string keyToCheck)
        {
            keyToCheck = keyToCheck.Length > 8 ? keyToCheck.Substring(0, 8) : keyToCheck;
            if (keyToCheck.Length < 8)
            {
                for (int i = keyToCheck.Length; i < 8; i++)
                {
                    keyToCheck += DEFAULT_KEY[i];
                }
            }
        }

        /// <summary>
        /// Hash a key.
        /// </summary>
        /// <param name="key">Key being hashed.</param>
        /// <param name="length">Length of the output.</param>
        /// <returns></returns>
        private static byte[] HashKey(string key, int length)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            // Hash the key
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] hash = sha1.ComputeHash(keyBytes);

            // Truncate hash
            byte[] truncatedHash = new byte[length];
            Array.Copy(hash, 0, truncatedHash, 0, length);
            return truncatedHash;
        }


        private static UInt64[] DESkey = { 13651584896517774118, 17666548998416544487, 14598027598290752101 };
        private static UInt64 DESIV = 1947855656165165240;

        private static int UInt64LENGTH = UInt64.MaxValue.ToString().Length;
        private static int BLOCK_SIZE = 8;

        /*
         * HINTS:
         * http://www.15seconds.com/issue/021210.htm
         * 
        */

        static public Int64 Crypt(int val)
        {
            TripleDESCryptoServiceProvider dprov = new TripleDESCryptoServiceProvider();

            dprov.BlockSize = 64;
            dprov.KeySize = 192;
            byte[] IVBuf = BitConverter.GetBytes(DESIV);
            byte[] keyBuf = new byte[24];
            byte[] keyB0 = BitConverter.GetBytes(DESkey[0]);
            byte[] keyB1 = BitConverter.GetBytes(DESkey[1]);
            byte[] keyB2 = BitConverter.GetBytes(DESkey[2]);
            for (int i = 0; i < 8; i++) keyBuf[i] = keyB0[i];
            for (int i = 0; i < 8; i++) keyBuf[i + 8] = keyB1[i];
            for (int i = 0; i < 8; i++) keyBuf[i + 16] = keyB2[i];

            ICryptoTransform ict = dprov.CreateEncryptor(keyBuf, IVBuf);

            byte[] inBuf = BitConverter.GetBytes(Convert.ToInt64(val));
            byte[] outBuf = new byte[8];

            ict.TransformBlock(inBuf, 0, 8, outBuf, 0);

            return BitConverter.ToInt64(outBuf, 0);
        }

        static public int Decrypt(Int64 val)
        {
            TripleDESCryptoServiceProvider dprov = new TripleDESCryptoServiceProvider();

            dprov.BlockSize = 64;
            dprov.KeySize = 192;
            byte[] IVBuf = BitConverter.GetBytes(DESIV);
            byte[] keyBuf = new byte[24];
            byte[] keyB0 = BitConverter.GetBytes(DESkey[0]);
            byte[] keyB1 = BitConverter.GetBytes(DESkey[1]);
            byte[] keyB2 = BitConverter.GetBytes(DESkey[2]);
            for (int i = 0; i < 8; i++) keyBuf[i] = keyB0[i];
            for (int i = 0; i < 8; i++) keyBuf[i + 8] = keyB1[i];
            for (int i = 0; i < 8; i++) keyBuf[i + 16] = keyB2[i];

            ICryptoTransform ict = dprov.CreateDecryptor(keyBuf, IVBuf);

            byte[] inBuf = BitConverter.GetBytes(val);
            byte[] outBuf = new byte[8];

            ict.TransformBlock(inBuf, 0, 8, outBuf, 0);
            ict.TransformBlock(inBuf, 0, 8, outBuf, 0);	// prvi vrati 0, pa treba jos jednom pozvat

            return ((int)(BitConverter.ToInt64(outBuf, 0)));
        }

        static public string CryptString(string val)
        {
            TripleDESCryptoServiceProvider dprov = new TripleDESCryptoServiceProvider();

            dprov.BlockSize = 64;
            dprov.KeySize = 192;
            byte[] IVBuf = BitConverter.GetBytes(DESIV);
            byte[] keyBuf = new byte[24];
            byte[] keyB0 = BitConverter.GetBytes(DESkey[0]);
            byte[] keyB1 = BitConverter.GetBytes(DESkey[1]);
            byte[] keyB2 = BitConverter.GetBytes(DESkey[2]);
            for (int i = 0; i < 8; i++) keyBuf[i] = keyB0[i];
            for (int i = 0; i < 8; i++) keyBuf[i + 8] = keyB1[i];
            for (int i = 0; i < 8; i++) keyBuf[i + 16] = keyB2[i];


            ICryptoTransform ict = dprov.CreateEncryptor(keyBuf, IVBuf);

            System.IO.MemoryStream mstream = new System.IO.MemoryStream();
            CryptoStream cstream = new CryptoStream(mstream, ict, CryptoStreamMode.Write);

            byte[] toEncrypt = new ASCIIEncoding().GetBytes(val);

            // Write the byte array to the crypto stream and flush it.
            cstream.Write(toEncrypt, 0, toEncrypt.Length);
            cstream.FlushFinalBlock();

            byte[] ret = mstream.ToArray();

            cstream.Close();
            mstream.Close();

            return Convert.ToBase64String(ret);
        }

        static public string DecryptString(string val)
        {
            TripleDESCryptoServiceProvider dprov = new TripleDESCryptoServiceProvider();

            dprov.BlockSize = 64;
            dprov.KeySize = 192;
            byte[] IVBuf = BitConverter.GetBytes(DESIV);
            byte[] keyBuf = new byte[24];
            byte[] keyB0 = BitConverter.GetBytes(DESkey[0]);
            byte[] keyB1 = BitConverter.GetBytes(DESkey[1]);
            byte[] keyB2 = BitConverter.GetBytes(DESkey[2]);
            for (int i = 0; i < 8; i++) keyBuf[i] = keyB0[i];
            for (int i = 0; i < 8; i++) keyBuf[i + 8] = keyB1[i];
            for (int i = 0; i < 8; i++) keyBuf[i + 16] = keyB2[i];

            ICryptoTransform ict = dprov.CreateDecryptor(keyBuf, IVBuf);

            byte[] byteData = Convert.FromBase64String(val);

            System.IO.MemoryStream mstream = new System.IO.MemoryStream(byteData);
            CryptoStream cstream = new CryptoStream(mstream, ict, CryptoStreamMode.Read);

            //byte[] toDecrypt = new byte[byteData.Length];

            //cstream.Read(toDecrypt, 0, toDecrypt.Length);

            System.IO.StreamReader str = new System.IO.StreamReader(cstream);

            string ret = str.ReadToEnd();

            //byte[] retb = new UnicodeEncoding().GetBytes(ret);

            str.Close();
            cstream.Close();
            mstream.Close();

            return ret;//UnicodeEncoding.ASCII.GetString(retb);

        }

        //VRACA NIZ BROJEVA
        static public string CryptStringUInt64(string val)
        {
            TripleDESCryptoServiceProvider dprov = new TripleDESCryptoServiceProvider();

            dprov.BlockSize = 64;
            dprov.KeySize = 192;
            byte[] IVBuf = BitConverter.GetBytes(DESIV);
            byte[] keyBuf = new byte[24];
            byte[] keyB0 = BitConverter.GetBytes(DESkey[0]);
            byte[] keyB1 = BitConverter.GetBytes(DESkey[1]);
            byte[] keyB2 = BitConverter.GetBytes(DESkey[2]);
            for (int i = 0; i < 8; i++) keyBuf[i] = keyB0[i];
            for (int i = 0; i < 8; i++) keyBuf[i + 8] = keyB1[i];
            for (int i = 0; i < 8; i++) keyBuf[i + 16] = keyB2[i];


            ICryptoTransform ict = dprov.CreateEncryptor(keyBuf, IVBuf);

            System.IO.MemoryStream mstream = new System.IO.MemoryStream();
            CryptoStream cstream = new CryptoStream(mstream, ict, CryptoStreamMode.Write);

            byte[] toEncrypt = new ASCIIEncoding().GetBytes(val);

            // Write the byte array to the crypto stream and flush it.
            cstream.Write(toEncrypt, 0, toEncrypt.Length);
            cstream.FlushFinalBlock();

            byte[] ret = mstream.ToArray();

            cstream.Close();
            mstream.Close();

            int startBlock = 0;
            string strret = "";
            do
            {
                strret += (BitConverter.ToUInt64(ret, startBlock)).ToString().PadLeft(UInt64LENGTH, '0');
                startBlock += BLOCK_SIZE;
            }
            while (ret.Length > startBlock);

            //strret += ret.Length.ToString() + " - " + startBlock.ToString();

            return strret;
        }

        static public string DecryptStringUInt64(string val)
        {
            TripleDESCryptoServiceProvider dprov = new TripleDESCryptoServiceProvider();

            dprov.BlockSize = 64;
            dprov.KeySize = 192;
            byte[] IVBuf = BitConverter.GetBytes(DESIV);
            byte[] keyBuf = new byte[24];
            byte[] keyB0 = BitConverter.GetBytes(DESkey[0]);
            byte[] keyB1 = BitConverter.GetBytes(DESkey[1]);
            byte[] keyB2 = BitConverter.GetBytes(DESkey[2]);
            for (int i = 0; i < 8; i++) keyBuf[i] = keyB0[i];
            for (int i = 0; i < 8; i++) keyBuf[i + 8] = keyB1[i];
            for (int i = 0; i < 8; i++) keyBuf[i + 16] = keyB2[i];

            ICryptoTransform ict = dprov.CreateDecryptor(keyBuf, IVBuf);

            byte[] byteData = new byte[(val.Length / UInt64LENGTH) * BLOCK_SIZE];
            UInt64 intVal;
            int startBlock = 0;
            do
            {
                intVal = UInt64.Parse(val.Substring(startBlock, UInt64LENGTH));
                for (int i = 0; i < BLOCK_SIZE; i++)
                {
                    byteData[((startBlock / UInt64LENGTH) * BLOCK_SIZE) + i] = BitConverter.GetBytes(intVal)[i];
                }
                startBlock += UInt64LENGTH;
            }
            while (val.Length > startBlock);

            System.IO.MemoryStream mstream = new System.IO.MemoryStream(byteData);
            CryptoStream cstream = new CryptoStream(mstream, ict, CryptoStreamMode.Read);

            //byte[] toDecrypt = new byte[byteData.Length];

            //cstream.Read(toDecrypt, 0, toDecrypt.Length);

            System.IO.StreamReader str = new System.IO.StreamReader(cstream);

            string ret = str.ReadToEnd();

            str.Close();
            cstream.Close();
            mstream.Close();

            return ret;

        }

        static public string MD5HashInt(int[] values)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] inBuf;
            byte[] outBuf = new byte[4];
            string hash = "";
            for (int i = 0; i < values.Length; i++)
            {
                inBuf = BitConverter.GetBytes(Convert.ToUInt32(values[i]));
                outBuf = md5.ComputeHash(inBuf);

                hash += BitConverter.ToUInt32(outBuf, 0).ToString();
            }

            return hash;

        }
    }
}
