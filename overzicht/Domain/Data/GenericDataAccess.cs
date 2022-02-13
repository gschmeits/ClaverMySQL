// ***********************************************************************
// Assembly         : GenericDataAccess - OleDb (Access database)
// Author           : G.H.M.H. Schmeits
// Created          : 12-18-2013
//
// Last Modified By : G.H.M.H. Schmeits
// Last Modified On : 12-24-2013
// ***********************************************************************
// <copyright file="GenericDataAccess.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.Sql;
using MySql.Data.MySqlClient;
/// <summary>
/// Class GenericDataAccess.
/// </summary>
public static class GenericDataAccess
{
	/// <summary>
	/// Initializes static members of the <see cref="GenericDataAccess" /> class.
	/// </summary>
	static GenericDataAccess()
	{
		//
		// TODO: Add contstructor logic here
		//
	}

	/// <summary>
	/// Executes the select command.
	/// </summary>
	/// <param name="command">The command.</param>
	/// <returns>DataTable.</returns>
	public static DataTable ExecuteSelectCommand(DbCommand command)
	{
		// The DataTable to be returned
		DataTable table;
		// Execute the command, making sure the connection gets closed in the
		// end
		table = new DataTable();
		try
		{
			// Open the data connection
			command.Connection.Open();
			// Execute the command and save the results in a DataTable
			var reader = command.ExecuteReader();

			table.Load(reader);

			// Close the reader
			reader.Close();
		}
		catch (OleDbException ex)
		{
			//Utilities.LogError(ex);
			//MessageBox.Show(ex.Message + ex.StackTrace, "Ger Exception Detail");
			var str = ex;
			string message = "De database kan niet geraadpleegd worden.\r\n\r\nControleer of er een netwerkconnectie is.";
			message += "\r\n\r\n" + ex.ErrorCode + "\r\n\r\n" + ex.Message + "\r\n" + ex.StackTrace;
			//melding frmMelding = new melding(message);
			//frmMelding.ShowDialog();

			//MessageBox.Show(message);
			throw;
		}
		finally
		{
			// Close the connection
			command.Connection.Close();
		}
		return table;
	}

	// creates and prepares a new DbCommand object on a new connection
	/// <summary>
	/// Creates the command.
	/// </summary>
	/// <returns>DbCommand.</returns>
	public static DbCommand CreateCommand()
	{
		string connectionString = GenericDataConnection.DbConnectionString;
		//OleDbConnection conn = new OleDbConnection();
		var conn = new MySqlConnection();
		// Set the connection string
		conn.ConnectionString = connectionString;
		// Create a database-specific command object
		DbCommand comm = conn.CreateCommand();
		// Set the command type to stored procedure
		comm.CommandType = CommandType.StoredProcedure;
		// Return the initialized command object
		return comm;
	}

	/// <summary>
	/// Creates the command text.
	/// </summary>
	/// <returns>DbCommand.</returns>
	public static DbCommand CreateCommandText()
	{
		string connectionString = GenericDataConnection.DbConnectionString;
        var conn = new MySqlConnection();
		conn.ConnectionString = connectionString;
		DbCommand comm = conn.CreateCommand();
		comm.CommandType = CommandType.Text;
		return comm;
	}

	/// <summary>
	/// Creates the command stored procedure.
	/// </summary>
	/// <returns>DbCommand.</returns>
	public static DbCommand CreateCommandStoredProcedure()
	{
		// Obtain the database provider name
		string connectionString = GenericDataConnection.DbConnectionString;
		// Create a new data provider factory
		//OleDbConnection conn = new OleDbConnection();
		var conn = new MySqlConnection();
		// Obtain a database-specific connection object
		// DbConnection conn = factory.CreateConnection();
		// Set the connection string
		conn.ConnectionString = connectionString;
		// Create a database-specific command object
		DbCommand comm = conn.CreateCommand();
		// Set the command type to stored procedure
		comm.CommandType = CommandType.StoredProcedure;
		// Return the initialized command object
		return comm;
	}

	/// <summary>
	/// Strings the scalar.
	/// </summary>
	/// <param name="command">The command.</param>
	/// <returns>System.String.</returns>
	public static string strScalar(DbCommand command)
	{
		string strWaarde = "";

		try
		{
			// Open the data connection
			command.Connection.Open();
			// Execute the command and save the results in a DataTable
			strWaarde = command.ExecuteScalar().ToString();
		}
		catch (MySqlException ex)
		{
			//Utilities.LogError(ex);
			//MessageBox.Show(ex.Message + ex.StackTrace, "Ger Exception Detail");
			var str = ex;
			string message = "De database kan niet geraadpleegd worden.\r\n\r\nControleer of er een netwerkconnectie is.";
			message += "\r\n\r\n" + ex.ErrorCode + "\r\n\r\n" + ex.Message + "\r\n" + ex.StackTrace;
			//melding frmMelding = new melding(message);
			//frmMelding.ShowDialog();
			throw;
		}
		finally
		{
			// Close the connection
			command.Connection.Close();
		}
		return strWaarde;
	}

	/// <summary>
	/// Executes the select non query.
	/// </summary>
	/// <param name="command">The command.</param>
	public static void ExecuteSelectNonQuery(DbCommand command)
	{
		try
		{
			// Open the data connection
			command.Connection.Open();
			command.ExecuteNonQuery();
		}
		catch (MySqlException ex)
		{
			//Utilities.LogError(ex);
			//MessageBox.Show(ex.Message + ex.StackTrace, "Ger Exception Detail");
			var str = ex;
			string message = "De database kan niet geraadpleegd worden.\r\n\r\nControleer of er een netwerkconnectie is.";
			message += "\r\n\r\n" + ex.ErrorCode + "\r\n\r\n" + ex.Message + "\r\n" + ex.StackTrace;
			//melding frmMelding = new melding(message);
			//frmMelding.ShowDialog();
			throw;
		}
		finally
		{
			// Close the connection
			command.Connection.Close();
		}
	}

	// execute an update, detele, or insert command
	/// <summary>
	/// Executes the non query.
	/// </summary>
	/// <param name="command">The command.</param>
	/// <returns>System.Int32.</returns>
	public static int ExecuteNonQuery(DbCommand command)
	{
		// The number of affected rows
		int affectedRows = -1;
		// Execute the command making sure the connection gets closed in the end
		try
		{
			// Opend the connection of the command
			command.Connection.Open();
			// Execute the command ad get the number of affected rows
			affectedRows = command.ExecuteNonQuery();
		}
		catch (MySqlException ex)
		{
			//Utilities.LogError(ex);
			//MessageBox.Show(ex.Message + ex.StackTrace, "Ger Exception Detail");
			var str = ex;
			string message = "De database kan niet geraadpleegd worden.\r\n\r\nControleer of er een netwerkconnectie is.";
			message += "\r\n\r\n" + ex.ErrorCode + "\r\n\r\n" + ex.Message + "\r\n" + ex.StackTrace;
			//melding frmMelding = new melding(message);
			//frmMelding.ShowDialog();
			throw;
		}
		finally
		{
			// Close the connection
			command.Connection.Close();
		}
		// return the number of affacted rows
		return affectedRows;
	}
}