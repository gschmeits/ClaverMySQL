using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

public static class Utilities
{
	static Utilities()
	{
		//
		// TODO: Add constructor logic here
		//
	}

	public static void LogError(Exception ex)
	{
#if DEBUG
		// get the current date and time
		string dateTime = DateTime.Now.ToLongDateString() + ", at "
											 + DateTime.Now.ToShortTimeString();
		// stores the error message
		string errorMessage = "Exception generated on " + dateTime;
		// obtain the page that generated the error
		//errorMessage += "\n\n Window location: " + context.Request.RawUrl;
		// build the error message
		errorMessage += "\n\n Message: " + ex.Message;
		errorMessage += "\n\n Source: " + ex.Source;
		errorMessage += "\n\n Method: " + ex.TargetSite;
		errorMessage += "\n\n Stack Trace: \n\n" + ex.StackTrace;
		errorMessage += "\n\n ErrorNumber: " + ex.Data.ToString();
		errorMessage += "Errornumber: " + ex.Data.ToString();
		MessageBox.Show(errorMessage);
#endif
#if RELEASE
			var frmMelding = new frm_018_Melding();
			frmMelding.Show();
#endif
	}

	public static DateTime dhLastDayInMonth(DateTime datum = default(DateTime))
	{
		Int32 jaar = datum.Year;
		Int32 maand = datum.Month;
		var maandOpmaak = Convert.ToDateTime(jaar + "-" + maand + "-" + DateTime.DaysInMonth(jaar, maand));
		return maandOpmaak;
	}
}