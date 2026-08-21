namespace HealthLibrary
{
    public class medicalNote
    {
        public DateTime date {  get; set; }
        public string? diagnosis { get; set; }
        public string? preescription { get; set; }
    }
    public class Patient
    {
        public Guid Id { get; } =  Guid.NewGuid();
        public string? name { get; set; }
        public string? address { get; set; }
        public DateTime birthDate { get; set; }
        public string? race { get; set; }
        public string? gender { get; set; }
        public List<medicalNote> notes { get; } = new List<medicalNote>();
    }
    public class Physician
    {
        public Guid Id { get; } =  Guid.NewGuid();
        public string? name { get; set; }
        public string? licenseNumber { get; set; }
        public DateTime graduationTime { get; set; }
        public List<string> specialization { get; set; } = new List<string>();
    }
    public class Appointment
    {
        public Guid Id { get; } = Guid.NewGuid();
        public Patient? patient {  get; set; }
        public Physician? physician { get; set; }
        public DateTime appointmentDate { get; set; }  
    }
    public class healthManagement
    {
        public List<Patient> patients { get; } = new List<Patient>();
        public List<Physician> physicians { get; } = new List<Physician>();
        public List<Appointment> appointments { get; } = new List<Appointment>();

        public void AddPatient(Patient patient) { patients.Add(patient); }
        public void AddPhysician (Physician physician) {  physicians.Add(physician);}

        public bool scheduleAnAppointment(Patient patient, Physician physician, DateTime date)
        {
            if (date.Hour < 8 || date.Hour >= 17 || date.DayOfWeek == DayOfWeek.Saturday
                || date.DayOfWeek == DayOfWeek.Sunday)
                return false;

            bool isDoubleBooked = appointments.Any(a => a.physician?.Id == physician?.Id && a.appointmentDate == date);
            if (isDoubleBooked) return false;

            appointments.Add(new Appointment
                {
                patient = patient,
                physician = physician,
                appointmentDate = date

            });
            return true;

        }
        public void listOfPatients()
        {
            if (!patients.Any()) Console.WriteLine("No patients found");
            else for (int i = 0; i < patients.Count; i++)
                    Console.WriteLine($"{i + 1}. {patients[i].name}");
        }
        public void listOfPhysicians()
        {
            if (!physicians.Any()) Console.WriteLine("No physicians found");
            else for (int i = 0; i < physicians.Count; i++)
                    Console.WriteLine($"{i + 1}. {physicians[i].name}");
        }
    }
}
