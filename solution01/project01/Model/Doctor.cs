using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project01.Model
{
    public class Doctor
    {
        public int doctorId { get; set; }

        public string doctorName { get; set; }

        public string doctorSpecialization { get; set; }

        public string doctorPhone { get; set; }

        public string doctorEmail { get; set; }


        public decimal consultationFee { get; set; }


        public Doctor(int doctorId, string nameDoctor, string specializationDoctor, string phoneDoctor, string emailDoctor, decimal consultationFeeDoctor)
        {
            doctorId = doctorId;
            doctorName = nameDoctor;
            doctorSpecialization = specializationDoctor;
            doctorPhone = phoneDoctor;
            doctorEmail = emailDoctor;
            consultationFee = consultationFeeDoctor;
        }
        public override string ToString() =>
            $"[{doctorId}] {doctorName,-10} | {doctorSpecialization,-8} | {doctorPhone,-8} | {doctorEmail,-8} | {consultationFee,9:F2}";

        public static void ShowData(int doctorId)
        {
            Console.WriteLine("Doctor Added Successfully with ID" + doctorId);
        }
    }
}
