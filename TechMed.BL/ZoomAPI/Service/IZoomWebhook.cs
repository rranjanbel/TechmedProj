﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ZoomAPI.Service
{
    public interface IZoomWebhook
    {
        Task<bool> ZoomWebhookService(string value);
    }
}