using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee.Migrations
{
    public partial class AddEmpToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ErpCifNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KnowledgeOfEnglishReading = table.Column<bool>(type: "bit", nullable: true),
                    KnowledgeOfEnglishSpeaking = table.Column<bool>(type: "bit", nullable: true),
                    KnowledgeOfEnglishWriting = table.Column<bool>(type: "bit", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignaturePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuanceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TaxSubsidy = table.Column<float>(type: "real", nullable: true),
                    TaxLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxCircle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EtinNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxZoneLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearOfExperience = table.Column<int>(type: "int", nullable: true),
                    ExperienceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExperienceDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherOccupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherOccupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherNid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherNid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermanentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeTelephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternativeMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeFax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficialEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyTelephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyFax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProbationPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SeparationPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeparationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractFor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RenewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Site = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayrollSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentalDivision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Function = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibilityCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankBranch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GrossSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PercentageOfBasicOnGrossSalary = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    NextIncrement = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KpiGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Buddy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupervisorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecruitmentChannel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SimCardAllocationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SalaryAdjustmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TaxableCarAllocated = table.Column<bool>(type: "bit", nullable: true),
                    CarAllocated = table.Column<bool>(type: "bit", nullable: true),
                    ShiftingGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShiftingPlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GratuityPolicy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeavePlanHolder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeavePlanVerifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeaveApprovalAuthority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelfAlso = table.Column<bool>(type: "bit", nullable: true),
                    IsTrainer = table.Column<bool>(type: "bit", nullable: true),
                    RequisitionApprovalAuthority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDepartmentalHead = table.Column<bool>(type: "bit", nullable: true),
                    MedicalAllow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PFMember = table.Column<bool>(type: "bit", nullable: true),
                    MailUser = table.Column<bool>(type: "bit", nullable: true),
                    SignatureAuthority = table.Column<bool>(type: "bit", nullable: true),
                    Expatriate = table.Column<bool>(type: "bit", nullable: true),
                    NonPublicProfile = table.Column<bool>(type: "bit", nullable: true),
                    PoliceVerificationDone = table.Column<bool>(type: "bit", nullable: true),
                    AttendanceLaxity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllocateLeaveQuota = table.Column<bool>(type: "bit", nullable: true),
                    RoundOffLeaveBalance = table.Column<bool>(type: "bit", nullable: true),
                    CBSBypassTransaction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DirectReportToFunctionHead = table.Column<bool>(type: "bit", nullable: true),
                    PromotionResponsible = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncrementResponsible = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmationResponsible = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
