// ***********************************************************************
// Assembly         : BikeBill2014
// Author           : G.H.M.H. Schmeits
// Created          : 12-18-2013
//
// Last Modified By : G.H.M.H. Schmeits
// Last Modified On : 12-20-2013
// ***********************************************************************
// <copyright file="GenericDataRead.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

/// <summary>
/// Class GenericDataRead.
/// </summary>
public static class GenericDataRead
{
	/// <summary>
	/// Initializes static members of the <see cref="GenericDataRead" /> class.
	/// </summary>
	static GenericDataRead()
	{
	}

	/// <summary>
	/// Gets the data.
	/// </summary>
	/// <param name="strQue">The string que.</param>
	/// <returns>DataTable.</returns>
	public static DataTable GetData(string strQue)
	{
        var comm = GenericDataAccess.CreateCommandText();
        comm.CommandText = strQue;
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

	public static bool INUPDEL(string strQue)
	{
		DbCommand comm = GenericDataAccess.CreateCommandText();
		comm.CommandText = strQue;

		int result = -1;
		try
		{
			result = GenericDataAccess.ExecuteNonQuery(comm);
		}
		catch (MySqlException ex)
		{
			var str = ex;
			throw;
		}
        return (result != -1);
	}
}