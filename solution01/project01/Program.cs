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

        //=======================================================================
        // --------- ** Book an Appointment ** ----------
        //=======================================================================
        public static void BookAnAppointment(HospitalContext context)
        {
            Console.WriteLine("\n=== Book an Appointment ===");

            Console.WriteLine("Enter your Patient Id:");
            int idPatient = int.Parse(Console.ReadLine());

            Patient patient = context.Patients.FirstOrDefault(p => p.patientId == idPatient);

            if (patient == null)
            {
                Console.WriteLine("Patient not found.");
                return;
            }

            ViewAllDoctorBySpecializtion(context.Doctors);

            Console.WriteLine("Enter your  Doctor Id to book with:");
            int idDoctor = int.Parse(Console.ReadLine());

            Doctor doctor = context.Doctors.FirstOrDefault(d => d.doctorId == idDoctor);

            if (doctor == null)
            {
                Console.WriteLine("Doctor not found>");
                return;
            }

            List<AvailableSlot> openSlots = context.AvailableSlots.Where(s => s.doctorId == idDoctor && s.isBooked == false)
                                                                  .ToList();

            if (openSlots.Count == 0)
            {
                Console.WriteLine("No available slots for this doctor at the moment.");
                return;
            }

            Console.WriteLine($"\n Available slot for Doctor. {doctor.doctorName}:");
            openSlots.ForEach(s =>
            Console.WriteLine($"Slot ID: {s.slotId}   |  Date: {s.slotDate}   |   Time{s.slotTime} ")

            );

            Console.WriteLine("Enter slot Id to book: ");
            int slotId = int.Parse(Console.ReadLine());

            AvailableSlot selectedSlot = openSlots.FirstOrDefault(s => s.slotId == slotId);

            if (selectedSlot == null)
            {
                Console.WriteLine("Slot not found or already booked.");
                return;
            }

            int appointmentId = context.Appointments.Count + 1;
            context.Appointments.Add(new Appointment
            {
                appointmentId = appointmentId,
                patientId = idPatient,
                doctorId = idDoctor,
                appointmentDate = selectedSlot.slotDate,
                appointmentTime = selectedSlot.slotTime,
                status = "Scheduled"
            });

            selectedSlot.isBooked = true;

            Console.WriteLine($"Appointment booked successfully! Appointment ID: {appointmentId}" +
                              $" | Date: {selectedSlot.slotDate} | Time: {selectedSlot.slotTime}");

            //    // اعرض مواعيد الدكتور الموجودة فقط
            //    foreach (AvailableSlot slot in context.AvailableSlots)
            //    {
            //        if (slot.doctorId == idDoctor && slot.isBooked == false)
            //        {
            //            Console.WriteLine($"Slot ID: {slot.slotId}");
            //            Console.WriteLine($"Date: {slot.slotDate}");
            //            Console.WriteLine($"Time: {slot.slotTime}");

            //            found = true;
            //        }
            //    }

            //    //إذا ما فيه مواعيد متاحة
            //        if (found == false)
            //    {
            //        Console.WriteLine("No available slots for this doctor.");
            //        return;
            //    }

            //    // اختيار الموعد
            //    Console.Write("Enter Slot ID: ");
            //    int slotId = Convert.ToInt32(Console.ReadLine());

            //    //البحث عن الموعد 
            //        foreach (AvailableSlot slot in context.AvailableSlots)
            //    {
            //        if (slot.slotId == slotId)
            //        {
            //            Appointment appointment = new Appointment();

            //            appointment.patientId = idPatient;
            //            appointment.doctorId = idDoctor;
            //            appointment.appointmentDate = slot.slotDate;
            //            appointment.appointmentTime = slot.slotTime;
            //            appointment.status = "Booked";

            //            context.Appointments.Add(appointment);

            //            // تحويل الموعد إلى محجوز
            //            slot.isBooked = true;

            //            Console.WriteLine("Appointment booked successfully.");

            //            return;
            //        }
            //    }

            //    Console.WriteLine("Invalid Slot ID.");
        }

        //==================================================
        // --------- ** Cancel an Appointment ** ----------
        //==================================================
        public static void CancelAnAppointment(HospitalContext context)
        {

            Console.WriteLine("\n=== Cancel an Appointment ===");

            Console.WriteLine("Enter Appointment Id:");
            int idAppointment = int.Parse(Console.ReadLine());

            Appointment appointment = context.Appointments.FirstOrDefault(a => a.appointmentId == idAppointment);

            if (appointment == null)
            {
                Console.WriteLine("Appointment not found.");
                return;
            }

            if (appointment.status == "Cancelled")
            {
                Console.WriteLine("This appointment is already cancelled.");
                return;
            }

            if (appointment.status == "Completed")
            {
                Console.WriteLine("Cannot cancel a completed appointment.");
                return;
            }

            AvailableSlot slot = context.AvailableSlots.FirstOrDefault(s =>
            s.doctorId == appointment.doctorId &&
            s.slotDate == appointment.appointmentDate &&
            s.slotTime == appointment.appointmentTime
            );

            if (slot != null)
            {
                slot.isBooked = false;

                appointment.status = "Cancelled";
                Console.WriteLine($"Appointment {idAppointment} has been cancelled and the time slot is now available again.");
            }



            //    foreach (var appointment in context.Appointments)
            //    {
            //        if (appointment.appointmentId == idAppointment)
            //        {
            //            if (appointment.status == "Cancelled")
            //            {
            //                Console.WriteLine("Appointment already cancelled.");
            //                return;
            //            }

            //            appointment.status = "Cancelled";

            //            Console.WriteLine("Appointment cancelled successfully.");
            //            return;
            //        }
            //    }
            //}
            //Console.WriteLine("Appointment not found.");

        }
        //=======================================================================
        // --------- ** Create a Medical Record After a Visit ** ----------
        //=======================================================================
        public static void CreateMedicalRecordAfterVisit(HospitalContext context)
        {

            Console.WriteLine("\n=== Create Medical Record ===");

            Console.WriteLine("Enter Appointment Id");
            int appointmentId = int.Parse(Console.ReadLine());

            Appointment appointment = context.Appointments.FirstOrDefault(a => a.appointmentId == appointmentId);

            if (appointment == null)
            {
                Console.WriteLine("Appointment not found.");
                return;
            }

            if (appointment.status == "Cancelled")
            {
                Console.WriteLine("Cannot create a medical recprd for a cancelled appointment ");
                return;

            }

            if (appointment.status == "Completed")
            {
                Console.WriteLine("A medical record already exists for this appointment.");
                return;

            }

            decimal fee = context.Doctors
                 .Where(d => d.doctorId == appointment.doctorId)
                 .Select(d => d.consultationFee)
                 .FirstOrDefault();

            Console.WriteLine("Enter Diagnosis:");
            string diagnosis = Console.ReadLine();

            Console.WriteLine("Enter Medication / prescription:");
            string medication = Console.ReadLine();

            Console.WriteLine("Enter visit date(e.g:: 2026-06-22): ");
            string visitData = Console.ReadLine();

            int recordId = context.MedicalRecords.Count + 1;

            context.MedicalRecords.Add(new MedicalRecord
            {
                recordId = recordId,
                patientId = appointment.appointmentId,
                doctorId = appointment.doctorId,
                appointmentId = appointmentId,
                diagnosis = diagnosis,
                prescription = medication,
                visitDate = visitData,
                visitFee = fee

            });

            appointment.status = "Completed";
            Console.WriteLine($"Medical record created successfully. Record Id: {recordId}" +
                              $" |  Fee charged: {fee}");


            //foreach (var appointment in context.Appointments)
            //{
            //    if (appointment.appointmentId == appointmentId)
            //    {
            //        Console.WriteLine("Enter Diagnosis:");
            //        string diagnosis = Console.ReadLine();

            //        Console.WriteLine("Enter Medication:");
            //        string medication = Console.ReadLine();

            //        Console.WriteLine("Enter Fee:");
            //        decimal fee = decimal.Parse(Console.ReadLine());

            //        context.MedicalRecords.Add(new MedicalRecord
            //        {
            //            appointmentId = appointmentId,
            //            diagnosis = diagnosis,
            //            prescription = medication,
            //            visitFee = fee
            //        });

            //        appointment.status = "Completed";

            //        Console.WriteLine("Medical record created successfully.");

            //        return;
            //    }
            //}

            //Console.WriteLine("Appointment not found.");
        }

        //=======================================================================
        // --------- ** Generate a Patient Medical History Report ** ----------
        //=======================================================================
        static void PrintPatients(HospitalContext context)
        {
            Console.WriteLine("\n=== Patient Medical History Report ===");

            Console.Write("Enter patient ID: ");
            int patientId = int.Parse(Console.ReadLine());

            Patient patient = context.Patients.FirstOrDefault(p => p.patientId == patientId);

            if(patient == null)
            {
                Console.WriteLine("Patient not found.");
                return;
            }

            List<MedicalRecord> records = context.MedicalRecords
                .Where(r => r.patientId == patientId)
                .ToList();

            if(records.Count == 0)
            {
                Console.WriteLine("No medical records found for this patient.");
                return;
            }

            Console.WriteLine($"\n--- Medical History for {patient.patientName} (ID: {patientId}) ---");

            records.ForEach(r =>
            {
                string doctorName = context.Doctors
                  .Where(d => d.doctorId == r.doctorId)
                  .Select(d => d.doctorName)
                  .FirstOrDefault() ?? "Unknown";

                Console.WriteLine($"\n  Record ID   : {r.doctorId}");
                Console.WriteLine($"  Visit Date  : {r.visitDate}");
                Console.WriteLine($"  Doctor      : {doctorName}");
                Console.WriteLine($"  Diagnosis   : {r.diagnosis}");
                Console.WriteLine($"  Prescription: {r.prescription}");
                Console.WriteLine($"  Fee Charged : {r.visitFee}");
            });

            decimal totalCharged = records.Sum(r => r.visitFee);
            Console.WriteLine($"\n Total Amount Charged: {totalCharged}");

        }

        //=======================================================================
        // --------- ** Doctor Workload and Revenue Summary ** ----------
        //=======================================================================
        static void PrintDoctors(HospitalContext context)
        {
            Console.WriteLine("\n=== Doctor Workload & Revenue Summary ===");

            if (context.Appointments.Count == 0)
            {
                Console.WriteLine("No appointments have been recorded yet.");
                return;
            }

            var summary = context.Doctors
                .Select(d => new
                {

                    d.doctorId,
                    d.doctorName,
                    d.doctorSpecialization,

                    completed = context.Appointments.Count(a => a.doctorId == d.doctorId && a.status == "Completed"),
                    cancelled = context.Appointments.Count(a => a.doctorId == d.doctorId && a.status == "Cancelled"),
                    totalRevenue = context.MedicalRecords
                    .Where(r => r.doctorId == d.doctorId)
                    .Sum(r => r.visitFee)

                })
                    .OrderByDescending(x => x.totalRevenue)
                    .ToList();

            Console.WriteLine("\n  Rank  | Doctor Name               | Specialization       | Completed | Cancelled | Total Revenue");
            Console.WriteLine("  " + new string('-', 95));

            for (int i = 0; i < summary.Count; i++)
            {
                var x = summary[i];
                Console.WriteLine($"  #{i + 1,-5} | {x.doctorName,-25} | {x.doctorSpecialization,-20} |" +
                                  $" {x.completed,-9} | {x.cancelled,-9} | {x.totalRevenue:C}");
            }

        }

        static void Main(string[] args)
{

            //Data storge for the system
            HospitalContext mainContext = new HospitalContext();
            mainContext.Doctors = new List<Doctor>();
            mainContext.Appointments = new List<Appointment>();
            mainContext.MedicalRecords = new List<MedicalRecord>();
            mainContext.AvailableSlots = new List<AvailableSlot>();

            mainContext.Patients = new List<Patient>()
                {
                    new Patient(1, "Ahmed", 23, "Male", "9876543", "ahmed.gmail.com","O" ),
                    new Patient(2, "Sara", 24, "Female", "987673", "sara.gmail.com","O" ),
                    new Patient(3, "Khalid", 33, "Male", "9123456", "khalid.gmail.com","A" ),
                    new Patient(4, "Ali", 36, "Male", "12345678", "ali.gmail.com","B" ),
                    new Patient(5, "Noura", 28, "Female", "98221543", "noura.gmail.com","A" ),

                };


            bool exit = false;
                while(exit == false)
                {

                Console.WriteLine("\n========================================");
                Console.WriteLine("   Hospital Management System");
                Console.WriteLine("========================================");
                Console.WriteLine("01- Patient Registration");
                Console.WriteLine("02- Add New Doctor");
                Console.WriteLine("03- View All Patients");
                Console.WriteLine("04- View All Doctor By Specializtion");
                Console.WriteLine("05- Add Available Time Slot For Doctor");
                Console.WriteLine("06- Book An Appointment");
                Console.WriteLine("07- Cancel An Appointment");
                Console.WriteLine("08- Create Medical Record After Visit");
                Console.WriteLine("09- Exit");
                Console.WriteLine("========================================");
                Console.Write("Select option: ");


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
                        ViewAllDoctorBySpecializtion(mainContext.Doctors);

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

                if (!exit)
                {
                    Console.WriteLine("Press any key to continue....");
                    Console.ReadKey();
                    Console.Clear();
                }

               
            }
        }
   
        }
    }






