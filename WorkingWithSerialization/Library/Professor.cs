namespace library.Shared;
using System.Security.Cryptography;
using System.Xml.Serialization; 
using System.Text;

public class Professor
{
    public Professor(){}
    const string passphrase  = "Sup3rS3cureHahah";
    //constructor
    public Professor(string? name, string? middleName, string? lastName, string? password, string? salaryAccount, string? division,List<string>? subject)
    {
        Name = name;
        MiddleName = middleName;
        LastName = lastName;
        SetEncryptedPassword(password);
        SetEncryptedSalaryAccount(salaryAccount);
        Division = division;
        Subject = subject;
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
    [XmlAttribute("salaryacc")]
    public byte[]? SalaryAccount { get; set; }
    [XmlAttribute("division")]
    public string? Division { get; set; }
    [XmlArrayItem]
    public List<string>? Subject {get; set;}





//encrypt password and Salary account methods
//initialization vector of 16 bytes
    private byte[] IV =
    {
        0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
        0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
    }; 
    
    
    public string GetDecryptedPassword()
    {
        return DecryptData(Password);
    }

    public string GetDecryptedSalaryAccount()
    {
        return DecryptData(SalaryAccount);
    }


    private string DecryptData(byte[] encryptedData)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(passphrase);
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
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
        Password = EncryptData(password);
    }

    private void SetEncryptedSalaryAccount(string salaryAccount)
    {
        SalaryAccount = EncryptData(salaryAccount);
    }

    private byte[] EncryptData(string data)
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
                        swEncrypt.Write(data);
                    }
                }

                return msEncrypt.ToArray();
            }
        }
    }
}