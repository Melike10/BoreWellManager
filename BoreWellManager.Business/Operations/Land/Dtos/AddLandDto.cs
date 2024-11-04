using BoreWellManager.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.Land.Dtos
{
    public class AddLandDto
    {
        public string City { get; set; }
        public string Town { get; set; }
        public string Block { get; set; } // ada
        public string Plot { get; set; }//parsel
        public string Street { get; set; }
        public string Location { get; set; }//mevki
        public string LandType { get; set; }
        public bool HasLien { get; set; }//şerh,irtifak,beyan var mı?
        public bool IsCksRequired { get; set; }//ÇKS GEREKLİ Mİ
        public LienType LienType { get; set; }

        public List<int> UserIds { get; set; }

    }
}
