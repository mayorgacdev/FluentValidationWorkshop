namespace Workshop.Api;

using System.Data;
using Dapper;

public static class CustomerStore
{
    public static async ValueTask<EntityId?> CreateCustomerAsync(this IStore<Customer> Store, CreateCustomerRequest Request)
    {
        DynamicParameters Parameters = Request.ToDynamicParameters();
        Parameters.Add(nameof(Customer.CustomerId), Guid.NewGuid(), DbType.Guid, ParameterDirection.Input);

        using var SqlTransaction = await Store.BeginTransactionAsync();

        try
        {
            await Store.ExecuteStoredProcedureAsync("[dbo].[InsertCustomer]", Parameters, SqlTransaction);
            SqlTransaction.Commit();

            return Parameters.Get<Guid?>(nameof(Customer.CustomerId));
        }
        catch
        {
            SqlTransaction.Rollback();
            throw;
        }
    }


    public static async ValueTask<CustomerResponse?> FetchCustomerByPhoneNumberAsync(this IStore<Customer> Store, string PhoneNumber)
    {
        const string Query =
        """
        SELECT * FROM [dbo].[Customer] WHERE [PhoneNumber] = @PhoneNumber
        """;

        return await Store.Connection.QueryFirstOrDefaultAsync<CustomerResponse>(Query, new { PhoneNumber = PhoneNumber });
    }

    public static async ValueTask<CustomerResponse?> FetchCustomerByEmailAsync(this IStore<Customer> Store, string Email)
    {
        const string Query =
        """
        SELECT * FROM [dbo].[Customer] WHERE [Email] = @Email
        """;

        return await Store.Connection.QueryFirstOrDefaultAsync<CustomerResponse>(Query, new { Email = Email });
    }

    public static async ValueTask<CustomerResponse?> FetchCustomerByPassportNumberAsync(this IStore<Customer> Store, string PassportNumber)
    {
        const string Query =
        """
        SELECT * FROM [dbo].[Customer] WHERE [PassportNumber] = @PassportNumber
        """;

        return await Store.Connection.QueryFirstOrDefaultAsync<CustomerResponse>(Query, new { PassportNumber });
    }
}
