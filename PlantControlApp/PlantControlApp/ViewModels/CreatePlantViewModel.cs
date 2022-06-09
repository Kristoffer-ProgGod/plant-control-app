using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using PlantControl.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.IO;

namespace PlantControlApp.ViewModels
{
    public class CreatePlantViewModel : Bindable
    {
        private string name;

        public string Name
        {
            get { return name; }
            set 
            { 
                name = value; OnPropertyChanged(); 
                ((Command)createPlantCommand).ChangeCanExecute(); 
            }
        }

        private ImageSource imageSource;

        public ImageSource ImageSource
        {
            get { return imageSource; }
            set 
            { 
                imageSource = value; 
                OnPropertyChanged();
                ((Command)createPlantCommand).ChangeCanExecute();
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

        private ICommand takePhotoCommand;

        public ICommand TakePhotoCommand
        {
            get { return takePhotoCommand; }
            set { takePhotoCommand = value; }
        }
        private FileResult photoFile;
        public FileResult PhotoFile 
        { 
            get { return photoFile; }
            set { photoFile = value; }
        }

        public CreatePlantViewModel()
        {
            CreatePlantCommand = new Command(async () =>
            {
                using var client = new HttpClient();
                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                var plantContent = new StringContent(Name);
                var imageContent = await FileResultToByteArrayContent(PhotoFile);
                multiContent.Add(plantContent, "name");
                multiContent.Add(imageContent, "image", "image");
                var response = await client.PostAsync("http://40.87.132.220:9092/plants", multiContent);
            }, () =>
            {
                return CreatePlantCanExecute();
            });

            TakePhotoCommand = new Command(async() =>
            {
                photoFile = await MediaPicker.CapturePhotoAsync();
                var photo = await photoFile.OpenReadAsync();
                ImageSource = StreamImageSource.FromStream(() =>
                {
                    return photo;
                });
            });
        }
            private bool CreatePlantCanExecute()
            {
                return (PhotoFile != null && !String.IsNullOrEmpty(Name));
            }
        private async Task<ByteArrayContent> FileResultToByteArrayContent(FileResult fileResult)
        {
            var stream = new StreamContent(await photoFile.OpenReadAsync());
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var byteArray = memoryStream.ToArray();
            return new ByteArrayContent(byteArray);
        }
    }
}