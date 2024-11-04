using BoreWellManager.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.User.Dtos
{
    public class LandsDto
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Block { get; set; }
        public string Plot { get; set; }
        public string Street { get; set; }
        public string Location { get; set; }
        public string LandType { get; set; }
        public LienType LienType { get; set; }
        public bool IsCksReIsCksRequired { get; set; }
    }
}
