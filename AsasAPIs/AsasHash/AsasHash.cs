using Asas.AsasHash.Asas.Contracts;
using Asas.AsasHash.Asas.Models;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Asas.AsasHash
{
    public class AsasHash : IAsasHashPassword
    {
        public Hash HashPassword(Hash context)
        {



            try
            {





                using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(context.RawPassword, context.GoodHash.Salt, context.GoodHash.Iterations))


                using (Aes aes = Aes.Create())
                {


                    aes.Key = pbkdf2.GetBytes(16);

                    byte[] generatedIV = aes.IV;


                    using (MemoryStream memory = new MemoryStream())
                    {

                        using (CryptoStream crypto = new CryptoStream(memory, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {


                            byte[] utfD1 = new System.Text.UTF8Encoding(false).GetBytes(context.RawPassword);

                            crypto.Write(utfD1, 0, utfD1.Length);

                            crypto.FlushFinalBlock();


                        }

                        byte[] encryptedBytes = memory.ToArray();

                        return new Hash
                        {

                            HashedPassword = Convert.ToBase64String(encryptedBytes),
                            RawPassword = context.RawPassword,
                            IsSucceeded = true,
                            GoodHash = new GoodHash
                            {
                                Salt = context.GoodHash.Salt,
                                Iterations = context.GoodHash.Iterations,
                                IV = generatedIV
                            }
                        };


                    }



                }


            }
            catch (Exception)
            {

                return new Hash
                {
                    HashedPassword = null,
                    RawPassword = context.RawPassword,
                    IsSucceeded = false,
                    GoodHash = new GoodHash
                    {
                        Salt = context.GoodHash.Salt,
                        Iterations = context.GoodHash.Iterations
                    }
                };


            }





        }

        public Hash VerifyPassword(Hash context)
        {



            try
            {




                using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(context.RawPassword, context.GoodHash.Salt, context.GoodHash.Iterations))



                using (Aes aes = Aes.Create())
                {


                    aes.Key = pbkdf2.GetBytes(16);

                    aes.IV = context.GoodHash.IV;

                    using (MemoryStream memory = new MemoryStream())
                    {


                        using (CryptoStream crypto = new CryptoStream(memory, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {

                            byte[] encryptedBytes = Convert.FromBase64String(context.HashedPassword);

                            crypto.Write(encryptedBytes, 0, encryptedBytes.Length);

                            crypto.FlushFinalBlock();

                        }

                        string decryptedPassword = Encoding.UTF8.GetString(memory.ToArray());



                        return new Hash
                        {
                            HashedPassword = context.HashedPassword,
                            RawPassword = context.RawPassword,
                            IsSucceeded = (decryptedPassword == context.RawPassword),
                            GoodHash = new GoodHash
                            {
                                Salt = context.GoodHash.Salt,
                                Iterations = context.GoodHash.Iterations,
                                IV = context.GoodHash.IV
                            }
                        };

                    }

                }



            }
            catch (Exception)
            {


                return new Hash
                {

                    HashedPassword = context.HashedPassword,
                    RawPassword = context.RawPassword,
                    IsSucceeded = false,

                    GoodHash = new GoodHash
                    {
                        Salt = context.GoodHash.Salt,
                        Iterations = context.GoodHash.Iterations,

                        IV = context.GoodHash.IV
                    }
                };

            }

        }



    }




}