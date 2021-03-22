using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ToStringTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var t = DateTime.Parse("0001-01-01T00:00:00");
            //var foo = t == DateTime.MinValue;
            //var test = true;
            //if (test) { throw new Exception(); }
            //Console.WriteLine(111.9345m.ToString("0"));
            //Console.ReadKey();
            //var cardInfo = new MfbRsp();
            //cardInfo.Data.First(x => x.CardType == "1");

            //var result = Decrypt("Fu5zcrYKTGctSKltYL0LuDaUzwQ5SdY2WQzZupExl9pDHjOuiBJYq/5/FKZuNA1A", "f2d5b90e", true);

            var r = Encrypt("testty", "12345678", "12345678");

            Console.WriteLine(r);
            var d = Decrypt(r, "12345678",false);

            Console.WriteLine(d);
            Console.ReadLine();
        }

        //static string Decrypt(string pToDecrypt, string sKey, string sIV)
        //{
        //    using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
        //    {
        //        byte[] inputByteArray = Encoding.UTF8.GetBytes(pToDecrypt);
        //        //設定加密金鑰(轉為Byte)
        //        des.Key = Encoding.ASCII.GetBytes(sKey);
        //        //設定初始化向量(轉為Byte)
        //        des.IV = Encoding.ASCII.GetBytes(sIV);
        //        des.Padding = PaddingMode.PKCS7;
        //        des.Mode = CipherMode.ECB;
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
        //            {
        //                //例外處理
        //                try
        //                {
        //                    cs.Write(inputByteArray, 0, inputByteArray.Length);
        //                    cs.FlushFinalBlock();
        //                    //輸出資料
        //                    return Encoding.Default.GetString(ms.ToArray());
        //                }
        //                catch (CryptographicException ex)
        //                {
        //                    //若金鑰或向量錯誤，傳回N/A
        //                    return "N/A";
        //                }
        //            }
        //        }
        //    }
        //}


        /// <!--DEC 加密法 -->
        /// <summary>
        /// DEC 加密法 - design By Phoenix 2008 -
        /// </summary>
        /// <param name="pToEncrypt">加密的字串</param>
        /// <param name="sKey">加密金鑰</param>
        /// <param name="sIV">初始化向量</param>
        /// <returns></returns>
        static string Encrypt(string pToEncrypt, string sKey, string sIV)
        {
            StringBuilder ret = new StringBuilder();
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                //將字元轉換為Byte
                byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
                des.Key = Encoding.UTF8.GetBytes(sKey);
                des.IV = Encoding.UTF8.GetBytes(sKey);
                des.Padding = PaddingMode.PKCS7;
                des.Mode = CipherMode.ECB;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                    }
                    //輸出資料
                    foreach (byte b in ms.ToArray())
                        ret.AppendFormat("{0:X2}", b);
                }
            }
            //回傳
            return ret.ToString();
        }

        public static string Decrypt(string cipherString, string key , bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            //Get your key from config file to open the lock!
          

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }



    }
    class MfbRsp
    {
        public string Success;
        public string Code;
        public string Message;
        public List<MfbData> Data;
    }
    class MfbData
    {
        public string CardType;
        public string BankCode;
        public string BankName;
        public string CardNumber;
        public string CardName;
    }
}
