using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.User.Dtos
{
    public class UserLandsDto
    {
        public int Id { get; set; }
        public string TC { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public List<LandsDto> Lands { get; set; }
    }
}
