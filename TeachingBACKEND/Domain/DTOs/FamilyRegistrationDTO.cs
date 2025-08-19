using System.ComponentModel.DataAnnotations;

public class FamilyMemberInput
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
            /// <summary>
        /// Class ID (Guid) or Class Name. Will be converted to Class ID internally.
        /// </summary>
        public string? CurrentClass { get; set; }
}

public class FamilyRegistrationDTO
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string FirstName { get; set; } // main user
    [Required]
    public string LastName { get; set; } // main user

    [Required]
    public string PlanId { get; set; }

    [Required]
    public List<FamilyMemberInput> FamilyMembers { get; set; } // family members to create
    public string? PhoneNumber { get; set; }
}
