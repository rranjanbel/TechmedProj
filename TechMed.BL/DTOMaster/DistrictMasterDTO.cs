﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class DistrictMasterDTO
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public string DistrictName { get; set; } = null!;
    }
}
