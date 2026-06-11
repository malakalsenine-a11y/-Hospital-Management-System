using project01.Model;

namespace project01
{
    public class Program
    {

        public static void PatientRegistration(HospitalContext context)
        {
            //Patient information:

            Console.WriteLine("Enter Patient Name: ");
            string userName = Console.ReadLine();

            Console.WriteLine("Enter Patient Age: ");
            int userAge = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Patient Gender: ");
            string userGender = Console.ReadLine();

            Console.WriteLine("Enter Patient Phone: ");
            string userPhone = Console.ReadLine();

            Console.WriteLine("Enter Patient Email: ");
            string userEmail = Console.ReadLine();

            Console.WriteLine("Enter Patient Blood Type: ");
            string userBloodType = Console.ReadLine();

            int userId = (context.Patients.Count) + 1;


            //add patient:
            context.Patients.Add(new Patient
            {
                patientId = userId,
                patientName = userName,
                patientAge = userAge,
                patientGender = userGender,
                patientPhone = userPhone,
                patientEmail = userEmail,
                patientBloodType = userBloodType
            });

            Console.WriteLine("Patient Added Successfully with ID" +userId);

        
        }
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


// public int patientId { get; set; }

