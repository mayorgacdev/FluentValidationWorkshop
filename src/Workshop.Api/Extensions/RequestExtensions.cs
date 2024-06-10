namespace Workshop.Api.Extensions;

using System.Data;
using System.Reflection;
using Dapper;

public static class RequestExtensions
{
    public static DynamicParameters ToDynamicParameters(this IRequest Request)
    {
        var Parameters = new DynamicParameters();

        PropertyInfo[] Properties = Request.GetType().GetProperties();

        foreach (var Property in Properties)
        {
            switch (Property.GetValue(Request))
            {
                case IEnumerable<IRequest> Values:
                    {
                        Parameters.Add(Property.Name, Values.ToDataTable(), DbType.Object, ParameterDirection.Input);
                        break;
                    }
                case object Value:
                    {
                        Parameters.Add(Property.Name, Value);
                        break;
                    }
            }
        }

        return Parameters;
    }
}
