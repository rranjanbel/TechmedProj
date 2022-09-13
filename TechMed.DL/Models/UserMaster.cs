using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class UserMaster
    {
        public UserMaster()
        {
            CdssguidelineMasterCreatedByNavigations = new HashSet<CdssguidelineMaster>();
            CdssguidelineMasterUpdatedByNavigations = new HashSet<CdssguidelineMaster>();
            ClusterMasterCreatedByNavigations = new HashSet<ClusterMaster>();
            ClusterMasterUpdatedByNavigations = new HashSet<ClusterMaster>();
            DivisionMasterCreatedByNavigations = new HashSet<DivisionMaster>();
            DivisionMasterUpdatedByNavigations = new HashSet<DivisionMaster>();
            DoctorMasterCreatedByNavigations = new HashSet<DoctorMaster>();
            DoctorMasterUpdatedByNavigations = new HashSet<DoctorMaster>();
            DoctorMasterUsers = new HashSet<DoctorMaster>();
            EmployeeTrainingCreatedByNavigations = new HashSet<EmployeeTraining>();
            EmployeeTrainingUpdatedByNavigations = new HashSet<EmployeeTraining>();
            EquipmentUptimeReportCreatedByNavigations = new HashSet<EquipmentUptimeReport>();
            EquipmentUptimeReportUpdatedByNavigations = new HashSet<EquipmentUptimeReport>();
            FirebaseUserTokens = new HashSet<FirebaseUserToken>();
            InverseCreatedByNavigation = new HashSet<UserMaster>();
            InverseUpdatedByNavigation = new HashSet<UserMaster>();
            LoginHistories = new HashSet<LoginHistory>();
            NotificationFromUserNavigations = new HashSet<Notification>();
            NotificationToUserNavigations = new HashSet<Notification>();
            PageMasterCreatedByNavigations = new HashSet<PageMaster>();
            PageMasterUpdatedByNavigations = new HashSet<PageMaster>();
            PhcmasterCreatedByNavigations = new HashSet<Phcmaster>();
            PhcmasterUpdatedByNavigations = new HashSet<Phcmaster>();
            PhcmasterUsers = new HashSet<Phcmaster>();
            UserDetailCreatedByNavigations = new HashSet<UserDetail>();
            UserDetailUpdatedByNavigations = new HashSet<UserDetail>();
            UserDetailUsers = new HashSet<UserDetail>();
            VideoCallTransactionFromUsers = new HashSet<VideoCallTransaction>();
            VideoCallTransactionToUsers = new HashSet<VideoCallTransaction>();
            ZoomUserDetailUsers=new HashSet<ZoomUserDetail>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string? Mobile { get; set; } = null!;
        public string? Name { get; set; } = null!;
        public string HashPassword { get; set; } = null!;
        public int? LoginAttempts { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool? IsPasswordChanged { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual UserMaster? CreatedByNavigation { get; set; }
        public virtual UserMaster? UpdatedByNavigation { get; set; }
        public virtual ICollection<CdssguidelineMaster> CdssguidelineMasterCreatedByNavigations { get; set; }
        public virtual ICollection<CdssguidelineMaster> CdssguidelineMasterUpdatedByNavigations { get; set; }
        public virtual ICollection<ClusterMaster> ClusterMasterCreatedByNavigations { get; set; }
        public virtual ICollection<ClusterMaster> ClusterMasterUpdatedByNavigations { get; set; }
        public virtual ICollection<DivisionMaster> DivisionMasterCreatedByNavigations { get; set; }
        public virtual ICollection<DivisionMaster> DivisionMasterUpdatedByNavigations { get; set; }
        public virtual ICollection<DoctorMaster> DoctorMasterCreatedByNavigations { get; set; }
        public virtual ICollection<DoctorMaster> DoctorMasterUpdatedByNavigations { get; set; }
        public virtual ICollection<DoctorMaster> DoctorMasterUsers { get; set; }
        public virtual ICollection<EmployeeTraining> EmployeeTrainingCreatedByNavigations { get; set; }
        public virtual ICollection<EmployeeTraining> EmployeeTrainingUpdatedByNavigations { get; set; }
        public virtual ICollection<EquipmentUptimeReport> EquipmentUptimeReportCreatedByNavigations { get; set; }
        public virtual ICollection<EquipmentUptimeReport> EquipmentUptimeReportUpdatedByNavigations { get; set; }
        public virtual ICollection<FirebaseUserToken> FirebaseUserTokens { get; set; }
        public virtual ICollection<UserMaster> InverseCreatedByNavigation { get; set; }
        public virtual ICollection<UserMaster> InverseUpdatedByNavigation { get; set; }
        public virtual ICollection<LoginHistory> LoginHistories { get; set; }
        public virtual ICollection<Notification> NotificationFromUserNavigations { get; set; }
        public virtual ICollection<Notification> NotificationToUserNavigations { get; set; }
        public virtual ICollection<PageMaster> PageMasterCreatedByNavigations { get; set; }
        public virtual ICollection<PageMaster> PageMasterUpdatedByNavigations { get; set; }
        public virtual ICollection<Phcmaster> PhcmasterCreatedByNavigations { get; set; }
        public virtual ICollection<Phcmaster> PhcmasterUpdatedByNavigations { get; set; }
        public virtual ICollection<Phcmaster> PhcmasterUsers { get; set; }
        public virtual ICollection<UserDetail> UserDetailCreatedByNavigations { get; set; }
        public virtual ICollection<UserDetail> UserDetailUpdatedByNavigations { get; set; }
        public virtual ICollection<UserDetail> UserDetailUsers { get; set; }
        public virtual ICollection<VideoCallTransaction> VideoCallTransactionFromUsers { get; set; }
        public virtual ICollection<VideoCallTransaction> VideoCallTransactionToUsers { get; set; }
        public virtual ICollection<ZoomUserDetail> ZoomUserDetailUsers { get; set; }
    }
}
