namespace MauiApp_MyResume.Models
{
    public class ResumeStructure
    {
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string Education { get; set; }
        public string Skills { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public DateOnly DateOnlyBirthday => new DateOnly(Birthday.Year, Birthday.Month, Birthday.Day);
    }
}
