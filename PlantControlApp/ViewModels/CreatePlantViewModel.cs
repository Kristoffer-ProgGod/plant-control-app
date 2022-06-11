using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using ObservableObject = CommunityToolkit.Mvvm.ComponentModel.ObservableObject;

namespace PlantControlApp.ViewModels;

public class CreatePlantViewModel : ObservableObject
{
    private readonly HttpClient _httpClient;

    private ImageSource _imageSource;
    private string _name;
    private FileResult _photoFile;

    public string Name
    {
        get => _name;
        set
        {
            SetProperty(ref _name, value);
            ((AsyncCommand)CreatePlantCommand).ChangeCanExecute();
        }
    }

    public ImageSource ImageSource
    {
        get => _imageSource;
        set
        {
            SetProperty(ref _imageSource, value);
            ((AsyncCommand)CreatePlantCommand).ChangeCanExecute();
        }
    }

    public ICommand TakePhotoCommand { get; }
    public ICommand CreatePlantCommand { get; }

    public CreatePlantViewModel(HttpClient httpClient)
    {
        _httpClient = httpClient;

        CreatePlantCommand = new AsyncCommand(CreatePlant, CreatePlantCanExecute);
        TakePhotoCommand = new AsyncCommand(TakePhoto);
    }

    private async Task CreatePlant()
    {
        var plantContent = new StringContent(Name);
        var imageContent = await FileResultToByteArrayContent();
        var multiContent = new MultipartFormDataContent();
        multiContent.Add(plantContent, "name");
        multiContent.Add(imageContent, "image", "image");

        await _httpClient.PostAsync("plants", multiContent);
    }

    private async Task TakePhoto()
    {
        _photoFile = await MediaPicker.CapturePhotoAsync();

        var photo = await _photoFile.OpenReadAsync();
        ImageSource = ImageSource.FromStream(() => photo);
    }

    private bool CreatePlantCanExecute()
    {
        return _photoFile != null && !string.IsNullOrEmpty(Name);
    }

    private async Task<ByteArrayContent> FileResultToByteArrayContent()
    {
        var stream = new StreamContent(await _photoFile.OpenReadAsync());
        var memoryStream = new MemoryStream();

        await stream.CopyToAsync(memoryStream);

        var byteArray = memoryStream.ToArray();

        return new ByteArrayContent(byteArray);
    }
}