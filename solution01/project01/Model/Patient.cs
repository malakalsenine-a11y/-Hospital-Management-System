using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project01.Model
{
    public class Patient
    {
        public int patientId { get; set; }

        public string patientName { get; set; }

        public int patientAge { get; set; }

        public string patientGender { get; set; }

        public string patientPhone { get; set; }

        public string patientEmail { get; set; }

        public string patientBloodType { get; set; }


        public Patient (int userId, string userName, int userAge, string userGender, string userPhone, string userBloodType, string userEmail)
        {
            patientId = userId;
            patientName = userName;
            patientAge = userAge;
            patientGender = userGender;
            patientPhone = userPhone;
            patientEmail = userEmail;
            patientBloodType = userBloodType;
        }

        public override string ToString() =>
            $" [{patientId}] {patientName, -10}|{patientAge, -8}|{patientGender, -8}|{patientPhone, -8}|{patientEmail,9 }|{patientBloodType: F2}";

        public static void ShowData()
        {
            Console.WriteLine($"Patient Added Successfully with ID" + patientId);
        }
        
       
}
        }
