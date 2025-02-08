// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CapgAppLibrary;

Console.WriteLine("Press a Key after the API Servers start");
Console.ReadKey();

Console.WriteLine("Fetching List of customers");
await Fetch.CustomersList();  //Fetching list of customers
Console.WriteLine("\n******End of Details********");

Console.WriteLine("Fetching details for Customers");
Console.WriteLine("Enter the Customer Id");
await Fetch.CustomersDetails("ALFKI");  //Fetching details for ALFKI
Console.WriteLine("\n******End of Details********");

Console.WriteLine("Press a key to Terminate");
Console.ReadKey();

public static class Fetch
{
    static string BaseServerUrl = "http://localhost:7001/";
    static string ApiUrl = "api/customers";
    static AuthenticationResponse authResponse;
    public static async Task CustomersList()
    {
        if(authResponse == null)
        {
            await GenerateToken();
        }
        Console.WriteLine($"Auth Token: {authResponse.Token}");
        Console.WriteLine("Token Generated. Press a key to continue");
        Console.ReadKey();
        using(HttpClient client=new HttpClient())
        {
            //set the base address
            client.BaseAddress = new Uri(BaseServerUrl);
            client.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer",authResponse.Token);
            //place the get requwst to the api
            var response = await client.GetAsync($"{ApiUrl}/list");
            //check weater the response is succssful
            if (response.IsSuccessStatusCode)
            {
                var customers = await response.Content.ReadFromJsonAsync<List<Customer>>();
                foreach (var customer in customers)
                {
                    Console.WriteLine(customer);
                }
            }
            else
            {
                Console.WriteLine("Error in Fetching data");
            }
        }
    }
    static async Task GenerateToken()
    {
        using (HttpClient client = new HttpClient())
        {
            AuthenticationRequest request = new AuthenticationRequest()
            {
                Email = "admin@example.com",
                Password = "admin"
            };
            client.BaseAddress = new Uri("http://localhost:7002");
            var response = await client.PostAsync($"api/accounts/signin",
             content: JsonContent.Create(request));
            if (response.IsSuccessStatusCode)
            {
              authResponse = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();
            }
        }
    }
    public static async Task CustomersDetails(string customerId)
    {

        using (HttpClient client = new HttpClient())
        {
            //set the base address
            client.BaseAddress = new Uri(BaseServerUrl);
            //place the get requwst to the api
            var response = await client.GetAsync($"{ApiUrl}/details/{customerId}");
            //check weater the response is succssful
            if (response.IsSuccessStatusCode)
            {
                var customers = await response.Content.ReadFromJsonAsync<Customer>();
              
                    Console.WriteLine(customers);
            }
            else
            {
                Console.WriteLine("Error in Fetching data");
            }
        }
    }

}
