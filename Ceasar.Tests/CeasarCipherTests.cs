using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;

namespace Ceasar.Tests
{

    public class CeasarCipher
    {
        public int cipherOffset { get; set; }
        public string[] alphabet { get; set; }

        public CeasarCipher(int offset)
        {
            cipherOffset = offset;
            //don't know how many characters i need to included. built based on the tests 
            alphabet = new[]
            {
                "!", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "~"
            };
        }

        public string Encrypt(string toEncrypt)
        {
            var encrypted = "";
            if (toEncrypt == "")
            {
                return toEncrypt;
            }
            else if (toEncrypt == null)
            {
                throw new ArgumentNullException("Null argument passed to encrypt");
            }
            else
            {
                for (int i = 0; i < toEncrypt.Length; i++)
                {
                    var index = Array.IndexOf(alphabet, toEncrypt[i].ToString());
                    //Console.WriteLine(index);
                    if (toEncrypt[i].ToString() == "\u0020")
                    {
                        encrypted += " ";
                    }
                    else if (index == -1)
                    {
                        throw new ArgumentOutOfRangeException("Wrong character passed");
                    }
                    
                    else
                    {
                        var goRight = index + cipherOffset;
                        while (goRight >= alphabet.Length)
                        {
                            goRight -= alphabet.Length;
                        }
                        encrypted += alphabet[goRight].ToString();
                    }
                }
                return encrypted;
            }
        }

        public string Decrypt(string toDecrypt)
        {
            var decrypted = "";
            if (toDecrypt == "")
            {
                return toDecrypt;
            }
            else if (toDecrypt == null)
            {
                throw new ArgumentNullException("Null argument passed to decrypt");
            }
            else
            {
                for (int i = 0; i < toDecrypt.Length; i++)
                {
                    var index = Array.IndexOf(alphabet, toDecrypt[i].ToString());
                    //Console.WriteLine(index);
                    if (toDecrypt[i].ToString() == "\u0020")
                    {
                        decrypted += " ";
                    }
                    else if (index == -1)
                    {
                        throw new ArgumentOutOfRangeException("Wrong character passed");
                    }
                    
                    else
                    {
                        var goLeft = index - cipherOffset;
                        if (goLeft < 0)
                        {
                            goLeft += alphabet.Length;
                        }
                        decrypted += alphabet[goLeft].ToString();
                    }
                }
                return decrypted;
            }
        }

    }

    public class CeasarCipherTests
    {
        [Test]
        public void Encrypt_WhenEmptyStringIsPassed_ReturnEmptyString()
        {
            var cipher = new CeasarCipher(offset: 0);

            var encrypted = cipher.Encrypt("");

            Assert.IsEmpty(encrypted);
        }

        [Test]
        public void Dencrypt_WhenEmptyStringIsPassed_ReturnEmptyString()
        {
            var cipher = new CeasarCipher(offset: 0);

            var encrypted = cipher.Decrypt("");

            Assert.IsEmpty(encrypted);
        }

        [Test]
        public void Encrypt_WhenPassedNull_ThrowsArgumentNullException()
        {
            var cipher = new CeasarCipher(offset: 0);

            Assert.Throws<ArgumentNullException>(() =>
            {
                cipher.Encrypt(null);
            });
        }

        [Test]
        public void Encrypt_WhenPassedWrongSymbol_ThrowsArgumentOutOfRangeException()
        {
            var cipher = new CeasarCipher(offset: 0);
           var nonSymbol = (char)127;
            var nonSymbolString = new string(new[] { nonSymbol });

           Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                cipher.Encrypt(nonSymbolString);
            });
        }

        [Test]
        public void Dencrypt_WhenPassedWrongSymbol_ThrowsArgumentOutOfRangeException()
        {
            var cipher = new CeasarCipher(offset: 0);
            var nonSymbol = (char)127;
            var nonSymbolString = new string(new[] { nonSymbol });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                cipher.Decrypt(nonSymbolString);
            });
        }

        [Test]
        public void Dencrypt_WhenPassedNull_ThrowsArgumentNullException()
        {
            var cipher = new CeasarCipher(offset: 0);

            Assert.Throws<ArgumentNullException>(() =>
            {
                cipher.Decrypt(null);
            });
        }

        [Test]
        public void Encrypt_WithZeroOffset_ReturnTheSameWord()
        {
            var cipher = new CeasarCipher(offset: 0);
            var word = "random";

            var encrypted = cipher.Encrypt(word);

            Assert.AreEqual(word, encrypted);
        }

        [Test]
        public void Decrypt_WithZeroOffset_ReturnTheSameWord()
        {
            var cipher = new CeasarCipher(offset: 0);
            var word = "random";

            var encrypted = cipher.Decrypt(word);

            Assert.AreEqual(word, encrypted);
        }

        [Test]
        public void Encrypt_WithOneOffset_ShiftsSymbolsOnOnePositionRight()
        {
            var cipher = new CeasarCipher(offset: 1);
            var word = "a";

            var encrypted = cipher.Encrypt(word);

            Assert.AreEqual("b", encrypted);
        }

        [Test]
        public void Decrypt_WithOneOffset_ShiftsSymbolsOnOnePosition()
        {
            var cipher = new CeasarCipher(offset: 1);
            var word = "b";

            var encrypted = cipher.Decrypt(word);

            Assert.AreEqual("a", encrypted);
        }

        [Test]
        public void Encrypt_WithOverflow_ShouldStartFromBegingingOfAlphabet()
        {
            var cipher = new CeasarCipher(offset: 1);
            var word = "~";

            var encrypted = cipher.Encrypt(word);

            Assert.AreEqual("!", encrypted);
        }

        [Test]
        public void Decrypt_WithOverflow_ShouldStartFromBegingingOfAlphabet()
        {
            var cipher = new CeasarCipher(offset: 1);
            var word = "!";

            var encrypted = cipher.Decrypt(word);

            Assert.AreEqual("~", encrypted);
        }

        [Test]
        public void EncryptDecrypt_WithRandomSting_ProducesTheSameResult()
        {
            var cipher = new CeasarCipher(offset: 5);
            var word = "Testing with some random string";

            var encrypted = cipher.Encrypt(word);

            Assert.AreEqual(word, cipher.Decrypt(encrypted));
        }

        //additional test
        [Test]
        public void Encrypt_WithLargeOffset_ShouldStartFromBegingingOfAlphabet()
        {
            var cipher = new CeasarCipher(offset: 109);
            var word = "~";

            var encrypted = cipher.Encrypt(word);

            Assert.AreEqual("!", encrypted);
        }
    }
}
