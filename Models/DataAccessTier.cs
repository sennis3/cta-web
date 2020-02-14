//
// Data Access Tier: uses ADO.NET to execute SQL against an underlying
// SQL Server database.
//

using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.ExceptionServices;

namespace DataAccessTier
{

  public static class DB
  {
    //
    // Fields:
    //
    private static string connectionInfo = String.Format(@"
Server=tcp:uiccs341.database.windows.net,1433;Initial Catalog=CTA;
Persist Security Info=False;User ID=student;Password=cs341!uic;
MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;
Connection Timeout=30;
");

    //
    // TestConnection:  returns true if the database can be successfully opened and closed,
    // false if not.
    //
    public static bool TestConnection()
    {
      SqlConnection db = new SqlConnection(connectionInfo);

      bool  state = false;

      try
      {
        db.Open();

        state = (db.State == ConnectionState.Open);
      }
      catch
      {
        // ignore and just report false
      }
      finally
      {
        db.Close();
      }

      return state;
    }

    //
    // ExecuteScalarQuery:  executes a scalar Select query, returning the single result 
    // as an object.  
    //
    // Example:  Select CID From Customers Where Name='Jane Doe';
    //
    public static object ExecuteScalarQuery(string sql)
    {
      SqlConnection db = null;

      try
      {
        db = new SqlConnection(connectionInfo);
        db.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = db;
        cmd.CommandText = sql;

        object result = cmd.ExecuteScalar();

        return result;
      }
      catch(Exception ex)
      {
        //
        // something failed, so rethrow the exception so caller knows:
        //
        ExceptionDispatchInfo.Capture(ex).Throw();  // rethrow while preserving stack
        throw;  // avoid compiler warnings
      }
      finally
      {
        //
        // close connection:
        //
        if (db != null && db.State != ConnectionState.Closed)
          db.Close();
      }
    }

    // 
    // ExecuteNonScalarQuery:  executes a Select query that generates a temporary table,
    // returning this table in the form of a Dataset.
    //
    public static DataSet ExecuteNonScalarQuery(string sql)
    {
      SqlConnection db = null;

      try
      {
        db = new SqlConnection(connectionInfo);
        db.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = db;

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();

        cmd.CommandText = sql;
        adapter.Fill(ds);

        db.Close();

        return ds;
      }
      catch (Exception ex)
      {
        //
        // something failed, so rethrow the exception so caller knows:
        //
        ExceptionDispatchInfo.Capture(ex).Throw();  // rethrow while preserving stack
        throw;  // avoid compiler warnings
      }
      finally
      {
        //
        // close connection:
        //
        if (db != null && db.State != ConnectionState.Closed)
          db.Close();
      }
    }

    //
    // ExecutionActionQuery:  executes an Insert, Update or Delete query, and returns
    // the number of records modified.
    //
    public static int ExecuteActionQuery(string sql)
    {
      SqlConnection db = null;

      try
			{
        db = new SqlConnection(connectionInfo);
        db.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = db;
        cmd.CommandText = sql;

        int rowsModified = cmd.ExecuteNonQuery();

        return rowsModified;
      }
      catch (Exception ex)
      {
        //
        // something failed, so rethrow the exception so caller knows:
        //
        ExceptionDispatchInfo.Capture(ex).Throw();  // rethrow while preserving stack
        throw;  // avoid compiler warnings
      }
      finally
      {
        //
        // close connection:
        //
        if (db != null && db.State != ConnectionState.Closed)
          db.Close();
      }
    }

  }//class

}//namespace
