using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.Well.Dtos
{
    public class AddWellMeasureDto
    {
        public decimal Debi { get; set; }
        public decimal StaticLevel { get; set; }
        public decimal DynamicLevel { get; set; }
    }
}
