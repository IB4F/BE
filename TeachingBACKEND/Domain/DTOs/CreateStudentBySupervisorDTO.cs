namespace TeachingBACKEND.Domain.DTOs
{
    public class CreateStudentBySupervisorDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CurrentClass { get; set; }
        public string School { get; set; }
        public DateTime DateOfBirth { get; set; }
        // Email nuk përfshihet - gjenerohet automatikisht si firstname.lastname@bga.al
    }
}

