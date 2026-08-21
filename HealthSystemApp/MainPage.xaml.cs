namespace HealthSystemApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }
        private async void OnPatients(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(PatientsPage));
        }

        private async void OnPhysicians(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(PhysiciansPage));
        }

        private async void OnAppointments(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AppointmentsPage));
        }
    }
}
