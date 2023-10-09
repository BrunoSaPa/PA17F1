namespace library.Shared;
using System.Security.Cryptography;
using System.Xml.Serialization; 
using System.Text;
public class Storer
{
    public Storer(){}
    const string passphrase  = "Sup3rS3cureHahah";
    public Storer(string? name, string? middleName, string? lastName, string? password)
    {
        Name = name;
        MiddleName = middleName;
        LastName = lastName;
        SetEncryptedPassword(password);
    }

    //members
    [XmlAttribute("fname")]
    public string? Name { get; set; }
    [XmlAttribute("mname")]
    public string? MiddleName { get; set; }
    [XmlAttribute("lname")]
    public string? LastName { get; set; }
    [XmlAttribute("password")]
    public byte[]? Password { get; set; }


//encryptation process
//initialization vector of 16 bytes
    private byte[] IV =
    {
        0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
        0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
    }; 

    public string GetDecryptedPassword()
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(passphrase);
            aesAlg.IV = IV; 

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(Password))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }


    private void SetEncryptedPassword(string password)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(passphrase);
            aesAlg.IV = IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(password);
                    }
                }

                Password = msEncrypt.ToArray();
            }
        }
    }


}



