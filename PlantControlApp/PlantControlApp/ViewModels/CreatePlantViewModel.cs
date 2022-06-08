using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using PlantControl.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PlantControlApp.ViewModels;

public class CreatePlantViewModel : Bindable
{
    private Plant _plant = new();

    public Plant Plant
    {
        get => _plant;
        set
        {
            _plant = value;
            OnPropertyChanged();
        }
    }

    public Image Image { get; set; } = new();
    public ICommand CreatePlantCommand { get; set; }
    public ICommand TakePhotoCommand { get; set; }

    public CreatePlantViewModel()
    {
        CreatePlantCommand = new Command( OnCreatePlant);
        TakePhotoCommand = new Command( OnTakePhoto);
    }

    private async void OnCreatePlant()
    {
        using var client = new HttpClient();
        var content = new StringContent(JsonConvert.SerializeObject(new { name = Plant.Name }), Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await client.PostAsync("http://40.87.132.220:9092/plants", content);
    }

    private async void OnTakePhoto()
    {
        var photoFile = await MediaPicker.CapturePhotoAsync();
        var photo = await photoFile.OpenReadAsync();
        Image.Source = ImageSource.FromStream(() => photo);
    }
}