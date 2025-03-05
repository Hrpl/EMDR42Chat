using EMDR42Chat.Domain.Commons.DTO;
using EMDR42Chat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMDR42Chat.Infrastructure.Services.Interfaces;

public interface IClientConnectionService
{
    public Task<int> CreateAsync(ClientConnectionModel model);
    public Task<string> GetConnectionId(string email);
    public Task<int> DeleteByConnectionId(string connectionId);
}
