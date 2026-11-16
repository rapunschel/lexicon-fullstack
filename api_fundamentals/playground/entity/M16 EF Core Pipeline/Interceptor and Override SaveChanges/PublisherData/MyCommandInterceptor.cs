using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Xml;

namespace PublisherData;

internal class MyCustomDbCommandInterceptor : DbCommandInterceptor
{
    public override InterceptionResult<DbDataReader> ReaderExecuting(
        DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
    {
        //notice that I've changed this since EF8 demo to be
        //explicit about when this is applied
        if (command.CommandText.Contains("query hint"))
        {
            command.CommandText += " OPTION (ROBUST PLAN)";

        }
        return result; 
    }

    //TODO: Duplicate logic for ReaderExecutingAsync method

}

