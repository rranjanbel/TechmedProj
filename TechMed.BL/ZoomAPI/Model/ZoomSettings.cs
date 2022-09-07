using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ZoomAPI.Model
{
    public class ZoomSettings
    {
        /// <summary>
        /// The zoom server to server account clientID.
        /// </summary>
        public string ZoomSSAccountId { get; set; }
        /// <summary>
        /// The zoom server to server oauth clientID.
        /// </summary>
        public string ZoomSSClientID { get; set; }
        /// <summary>
        /// The zoom server to server oauth ClientSecret.
        /// </summary>
        public string ZoomSSClientSecret { get; set; }

    }
}
