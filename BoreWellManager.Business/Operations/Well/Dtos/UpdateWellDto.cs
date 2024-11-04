using BoreWellManager.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.Well.Dtos
{
    public class UpdateWellDto
    {
        public string XCordinat { get; set; }
        public string YCordinat { get; set; }
        public int UserId { get; set; }
        public int LandId { get; set; }
        public decimal Debi { get; set; }
        public decimal StaticLevel { get; set; }
        public decimal DynamicLevel { get; set; }
    }
}
