﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class PatientCaseMedicineDTO
    {
        public long Id { get; set; }
        public long PatientCaseId { get; set; }
        public string Medicine { get; set; } = null!;
        public bool? Morning { get; set; }
        public bool? Noon { get; set; }
        public bool? Night { get; set; }
        public bool? EmptyStomach { get; set; }
        public bool? AfterMeal { get; set; }
        public bool? Od { get; set; }
        public bool? Bd { get; set; }
        public bool? Td { get; set; }
        public string? Dose { get; set; }
    }
}