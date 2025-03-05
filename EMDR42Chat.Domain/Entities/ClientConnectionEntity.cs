using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMDR42Chat.Domain.Entities;

public class ClientConnectionEntity : BaseEntity
{
    public string ClientEmail { get; set; }
    public string ConnectionId { get; set; }
}
