using System;

namespace DataStorage.Data
{
    public static class clsBankafschriften
    {
        public static Boolean CheckBankrekeningnummer(string rekeningnummer)
        {
            Boolean waar = true;

            var query = "SELECT Count(*) FROM tbl_bank WHERE IBAN = '" + rekeningnummer + "'";

            var dt = GenericDataRead.GetData(query);
            if (Convert.ToInt16(dt.Rows[0][0].ToString()) == 0)
            {
                waar = false;
            }

            return waar;
        }
    }
}
