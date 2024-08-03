using Microsoft.AspNetCore.DataProtection;
using System;

public class DataProtectionHelper
{
    private readonly IDataProtector _protector;

    public DataProtectionHelper(IDataProtectionProvider provider)
    {
        _protector = provider.CreateProtector("EmployeeApp.EncryptionKey");
    }

    // to encrypt the data
    public string Encrypt(string data)
    {
        if (string.IsNullOrEmpty(data))
            return data;

        return _protector.Protect(data);
    }

    // TO Decrypt It
    public string Decrypt(string data)
    {
        if (string.IsNullOrEmpty(data))
            return data;

        return _protector.Unprotect(data);
    }
}
