using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMegStore.DTO
{
    public class UseerDTO
    {
        public int IdUser { get; set; }
        public string? CompleteName { get; set; }
        public string? Email { get; set; }
        public int? IdRole { get; set; }
        public string? RoleDescription { get; set; }
        public string? Password { get; set; }
        public int? Active { get; set; } //Cabiamos de bool a int ya que true sera 1, y el false sera 0  
    }
}
