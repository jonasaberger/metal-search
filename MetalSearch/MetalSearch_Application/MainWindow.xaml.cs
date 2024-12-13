using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace MetalSearch_Application
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _client;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize HttpClient
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://metal-api.dev/")
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
        }

        public async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string input = TextBox_Input.Text;

            // TODO Verify Input from the Textfield

            
            

            BandSearch(input);
        }


        public async void BandSearch(string input)
        {
            try
            {
                List<BandModel> ?bands = await GetBand("/search/bands/name/" + input);
                if (bands != null)
                {
                    foreach (BandModel band in bands)
                    {
                        Label_Output.Content = band.Name + "\n";
                        Label_Output.Content += band.Genre;
                    }
                }
                else
                {
                    Label_Output.Content = "No band information found.";
                }
            }
            catch (Exception ex)
            {
                // Log error and provide user feedback
                Console.WriteLine($"Error: {ex.Message}");
                Label_Output.Content = "An error occurred while fetching band information.";
            }
        }

        private async Task<List<BandModel>?> GetBand(string path)
        {
            try
            {
                HttpResponseMessage responseMessage = await _client.GetAsync(path);
                
                if (responseMessage.IsSuccessStatusCode)
                {

                    Console.Write(responseMessage.Content);
                    // Deserialize the response as a list of BandModel
                    return await responseMessage.Content.ReadFromJsonAsync<List<BandModel>>();
                }
                else
                {
                    Console.WriteLine($"Request failed with status code: {responseMessage.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during GetBand: {ex.Message}");
            }
            return null;
        }

    }

}
