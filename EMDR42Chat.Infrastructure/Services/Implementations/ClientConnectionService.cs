﻿using EMDR42Chat.Domain.Commons.DTO;
using EMDR42Chat.Domain.Models;
using EMDR42Chat.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using SqlKata.Execution;

namespace EMDR42Chat.Infrastructure.Services.Implementations;

public class ClientConnectionService(IDbConnectionManager connection) : IClientConnectionService
{
    private const string TableName = "client_connections";
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

    public async Task<string?> GetConnectionId(string email)
    {
        var query = _query.Query(TableName)
            .Where("client_email", email)
            .Select("connection_id")
            .OrderByDesc("created_at");

        var result = await _query.FirstOrDefaultAsync<string>(query);
        return result;
    }

    public async Task<string> GetEmailAsync(string connectionId)
    {
        var query = _query.Query(TableName)
            .Where("connection_id", connectionId)
            .Select("client_email")
            .OrderByDesc("created_at");

        var result = await _query.FirstOrDefaultAsync<string>(query);
        return result;
    }

    public int UpdateConnection(string email, string connectionId)
    {
        return _query.Query(TableName)
            .Where("client_email", email)
            .Update( new { connection_id = connectionId});
    }
}
