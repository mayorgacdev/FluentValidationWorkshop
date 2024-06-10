namespace Workshop.Api.Extensions;

using System.Collections;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;

/// <summary>
///     Utility methods for operations on collections.<br/><br/>
///
///     Introduction to AOT warnings:<br/>
///     <see href="https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/fixing-warnings"/><br/><br/>
///
///     How to make libraries compatible with native AOT:<br/>
///     <see href="https://devblogs.microsoft.com/dotnet/creating-aot-compatible-libraries/"/>
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    ///     Converts an array of <see cref="IRequest" /> into a <see cref="DataTable" />.
    /// </summary>
    /// <param name="Source">
    ///     The collection to be converted into a <see cref="DataTable" />.
    /// </param>
    /// <returns>
    ///     Returns a <see cref="DataTable" /> as the result of converting <paramref name="Source" />.
    /// </returns>
    public static DataTable ToDataTable(this IEnumerable<IRequest> Source)
    {
        if (RuntimeFeature.IsDynamicCodeSupported)
        {
            return Source.ToDataTableWithReflectionEmit();
        }

        // TODO: Create Source Generator with Interceptors for each IRequest that uses this method.
        throw new NotSupportedException();
    }

    /// <summary>
    ///     Converts an array of <see cref="IRequest" /> into a <see cref="DataTable" />.
    /// </summary>
    /// <param name="Source">
    ///     The collection to be converted into a <see cref="DataTable" />.
    /// </param>
    /// <returns>
    ///     Returns a <see cref="DataTable" /> as the result of converting <paramref name="Source" />.
    /// </returns>
    public static DataTable ToDataTableWithReflectionEmit(this IEnumerable<IRequest> Source)
    {
        DataTable Table = new();

        Type ViewModelType = Source.GetType().GetInterface($"{nameof(IEnumerable)}`1")!.GenericTypeArguments[0];
        PropertyInfo[] Properties = ViewModelType.GetProperties();

        foreach (PropertyInfo Property in Properties)
        {
            var ColumnType = Property.PropertyType.GetUnderlyingType();
            Table.Columns.Add(Property.Name, ColumnType);
        }

        foreach (var ViewModel in Source)
        {
            Table.Rows.Add(Properties.Select(Property => Property.GetValue(ViewModel)).ToArray());
        }

        return Table;
    }

    /// <summary>
    ///     Adds a range of elements to <paramref name="Source" />.
    /// </summary>
    /// <typeparam name="TSource">
    ///     The type of the collection.
    /// </typeparam>
    /// <param name="Source">
    ///     The collection to which the new elements will be added.
    /// </param>
    /// <param name="Values">
    ///     List of values to be added.
    /// </param>
    public static void AddRange<TSource>(this ICollection<TSource> Source, params TSource[] Values)
    {
        AddRange(Source, (IEnumerable<TSource>)Values);
    }

    /// <inheritdoc cref="AddRange{TSource}(ICollection{TSource}, TSource[])" />
    public static void AddRange<TSource>(this ICollection<TSource> Source, IEnumerable<TSource> Values)
    {
        foreach (TSource Value in Values)
        {
            Source.Add(Value);
        }
    }

    /// <summary>
    ///     Checks whether enumerable is null or empty.
    /// </summary>
    /// <typeparam name="TSource">
    ///     The type of the enumerable.
    /// </typeparam>
    /// <param name="Source">
    ///     The System.Collections.Generic.IEnumerable`1 to be checked.
    /// </param>
    /// <returns>
    ///     True if enumerable is null or empty, false otherwise.
    /// </returns>
    public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> Source)
    {
        return Source == null || !Source.Any();
    }

    /// <summary>
    ///     Returns an empty <see cref="IEnumerable{T}" /> if <paramref name="Source" /> is null otherwise a default value.
    /// </summary>
    /// <typeparam name="TSource">
    ///     The type of the collection.
    /// </typeparam>
    /// <param name="Source">
    ///     The collection to be checked.
    /// </param>
    /// <returns>
    ///     Returns an <see cref="Enumerable.Empty{TResult}" /> if <paramref name="Source" /> is null
    ///     otherwise a <see cref="Enumerable.DefaultIfEmpty{TSource}(IEnumerable{TSource}, TSource)" />.
    /// </returns>
    public static IEnumerable<TSource> DefaultIfNullOrEmpty<TSource>(this IEnumerable<TSource>? Source)
    {
        return Source ?? [];
    }
}
