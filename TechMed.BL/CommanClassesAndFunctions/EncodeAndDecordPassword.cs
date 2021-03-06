using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptSharp.Core;

namespace TechMed.BL.CommanClassesAndFunctions
{
    public sealed class EncodeAndDecordPassword
    {
        private EncodeAndDecordPassword()
        {

        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex);
            }
        }

        public static string DecodeFrom64(string encodedData)
        {
            try
            {
                UTF8Encoding encoder = new UTF8Encoding();
                Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encodedData);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new string(decoded_char);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in DecodeFrom64" + ex);
            }

        }

        public static string EncodePassword(string password)
        {

            string cryptedpassword = Crypter.Blowfish.Crypt(password, new CrypterOptions() { { CrypterOption.Variant, BlowfishCrypterVariant.Corrected }, { CrypterOption.Rounds, 10 } });

            return cryptedpassword;
        }

        public static bool MatchPassword(string password, string cryptedPassword)
        {
            bool matches = Crypter.CheckPassword(password, cryptedPassword);
            return matches;
        }
    }
}
