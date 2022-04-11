﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.DL.Models;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IPHCRepository:IRepository<Phcmaster>
    {
        Task<Phcmaster> GetByID(int id);
        Task<Phcmaster> GetByPHCUserID(int userId);
    }
}