using HealthLibrary;
using System.Collections.ObjectModel;
namespace HealthSystemApp;

public partial class PhysiciansPage : ContentPage
{
    private healthManagement health = new healthManagement();
    public ObservableCollection<Physician> Physicians { get; set; } = new();
    public PhysiciansPage()
	{

		InitializeComponent();
        BindingContext = this;
	}
    private async void OnAddPhysician(object sender, EventArgs e)
    {
        string name = await DisplayPromptAsync("New Physician", "Enter name:");
        string license = await DisplayPromptAsync("New Physician", "Enter license number:");
        string grad = await DisplayPromptAsync("New Physician", "Enter graduation date (yyyy-MM-dd):");
        DateTime graduationDate = DateTime.TryParse(grad, out var g) ? g : DateTime.MinValue;

        var doctor = new Physician { name = name, licenseNumber = license, graduationTime = graduationDate };
        health.AddPhysician(doctor);
        Physicians.Add(doctor);
    }
    private async void OnEditPhysician(object sender, EventArgs e)
    {
        if ((sender as Button)?.BindingContext is Physician doctor)
        {
            string newName = await DisplayPromptAsync("Edit Physician", "Enter new name:", initialValue: doctor.name);
            if (!string.IsNullOrWhiteSpace(newName))
                doctor.name = newName;
        }
    }
    private void OnDeletePhysician(object sender, EventArgs e)
    {
        if ((sender as Button)?.BindingContext is Physician doctor)
        {
            health.physicians.Remove(doctor);
            Physicians.Remove(doctor);
        }
    }
}