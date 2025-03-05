using Npgsql;
using SqlKata.Execution;

namespace EMDR42Chat.Infrastructure.Services.Interfaces;

public interface IDbConnectionManager
{
    public QueryFactory PostgresQueryFactory { get; }
    public NpgsqlConnection PostgresDbConnection { get; }
}
