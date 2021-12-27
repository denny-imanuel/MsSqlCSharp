using System;

namespace MsSqlCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var mssql = new MsSqlConnector();
            var tables = mssql.getTableList();
            var views = mssql.getViewList();
            var procs = mssql.getStoredProcList();
            var funcs = mssql.getFunctionList();
        }
    }
}