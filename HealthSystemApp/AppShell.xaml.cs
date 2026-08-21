namespace HealthSystemApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(PatientsPage), typeof(PatientsPage));
            Routing.RegisterRoute(nameof(PhysiciansPage), typeof(PhysiciansPage));
            Routing.RegisterRoute(nameof(AppointmentsPage), typeof(AppointmentsPage));
        }
    }
}
