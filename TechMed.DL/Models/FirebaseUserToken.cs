using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class FirebaseUserToken
    {
        public long Id { get; set; }
        public int UserMasterId { get; set; }
        public string FirebaseToken { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }

        public virtual UserMaster UserMaster { get; set; } = null!;
    }
}
