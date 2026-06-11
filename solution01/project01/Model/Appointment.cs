using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project01.Model
{
    public class Appointment
    {
        public int appointmentId { get; set; }

        public int patientId { get; set; }

        public int doctorId { get; set; }

        public string appointmentDate { get; set; }

        public string appointmentTime { get; set; }

        public string status { get; set; }


    }
}
