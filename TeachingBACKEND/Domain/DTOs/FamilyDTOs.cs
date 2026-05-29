using System.ComponentModel.DataAnnotations;

namespace TeachingBACKEND.Domain.DTOs
{
    public class CreateChildInputDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? CurrentClass { get; set; }
    }

    public class CreateChildrenBulkRequestDto
    {
        [Required]
        public List<CreateChildInputDto> Children { get; set; }
    }

    public class ChildCreatedDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TemporaryPassword { get; set; }
        public string? CurrentClass { get; set; }
    }

    public class CreateChildrenBulkResponseDto
    {
        public List<ChildCreatedDto> Children { get; set; }
    }

    public class UpdateChildDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CurrentClass { get; set; }
    }

    public class ChildPasswordResetResponseDto
    {
        public string NewPassword { get; set; }
    }
}
