using EMDR42Chat.Domain.Commons.DTO;
using EMDR42Chat.Domain.Models;
using EMDR42Chat.Infrastructure.Services.Interfaces;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EMDR42Chat.Infrastructure.Services.Implementations;

public class TherapeftClientsService(IDbConnectionManager connection) : ITherapeftClientsService
{
    private const string TableName = "therapeft_clients";
    private readonly QueryFactory _query = connection.PostgresQueryFactory;

    public async Task<int> Create(TherapeftClientsModel model)
    {
        var query = _query.Query(TableName)
            .AsInsert(model);

        return await _query.ExecuteAsync(query);
    }

    public async Task<string> Get(string clientEmail)
    {
        var query = _query.Query(TableName)
            .Where("client_email", clientEmail)
            .Select("therapeft_email");

        var result = await _query.FirstOrDefaultAsync<string>(query);
        return result;
    }

    public void Update(string clientEmail, string therapeftEmail)
    {
        _query.Query(TableName)
            .Where("therapeft_email", therapeftEmail)
            .Update(new { client_email = clientEmail });
    }
}
