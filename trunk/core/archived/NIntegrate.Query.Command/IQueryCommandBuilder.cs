using System.Data.Common;

namespace NIntegrate.Query.Command
{
    /// <summary>
    /// The intefrace for all QueryCommandBuilders
    /// </summary>
    public interface IQueryCommandBuilder
    {
        /// <summary>
        /// Format a name string to a parameter name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        string ToParameterName(string name);
        /// <summary>
        /// Gets the database object name quote characters.
        /// </summary>
        /// <returns></returns>
        string GetDatabaseObjectNameQuoteCharacters();
        /// <summary>
        /// Gets the db provider factory.
        /// </summary>
        /// <returns></returns>
        DbProviderFactory GetDbProviderFactory();
        /// <summary>
        /// Builds the query command.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        DbCommand BuildQueryCommand(Criteria criteria);
        /// <summary>
        /// Builds the count command.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        DbCommand BuildCountCommand(Criteria criteria);
    }
}
