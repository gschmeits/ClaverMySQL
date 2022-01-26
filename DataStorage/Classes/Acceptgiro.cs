using System.Collections.Generic;
using System.Data;
using DataStorage.Data;

namespace DataStorage.Classes
{
    public class AcceptgiroClass
    {
        public List<Acceptgiro> acceptgiros { get; set; }
    }

    public class Acceptgiro
    {
        public string IDCode { get; set; }
        public string Code { get; set; }
    }

    public class AcceptgiroFactory
    {
        public List<Acceptgiro> GetAcceptgiro()
        {
            var acceptgiros = new List<Acceptgiro>();
            var sSQL = clsDataAcceptgiro.GetCode();
            DataTable dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
            {
                for (int intX = 0; intX < dt.Rows.Count; intX++)
                {
                    acceptgiros.Add(new Acceptgiro
                    {
                        IDCode = dt.Rows[intX][0].ToString(),
                        Code = dt.Rows[intX][1].ToString()
                    });
                }
            }
            return acceptgiros;
        }
    }
}
