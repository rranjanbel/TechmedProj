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
        public virtual DbSet<CalenderMaster> CalenderMasters { get; set; } = null!;
        public virtual DbSet<CaseFileStatusMaster> CaseFileStatusMasters { get; set; } = null!;
        public virtual DbSet<CdssguidelineMaster> CdssguidelineMasters { get; set; } = null!;
        public virtual DbSet<ClusterMaster> ClusterMasters { get; set; } = null!;
        public virtual DbSet<CountryMaster> CountryMasters { get; set; } = null!;
        public virtual DbSet<DiagnosticTestMaster> DiagnosticTestMasters { get; set; } = null!;
        public virtual DbSet<DistrictMaster> DistrictMasters { get; set; } = null!;
        public virtual DbSet<CityMaster> CityMasters { get; set; } = null!;
        public virtual DbSet<DivisionMaster> DivisionMasters { get; set; } = null!;
        public virtual DbSet<DoctorMaster> DoctorMasters { get; set; } = null!;
        //public virtual DbSet<DoctorMeetingRoomInfo> DoctorMeetingRoomInfos { get; set; } = null!;
        public virtual DbSet<DrugsMaster> DrugsMasters { get; set; } = null!;
        public virtual DbSet<EmployeeTraining> EmployeeTrainings { get; set; } = null!;
        public virtual DbSet<EquipmentUptimeReport> EquipmentUptimeReports { get; set; } = null!;
        public virtual DbSet<FeedbackQuestionMaster> FeedbackQuestionMasters { get; set; } = null!;
        public virtual DbSet<GenderMaster> GenderMasters { get; set; } = null!;
        public virtual DbSet<HolidayMaster> HolidayMasters { get; set; } = null!;
        public virtual DbSet<IdproofTypeMaster> IdproofTypeMasters { get; set; } = null!;
        public virtual DbSet<LoginHistory> LoginHistories { get; set; } = null!;
        public virtual DbSet<LoginRoleDelete> LoginRoleDeletes { get; set; } = null!;
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; } = null!;
        public virtual DbSet<MedicineMaster> MedicineMasters { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<OfficialWorkingHour> OfficialWorkingHours { get; set; } = null!;
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
        public virtual DbSet<ServerHealth> ServerHealths { get; set; } = null!;
        public virtual DbSet<Setting> Settings { get; set; } = null!;
        public virtual DbSet<SpecialityMasterDelete> SpecialityMasterDeletes { get; set; } = null!;
        public virtual DbSet<SpecializationMaster> SpecializationMasters { get; set; } = null!;
        public virtual DbSet<SpokeMaintenance> SpokeMaintenances { get; set; } = null!;
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
        public virtual DbSet<PatientCaseDiagonosticTest> PatientCaseDiagonostics { get; set; } = null!;
        public virtual DbSet<RemoteSiteDowntimeReport> RemoteSiteDowntimeReports { get; set; } = null!;
        public virtual DbSet<ServerUpTimeReport> ServerUpTimeReports { get; set; } = null!;
        public virtual DbSet<CDSSGuideline> CDSSGuidelines { get; set; } = null!;
        public virtual DbSet<AgeGroupMaster> AgeGroupMasters { get; set; } = null!;
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; } = null!;


        public virtual DbSet<SPResultGetPatientDetails> SPResultGetPatientDetails { get; set; } = null!;
        public virtual DbSet<SpecializationReportVM> SpecializationReport { get; set; } = null!;
        public virtual DbSet<LoggedUserCountVM> LoggedUserCount { get; set; } = null!;
        public virtual DbSet<CompletedConsultantVM> CompletedConsultant { get; set; } = null!;
        public virtual DbSet<PatientCaseQueDetail> PatientCaseQueDetails { get; set; } = null!;
        public virtual DbSet<PatientSearchResultVM> PatientSearchResults { get; set; } = null!;
        public virtual DbSet<DoctorPatientSearchVM> DoctorPatientSearchResults { get; set; } = null!;
        public virtual DbSet<ConsultedPatientByDoctorAndPHCVM> ConsultedPatientByDoctorAndPHCResults { get; set; } = null!;
        public virtual DbSet<CompletedConsultationChartVM> CompletedConsultationChartResults { get; set; } = null!;
        public virtual DbSet<DashboardConsultationVM> GetDashboardConsultation { get; set; } = null!;
        public virtual DbSet<DashboardReportSummaryVM> GetDashboardReportSummary { get; set; } = null!;
        public virtual DbSet<PHCLoginHistoryReportVM> PHCLoginHistoryReports { get; set; } = null!;
        public virtual DbSet<PHCConsultationVM> PHCConsultationResult { get; set; } = null!;
        public virtual DbSet<DashboardReportSummaryVM> GetDashboardReportSummaryMonthly { get; set; } = null!;
        public virtual DbSet<DashboardReportConsultationVM> GetDashboardReportConsultation { get; set; } = null!;
        public virtual DbSet<PHCManpowerVM> PHCManpowerReports { get; set; } = null!;
        public virtual DbSet<RegisterPatientVM> RegisterPatientReports { get; set; } = null!;
        public virtual DbSet<GetReferredPatientVM> ReferredPatientReports { get; set; } = null!;
        public virtual DbSet<GetReviewPatientVM> ReviewPatientReport { get; set; } = null!;
        public virtual DbSet<GetDashboardSpokeMaintenanceVM> GetDashboardSpokeMaintenance { get; set; } = null!;
        public virtual DbSet<GetDashboardEmployeeFeedbackVM> GetDashboardEmployeeFeedback { get; set; } = null!;
        public virtual DbSet<TwilioMeetingRoomInfo> TwilioMeetingRoomInfos { get; set; } = null!;
        public virtual DbSet<GetDashboardEquipmentUptimeReportVM> GetDashboardEquipmentUptimeReport { get; set; } = null!;
        public virtual DbSet<GetDashboardAppointmentVM> GetDashboardAppointment { get; set; } = null!;
        public virtual DbSet<GetDashboardDoctorAvgTimeVM> GetDashboardDoctorAvgTime { get; set; } = null!;
        public virtual DbSet<GetDashboardDoctorAvailabilityVM> GetDashboardDoctorAvailability { get; set; } = null!;
        public virtual DbSet<GetDashboardEquipmentHeaderReportVM> GetDashboardEquipmentHeaderReport { get; set; } = null!;
        public virtual DbSet<VisitedPatientsVM> VisitedPatientsList { get; set; } = null!;
        public virtual DbSet<PrescribedMedicineVM> PrescribedMedicineReport { get; set; } = null!;
        public virtual DbSet<GetDashboardDiagnosticPrescribedPHCWiseVM> GetDashboardDiagnosticPrescribedPHCWise { get; set; } = null!;
        public virtual DbSet<GetDashboardDiagnosticPrescribedTestWiseVM> GetDashboardDiagnosticPrescribedTestWise { get; set; } = null!;
        public virtual DbSet<PrescribedMedicinePHCWiseVM> PrescribedMedicinePHCWiseReport { get; set; } = null!;
        public virtual DbSet<GetDashboardGraphVM> GetDashboardGraph { get; set; } = null!;
        public virtual DbSet<GetDashboardFeedbackSummaryReportVM> GetDashboardFeedbackSummaryReport { get; set; } = null!;
        public virtual DbSet<GetDashboardFeedbackReportVM> GetDashboardFeedbackReport { get; set; } = null!;
        public virtual DbSet<GetDashboardDignosisSpecialityWiseVM> GetDashboardDignosisSpecialityWise { get; set; } = null!;
        public virtual DbSet<GetDashboardDiseasephcWiseVM> GetDashboardDiseasephcWise { get; set; } = null!;
        public virtual DbSet<GetDashboardDiseaseAgeWiseVM> GetDashboardDiseaseAgeWise { get; set; } = null!;
        public virtual DbSet<GetDashboardSystemHealthReportVM> GetDashboardSystemHealthReport { get; set; } = null!;
        public virtual DbSet<RemoteSiteDowntimeSummaryDailyVM> RemoteSiteDowntimeSummaryDaily { get; set; } = null!;
        public virtual DbSet<RemoteSiteDowntimeSummaryMonthlyVM> RemoteSiteDowntimeSummaryMonthly { get; set; } = null!;
        public virtual DbSet<SnomedCTCode> SnomedCTCodes { get; set; } = null!;
        public virtual DbSet<GetDashboardFeedbackSummaryReportDataVM> GetDashboardFeedbackSummaryReportData { get; set; } = null!;
        public virtual DbSet<TodaysPatientCountVM> TodaysPatientCount { get; set; } = null!;
        public virtual DbSet<PatientQueueByDoctor> PatientQueueByDoctorList { get; set; } = null!;
        public virtual DbSet<PatientQueueVM> PatientQueuesList { get; set; } = null!;
        public virtual DbSet<UpdateServerHealthVM> UpdateServerHealth { get; set; } = null!;
        public virtual DbSet<UpdateServerHealthVM> UpdateLogout { get; set; } = null!;
        public virtual DbSet<ALLPatientsQueueVM> AllPatientQueueList { get; set; } = null!;
        public virtual DbSet<AllPendingPatient> AllPendingPatientList { get; set; } = null!;
        public virtual DbSet<UpdateServerHealthVM> UpdateYesterdayPedingCaseToOrphan { get; set; } = null!;
        public virtual DbSet<UpdatePrescriptionDocumentFlagVM> UpdatePrescriptionDocumentFlag { get; set; } = null!;

        


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

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.BlockMasters)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlockMaster_DistrictMaster");
            });

            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.ToTable("EmailTemplate");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Subject)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Body)
                   .HasMaxLength(4000)
                   .IsUnicode(false);
                entity.Property(e => e.ApplicationURL)
                .HasMaxLength(500)
                .IsUnicode(false);

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.EmailTemplates)
                    .HasForeignKey(d => d.UsertTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmailTemplate_UserTypeMaster");
            });

            modelBuilder.Entity<AgeGroupMaster>(entity =>
            {
                entity.ToTable("AgeGroupMaster");
                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.AgeMaxLimit).HasColumnName("AgeMaxLimit");
                entity.Property(e => e.AgeMinLimit).HasColumnName("AgeMinLimit");
                entity.Property(e => e.GenderID).HasColumnName("GenderID");
                entity.Property(e => e.SpecializationID).HasColumnName("SpecializationID");
                entity.Property(e => e.DaysOrYear).HasColumnName("DaysOrYear");

                entity.Property(e => e.AgeRange)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                
            });

            modelBuilder.Entity<CalenderMaster>(entity =>
            {
                entity.ToTable("CalenderMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Month).HasMaxLength(50);
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

            modelBuilder.Entity<CDSSGuideline>(entity =>
            {
                entity.ToTable("CDSSGuideline");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Age)
                    .IsUnicode(true)
                    .HasColumnName("Age");
                entity.Property(e => e.Diseases)
                    .IsUnicode(true)
                    .HasColumnName("Diseases");

                entity.Property(e => e.Treatment)
                    .IsUnicode(true)
                    .HasColumnName("Treatment");
            });

            modelBuilder.Entity<ClusterMaster>(entity =>
            {
                entity.ToTable("ClusterMaster");

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

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DiagnosticTestMaster>(entity =>
            {
                entity.ToTable("DiagnosticTestMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(150);
            });

            modelBuilder.Entity<DistrictMaster>(entity =>
            {
                entity.ToTable("DistrictMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DistrictName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DivisionId)
                    .HasColumnName("DivisionID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.DistrictMasters)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DistrictMaster_ZoneMaster");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.DistrictMasters)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DistrictMaster_StateMaster");
            });
            modelBuilder.Entity<CityMaster>(entity =>
            {
                entity.ToTable("CityMaster");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.CityName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<DivisionMaster>(entity =>
            {
                entity.ToTable("DivisionMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClusterId).HasColumnName("ClusterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Cluster)
                    .WithMany(p => p.DivisionMasters)
                    .HasForeignKey(d => d.ClusterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZoneMaster_ClusterMaster");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.DivisionMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_ZoneMaster_UserMasterCreatedBy");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.DivisionMasterUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ZoneMaster_UserMasterUpdatedBy");
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

                entity.Property(e => e.BlockId).HasColumnName("BlockID");

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

                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

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

                entity.HasOne(d => d.Block)
                    .WithMany(p => p.DoctorMasters)
                    .HasForeignKey(d => d.BlockId)
                    .HasConstraintName("FK_DoctorMaster_BlockMaster");

                entity.HasOne(d => d.Cluster)
                    .WithMany(p => p.DoctorMasters)
                    .HasForeignKey(d => d.ClusterId)
                    .HasConstraintName("FK_DoctorMaster_ClusterMaster");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.DoctorMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_DoctorMaster_UserMasterCreatedBy");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.DoctorMasters)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_DoctorMaster_DistrictMaster");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.DoctorMasters)
                    .HasForeignKey(d => d.DivisionId)
                    .HasConstraintName("FK_DoctorMaster_DivisionMaster");

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
            });

            modelBuilder.Entity<DocumentMaster>(entity =>
            {
                entity.ToTable("DocumentMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DrugsMaster>(entity =>
            {
                entity.ToTable("DrugsMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category).HasMaxLength(150);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DrugCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DrugformAndStrength).HasMaxLength(150);

                entity.Property(e => e.GroupOfDrug)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MpaushidhiCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MPAushidhiCode");

                entity.Property(e => e.NameOfDrug)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PakAndVolume).HasMaxLength(150);

                entity.Property(e => e.Reference)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.SubGroupOfDrug)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<EmployeeTraining>(entity =>
            {
                entity.ToTable("EmployeeTraining");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PhcId).HasColumnName("PhcID");

                entity.Property(e => e.TraingDate).HasColumnType("datetime");

                entity.Property(e => e.TraingPeriod)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TrainingBy)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.TrainingSubject)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.EmployeeTrainingCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_EmployeeTraining_UserMaster");

                entity.HasOne(d => d.Phc)
                    .WithMany(p => p.EmployeeTrainings)
                    .HasForeignKey(d => d.PhcId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeTraining_PHCMaster");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.EmployeeTrainingUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_EmployeeTraining_UserMaster1");
            });

            modelBuilder.Entity<EquipmentUptimeReport>(entity =>
            {
                entity.ToTable("EquipmentUptimeReport");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.EquipmentUptimeReportCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_EquipmentUptimeReport_UserMaster");

                entity.HasOne(d => d.Phc)
                    .WithMany(p => p.EquipmentUptimeReports)
                    .HasForeignKey(d => d.PhcId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentUptimeReport_PHCMaster");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.EquipmentUptimeReportUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_EquipmentUptimeReport_UserMaster1");
            });

            modelBuilder.Entity<FirebaseUserToken>(entity =>
            {
                entity.ToTable("FirebaseUserToken");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirebaseToken).HasMaxLength(1000);

                entity.HasOne(d => d.UserMaster)
                    .WithMany(p => p.FirebaseUserTokens)
                    .HasForeignKey(d => d.UserMasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FirebaseU__UserM__02925FBF");
            });

            modelBuilder.Entity<GenderMaster>(entity =>
            {
                entity.ToTable("GenderMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HolidayMaster>(entity =>
            {
                entity.ToTable("HolidayMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CalenderId).HasColumnName("CalenderID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Calender)
                    .WithMany(p => p.HolidayMasters)
                    .HasForeignKey(d => d.CalenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HolidayMaster_CalenderMaster");
            });

            modelBuilder.Entity<IdproofTypeMaster>(entity =>
            {
                entity.ToTable("IDProofTypeMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdproofType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IDProofType");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<LoginHistory>(entity =>
            {
                entity.ToTable("LoginHistory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogedInTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LogedoutTime).HasColumnType("datetime");
                entity.Property(e => e.LastUpdateOn).HasColumnType("datetime");
                
                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.UserToken).HasColumnName("UserToken");
                // entity.Property(e => e.RefreshToken).HasColumnName("RefreshToken");

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

            modelBuilder.Entity<OfficialWorkingHour>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
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

                entity.Property(e => e.Opdno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OPDNo");

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.Property(e => e.Prescription).IsUnicode(false);

                entity.Property(e => e.ProvisionalDiagnosis)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ReferredTo)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.SpecializationId).HasColumnName("SpecializationID");

                entity.Property(e => e.SuggestedDiagnosis)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Symptom)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientCases)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientCase_PatientMaster");
            });

            modelBuilder.Entity<PatientCaseDiagonosticTest>(entity =>
            {
                entity.ToTable("PatientCaseDiagonosticTest");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DiagonosticTestId).HasColumnName("DiagonosticTestID");

                entity.Property(e => e.PatientCaseId).HasColumnName("PatientCaseID");

                entity.HasOne(d => d.DiagonosticTest)
                    .WithMany(p => p.PatientCaseDiagonosticTests)
                    .HasForeignKey(d => d.DiagonosticTestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientCaseDiagonosticTest_DiagnosticTestMaster");

                entity.HasOne(d => d.PatientCase)
                    .WithMany(p => p.PatientCaseDiagonosticTests)
                    .HasForeignKey(d => d.PatientCaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientCaseDiagonosticTest_PatientCase");
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

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

                entity.Property(e => e.PatientCaseId).HasColumnName("PatientCaseID");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.PatientCaseDocuments)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientCaseDocument_DocumentMaster");

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

                entity.Property(e => e.DrugMasterId).HasColumnName("DrugMasterID");

                entity.Property(e => e.Od).HasColumnName("OD");

                entity.Property(e => e.PatientCaseId).HasColumnName("PatientCaseID");

                entity.Property(e => e.Td).HasColumnName("TD");
                entity.Property(e => e.Qid).HasColumnName("QID");

                entity.Property(e => e.Comment).HasColumnName("Comment")
                .HasMaxLength(150)
                .IsUnicode(false);

                entity.HasOne(d => d.DrugMaster)
                    .WithMany(p => p.PatientCaseMedicines)
                    .HasForeignKey(d => d.DrugMasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientCaseMedicine_DrugsMaster");

                entity.HasOne(d => d.PatientCase)
                    .WithMany(p => p.PatientCaseMedicines)
                    .HasForeignKey(d => d.PatientCaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientCaseMedicine_PatientCase");
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

                entity.Property(e => e.BlockID).HasColumnName("BlockID");                   

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

                entity.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatusID");

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
                entity.HasOne(d => d.Block)
                  .WithMany(p => p.PatientMasters)
                  .HasForeignKey(d => d.BlockID)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_NEWPatientMaster_BlockMaster");

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

                entity.HasOne(d => d.MaritalStatus)
                    .WithMany(p => p.PatientMasters)
                    .HasForeignKey(d => d.MaritalStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientMaster_MaritalStatus");

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

                entity.HasIndex(e => new {  e.Phcname }, "UC_PHCName")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BlockId).HasColumnName("BlockID");

                entity.Property(e => e.ClusterId).HasColumnName("ClusterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

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

                entity.HasOne(d => d.Block)
                    .WithMany(p => p.Phcmasters)
                    .HasForeignKey(d => d.BlockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHCMaster_BlockMaster");

                entity.HasOne(d => d.Cluster)
                    .WithMany(p => p.Phcmasters)
                    .HasForeignKey(d => d.ClusterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHCMaster_ClusterMaster");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PhcmasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_PHCMaster_UserMasterCreatedBy");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Phcmasters)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHCMaster_DistrictMaster");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Phcmasters)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHCMaster_DivisionMaster");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.PhcmasterUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_PHCMaster_UserMasterUpdatedBy");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PhcmasterUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHCMaster_UserMaster");
            });

            modelBuilder.Entity<ServerHealth>(entity =>
            {
                entity.ToTable("ServerHealth");

                entity.Property(e => e.Id).HasColumnName("ID");

                //entity.Property(e => e.Availability).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");
                entity.Property(e => e.EndDateTime).HasColumnType("datetime");
                entity.Property(e => e.TimeDuration).HasColumnType("timespan");
                entity.Property(e => e.TimeDurationSS).HasColumnType("bigint");
                entity.Property(e => e.CurrentStatus)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.Details)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    ;
                
    });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("Setting");
            });

            modelBuilder.Entity<SpecializationMaster>(entity =>
            {
                entity.ToTable("SpecializationMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Specialization)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpokeMaintenance>(entity =>
            {
                entity.ToTable("SpokeMaintenance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.FilePath)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Phcid).HasColumnName("PHCID");

                entity.HasOne(d => d.Phc)
                    .WithMany(p => p.SpokeMaintenances)
                    .HasForeignKey(d => d.Phcid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpokeMaintenance_PHCMaster");
            });

            modelBuilder.Entity<RemoteSiteDowntimeReport>(entity =>
            {
                entity.ToTable("RemoteSiteDowntimeReport");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.PHCDownTime);

                entity.Property(e => e.PHCId).HasColumnName("PHCId");

            });
            modelBuilder.Entity<ServerUpTimeReport>(entity =>
            {
                entity.ToTable("ServerUpTimeReport");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.WorkingHours);
                entity.Property(e => e.WorkingTime);
                entity.Property(e => e.ServerUpTime);
                entity.Property(e => e.ServerDownTime);
                entity.Property(e => e.DownTimings);
                entity.Property(e => e.Availability);

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

            modelBuilder.Entity<TwilioMeetingRoomInfo>(entity =>
            {
                entity.ToTable("TwilioMeetingRoomInfo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CloseDate).HasColumnType("datetime");

                entity.Property(e => e.CompositeVideoSid)
                    .HasMaxLength(250)
                    .HasColumnName("CompositeVideoSID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsClosed).HasDefaultValueSql("((0))");

                entity.Property(e => e.MeetingSid)
                    .HasMaxLength(250)
                    .HasColumnName("MeetingSID");

                entity.Property(e => e.PatientCaseId).HasColumnName("PatientCaseID");

                entity.Property(e => e.RoomName).HasMaxLength(500);

                entity.Property(e => e.RoomStatusCallback).HasMaxLength(1000);

                entity.Property(e => e.TwilioRoomStatus).HasMaxLength(250);

                entity.HasOne(d => d.PatientCase)
                    .WithMany(p => p.TwilioMeetingRoomInfos)
                    .HasForeignKey(d => d.PatientCaseId)
                    .HasConstraintName("FK__TwilioMee__Patie__28B808A7");
                
                entity.Property(e => e.AssignedDoctorId).IsRequired(false).HasColumnName("AssignedDoctorID");
                entity.Property(e => e.AssignedBy).IsRequired(false).HasColumnName("AssignedBy");
                
                entity.HasOne(d => d.AssignedByNavigation)
                  .WithMany(p => p.TwilioMeetingRoomInfos)
                  .HasForeignKey(d => d.AssignedBy)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_TwilioMeeting_PHCMasterAssignedBy");
                entity.HasOne(d => d.AssignedDoctor)
                 .WithMany(p => p.TwilioMeetingRoomInfos)
                 .HasForeignKey(d => d.AssignedDoctorId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_TwilioMeeting_DoctorMaster");
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
                    .HasColumnName("DOB").IsRequired(false);

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

                entity.HasIndex(e => new { e.Email }, "UC_UserMaster")
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

                entity.Property(e => e.CallEndTime).HasColumnType("datetime");

                entity.Property(e => e.CallStartTime).HasColumnType("datetime");

                entity.Property(e => e.CompositionSid)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CompositionSID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FromUserId).HasColumnName("FromUserID");

                entity.Property(e => e.PatientCaseId).HasColumnName("PatientCaseID");

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("RoomID");

                entity.Property(e => e.RoomName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RoomSid)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("RoomSID");

                entity.Property(e => e.ToUserId).HasColumnName("ToUserID");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.VideoCallTransactionFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VideoCallTransaction_UserMasterFromUser");

                entity.HasOne(d => d.PatientCase)
                    .WithMany(p => p.VideoCallTransactions)
                    .HasForeignKey(d => d.PatientCaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VideoCallTransaction_PatientCase");

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

            modelBuilder.Entity<SnomedCTCode>(entity =>
            {
                entity.ToTable("SnomedCTCode");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CodeName)
                   .HasMaxLength(500)
                   .IsUnicode(true);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
