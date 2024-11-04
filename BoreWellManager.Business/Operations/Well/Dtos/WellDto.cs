using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.Well.Dtos
{
    public class WellDto
    {
        public int Id { get; set; }
        public string XCordinat { get; set; }
        public string YCordinat { get; set; }
        public decimal Debi { get; set; }
        public decimal StaticLevel { get; set; }
        public decimal DynamicLevel { get; set; }
        public int UserId { get; set; }
        public int LandId { get; set; }
    }
}
