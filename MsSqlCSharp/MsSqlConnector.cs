using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MsSqlCSharp
{
    public class MsSqlConnector
    {
        private string connStr = @"Data Source=mssql/SQLEXPRESS; Initial DB=tempdb; User ID=SA; Password=ABCabc123!";
        private SqlConnection mssql;
        
        public MsSqlConnector()
        {
            try
            {
                mssql = new SqlConnection(connStr);
                mssql.Open();
                Console.WriteLine("Connection Successful");
            }
            catch (Exception err)
            {
                Console.WriteLine("Connection Failed" + err.Message);
            }
        }

        public DataTable getTableList()
        {
            var table = new DataTable();
            try
            {
                table = mssql.GetSchema("Tables");
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return table;
        }

        public DataTable getViewList()
        {
            var table = new DataTable();
            try
            {
                table = mssql.GetSchema("Views");
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return table;
        }

        public DataTable getStoredProcList()
        {
            var table = new DataTable();
            try
            {
                table = mssql.GetSchema("StoredProcedures");
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return table;
        }

        public DataTable getFunctionList()
        {
            var table = new DataTable();
            try
            {
                table = mssql.GetSchema("Functions");
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return table;
        }

        public DataTable executeSQLQuery(string query)
        {
            var table = new DataTable();
            try
            {
                var command = new SqlCommand(query, mssql);
                command.CommandType = CommandType.Text;
                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return table;
        }
        
        public DataTable readTable(string tableName)
        {
            var table = new DataTable();
            try
            {
                var command = new SqlCommand(tableName, mssql);
                command.CommandType = CommandType.TableDirect;
                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return table;
        }
        
        public DataTable readView(string viewName)
        {
            var table = new DataTable();
            try
            {
                var query = $"select * from {viewName}";
                table = executeSQLQuery(query);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return table;
        }
        
        public DataTable executeStoredProcedure(string storedProcName)
        {
            var table = new DataTable();
            try
            {
                var command = new SqlCommand(storedProcName, mssql);
                command.CommandType = CommandType.StoredProcedure;
                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return table;
        }

        public DataTable executeFunction(string functionName)
        {
            var table = new DataTable();
            try
            {
                var query = $@"select {functionName}()";
                table = executeSQLQuery(query);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return table;
        }
    }
}