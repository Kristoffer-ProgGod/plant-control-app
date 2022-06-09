using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PlantControlApp.ViewModels;

public class CreatePlantViewModel : Bindable
{
    private readonly HttpClient _httpClient;
    private ImageSource _imageSource;
    private string _name;
    private FileResult _photoFile;


    public CreatePlantViewModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
        CreatePlantCommand = new Command(async () =>
        {
            var plantContent = new StringContent(Name);
            var imageContent = await FileResultToByteArrayContent();
            var multiContent = new MultipartFormDataContent();
            multiContent.Add(plantContent, "name");
            multiContent.Add(imageContent, "image", "image");

            await _httpClient.PostAsync("plants", multiContent);
        }, CreatePlantCanExecute);

        TakePhotoCommand = new Command(async () =>
        {
            _photoFile = await MediaPicker.CapturePhotoAsync();
            var photo = await _photoFile.OpenReadAsync();
            ImageSource = ImageSource.FromStream(() => photo);
        });
    }


    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
            ((Command) CreatePlantCommand).ChangeCanExecute();
        }
    }

    public ImageSource ImageSource
    {
        get => _imageSource;
        set
        {
            _imageSource = value;
            OnPropertyChanged();
            ((Command) CreatePlantCommand).ChangeCanExecute();
        }
    }

    public ICommand TakePhotoCommand { get; }
    public ICommand CreatePlantCommand { get; }

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