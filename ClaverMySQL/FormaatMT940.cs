using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaverMySQL
{
    public class FormaatMT940
    {
        /// <summary>
        /// Class SubcodeList.
        /// </summary>
        public class SubcodeList
        {
            /// <summary>
            /// Gets or sets the IDL code.
            /// </summary>
            /// <value>The IDL code.</value>
            public int IDLCode { get; set; }

            /// <summary>
            /// Gets or sets the rekeningnummer.
            /// </summary>
            /// <value>The rekeningnummer.</value>
            public string Rekeningnummer { get; set; }

            /// <summary>
            /// Gets or sets the sub oms.
            /// </summary>
            /// <value>The sub oms.</value>
            public string SubOms { get; set; }

            /// <summary>
            /// Gets or sets the idlcode rekeningnummer suboms.
            /// </summary>
            /// <value>The idlcode rekeningnummer suboms.</value>
            public string idlcode_rekeningnummer_suboms { get; set; }
        }

        /// <summary>
        /// Class Afschriftregel.
        /// </summary>
        public class Afschriftregel
        {
            /// <summary>
            /// Gets or sets the aanvulling.
            /// </summary>
            /// <value>The aanvulling.</value>
            public string Aanvulling { get; set; }

            /// <summary>
            /// Gets or sets the bedrag.
            /// </summary>
            /// <value>The bedrag.</value>
            public string Bedrag { get; set; }

            /// <summary>
            /// Gets or sets the betalingskenmerk.
            /// </summary>
            /// <value>The betalingskenmerk.</value>
            public string Betalingskenmerk { get; set; }

            /// <summary>
            /// Gets or sets the boek datum.
            /// </summary>
            /// <value>The boek datum.</value>
            public string BoekDatum { get; set; } // YYMMDD

            /// <summary>
            /// Gets or sets the credit debet.
            /// </summary>
            /// <value>The credit debet.</value>
            public string Credit_Debet { get; set; }

            /// <summary>
            /// Gets or sets the iban.
            /// </summary>
            /// <value>The iban.</value>
            public string IBAN { get; set; }

            /// <summary>
            /// Gets or sets the naam.
            /// </summary>
            /// <value>The naam.</value>
            public string Naam { get; set; }

            /// <summary>
            /// Gets or sets the omschrijving.
            /// </summary>
            /// <value>The omschrijving.</value>
            public string Omschrijving { get; set; }

            /// <summary>
            /// Gets or sets the tag.
            /// </summary>
            /// <value>The tag.</value>
            public string Tag { get; set; } // :61: 

            /// <summary>
            /// Gets or sets the transactiereferentie.
            /// </summary>
            /// <value>The transactiereferentie.</value>
            public string Transactiereferentie { get; set; }

            /// <summary>
            /// Gets or sets the transactietype.
            /// </summary>
            /// <value>The transactietype.</value>
            public string Transactietype { get; set; }

            /// <summary>
            /// Gets or sets the valuta datum.
            /// </summary>
            /// <value>The valuta datum.</value>
            public string ValutaDatum { get; set; } // YYMMDD

            /// <summary>
            /// Gets or sets the iban eigen.
            /// </summary>
            /// <value>The iban eigen.</value>
            public string IBANEigen { get; set; }

            /// <summary>
            /// Gets or sets the bank naam.
            /// </summary>
            /// <value>The bank naam.</value>
            public string bankNaam { get; set; }

            /// <summary>
            /// Gets or sets the bank identifier.
            /// </summary>
            /// <value>The bank identifier.</value>
            public string bankID { get; set; }

            /// <summary>
            /// Gets or sets the code identifier.
            /// </summary>
            /// <value>The code identifier.</value>
            public string CodeID { get; set; }

            /// <summary>
            /// Gets or sets the naam identifier.
            /// </summary>
            /// <value>The naam identifier.</value>
            public string NaamID { get; set; }

            /// <summary>
            /// Gets or sets the IDL code.
            /// </summary>
            /// <value>The IDL code.</value>
            public int IDLCode { get; set; }

            /// <summary>
            /// Gets or sets the rekeningummer.
            /// </summary>
            /// <value>The rekeningummer.</value>
            public string Rekeningummer { get; set; }

            /// <summary>
            /// Gets or sets the sub oms.
            /// </summary>
            /// <value>The sub oms.</value>
            public string SubOms { get; set; }

            /// <summary>
            /// Gets or sets the idlcode rek oms.
            /// </summary>
            /// <value>The idlcode rek oms.</value>
            public string idlcode_rek_oms { get; set; }

            /// <summary>
            /// Gets or sets the sub code.
            /// </summary>
            /// <value>The sub code.</value>
            public Array subCode { get; set; }
        }

        /// <summary>
        /// Class SubCode.
        /// </summary>
        public class SubCode
        {
            /// <summary>
            /// Gets or sets the IDL code.
            /// </summary>
            /// <value>The IDL code.</value>
            public int IDLCode { get; set; }

            /// <summary>
            /// Gets or sets the rekeningummer.
            /// </summary>
            /// <value>The rekeningummer.</value>
            public string Rekeningummer { get; set; }

            /// <summary>
            /// Gets or sets the sub oms.
            /// </summary>
            /// <value>The sub oms.</value>
            public string SubOms { get; set; }

            /// <summary>
            /// Gets or sets the IDL code rek oms.
            /// </summary>
            /// <value>The IDL code rek oms.</value>
            public string idl_code_rek_oms { get; set; }
        }


        public class CodeKeuze
        {
            public string keuze { get; set; }
        }


        /// <summary>
        /// Class Beginsaldo.
        /// </summary>
        public class Beginsaldo
        {
            /// <summary>
            /// Gets or sets the bedrag.
            /// </summary>
            /// <value>The bedrag.</value>
            public decimal Bedrag { get; set; }

            /// <summary>
            /// Gets or sets the credit debet.
            /// </summary>
            /// <value>The credit debet.</value>
            public string Credit_Debet { get; set; }

            /// <summary>
            /// Gets or sets the datum.
            /// </summary>
            /// <value>The datum.</value>
            public DateTime Datum { get; set; }

            /// <summary>
            /// Gets or sets the tag.
            /// </summary>
            /// <value>The tag.</value>
            public string Tag { get; set; }

            /// <summary>
            /// Gets or sets the valuta.
            /// </summary>
            /// <value>The valuta.</value>
            public string Valuta { get; set; }
        }

        /// <summary>
        /// Class Eindsaldo.
        /// </summary>
        public class Eindsaldo
        {
            /// <summary>
            /// Gets or sets the bedrag.
            /// </summary>
            /// <value>The bedrag.</value>
            public decimal Bedrag { get; set; }

            /// <summary>
            /// Gets or sets the credit debet.
            /// </summary>
            /// <value>The credit debet.</value>
            public string Credit_Debet { get; set; }

            /// <summary>
            /// Gets or sets the datum.
            /// </summary>
            /// <value>The datum.</value>
            public DateTime Datum { get; set; }

            /// <summary>
            /// Gets or sets the tag.
            /// </summary>
            /// <value>The tag.</value>
            public string Tag { get; set; }

            /// <summary>
            /// Gets or sets the valuta.
            /// </summary>
            /// <value>The valuta.</value>
            public string Valuta { get; set; }
        }

        /// <summary>
        /// Class Rekeninghouder.
        /// </summary>
        public class Rekeninghouder
        {
            /// <summary>
            /// Gets or sets the iban.
            /// </summary>
            /// <value>The iban.</value>
            public string IBAN { get; set; }

            /// <summary>
            /// Gets or sets the naam.
            /// </summary>
            /// <value>The naam.</value>
            public string Naam { get; set; }

            /// <summary>
            /// Gets or sets the omschrijving.
            /// </summary>
            /// <value>The omschrijving.</value>
            public string Omschrijving { get; set; }

            /// <summary>
            /// Gets or sets the tag.
            /// </summary>
            /// <value>The tag.</value>
            public string Tag { get; set; }
        }

        /// <summary>
        /// Class Rekeningidentificatie.
        /// </summary>
        public class Rekeningidentificatie
        {
            /// <summary>
            /// Gets or sets the rekeningnummer.
            /// </summary>
            /// <value>The rekeningnummer.</value>
            public string Rekeningnummer { get; set; }

            /// <summary>
            /// Gets or sets the tag.
            /// </summary>
            /// <value>The tag.</value>
            public string Tag { get; set; } // 25
        }

        /// <summary>
        /// Class Transactiereferentienummer.
        /// </summary>
        public class Transactiereferentienummer
        {
            /// <summary>
            /// Gets or sets the tag.
            /// </summary>
            /// <value>The tag.</value>
            public string Tag { get; set; } // 20

            /// <summary>
            /// Gets or sets the transactiereferentie nummer.
            /// </summary>
            /// <value>The transactiereferentie nummer.</value>
            public string Transactiereferentie_nummer { get; set; }
        }
    }
}
