using EMDR42Chat.Domain.Commons.DTO;
using EMDR42Chat.Domain.Models;
using EMDR42Chat.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using SqlKata.Execution;

namespace EMDR42Chat.Infrastructure.Services.Implementations;

public class ClientConnectionService(IDbConnectionManager connection) : IClientConnectionService
{
    private const string TableName = "table";
    private readonly QueryFactory _query = connection.PostgresQueryFactory;
    public async Task<int> CreateAsync(ClientConnectionModel model)
    {
        var query = _query.Query(TableName)
            .AsInsert(model);

        return await _query.ExecuteAsync(query);
    }

    public async Task<int> DeleteByConnectionId(string connectionId)
    {
        var query = _query.Query(TableName)
            .Where("connection_id", connectionId)
            .Delete();

        return query;
    }

    public async Task<string> GetConnectionId(string email)
    {
        var query = _query.Query(TableName)
            .Where("client_email", email)
            .Select("connection_id")
            .OrderByDesc("created_at");

        return await _query.FirstOrDefaultAsync<string>(query);
    }
}
