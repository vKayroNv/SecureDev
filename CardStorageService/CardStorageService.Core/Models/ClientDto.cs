using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardStorageService.Core.Models
{
    public class ClientDto
    {
        public int ClientId { get; set; }
        public string Surname { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Patronymic { get; set; }
    }
}
