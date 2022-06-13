using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PlantControlApp.ViewModels;

public class DataViewModel
{
    private readonly HttpClient _http;
    
    public ICommand AppearingCommand { get; }

    public DataViewModel(HttpClient http)
    {
        _http = http;

        AppearingCommand = new AsyncCommand(Refresh);
    }

    private async Task Refresh()
    {
        // Todo
    }
}