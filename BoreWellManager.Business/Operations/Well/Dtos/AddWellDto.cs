using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.Well.Dtos
{
    public class AddWellDto
    {
        public string XCordinat { get; set; }
        public string YCordinat { get; set; }
        public int UserId { get; set; }
        public int LandId { get; set; }
    }
}
