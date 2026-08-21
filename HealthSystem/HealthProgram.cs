using HealthLibrary;
using System;

namespace MyApp
{
    internal class HealthProgram
    {
        static healthManagement health = new healthManagement();
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("\n ------ Health System for hospital ------");
                Console.WriteLine("To Add a patient press A");
                Console.WriteLine("To Add a Physician press B");
                Console.WriteLine("To schedule an appointment press C");
                Console.WriteLine("To add notes on the patient and preescriptions for him press D");
                Console.WriteLine("To list all of the patients press E");
                Console.WriteLine("To list all of the physicians press F");
                Console.WriteLine("To make a change on a patients data press G");
                Console.WriteLine("To make a change on a physicians data press H");
                Console.WriteLine("To delete the data of a patient press I");
                Console.WriteLine("To delete the data of a physician press J");
                Console.WriteLine("To make a change to an appointment press K");
                Console.WriteLine("To delete an appointment press L");
                Console.WriteLine("To exit program press 1");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "A":
                    case "a":
                        {
                            var patient = new Patient();
                            Console.WriteLine("Name: "); patient.name = Console.ReadLine();
                            Console.WriteLine("Address: "); patient.address = Console.ReadLine();
                            Console.WriteLine("Birthdate (yyyy-MM-dd): "); patient.birthDate = DateTime.Parse(Console.ReadLine()??DateTime.MinValue.ToString());//Change later so its dd-MM-yyyy
                            Console.WriteLine("Race: "); patient.race = Console.ReadLine();
                            Console.WriteLine("Gender: "); patient.gender = Console.ReadLine();
                            health.AddPatient(patient);
                            Console.WriteLine("Patient was added to the system");
                        }
                        break;
                    case "B": 
                    case "b":
                        {
                            var doctor = new Physician();
                            Console.WriteLine("Name: "); doctor.name = Console.ReadLine();
                            Console.WriteLine("License Number: "); doctor.licenseNumber = Console.ReadLine();
                            Console.WriteLine("Graduation date (yyyy-MM-dd): "); doctor.graduationTime = DateTime.Parse(Console.ReadLine() ?? DateTime.MinValue.ToString());//Change later so its dd-MM-yyyy
                            Console.WriteLine("Specialization: ");
                            string input = Console.ReadLine() ?? ""; //This took so looooong
                            doctor.specialization = input.Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToList(); //aaaaaa
                            health.AddPhysician(doctor);
                            Console.WriteLine("The Physician was added");

                        }
                        break;
                    case "C":
                    case "c":
                        {
                            if (!health.physicians.Any() || !health.patients.Any())
                                Console.WriteLine("You need at least one patient and one physician to make a schedule");
                            Console.WriteLine("Select a patient: ");
                            for (int i = 0; i < health.patients.Count; i++)
                                Console.WriteLine($"{i + 1}. {health.patients[i].name}");
                            int numOfPatients = int.Parse(Console.ReadLine()?? "0") -1;

                            Console.WriteLine("Select a physician: ");
                            for (int i = 0; i < health.physicians.Count; i++)
                                Console.WriteLine($"{i + 1}. {health.physicians[i].name}");
                            int numOfPhysicians = int.Parse(Console.ReadLine() ?? "1") -1;

                            Console.WriteLine("Enter the date and time for the appointment (yyyy-MM-dd HH:mm): ");
                            DateTime appointmentDate = DateTime.Parse(Console.ReadLine() ?? DateTime.MinValue.ToString());

                            bool success = health.scheduleAnAppointment(health.patients[numOfPatients], health.physicians[numOfPhysicians], appointmentDate);
                            Console.WriteLine(success ? "The appointment has been scheduled, Hope we see you that day!!" : "Failed to schedule the appointment (Try to make sure its not double booked");
                                                            
                        }
                        break;
                    case "D":
                    case "d":
                        {
                            Console.WriteLine("Select the patient to add a note:");
                            health.listOfPatients();
                            int index = int.Parse(Console.ReadLine()?? "1") - 1;
                            var note = new medicalNote();
                            Console.WriteLine("Date (yyyy-MM-dd): ");
                            note.date = DateTime.Parse(Console.ReadLine()?? "0");
                            Console.WriteLine("Write the diagnosis: ");
                            note.diagnosis = Console.ReadLine();
                            Console.WriteLine("Write the preescription: ");
                            note.preescription = Console.ReadLine();
                            health.patients[index].notes.Add(note);
                            Console.WriteLine("The medical note has been added for the patient!");

                        }
                        break;
                    case "E":
                    case "e":
                        {
                            health.listOfPatients();
                        }
                        break;
                    case "F":
                    case "f":
                        {
                            health.listOfPhysicians();
                        }
                        break;
                    case "G":
                    case "g":
                        {
                            Console.WriteLine("Select the patient you wish to update:");
                            health.listOfPatients();
                            int index = int.Parse(Console.ReadLine() ?? "1") - 1;

                            var patient = health.patients[index];
                            Console.Write("New name (Press enter to keep): ");
                            string? newName = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newName)) patient.name = newName;

                            Console.Write("New address (Press enter to keep): ");
                            string? newAddress = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newAddress)) patient.address = newAddress;

                            Console.WriteLine("Patient has been updated.");
                        }
                        break;
                    case "H":
                    case "h":
                        {
                            Console.WriteLine("Select a physician to update:");
                            health.listOfPatients();
                            int index = int.Parse((Console.ReadLine() ?? "1")) - 1;

                            var doctor = health.physicians[index];
                            Console.Write("New name (leave blank to keep): ");
                            string? newName = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newName)) doctor.name = newName;

                            Console.Write("New license number (leave blank to keep): ");
                            string? newLicense = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newLicense)) doctor.licenseNumber = newLicense;

                            Console.WriteLine("Physician updated.");
                                             
                }
                        break;
                    case "I":
                    case "i":
                        {
                            Console.WriteLine("Select patient to delete:");
                            health.listOfPatients();
                            int index = int.Parse(Console.ReadLine() ?? "1") - 1;
                            var patient = health.patients[index];
                            health.patients.Remove(patient);
                            Console.WriteLine("Patient deleted.");
                        }
                        break;
                    case "J":
                    case "j":
                        {
                            Console.WriteLine("Select physician to delete:");
                            health.listOfPhysicians();
                            int index = int.Parse(Console.ReadLine() ?? "1") - 1;
                            var doctor = health.physicians[index];
                            health.physicians.Remove(doctor);
                            Console.WriteLine("Physician deleted.");
                        }
                        break;
                    case "K":
                    case "k":
                        {
                            Console.WriteLine("Select appointment to update:");
                            for (int i = 0; i < health.appointments.Count; i++)
                                Console.WriteLine($"{i + 1}. {health.appointments[i].patient?.name} with {health.appointments[i].physician?.name} at {health.appointments[i].appointmentDate}");

                            int index = int.Parse(Console.ReadLine() ?? "1") - 1;
                            var appt = health.appointments[index];

                            Console.Write("New date/time (yyyy-MM-dd HH:mm): ");
                            DateTime newDate = DateTime.Parse(Console.ReadLine() ?? DateTime.MinValue.ToString());

                            appt.appointmentDate = newDate;
                            Console.WriteLine("Appointment updated.");
                        }
                        break;
                    case "1":
                        return;

                    default:
                        Console.WriteLine("Invalid command, please try again");
                        break;

                }
            } while (true);
        }
    }
}