using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMDR42Chat.Infrastructure.Services.Interfaces;

public interface IRedisService
{
    public Task SetValueAsync(string key, string value);

    public Task<string> GetValueAsync(string key);

    public Task DeleteAsync(string key);
}
