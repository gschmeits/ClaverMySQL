using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace ClaverMySQL.Models
{
    public class InlezenBankSaldi
    {
        public Int64 ID { get; set; }
        public Int64 idadmin { get; set; }
        public DateTime Aanmaakdatum { get; set; }
        public string IBAN { get; set; }
        public string BeginsaldoTag { get; set; }
        public string BeginsaldoCreditDebet { get; set; }
        public DateTime BeginsaldoDatum { get; set; }
        public string BeginsaldoValuta { get; set; }
        public decimal BeginsaldoBedrag { get; set; }
        public string EindsaldoTag { get; set; }
        public string EindsaldoCreditDebet { get; set; }
        public DateTime EindsaldoDatum { get; set; }
        public string EindsaldoValuta { get; set; }
        public decimal EindsaldoBedrag { get; set; }
    }
}
