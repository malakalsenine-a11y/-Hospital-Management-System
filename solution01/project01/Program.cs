using project01;
using project01.Model;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;
using System.Timers;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public static void AddNewDoctor(HospitalContext context)
        {
            Console.WriteLine("Enter Doctor Name: ");
            string nameDoctor = Console.ReadLine();


            Console.WriteLine("Enter Doctor Specialization: ");
            string specializationDoctor = Console.ReadLine();

            Console.WriteLine("Enter Doctor Phone: ");
            string phoneDoctor = Console.ReadLine();

            Console.WriteLine("Enter Doctor Email: ");
            string emailDoctor = Console.ReadLine();

            Console.WriteLine("Enter ConsultationFee: ");
            string consultationFeeDoctor = Console.ReadLine();

            int doctorId = (context.Doctors.Count) + 1;

            Console.WriteLine("Doctor Added Successfully with ID" + doctorId);


        }

        public static void ViewAllPatients (HospitalContext context)
        {
            foreach( var patient in context.Patients)
            {
                if (patient.patientId == 0)
                {
                    Console.WriteLine("No Patient");
                }

                else
                {
                    Console.WriteLine($" The Patien Id : {patient.patientId} , the name is : {patient.patientName}, the age is: {patient.patientAge}, the gender is: {patient.patientGender} , the phine number is: {patient.patientPhone}, the email is:{patient.patientEmail}, the Blood Type is: {patient.patientBloodType}  ");
                }
            }
        }

        public static void ViewAllDoctorBySpecializtion(HospitalContext context)
        {
            Console.WriteLine("The Specialization: ");
            string theSpecialization = Console.ReadLine();

            bool found = false;

            foreach (var doctor in context.Doctors)
            {
                if(doctor.doctorSpecialization == theSpecialization)
                {
                    Console.WriteLine($"The Doctor Id is: {doctor.doctorId} , The Name is : {doctor.doctorName} , The Doctor Email is : {doctor.doctorEmail} , The Doctor Phone is: {doctor.doctorPhone} , The Doctor Specialization : {doctor.doctorSpecialization} , The Doctor ConsultationFee {doctor.consultationFee}");
                    found = true;
                }
                if (found == false)
                {
                    Console.WriteLine("No Doctot Found!");

                }
            }
        }

        public static void AddAvailableTimeSlotForDoctor(HospitalContext context)
        {
            Console.WriteLine("Enter Doctor Id:");
            int doctorId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Date:");
            string data = Console.ReadLine();

            Console.WriteLine("Enter Time:");
            string time = Console.ReadLine();

            int idSlot = (context.Patients.Count) + 1;


            context.AvailableSlots.Add(new AvailableSlot
            {
                slotId = idSlot,
                doctorId = doctorId,
                slotDate = data,
                slotTime = time,
                isBooked = false
            });


            Console.WriteLine($"TheSlot add successfully Slot ID :{idSlot} with {doctorId} " );

        }

        public static void BookAnAppointment(HospitalContext context)
        {
            Console.WriteLine("Enter Patient Id:");
            int idPatient = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Doctor Id:");
            int idDoctor = int.Parse(Console.ReadLine());


            bool found = false;

            Console.WriteLine("Available Slots:");

            // اعرض مواعيد الدكتور الموجودة فقط
            foreach (AvailableSlot slot in context.AvailableSlots)
            {
                if (slot.doctorId == idDoctor && slot.isBooked == false)
                {
                    Console.WriteLine($"Slot ID: {slot.slotId}");
                    Console.WriteLine($"Date: {slot.slotDate}");
                    Console.WriteLine($"Time: {slot.slotTime}");

                    found = true;
                }
            }

            //إذا ما فيه مواعيد متاحة
                if (found == false)
            {
                Console.WriteLine("No available slots for this doctor.");
                return;
            }

            // اختيار الموعد
            Console.Write("Enter Slot ID: ");
            int slotId = Convert.ToInt32(Console.ReadLine());

            //البحث عن الموعد 
                foreach (AvailableSlot slot in context.AvailableSlots)
            {
                if (slot.slotId == slotId)
                {
                    Appointment appointment = new Appointment();

                    appointment.patientId = idPatient;
                    appointment.doctorId = idDoctor;
                    appointment.appointmentDate = slot.slotDate;
                    appointment.appointmentTime = slot.slotTime;
                    appointment.status = "Booked";

                    context.Appointments.Add(appointment);

                    // تحويل الموعد إلى محجوز
                    slot.isBooked = true;

                    Console.WriteLine("Appointment booked successfully.");

                    return;
                }
            }

            Console.WriteLine("Invalid Slot ID.");
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







