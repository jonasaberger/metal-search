using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MetalSearch_Application
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _client;

        private const int TIMEOUT = 5;

        private List<BandModel_Preview> _bands;

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
            string fString = string.Empty;
            try
            {
                _bands = await GetBand("/search/bands/name/" + input);
                if (_bands.Count != 0)
                {
                    ListBox_Output.Items.Clear();

                    foreach (BandModel_Preview band in _bands)
                    {
                        ListBox_Output.Items.Add(band.Name + " - " + band.Genre);
                    }
                }
                else
                {
                    // No Bands were found
                    ListBox_Output.Items.Add("No bands were found!");
                }

            }
            catch (Exception ex)
            {
                // Log error and provide user feedback
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private async Task<List<BandModel_Preview>?> GetBand(string path)

        {
            try
            {
                    // First, initiate the HTTP request
                    HttpResponseMessage responseMessage = await _client.GetAsync(path);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        Console.Write(responseMessage.StatusCode);

                        // Deserialize the response as a list of BandModel, using the same cancellation token
                        return await responseMessage.Content.ReadFromJsonAsync<List<BandModel_Preview>>();
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

        private void ListBox_Output_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BandModel_Preview selectedBand = _bands[((ListBox)sender).SelectedIndex];
            Console.WriteLine(selectedBand.Name);
        }
    }

}
