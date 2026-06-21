using project01;
using project01.Model;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;
using System.Timers;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace project01
{
    public class Program
    {
        //==================================================
        // --------- ** Patient Registration ** ----------
        //==================================================

        public static void PatientRegistration(List<Patient> patientsList)
        {
            //Patient information:

            Console.WriteLine("\n ==== Register New Patient === ");

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

            int userId = (patientsList.Count) + 1;

            patientsList.Add(new Patient(userId, userName, userAge, userGender, userPhone, userBloodType, userEmail));
            Console.WriteLine($"Patient registered successfully. Assigned ID: {userId} ");


        }

        //==================================================
        // --------- ** Add a New Doctor ** ----------
        //==================================================

        public static void AddNewDoctor(List<Doctor> doctorsList)
        {

            Console.WriteLine("\n === Add New Doctor === ");

            Console.WriteLine("Enter Doctor Name: ");
            string nameDoctor = Console.ReadLine();


            Console.WriteLine("Enter Doctor Specialization: ");
            string specializationDoctor = Console.ReadLine();

            Console.WriteLine("Enter Doctor Phone: ");
            string phoneDoctor = Console.ReadLine();

            Console.WriteLine("Enter Doctor Email: ");
            string emailDoctor = Console.ReadLine();

            Console.WriteLine("Enter ConsultationFee: ");
            decimal consultationFeeDoctor = decimal.Parse(Console.ReadLine());

            int doctorId = (doctorsList.Count) + 1;

            doctorsList.Add(new Doctor

            {

                doctorId = doctorId,
                doctorName = nameDoctor,
                doctorSpecialization = specializationDoctor,
                doctorPhone = phoneDoctor,
                doctorEmail = emailDoctor

            });


            Console.WriteLine($"Doctor Added Successfully. Assigned ID: {doctorId}"); ;


        }

        //==================================================
        // --------- ** View All Patients ** ----------
        //==================================================
        public static void ViewAllPatients (List <Patient> patientList)
        {

            Console.WriteLine("\n=== All Registered Patients ===");

            if (patientList.Count == 0)
            {
                Console.WriteLine("No patients have been registerd yet.");
                return;
            }
           
            foreach(Patient P in patientList)
            {
                P.ShowData();
            }

            //foreach (var patient in context.Patients)
            //{
            //    if (patient.patientId == 0)
            //    {
            //        Console.WriteLine("No Patient");
            //    }

            //    else
            //    {
            //        Console.WriteLine($" The Patien Id:   {patient.patientId}");

            //        Console.WriteLine($"The Name is:  {patient.patientName}");

            //        Console.WriteLine($"The Age is:   {patient.patientAge}");

            //        Console.WriteLine($"The Gender is:   {patient.patientGender}");

            //        Console.WriteLine($"The Phine number is:   {patient.patientPhone}");

            //        Console.WriteLine($"The Email is:  {patient.patientEmail}");

            //        Console.WriteLine($"The Blood Type is:   {patient.patientBloodType}");




            //    }
            //}
        }

        //=======================================================================
        // --------- ** View All Doctors by Specialization ** ----------
        //=======================================================================
        public static void ViewAllDoctorBySpecializtion(List <Doctor> doctorsList)
        {
            Console.WriteLine("\n=== Search Doctors by Specialization ===");

            Console.WriteLine("The Specialization is: ");
            string theSpecialization = Console.ReadLine().ToLower();


            List<Doctor> matched = doctorsList.Where(d => d.doctorSpecialization.ToLower() == theSpecialization).ToList();
            if (matched.Count == 0)
            {
                Console.WriteLine($"No doctors found with specialization '{theSpecialization}'. ");
                return;
            }

            foreach( Doctor doctor in matched)
            {
                Console.WriteLine("Doctor ID= " + doctor.doctorId + "Doctor Name: " + doctor.doctorName );
            }

            matched.ForEach(Doctor => Console.WriteLine("Doctor ID= " + Doctor.doctorId + "Doctor Name: " + Doctor.doctorName));


            //bool found = false;

            //foreach (var doctor in context.Doctors)
            //{
            //    if (doctor.doctorSpecialization == theSpecialization)
            //    {
            //        Console.WriteLine($"The Doctor Id is: {doctor.doctorId}");
            //        Console.WriteLine($" The Name is : {doctor.doctorName} ");
            //        Console.WriteLine($" The Doctor Email is : {doctor.doctorEmail}");
            //        Console.WriteLine($"The Doctor Phone is: {doctor.doctorPhone}");
            //        Console.WriteLine($"The Doctor Specialization : {doctor.doctorSpecialization} ");
            //        Console.WriteLine($"The Doctor ConsultationFee: {doctor.consultationFee}");

            //            found = true;
            //            }
            //            if (found == false)
            //            {
            //                Console.WriteLine("No Doctot Found!");

            //            }
            //        }
        }

        //=======================================================================
        // --------- ** Add an Available Time Slot for a Doctor ** ----------
        //=======================================================================
        public static void AddAvailableTimeSlotForDoctor(HospitalContext context)
        {
            Console.WriteLine("\n=== Add Available Slot for Doctor ===");

            if (context.Doctors.Count == 0)
            {
                Console.WriteLine("No doctors in the system yet. Please add a doctor first.");
                return;

            }


            Console.WriteLine("Available Doctors: ");
            context.Doctors.ForEach(d =>
            Console.WriteLine($" ID: {d.doctorId}  |  {d.doctorName}   | ({d.doctorSpecialization})")
            );

            Console.WriteLine("Enter doctor Id: ");
            int doctorId = int.Parse(Console.ReadLine());


            bool result = context.Doctors.Any(d => d.doctorId == doctorId);

            if (result == false)
            {
                Console.WriteLine( "No doctor found with id");
                return;
            }

            Console.WriteLine("Enter Date (e.g:: 2026-06-21) : ");
            string data = Console.ReadLine();

            Console.WriteLine("Enter Time (e.g::12:00 PM ): ");
            string time = Console.ReadLine();

            int idSlot = context.AvailableSlots.Count + 1;

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

        public static void CancelAnAppointment(HospitalContext context)
        {
            Console.WriteLine("Enter Appointment Id:");
            int idAppointment = int.Parse(Console.ReadLine());

            foreach (var appointment in context.Appointments)
            {
                if (appointment.appointmentId == idAppointment)
                {
                    if (appointment.status == "Cancelled")
                    {
                        Console.WriteLine("Appointment already cancelled.");
                        return;
                    }

                    appointment.status = "Cancelled";

                    Console.WriteLine("Appointment cancelled successfully.");
                    return;
                }
            }

            Console.WriteLine("Appointment not found.");
        }

        public static void CreateMedicalRecordAfterVisit(HospitalContext context)
        {
            Console.WriteLine("Enter Appointment Id");
            int appointmentId = int.Parse(Console.ReadLine());

            foreach (var appointment in context.Appointments)
            {
                if (appointment.appointmentId == appointmentId)
                {
                    Console.WriteLine("Enter Diagnosis:");
                    string diagnosis = Console.ReadLine();

                    Console.WriteLine("Enter Medication:");
                    string medication = Console.ReadLine();

                    Console.WriteLine("Enter Fee:");
                    decimal fee = decimal.Parse(Console.ReadLine());

                    context.MedicalRecords.Add(new MedicalRecord
                    {
                        appointmentId = appointmentId,
                        diagnosis = diagnosis,
                        prescription = medication,
                        visitFee = fee
                    });

                    appointment.status = "Completed";

                    Console.WriteLine("Medical record created successfully.");

                    return;
                }
            }

            Console.WriteLine("Appointment not found.");
        }

        static void PrintPatients(List<Patient> PatientsList)
        {
            foreach (var X in PatientsList)
            {
                X.ShowData();
            }
        }

        static void PrintDoctors(List<Doctor> doctorsList)
        {
            foreach (var P in doctorsList)
            {
                P.ShowData();
            }
        }

        static void Main(string[] args)
{

            //Data storge for the system
            HospitalContext mainContext = new HospitalContext();

            mainContext.Patients = new List<Patient>()
                {
                    new Patient(1, "Ahmed", 23, "Male", "9876543", "ahmed.gmail.com","O" ),
                    new Patient(2, "Sara", 24, "Female", "987673", "sara.gmail.com","O" ),
                    new Patient(3, "Khalid", 33, "Male", "9123456", "khalid.gmail.com","A" ),
                    new Patient(4, "Ali", 36, "Male", "12345678", "ali.gmail.com","B" ),
                    new Patient(5, "Noura", 28, "Female", "98221543", "noura.gmail.com","A" ),

                };

            mainContext.Doctors = new List<Doctor>()

            {
                new Doctor(1, "Hamed", "Diabetes", "9876543", "hamed.gmail.com", 15),
                new Doctor(2, "Noof", "Heart", "987673", "noof.gmail.com", 25),
                new Doctor(3, "Salah", "High blood pressure", "9123456", "salah.gmail.com", 30),
                new Doctor(4, "Omar", "Chronic", "12345678", "omar.gmail.com", 22),
                new Doctor(5, "Lina", "Heart", "98221543", "lina.gmail.com", 24),
            }
            ;


            mainContext.Appointments = new List<Appointment>();
                mainContext.MedicalRecords = new List<MedicalRecord>();
                mainContext.AvailableSlots = new List<AvailableSlot>();





            bool exit = false;
                while(exit == false)
                {

                Console.WriteLine("Welcome to the  Hospital Management System! ");
                Console.WriteLine("Please select option:");

                Console.WriteLine("01- Patient Registration");
                Console.WriteLine("02- Add New Doctor");
                Console.WriteLine("03- View All Patients");
                Console.WriteLine("04- View All Doctor By Specializtion");
                Console.WriteLine("05- Add Available Time Slot For Doctor");
                Console.WriteLine("06- Book An Appointment");
                Console.WriteLine("07- Cancel An Appointment");
                Console.WriteLine("08- Create Medical Record After Visit");
                Console.WriteLine("09- Exit");

                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 01:
                        PatientRegistration(mainContext.Patients);

                        break;

                    case 02:
                        AddNewDoctor(mainContext.Doctors);

                        break;

                    case 03:
                        ViewAllPatients(mainContext.Patients);

                        break;

                    case 04:
                        ViewAllDoctorBySpecializtion(mainContext);

                        break;

                    case 05:
                        AddAvailableTimeSlotForDoctor(mainContext);

                        break;

                    case 06:
                        BookAnAppointment(mainContext);

                        break;

                    case 07:
                        CancelAnAppointment(mainContext);

                        break;

                    case 08:
                        CreateMedicalRecordAfterVisit(mainContext);

                        break;

                    case 09:
                        exit = true;

                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try agin.");
                        break;
                }

                Console.WriteLine("Press any key to continue....");
                Console.ReadKey();
                Console.Clear();
            }
        }
   
        }
    }






