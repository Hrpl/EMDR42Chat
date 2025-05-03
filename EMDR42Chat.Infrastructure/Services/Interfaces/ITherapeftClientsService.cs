using EMDR42Chat.Domain.Commons.DTO;
using EMDR42Chat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMDR42Chat.Infrastructure.Services.Interfaces;

public interface ITherapeftClientsService
{
    public Task<string> Get(string clientEmail);
    public Task<int> Create(TherapeftClientsModel model);
    public void Update(string clientEmail, string therapeftEmail);
    public void Delete(string therapeftEmail);
}
