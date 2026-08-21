using HealthLibrary;
using System.Collections.ObjectModel;
namespace HealthSystemApp;

public partial class PatientsPage : ContentPage
{
	private healthManagement health = new healthManagement();
	public ObservableCollection<Patient> Patients { get; set; } = new();
    public PatientsPage()
	{
		InitializeComponent();
		BindingContext = this;
	}
	private async void OnAddPatients(object sender, EventArgs e)
	{
        string name = await DisplayPromptAsync("New Patient", "Enter name:");
        if (!string.IsNullOrWhiteSpace(name))
        {
            var patient = new Patient { name = name };
            health.AddPatient(patient);
            Patients.Add(patient);
        }
    }
    private async void OnEditPatient(object sender, EventArgs e)
    {
        if ((sender as Button)?.BindingContext is Patient patient)
        {
            string newName = await DisplayPromptAsync("Edit Patient", "Enter new name:", initialValue: patient.name);
            if (!string.IsNullOrWhiteSpace(newName))
                patient.name = newName;
        }
    }
    private void OnDeletePatient(object sender, EventArgs e)
    {
        if ((sender as Button)?.BindingContext is Patient patient)
        {
            health.patients.Remove(patient);
            Patients.Remove(patient);
        }
    }
    private async void OnAddNote(object sender, EventArgs e)
    {
        if ((sender as Button)?.BindingContext is Patient patient)
        {
            string diagnosis = await DisplayPromptAsync("Medical Note", "Diagnosis:");
            string prescription = await DisplayPromptAsync("Medical Note", "Prescription:");
            patient.notes.Add(new medicalNote { date = DateTime.Now, diagnosis = diagnosis, preescription = prescription });
        }
    }
}
