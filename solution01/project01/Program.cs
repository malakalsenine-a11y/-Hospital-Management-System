using project01.Model;

namespace project01
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Data storge for the system
            HospitalContext mainContext = new HospitalContext();
            mainContext.Patients = new List<Patient>();
            mainContext.Doctors = new List<Doctor>();
            mainContext.Appointments = new List<Appointment>();
            mainContext.MedicalRecords = new List<MedicalRecord>();
            mainContext.AvailableSlots = new List<AvailableSlot>();




        }
    }
}
