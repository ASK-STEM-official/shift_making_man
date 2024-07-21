namespace shift_making_man.Models
{
    public class Staff
    {
        public int StaffID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string EmploymentType { get; set; }
    }
}
