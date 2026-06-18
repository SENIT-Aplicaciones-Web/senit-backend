using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions for shared database conventions.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies snake case naming convention to database objects.
    /// </summary>
    /// <param name="builder">
    ///     The model builder.
    /// </param>
    public static void UseSnakeCaseNamingConvention(this ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (!string.IsNullOrWhiteSpace(tableName))
                entity.SetTableName(tableName.Pluralize().Underscore());

            foreach (var property in entity.GetProperties())
                property.SetColumnName(property.GetColumnName().Underscore());

            foreach (var key in entity.GetKeys())
            {
                var keyName = key.GetName();
                if (!string.IsNullOrWhiteSpace(keyName))
                    key.SetName(keyName.Underscore());
            }

            foreach (var foreignKey in entity.GetForeignKeys())
            {
                var foreignKeyName = foreignKey.GetConstraintName();
                if (!string.IsNullOrWhiteSpace(foreignKeyName))
                    foreignKey.SetConstraintName(foreignKeyName.Underscore());
            }

            foreach (var index in entity.GetIndexes())
            {
                var indexName = index.GetDatabaseName();
                if (!string.IsNullOrWhiteSpace(indexName))
                    index.SetDatabaseName(indexName.Underscore());
            }
        }
    }
}
