using System;

namespace ClaverMySQL.Models
{
    public class InlezenAfschriften
    {
        public Int64 ID { get; set; }
        public Int64 idadmin { get; set; }
        public DateTime aanmaakdatum { get; set; }
        public DateTime boekdatum { get; set; }
        public string IBANRekening { get; set; }
        public Int64 grootboekCodeSoort { get; set; }
        public Int64 grootboekSleutelID { get; set; }
        public string grootboek { get; set; }
        public string groetboek_num_oms { get; set; }
        public string tag { get; set; }
        public string credit_debit { get; set; }
        public decimal bedrag { get; set; }
        public string transactietype { get; set; }
        public string betalingskenmerk { get; set; }
        public string transactiereferentie { get; set; }
        public string IBAN { get; set; }
        public string naam { get; set; }
        public string omschrijving { get; set; }
        public bool reedsgeboekt { get; set; }
        public string IBANEigen { get; set; }
        public string BankNaam { get; set; }
        public string BankID { get; set; }
        public string NaamID { get; set; }
        public string CodeID { get; set; }
        public int IDLCode { get; set; }
        public string Rekeningnummer { get; set; }
        public string SubOms { get; set; }
        public string idlcode_rek_oms { get; set; }
    }
}
