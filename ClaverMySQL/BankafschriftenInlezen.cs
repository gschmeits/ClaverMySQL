using ClaverMySQL.Models;
using DataStorage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ClaverMySQL
{
    public class BankafschriftenInlezen : INotifyPropertyChanged
    {
        public List<FormaatMT940.SubcodeList> _SubcodeLists1 =
            new List<FormaatMT940.SubcodeList>();

        public List<FormaatMT940.SubcodeList> _SubcodeLists2 =
            new List<FormaatMT940.SubcodeList>();

        public List<FormaatMT940.SubcodeList> _SubcodeLists3 =
            new List<FormaatMT940.SubcodeList>();

        public List<FormaatMT940.SubcodeList> _SubcodeLists4 =
            new List<FormaatMT940.SubcodeList>();

        /// <summary>
        ///     Gets or sets the inlezen afschriftens list.
        /// </summary>
        /// <value>The inlezen afschriftens list.</value>
        public List<InlezenAfschriften> InlezenAfschriftensList { get; set; }

        /// <summary>
        ///     Gets or sets the subcode1.
        /// </summary>
        /// <value>The subcode1.</value>
        public List<FormaatMT940.SubCode> Subcode1 { get; set; }

        /// <summary>
        ///     Gets or sets the subcode2.
        /// </summary>
        /// <value>The subcode2.</value>
        public List<FormaatMT940.SubCode> Subcode2 { get; set; }

        /// <summary>
        ///     Gets or sets the subcode3.
        /// </summary>
        /// <value>The subcode3.</value>
        public List<FormaatMT940.SubCode> Subcode3 { get; set; }

        /// <summary>
        ///     Gets or sets the subcode4.
        /// </summary>
        /// <value>The subcode4.</value>
        public List<FormaatMT940.SubCode> Subcode4 { get; set; }

        /// <summary>
        ///     Gets or sets the subcode5.
        /// </summary>
        /// <value>The subcode5.</value>
        public List<FormaatMT940.SubCode> Subcode5 { get; set; }

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public void FillBankafschriftenInlezen()
        {
            var sSQL = "SELECT * FROM InlezenAfschriften";
            var dt = GenericDataRead.GetData(sSQL);
            InlezenAfschriftensList = (from DataRow dr in dt.Rows
                select new InlezenAfschriften
                {
                    ID = Convert.ToInt64(dr["ID"]),
                    idadmin = Convert.ToInt64(dr["idadmin"]),
                    aanmaakdatum = Convert.ToDateTime(dr["aanmaakdatum"]),
                    boekdatum = Convert.ToDateTime(dr["boekdatum"]),
                    IBANRekening = dr["IBANRekening"].ToString(),
                    grootboekCodeSoort =
                        Convert.ToInt64(dr["grootboekCodeSoort"]),
                    grootboekSleutelID =
                        Convert.ToInt64(dr["grootboekSleutelID"]),
                    grootboek = dr["grootboek"].ToString(),
                    groetboek_num_oms = dr["grootboek_num_oms"].ToString(),
                    tag = dr["tag"].ToString(),
                    credit_debit = dr["credit_debet"].ToString(),
                    bedrag = Convert.ToDecimal(dr["bedrag"]),
                    transactietype = dr["transactietype"].ToString(),
                    betalingskenmerk = dr["betalingskenmerk"].ToString(),
                    transactiereferentie =
                        dr["transactiereferentie"].ToString(),
                    IBAN = dr["IBAN"].ToString(),
                    naam = dr["naam"].ToString(),
                    omschrijving = dr["omschrijving"].ToString(),
                    reedsgeboekt = Convert.ToBoolean(dr["reedsgeboek"]),
                    IBANEigen = dr["IBANEigen"].ToString(),
                    BankNaam = dr["banknaam"].ToString(),
                    BankID = dr["BankID"].ToString(),
                    NaamID = dr["NaamID"].ToString(),
                    CodeID = dr["CodeID"].ToString(),
                    IDLCode = Convert.ToInt16(dr["IDLCode"]),
                    Rekeningnummer = dr["Rekeningnummer"].ToString(),
                    SubOms = dr["SubOms"].ToString(),
                    idlcode_rek_oms = dr["idlcode_rek_oms"].ToString()
                }).ToList();
        }

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateAfschrift(InlezenAfschriften rij)
        {
            /*var query = (from a in this._context.InlezenAfschriftens
                where a.id == rij.id
                select a).ToList();
            foreach (var que in query)
            {
                if (rij.grootboek_num_oms.Length > 4)
                {
                    que.grootboek = rij.grootboek_num_oms.Substring(0, 4);
                    que.grootboek_num_oms = rij.grootboek_num_oms;
                }

                if (rij.idlcode_rek_oms != null)
                    if (rij.idlcode_rek_oms.Length > 4)
                    {
                        string[] subCode1 = { };
                        subCode1 = rij.idlcode_rek_oms.Split('_');
                        if (subCode1.Length == 3)
                        {
                            que.Rekeningummer = subCode1[1];
                            que.IDLCode = Convert.ToInt32(subCode1[2]
                                .Substring(1, subCode1[2].Length - 2));
                            que.SubOms = subCode1[0];
                            //que.omschrijving = que.betalingskenmerk;
                        }
                    }
            }

            this._context.SubmitChanges();*/
            FillBankafschriftenInlezen();
        }

        public void CodeToevoegenModel()
        {
            /*var query = (from a in this._context.InlezenAfschriftens
                select a).ToList();

            foreach (var qu in query)
            {
                var tekst = qu.omschrijving.ToUpper();

                var queryGrootboek = (from b in this._context.CodeTabels
                        where b.zoekbegrippen != string.Empty
                        select b)
                    .ToList();

                foreach (var grootboekB in queryGrootboek)
                {
                    var zoekterm = grootboekB.zoekbegrippen.Split(',');
                    foreach (var t in zoekterm)
                        if (tekst.IndexOf(t.ToUpper().Trim(),
                            StringComparison.Ordinal) >= 0 && t.Length > 0)
                        {
                            qu.grootboekCodeSoort =
                                Convert.ToInt32(grootboekB.Code);
                            qu.grootboekSleutelID = grootboekB.IDCodeSleutel;
                            qu.grootboek_num_oms = grootboekB.IDCode + " " +
                                                   grootboekB.CodeOmschrijving;
                            qu.grootboek = grootboekB.IDCode;
                            var query1 =
                                "SELECT TOP 1 IDLCODE, SumOms FROM SubCode WHERE Rekeningnummer = " +
                                grootboekB.IDCode;
                            var db = GenericDataRead.GetData(query1);
                            if (db.Rows.Count > 0)
                            {
                                qu.IDLCode =
                                    Convert.ToInt32(db.Rows[0][0].ToString());
                                qu.SubOms = db.Rows[0][1].ToString();
                                qu.idlcode_rek_oms = db.Rows[0][1] + "_" +
                                    grootboekB.IDCode + "_(" + db.Rows[0][0] +
                                    ")";
                            }
                        }
                }
            }

            this._context.SubmitChanges();*/
            FillBankafschriftenInlezen();
        }

        public void CodeToevoegenCodeId()
        {
            // Haal alle gegevens op waar CodeID ingevuld is.
            /*var query = (from a in this._context.InlezenAfschriftens
                where a.CodeID != string.Empty
                select a).ToList();

            // Voor elk record
            foreach (var que in query)
            {
                var grootboekCode =
                    (from b in this._context.CodeTabels select b).ToList();
                foreach (var code in grootboekCode)
                    if (que.CodeID == code.IDCode)
                    {
                        que.grootboekCodeSoort = Convert.ToInt32(code.Code);
                        que.grootboekSleutelID = code.IDCodeSleutel;
                        que.grootboek = code.IDCode;
                        que.grootboek_num_oms =
                            code.IDCode + " " + code.CodeOmschrijving;
                        var query1 =
                            "SELECT TOP 1 IDLCODE, SumOms FROM SubCode WHERE Rekeningnummer = " +
                            code.IDCode;
                        var db = GenericDataRead.GetData(query1);
                        if (db.Rows.Count > 0)
                        {
                            que.IDLCode =
                                Convert.ToInt32(db.Rows[0][0].ToString());
                            que.SubOms = db.Rows[0][1].ToString();
                            que.idlcode_rek_oms = db.Rows[0][1] + "_" +
                                                  code.IDCode + "_(" +
                                                  db.Rows[0][0] + ")";
                        }

                        break;
                    }

                this._context.SubmitChanges();*/
                //FillBankafschriftenInlezen();
            //}
        }
        
        public void CodeToevoegenModelNaam()
        {
            /*var query = (from a in this._context.InlezenAfschriftens
                where a.grootboek == string.Empty
                select a).ToList();

            foreach (var que in query)
            {
                var tekst = que.omschrijving.ToUpper().Trim();

                var grootboekOpNaam =
                    (from a in this._context.ZoekGrootboekOpNaam(tekst)
                        select a).ToList();
                foreach (var naam in grootboekOpNaam)
                    if (tekst.Length > 5)
                    {
                        que.grootboek = naam.nummer;
                        que.grootboek_num_oms =
                            naam.nummer + " " + naam.omschrijving;
                    }
            }

            this._context.SubmitChanges();*/
            FillBankafschriftenInlezen();
        }

        public void LeegmakenAfschriften()
        {
            /*var query = (from a in this._context.InlezenAfschriftens
                select a).ToList();
            _context.InlezenAfschriftens.DeleteAllOnSubmit(query);
            _context.SubmitChanges();*/
        }

        public void LeegmakenAfschriftenSaldi()
        {
            /*var query = (from a in this._context.InlezenBanksaldis
                select a).ToList();
            this._context.InlezenBanksaldis.DeleteAllOnSubmit(query);
            this._context.SubmitChanges();*/
        }
    }
}