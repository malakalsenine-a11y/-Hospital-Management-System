using project01.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace project01
{
    public class HospitalContext
    {
        public List <Patient> Patients { get; set; }
        public List<Doctor> Doctors { get; set; }

        public List<Appointment> Appointments { get; set; }

        public List<MedicalRecord> MedicalRecords { get; set; }

        public List<AvailableSlot> AvailableSlots { get; set; }


    }
}
