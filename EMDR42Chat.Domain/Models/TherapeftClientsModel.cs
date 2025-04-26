using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMDR42Chat.Domain.Models;

public class TherapeftClientsModel
{
    [SqlKata.Column("client_email")]
    public string ClientEmail { get; set; }

    [SqlKata.Column("therapeft_email")]
    public string TherapeftEmail { get; set; }
    [SqlKata.Column("created_at")]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("updated_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("is_deleted")]
    public bool? IsDeleted { get; set; } = false;
}
