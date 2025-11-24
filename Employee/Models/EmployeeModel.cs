using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class EmployeeModel
{
    // General Information

    [Key]
    public int EmployeeId { get; set; }
    
    [Required]
    public string? ErpCifNo { get; set; }
    
    [Required]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name must contain only letters")]
    [Display(Prompt = "Enter your first name")]
    public string? FirstName { get; set; }

    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Middle Name must contain only letters")]
    [Display(Prompt = "Enter your Middle name")]
    public string? MiddleName { get; set; }
    
    [Required]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name must contain only letters")]
    [Display(Prompt = "Enter your Last name [Required]")]
    public string? LastName { get; set; }
    
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Nick Name must contain only letters")]
    [Display(Prompt = "Enter your Nick name")]
    public string? NickName { get; set; }
    
    [Display(Prompt = "Enter your Religion")]
    public string? Religion { get; set; }

    [Display(Prompt = "Enter your Gender")]
    public string? Gender { get; set; }
    
    public string? BloodGroup { get; set; }
    public string? MaritalStatus { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Nationality must contain only letters")]
    [Display(Prompt = "Enter your Nationality [Required]")]
    public string? Nationality { get; set; }

    [RegularExpression(@"^\d{10}$", ErrorMessage = "National ID must be a 10-digit number.")]
    [Display(Prompt = "Enter 10 digit NationalId [Required]")]
    public string? NationalId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    public bool? KnowledgeOfEnglishReading { get; set; } 
    public bool? KnowledgeOfEnglishSpeaking { get; set; }
    public bool? KnowledgeOfEnglishWriting { get; set; }


    public string? PhotoPath { get; set; }
    public string? SignaturePath { get; set; }

    // Passport Information
    [RegularExpression(@"^PB-\d{8}$", ErrorMessage = "Invalid Passport Number.")]
    [Display(Prompt = "PB-[8 digit]")]
    public string? PassportNumber { get; set; }
    [DataType(DataType.Date)]
    public DateTime? IssuanceDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    // Income Tax Information

    [Display(Prompt = "Enter Tax Subsidy")]
    public float? TaxSubsidy { get; set; }

    [Display(Prompt = "Enter Tax Location")]
    public string? TaxLocation { get; set; }

    [Display(Prompt = "Enter Tax Circle")]
    public string? TaxCircle { get; set; }

    [Display(Prompt = "Enter E-TIN")]
    public string? EtinNumber { get; set; }

    [Display(Prompt = "Enter Tax Zone")]
    public string? TaxZone { get; set; }

    [Display(Prompt = "Enter Tax Zone Location")]
    public string? TaxZoneLocation { get; set; }


    // Place of Birth

    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Place must contain only letters")]
    [Display(Prompt = "Enter Place of Birth")]
    public string? Place { get; set; }

    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Country must contain only letters")]
    [Display(Prompt = "Enter Country Name")]
    public string? Country { get; set; }


    // Previous Experience

    [Display(Prompt = "Enter Years of Experience")]
    public int? YearOfExperience { get; set; }

    [Display(Prompt = "Enter Years of Experience Type")]
    public string? ExperienceType { get; set; }

    [Display(Prompt = "Enter Years of Experience details")]
    public string? ExperienceDetails { get; set; }

    // Family Information

    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Mother Name must contain only letters")]
    [Display(Prompt = "Enter Mother Name")]
    public string? MotherName { get; set; }

    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Father Name must contain only letters")]
    [Display(Prompt = "Enter Father name")]
    public string? FatherName { get; set; }
    
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Father Occupation must contain only letters")]
    [Display(Prompt = "Enter Father's Occupation")]
    public string? FatherOccupation { get; set; }


    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Mother Occupation must contain only letters")]
    [Display(Prompt = "Enter Mother's Occupation")]
    public string? MotherOccupation { get; set; }

    [RegularExpression(@"^\d{10}$", ErrorMessage = "National ID must be a 10-digit number.")]
    [Display(Prompt = "Enter Father's Nid")]
    public string? FatherNid { get; set; }

    [RegularExpression(@"^\d{10}$", ErrorMessage = "National ID must be a 10-digit number.")]
    [Display(Prompt = "Enter Mother's Nid")]
    public string? MotherNid { get; set; }

    [Display(Prompt = "Enter Parent's Address")]
    public string? ParentAddress { get; set; }


    // Contact Information
    [Display(Prompt = "Enter Permanent Address")]
    public string? PermanentAddress { get; set; }
    [Display(Prompt = "Enter Present Address")]
    public string? PresentAddress { get; set; }

    [RegularExpression(@"^01[3-9]\d{8}$", ErrorMessage = "Not a valid number")]
    [Display(Prompt = "Enter Mobile No")]
    public string? EmployeeMobile { get; set; }

    [RegularExpression(@"^(\+88)?01[3-9]\d{8}$", ErrorMessage = "Not a valid Telephone number")]
    [Display(Prompt = "Enter TelePhone")]
    public string? EmployeeTelephone { get; set; }
    
    [RegularExpression(@"^01[3-9]\d{8}$", ErrorMessage = "Not a valid number")]

    [Display(Prompt = "Enter Alternative Mobile")]
    public string? AlternativeMobile { get; set; }

    
    [Display(Prompt = "Enter Fax")]
    public string? EmployeeFax { get; set; }

    [RegularExpression(@"^[a-z][a-z0-9]*@[a-z]+\.[a-z]+$", ErrorMessage = "Invalid email format")]
    [Display(Prompt = "Enter Official Email")]
    public string? OfficialEmail { get; set; }


    // Emergency Contact

    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Contact Person must contain only letters")]
    [Display(Prompt = "Enter name")]
    public string? ContactPerson { get; set; }

    [Display(Prompt = "Enter Relation")]
    public string? Relation { get; set; }
    [RegularExpression(@"^01[3-9]\d{8}$", ErrorMessage = "Not a valid number")]

    [Display(Prompt = "Enter Mobile")]
    public string? EmergencyMobile { get; set; }

    [RegularExpression(@"^(\+88)?01[3-9]\d{8}$", ErrorMessage = "Not a valid Telephone number")]
    [Display(Prompt = "Enter TelePhone")]
    public string? EmergencyTelephone { get; set; }

    [RegularExpression(@"^[a-z][a-z0-9]*@[a-z]+\.[a-z]+$", ErrorMessage = "Invalid email format")]
    [Display(Prompt = "Enter Email")]
    public string? Email { get; set; }
    [Display(Prompt = "Enter Address")]
    public string? ContactAddress { get; set; }
    [Display(Prompt = "Enter Fax")]
    public string? EmergencyFax { get; set; }


    // Job Information
    [DataType(DataType.Date)]
    public DateTime? JoiningDate { get; set; }

    [Display(Prompt = "Enter Activity")]
    public string? Activity { get; set; }
    
    [Display(Prompt = "Enter Probation Period Time")]
    public string? ProbationPeriod { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? ConfirmationDate { get; set; }
    
    [Display(Prompt = "Enter Separation Period")]
    public string? SeparationPeriod { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? SeparationDate { get; set; }
    
    [Display(Prompt = "Enter Contract")]
    public string? ContractFor { get; set; }
    
    [DataType(DataType.Date)]
    
    public DateTime? RenewDate { get; set; }
    
    [Display(Prompt = "Enter Company")]
    public string? Company { get; set; }
    
    [Display(Prompt = "Enter Region")]
    public string? Region { get; set; }
    
    [Display(Prompt = "Enter Site")]
    public string? Site { get; set; }
    
    [Display(Prompt = "Enter Payroll Site")]
    public string? PayrollSite { get; set; }
    
    [Display(Prompt = "Enter Departmental Division")]
    public string? DepartmentalDivision { get; set; }
    

    [Display(Prompt = "Enter Departmental")]
    public string? Department { get; set; }
    
    [Display(Prompt = "Enter Function")]
    public string? Function { get; set; }
    
    [Display(Prompt = "Enter Job")]
    public string? Job { get; set; }
    
    [Display(Prompt = "Enter Grade")]
    public string? Grade { get; set; }

    [Display(Prompt = "Enter Designation")]
    public string? Designation { get; set; }
    
    [Display(Prompt = "Enter Responsibility Center")]
    public string? ResponsibilityCenter { get; set; }
    
    [Display(Prompt = "Enter Cost Center")]
    public string? CostCenter { get; set; }
    
    
    public string? PaymentMode { get; set; }

    [RegularExpression(@"^[1-9]\d{9}$", ErrorMessage = "Bank Account No must start with a digit between 1 and 9, followed by exactly 9 digits.")]
    [Display(Prompt = "Enter 10 digit number")]
    public string? BankAccountNo { get; set; }

    [Display(Prompt = "Enter bank name")]
    public string? Bank { get; set; }

    [Display(Prompt = "Enter Bank Branch")]
    public string? BankBranch { get; set; }

    [Display(Prompt = "Enter Employee Type")]
    public string? EmployeeType { get; set; }

    [Display(Prompt = "Enter Employee Category")]
    public string? EmployeeCategory { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Range(0, double.MaxValue, ErrorMessage = "Basic Salary must be a positive number.")]
    [Display(Prompt = "Enter Basic Salary")]
    public decimal? BasicSalary { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Range(0, double.MaxValue, ErrorMessage = "Gross Salary must be a positive number.")]
    [Display(Prompt = "Enter Gross Salary")]
    public decimal? GrossSalary { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100, ErrorMessage = "Percentage Of Basic On Gross Salary must be between 0 and 100.")]
    [Display(Prompt = "Enter % On Gross Salary")]
    public decimal? PercentageOfBasicOnGrossSalary { get; set; }
    
    
    [DataType(DataType.Date)]
    public DateTime? NextIncrement { get; set; }
    

    [Display(Prompt = "Enter KpiGroup")]
    public string? KpiGroup { get; set; }
    
    [Display(Prompt = "Enter Buddy")]
    public string? Buddy { get; set; }
    
    [Display(Prompt = "Enter Supervisor Id")]
    public string? SupervisorId { get; set; }
    

    public string? RecruitmentChannel { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? SimCardAllocationDate { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? SalaryAdjustmentDate { get; set; }
    
    public bool? TaxableCarAllocated { get; set; } = false;
    
    public bool? CarAllocated { get; set; } = false;
    
    public string? ShiftingGroup { get; set; }
    
    [Display(Prompt = "Enter Shifting Plan")]
    public string? ShiftingPlan { get; set; }
    
    [Display(Prompt = "Enter Gratuity Policy")]
    public string? GratuityPolicy { get; set; }
    
    [Display(Prompt = "Enter Leave Plan Holder")]
    public string? LeavePlanHolder { get; set; }
    
    [Display(Prompt = "Enter Leave Plan Verifier")]
    public string? LeavePlanVerifier { get; set; }
    
    [Display(Prompt = "Enter Leave Approval Authority")]
    public string? LeaveApprovalAuthority { get; set; }
    
    
    public bool? SelfAlso { get; set; } = false;
    
    public bool? IsTrainer { get; set; } = false;
    
    public string? RequisitionApprovalAuthority { get; set; }
    
    public bool? IsDepartmentalHead { get; set; } = false;
    
    
    public string? MedicalAllow { get; set; }
    
    public bool? PFMember { get; set; } = false;
    
    public bool? MailUser { get; set; } = false;
    
    public bool? SignatureAuthority { get; set; } = false;
    
    public bool? Expatriate { get; set; } = false;
    
    public bool? NonPublicProfile { get; set; } = false;
    
    public bool? PoliceVerificationDone { get; set; } = false;
    
    public string? AttendanceLaxity { get; set; }
    
    public bool? AllocateLeaveQuota { get; set; } = false;
    
    public bool? RoundOffLeaveBalance { get; set; } = false;
    
    public string? CBSBypassTransaction { get; set; }
    
    public bool? DirectReportToFunctionHead { get; set; } = false;

    public string? PromotionResponsible { get; set; }
    
    public string? IncrementResponsible { get; set; }
    
    public string? ConfirmationResponsible { get; set; }
}







