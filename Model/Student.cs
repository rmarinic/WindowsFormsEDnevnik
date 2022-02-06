using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTP_Projekt.Model
{
    public class Student
    {
        public int JMBAG { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        [JsonProperty("Date of Birth")]
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
        [JsonProperty("Enrollment Date")]
        public DateTime Enrollment_Date { get; set; }
    }
}
