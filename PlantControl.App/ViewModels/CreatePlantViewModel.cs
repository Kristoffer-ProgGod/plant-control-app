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

    /// <summary>
    /// The user takes a photo and inputs a name and then posts it to the database.
    /// </summary>
    private async Task CreatePlant()
    {
        var plantContent = new StringContent(Name);
        var imageContent = await FileResultToByteArrayContent();
        var multiContent = new MultipartFormDataContent();
        multiContent.Add(plantContent, "name");
        multiContent.Add(imageContent, "image", "image");

        await _httpClient.PostAsync("plants", multiContent);
        await Shell.Current.GoToAsync("//PlantView");
    }

    /// <summary>
    /// Opens the camera to take a photo, then reads the photoFile into a stream which is converted to an image source.
    /// </summary>
    private async Task TakePhoto()
    {
        _photoFile = await MediaPicker.CapturePhotoAsync();

        var photo = await _photoFile.OpenReadAsync();
        ImageSource = ImageSource.FromStream(() => photo);
    }

    /// <summary>
    /// Checks if the a photo has been taken and a name has been given to the plant.
    /// Used to validate if the createPlantCommand can be called.
    /// </summary>
    /// <returns>True or false</returns>
    private bool CreatePlantCanExecute()
    {
        return _photoFile != null && !string.IsNullOrEmpty(Name);
    }

    /// <summary>
    /// Converts a FileResult to a ByteArrayContent
    /// by reading it into a StreamContent which is then copied to a MemoryStream.
    /// </summary>
    /// <returns></returns>
    private async Task<ByteArrayContent> FileResultToByteArrayContent()
    {
        var stream = new StreamContent(await _photoFile.OpenReadAsync());
        var memoryStream = new MemoryStream();

        await stream.CopyToAsync(memoryStream);

        var byteArray = memoryStream.ToArray();

        return new ByteArrayContent(byteArray);
    }
}