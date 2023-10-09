namespace library.Shared;
using System.Collections.Generic;
using System;

public class ProfessorEqualityComparer : IEqualityComparer<Professor>
    {
        public bool Equals(Professor x, Professor y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            return AreSalaryAccountsEqual(x, y);
        }

        private bool AreSalaryAccountsEqual(Professor x, Professor y)
        {

            string xDecryptedSalaryAccount = x.GetDecryptedSalaryAccount();
            string yDecryptedSalaryAccount = y.GetDecryptedSalaryAccount();

            return string.Equals(xDecryptedSalaryAccount, yDecryptedSalaryAccount, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(Professor obj)
        {
            string decryptedSalaryAccount = obj.GetDecryptedSalaryAccount();
            return decryptedSalaryAccount.ToLower().GetHashCode();
        }
    }
