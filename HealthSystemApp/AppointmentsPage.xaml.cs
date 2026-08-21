using HealthLibrary;
using System.Collections.ObjectModel;
namespace HealthSystemApp;

public partial class AppointmentsPage : ContentPage
{
    private healthManagement health = new healthManagement();
    public ObservableCollection<Appointment> Appointments { get; set; } = new();
    public AppointmentsPage()
    {
        InitializeComponent();
        BindingContext = this;
    }
    private async void OnAddAppointment(object sender, EventArgs e)
    {
        if (!health.patients.Any() || !health.physicians.Any())
        {
            await DisplayAlert("Error", "You Need at least one patient and one physician.", "OK");
            return;
        }
        var patient = health.patients.First();
        var physician = health.physicians.First();

        string dateInput = await DisplayPromptAsync("Appointment", "Enter date/time (yyyy-MM-dd HH:mm):");
        if (DateTime.TryParse(dateInput, out DateTime dt))
        {
            bool success = health.scheduleAnAppointment(patient, physician, dt);
            if (success)
            {
                var appt = new Appointment { patient = patient, physician = physician, appointmentDate = dt };
                Appointments.Add(appt);
            }
            else
            {
                await DisplayAlert("Error", "This is and invalid time or double-booked.", "OK");
            }
        }
    }
    private void OnDeleteAppointment(object sender, EventArgs e)
    {
        if ((sender as Button)?.BindingContext is Appointment appt)
        {
            health.appointments.Remove(appt);
            Appointments.Remove(appt);
        }
    }


}