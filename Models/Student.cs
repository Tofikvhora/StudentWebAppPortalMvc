using Microsoft.Build.Framework;

namespace StudentWebAppPortal.Models
{
    public class Student
    {

      
            public int id { get; set; }
        [Required]
        public string studentName { get; set; }
        [Required]
        public string studentGender { get; set; }
        [Required]
        public int studentAge { get; set; }
        [Required]
        public int standard { get; set; }
        [Required]
        public string fatherName { get; set; }
        

    }
}
