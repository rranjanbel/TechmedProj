using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using TechMed.DL.ViewModel;

namespace TechMed.DL.Models
{
    public partial class TeleMedecineContext : DbContext
    {
        public TeleMedecineContext()
        {
        }

        public TeleMedecineContext(DbContextOptions<TeleMedecineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BlockMaster> BlockMasters { get; set; } = null!;
        public virtual DbSet<CaseFileStatusMaster> CaseFileStatusMasters { get; set; } = null!;
        public virtual DbSet<CdssguidelineMaster> CdssguidelineMasters { get; set; } = null!;
        public virtual DbSet<ClusterMaster> ClusterMasters { get; set; } = null!;
        public virtual DbSet<CountryMaster> CountryMasters { get; set; } = null!;
        public virtual DbSet<DistrictMaster> DistrictMasters { get; set; } = null!;
        public virtual DbSet<DivisionMaster> DivisionMasters { get; set; } = null!;
        public virtual DbSet<DoctorMaster> DoctorMasters { get; set; } = null!;
        public virtual DbSet<FeedbackQuestionMaster> FeedbackQuestionMasters { get; set; } = null!;
        public virtual DbSet<GenderMaster> GenderMasters { get; set; } = null!;
        public virtual DbSet<IdproofTypeMaster> IdproofTypeMasters { get; set; } = null!;
        public virtual DbSet<LoginHistory> LoginHistories { get; set; } = null!;
        public virtual DbSet<LoginRoleDelete> LoginRoleDeletes { get; set; } = null!;
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; } = null!;
        public virtual DbSet<MedicineMaster> MedicineMasters { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<PageAccess> PageAccesses { get; set; } = null!;
        public virtual DbSet<PageMaster> PageMasters { get; set; } = null!;
        public virtual DbSet<PatientCase> PatientCases { get; set; } = null!;
        public virtual DbSet<PatientCaseDocument> PatientCaseDocuments { get; set; } = null!;
        public virtual DbSet<PatientCaseFeedback> PatientCaseFeedbacks { get; set; } = null!;
        public virtual DbSet<PatientCaseMedicine> PatientCaseMedicines { get; set; } = null!;
        public virtual DbSet<PatientCasePrescriptionDelete> PatientCasePrescriptionDeletes { get; set; } = null!;
        public virtual DbSet<PatientCaseSymptomDelete> PatientCaseSymptomDeletes { get; set; } = null!;
        public virtual DbSet<PatientCaseVital> PatientCaseVitals { get; set; } = null!;
        public virtual DbSet<PatientMaster> PatientMasters { get; set; } = null!;
        public virtual DbSet<PatientQueue> PatientQueues { get; set; } = null!;
        public virtual DbSet<PatientStatusMaster> PatientStatusMasters { get; set; } = null!;
        public virtual DbSet<Phcmaster> Phcmasters { get; set; } = null!;
        public virtual DbSet<RoleMasterDelete> RoleMasterDeletes { get; set; } = null!;
        public virtual DbSet<Setting> Settings { get; set; } = null!;
        public virtual DbSet<SpecialityMasterDelete> SpecialityMasterDeletes { get; set; } = null!;
        public virtual DbSet<SpecializationMaster> SpecializationMasters { get; set; } = null!;
        public virtual DbSet<StateMaster> StateMasters { get; set; } = null!;
        public virtual DbSet<SubSpecializationMaster> SubSpecializationMasters { get; set; } = null!;
        public virtual DbSet<SymptomsMaster> SymptomsMasters { get; set; } = null!;
        public virtual DbSet<TitleMaster> TitleMasters { get; set; } = null!;
        public virtual DbSet<UserDetail> UserDetails { get; set; } = null!;
        public virtual DbSet<UserMaster> UserMasters { get; set; } = null!;
        public virtual DbSet<UserTypeMaster> UserTypeMasters { get; set; } = null!;
        public virtual DbSet<UserUsertype> UserUsertypes { get; set; } = null!;
        public virtual DbSet<VideoCallTransaction> VideoCallTransactions { get; set; } = null!;
        public virtual DbSet<VitalMaster> VitalMasters { get; set; } = null!;
        public virtual DbSet<ZoneMaster> ZoneMasters { get; set; } = null!;

        public virtual DbSet<SPResultGetPatientDetails> SPResultGetPatientDetails { get; set; } = null!;
        public virtual DbSet<SpecializationReportVM> SpecializationReport { get; set; } = null!;
        public virtual DbSet<LoggedUserCountVM> LoggedUserCount { get; set; } = null!;
        public virtual DbSet<CompletedConsultantVM> CompletedConsultant { get; set; } = null!;
        public virtual DbSet<PatientCaseQueDetail> PatientCaseQueDetails { get; set; } = null!;
        public virtual DbSet<PatientSearchResultVM> PatientSearchResults { get; set; } = null!;
        public virtual DbSet<DoctorPatientSearchVM> DoctorPatientSearchResults { get; set; } = null!;
        public virtual DbSet<ConsultedPatientByDoctorAndPHCVM> ConsultedPatientByDoctorAndPHCResults { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=20.219.126.193;Database=TeleMedecine;User Id= rroshan; Password= Te!e#2002;");
                IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
                var connectionString = configuration.GetConnectionString("TeliMedConn");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlockMaster>(entity =>
            {
                entity.ToTable("BlockMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BlockName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.ZoneId).HasColumnName("ZoneID");
            });

            modelBuilder.Entity<CaseFileStatusMaster>(entity =>
            {
                entity.ToTable("CaseFileStatusMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FileStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CdssguidelineMaster>(entity =>
            {
                entity.ToTable("CDSSGuidelineMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cdssguideline)
                    .IsUnicode(false)
                    .HasColumnName("CDSSGuideline");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CdssguidelineMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_CDSSGuidelineMaster_UserMasterCreatedby");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.CdssguidelineMasterUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_CDSSGuidelineMaster_UserMasterUpdatedBy");
            });

            modelBuilder.Entity<ClusterMaster>(entity =>
            {
                entity.ToTable("ClusterMaster");

                entity.HasIndex(e => e.Cluster, "IX_ClusterMaster")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cluster)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ClusterMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_ClusterMaster_UserMasterCreatedBy");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ClusterMasterUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ClusterMaster_UserMasterUpdatedBy");
            });

            modelBuilder.Entity<CountryMaster>(entity =>
            {
                entity.ToTable("CountryMaster");

                entity.HasIndex(e => e.CountryName, "IX_CountryMaster")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DistrictMaster>(entity =>
            {
                entity.ToTable("DistrictMaster");

                entity.HasIndex(e => e.DistrictName, "IX_DistrictMaster")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DistrictName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.ZoneId)
                    .HasColumnName("ZoneID")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.DistrictMasters)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DistrictMaster_StateMaster");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.DistrictMasters)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DistrictMaster_ZoneMaster");
            });

            modelBuilder.Entity<DivisionMaster>(entity =>
            {
                entity.ToTable("DivisionMaster");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ClusterId).HasColumnName("ClusterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DivisionName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<DoctorMaster>(entity =>
            {
                entity.ToTable("DoctorMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.BranchName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ClusterId).HasColumnName("ClusterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Designation)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DigitalSignature)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Ifsccode)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("IFSCCode");

                entity.Property(e => e.LastOnlineAt).HasColumnType("datetime");

                entity.Property(e => e.Mciid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MCIID");

                entity.Property(e => e.Panno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PANNo")
                    .IsFixedLength();

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Qualification)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SpecializationId).HasColumnName("SpecializationID");

                entity.Property(e => e.SubSpecializationId).HasColumnName("SubSpecializationID");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ZoneId).HasColumnName("ZoneID");

                entity.HasOne(d => d.Cluster)
                    .WithMany(p => p.DoctorMasters)
                    .HasForeignKey(d => d.ClusterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DoctorMaster_ClusterMaster");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.DoctorMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_DoctorMaster_UserMasterCreatedBy");

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.DoctorMasters)
                    .HasForeignKey(d => d.SpecializationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DoctorMaster_SpecializationMaster");

                entity.HasOne(d => d.SubSpecialization)
                    .WithMany(p => p.DoctorMasters)
                    .HasForeignKey(d => d.SubSpecializationId)
                    .HasConstraintName("FK_DoctorMaster_SubSpecializationMaster");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.DoctorMasterUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_DoctorMaster_UserMasterUpdatedBy");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DoctorMasterUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DoctorMaster_UserMaster");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.DoctorMasters)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DoctorMaster_ZoneMaster");
            });

            modelBuilder.Entity<FeedbackQuestionMaster>(entity =>
            {
                entity.ToTable("FeedbackQuestionMaster");

                entity.HasIndex(e => e.Question, "IX_FeedbackQuestionMaster")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Question)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GenderMaster>(entity =>
            {
                entity.ToTable("GenderMaster");

                entity.HasIndex(e => e.Gender, "IX_GenderMaster")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IdproofTypeMaster>(entity =>
            {
                entity.ToTable("IDProofTypeMaster");

                entity.HasIndex(e => e.IdproofType, "IX_IDProofTypeMaster")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdproofType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IDProofType");
            });

            modelBuilder.Entity<LoginHistory>(entity =>
            {
                entity.ToTable("LoginHistory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogedInTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LogedoutTime).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LoginHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoginHistory_UserMaster");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.LoginHistories)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoginHistory_UserTypeMaster");
            });

            modelBuilder.Entity<LoginRoleDelete>(entity =>
            {
                entity.ToTable("LoginRole.delete");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.LoginId).HasColumnName("LoginID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");
            });

            modelBuilder.Entity<MaritalStatus>(entity =>
            {
                entity.ToTable("MaritalStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MedicineMaster>(entity =>
            {
                entity.ToTable("MedicineMaster");

                entity.HasIndex(e => e.MedicineName, "IX_MedicineMaster")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Details)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MedicineName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Message).IsUnicode(false);

                entity.Property(e => e.SeenOn).HasColumnType("datetime");

                entity.HasOne(d => d.FromUserNavigation)
                    .WithMany(p => p.NotificationFromUserNavigations)
                    .HasForeignKey(d => d.FromUser)
                    .HasConstraintName("FK_Notification_UserMasterFrom");

                entity.HasOne(d => d.ToUserNavigation)
                    .WithMany(p => p.NotificationToUserNavigations)
                    .HasForeignKey(d => d.ToUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_UserMasterTo");
            });

            modelBuilder.Entity<PageAccess>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PageAccess");

                entity.Property(e => e.PageId).HasColumnName("PageID");

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

                entity.HasOne(d => d.Page)
                    .WithMany()
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PageAccess_PageMaster");

                entity.HasOne(d => d.UserType)
                    .WithMany()
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PageAccess_UserTypeMaster");
            });

            modelBuilder.Entity<PageMaster>(entity =>
            {
                entity.ToTable("PageMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Detail)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Module)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PageName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PageMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_PageMaster_UserMasterCreatedBy");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.PageMasterUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_PageMaster_UserMasterUpdatedBy");
            });

            modelBuilder.Entity<PatientCase>(entity =>
            {
                entity.ToTable("PatientCase");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Allergies)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.CaseFileNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CaseHeading)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Diagnosis)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.FamilyHistory)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Finding)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Instruction)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Observation)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.Property(e => e.Prescription).IsUnicode(false);

                entity.Property(e => e.SpecializationId).HasColumnName("SpecializationID");

                entity.Property(e => e.Symptom)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Test)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PatientCaseCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientCase_UserMasterCreatedBy");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientCases)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientCase_PatientMaster");

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.PatientCases)
                    .HasForeignKey(d => d.SpecializationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientCase_SpecializationMaster");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.PatientCaseUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_PatientCase_UserMasterUpdatedBy");
            });

            modelBuilder.Entity<PatientCaseDocument>(entity =>
            {
                entity.ToTable("PatientCaseDocument");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentPath)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.PatientCaseId).HasColumnName("PatientCaseID");

                entity.HasOne(d => d.PatientCase)
                    .WithMany(p => p.PatientCaseDocuments)
                    .HasForeignKey(d => d.PatientCaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientCaseDocument_PatientCase");
            });

            modelBuilder.Entity<PatientCaseFeedback>(entity =>
            {
                entity.ToTable("PatientCaseFeedback");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Datetime).HasColumnType("datetime");

                entity.Property(e => e.PatientCaseId).HasColumnName("PatientCaseID");

                entity.Property(e => e.Question)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.PatientCase)
                    .WithMany(p => p.PatientCaseFeedbacks)
                    .HasForeignKey(d => d.PatientCaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientCaseFeedback_PatientCase");
            });

            modelBuilder.Entity<PatientCaseMedicine>(entity =>
            {
                entity.ToTable("PatientCaseMedicine");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Bd).HasColumnName("BD");

                entity.Property(e => e.Dose)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Medicine)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Od).HasColumnName("OD");

                entity.Property(e => e.PatientCaseId).HasColumnName("PatientCaseID");

                entity.Property(e => e.Td).HasColumnName("TD");

                entity.HasOne(d => d.PatientCase)
                    .WithMany(p => p.PatientCaseMedicines)
                    .HasForeignKey(d => d.PatientCaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientCaseMedicine_PatientCase");
            });

            modelBuilder.Entity<PatientCasePrescriptionDelete>(entity =>
            {
                entity.ToTable("PatientCasePrescription.delete");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<PatientCaseSymptomDelete>(entity =>
            {
                entity.ToTable("PatientCaseSymptom.delete");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<PatientCaseVital>(entity =>
            {
                entity.ToTable("PatientCaseVital");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.PatientCaseId).HasColumnName("PatientCaseID");

                entity.Property(e => e.Value)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VitalId).HasColumnName("VitalID");

                entity.HasOne(d => d.PatientCase)
                    .WithMany(p => p.PatientCaseVitals)
                    .HasForeignKey(d => d.PatientCaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientCaseVital_PatientCase");

                entity.HasOne(d => d.Vital)
                    .WithMany(p => p.PatientCaseVitals)
                    .HasForeignKey(d => d.VitalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientCaseVital_VitalMaster");
            });

            modelBuilder.Entity<PatientMaster>(entity =>
            {
                entity.ToTable("PatientMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.IdproofId).HasColumnName("IDProofID");

                entity.Property(e => e.IdproofNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IDProofNumber");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.Property(e => e.PatientStatusId).HasColumnName("PatientStatusID");

                entity.Property(e => e.Phcid).HasColumnName("PHCID");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PinCode)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.PatientMasters)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientMaster_CountryMaster");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PatientMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_PatientMaster_PHCMasterCreatedBy");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.PatientMasters)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientMaster_DistrictMaster");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.PatientMasters)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientMaster_GenderMaster");

                entity.HasOne(d => d.Idproof)
                    .WithMany(p => p.PatientMasters)
                    .HasForeignKey(d => d.IdproofId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientMaster_IDProofTypeMaster");

                entity.HasOne(d => d.PatientStatus)
                    .WithMany(p => p.PatientMasters)
                    .HasForeignKey(d => d.PatientStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientMaster_PatientStatusMaster");

                entity.HasOne(d => d.Phc)
                    .WithMany(p => p.PatientMasterPhcs)
                    .HasForeignKey(d => d.Phcid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientMaster_PHCMaster");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.PatientMasters)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientMaster_StateMaster");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.PatientMasterUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_PatientMaster_PHCMasterUpdatedBy");
            });

            modelBuilder.Entity<PatientQueue>(entity =>
            {
                entity.ToTable("PatientQueue");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AssignedDoctorId).HasColumnName("AssignedDoctorID");

                entity.Property(e => e.AssignedOn).HasColumnType("datetime");

                entity.Property(e => e.CaseFileStatusId).HasColumnName("CaseFileStatusID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PatientCaseId).HasColumnName("PatientCaseID");

                entity.Property(e => e.StatusOn).HasColumnType("datetime");

                entity.HasOne(d => d.AssignedByNavigation)
                    .WithMany(p => p.PatientQueues)
                    .HasForeignKey(d => d.AssignedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientQueue_PHCMasterAssignedBy");

                entity.HasOne(d => d.AssignedDoctor)
                    .WithMany(p => p.PatientQueues)
                    .HasForeignKey(d => d.AssignedDoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientQueue_DoctorMaster");

                entity.HasOne(d => d.CaseFileStatus)
                    .WithMany(p => p.PatientQueues)
                    .HasForeignKey(d => d.CaseFileStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientQueue_CaseFileStatusMaster");

                entity.HasOne(d => d.PatientCase)
                    .WithMany(p => p.PatientQueues)
                    .HasForeignKey(d => d.PatientCaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientQueue_PatientCase");
            });

            modelBuilder.Entity<PatientStatusMaster>(entity =>
            {
                entity.ToTable("PatientStatusMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PatientStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Phcmaster>(entity =>
            {
                entity.ToTable("PHCMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ClusterId).HasColumnName("ClusterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.MailId)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("MailID");

                entity.Property(e => e.Moname)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("MOName");

                entity.Property(e => e.Phcname)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("PHCName");

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ZoneId).HasColumnName("ZoneID");

                entity.HasOne(d => d.Cluster)
                    .WithMany(p => p.Phcmasters)
                    .HasForeignKey(d => d.ClusterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHCMaster_ClusterMaster");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PhcmasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_PHCMaster_UserMasterCreatedBy");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.PhcmasterUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_PHCMaster_UserMasterUpdatedBy");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PhcmasterUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHCMaster_UserMaster");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.Phcmasters)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHCMaster_ZoneMaster");
            });

            modelBuilder.Entity<RoleMasterDelete>(entity =>
            {
                entity.ToTable("RoleMaster.delete");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("Setting");
            });

            modelBuilder.Entity<SpecialityMasterDelete>(entity =>
            {
                entity.ToTable("SpecialityMaster.delete");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Speciality)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecializationMaster>(entity =>
            {
                entity.ToTable("SpecializationMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Specialization)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StateMaster>(entity =>
            {
                entity.ToTable("StateMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StateName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubSpecializationMaster>(entity =>
            {
                entity.ToTable("SubSpecializationMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.SpecializationId).HasColumnName("SpecializationID");

                entity.Property(e => e.SubSpecialization)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.SubSpecializationMasters)
                    .HasForeignKey(d => d.SpecializationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubSpecializationMaster_SpecializationMaster");
            });

            modelBuilder.Entity<SymptomsMaster>(entity =>
            {
                entity.ToTable("SymptomsMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Symptom)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TitleMaster>(entity =>
            {
                entity.ToTable("TitleMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Title)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.ToTable("UserDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(150)
                    .HasColumnName("EMailID");

                entity.Property(e => e.FatherName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.IdproofNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IDProofNumber");

                entity.Property(e => e.IdproofTypeId).HasColumnName("IDProofTypeID");

                entity.Property(e => e.LastName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Occupation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PinCode)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.TitleId).HasColumnName("TitleID");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_UserDetail_CountryMaster");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.UserDetailCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_UserDetail_UserMasterCreatedBy");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDetail_GenderMaster");

                entity.HasOne(d => d.IdproofType)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.IdproofTypeId)
                    .HasConstraintName("FK_UserDetail_IDProofTypeMaster");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_UserDetail_StateMaster");

                entity.HasOne(d => d.Title)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.TitleId)
                    .HasConstraintName("FK_UserDetail_TitleMaster");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.UserDetailUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_UserDetail_UserMasterUpdatedBy");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserDetailUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDetail_UserMaster");
            });

            modelBuilder.Entity<UserMaster>(entity =>
            {
                entity.ToTable("UserMaster");

                entity.HasIndex(e => e.Email, "IX_UserMaster")
                    .IsUnique();

                entity.HasIndex(e => e.Mobile, "IX_UserMaster_1")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.HashPassword)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LastLoginAt).HasColumnType("datetime");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.InverseCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_UserMaster_UserMasterCreatedBy");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.InverseUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_UserMaster_UserMasterUpdatedBy");
            });

            modelBuilder.Entity<UserTypeMaster>(entity =>
            {
                entity.ToTable("UserTypeMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UserType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserUsertype>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UserUsertype");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserUsertype_UserMaster");

                entity.HasOne(d => d.UserType)
                    .WithMany()
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserUsertype_UserTypeMaster");
            });

            modelBuilder.Entity<VideoCallTransaction>(entity =>
            {
                entity.ToTable("VideoCallTransaction");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FromUserId).HasColumnName("FromUserID");

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.Property(e => e.RecordingLink)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.RoomId)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("RoomID");

                entity.Property(e => e.ToUserId).HasColumnName("ToUserID");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.VideoCallTransactionFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VideoCallTransaction_UserMasterFromUser");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.VideoCallTransactions)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VideoCallTransaction_PatientMaster");

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.VideoCallTransactionToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VideoCallTransaction_UserMaster");
            });

            modelBuilder.Entity<VitalMaster>(entity =>
            {
                entity.ToTable("VitalMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Unit)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vital)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ZoneMaster>(entity =>
            {
                entity.ToTable("ZoneMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClusterId).HasColumnName("ClusterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.Zone)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cluster)
                    .WithMany(p => p.ZoneMasters)
                    .HasForeignKey(d => d.ClusterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZoneMaster_ClusterMaster");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ZoneMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_ZoneMaster_UserMasterCreatedBy");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ZoneMasterUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ZoneMaster_UserMasterUpdatedBy");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
