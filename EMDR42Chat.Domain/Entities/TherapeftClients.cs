using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMDR42Chat.Domain.Entities;

public class TherapeftClients : BaseEntity
{
    public string ClientEmail { get; set; }
    public string TherapeftEmail { get; set; }
}
