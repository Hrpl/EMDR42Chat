using EMDR42Chat.Infrastructure.Services.Interfaces;
using EMDR42Chat.Domain.Commons.Singleton;
using Microsoft.Extensions.Logging;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace EMDR42Chat.Infrastructure.Services.Implementations;

public class DbConnectionManager : IDbConnectionManager
{
    private readonly Config _config;
    private readonly ILogger<DbConnectionManager> _logger;

    public DbConnectionManager(Config configuration, ILogger<DbConnectionManager> logger)
    {
        _config = configuration;
        _logger = logger;
    }
    private string NpgsqlConnectionString => $"Host={_config.DbHost};Port={_config.DbPort};Database={_config.DbName};Username={_config.DbUser};Password={_config.DbPassword};";


    public NpgsqlConnection PostgresDbConnection => new(NpgsqlConnectionString);

    public QueryFactory PostgresQueryFactory => new(PostgresDbConnection, new PostgresCompiler(), 60)
    {
        Logger = compiled => { _logger.LogInformation("Query = {@Query}", compiled.ToString()); }
    };
}
