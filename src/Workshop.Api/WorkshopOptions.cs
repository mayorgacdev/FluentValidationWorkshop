namespace Workshop.Api;

using System.Data.Common;
using System.Data.SqlClient;

/// <summary>
///     Options for configuring the workshop.
/// </summary>
public class WorkshopOptions
{
    /// <summary>
    ///     Connection string to the server.
    /// </summary>
    /// <seealso cref="ConnectionType" />
    public required string ConnectionString { get; set; }

    /// <summary>
    ///     Type of server connection string.
    ///     Allows creating the database provider correctly.
    /// </summary>
    /// <seealso cref="CreateDbConnection" />
    public required string ConnectionType { get; set; }

    /// <summary>
    ///     Factory method that creates a database provider.
    /// </summary>
    /// <returns>
    ///     Returns a configured database provider:
    ///
    ///     <list type="bullet">
    ///         <item><see cref="SqlConnection" /></item>
    ///         <description>SQL Server provider</description>
    ///     </list>
    /// </returns>
    public DbConnection CreateDbConnection()
    {
        return ConnectionType switch
        {
            nameof(SqlConnection) => new SqlConnection(ConnectionString),
            _ => throw new NotImplementedException()
        };
    }

}
