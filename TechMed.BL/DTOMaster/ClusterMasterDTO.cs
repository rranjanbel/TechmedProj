﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class ClusterMasterDTO
    {
        public int Id { get; set; }
        public string Cluster { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
