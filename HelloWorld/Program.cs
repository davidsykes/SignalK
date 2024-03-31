// See https://aka.ms/new-console-template for more information
using System.Net.Http;

Console.WriteLine("Hello, World!");



var _httpClient = new HttpClient();
var url = "http://192.168.1.87:3000/signalk";

var response = await _httpClient.GetAsync(url);
var responseContent = response.Content;
using var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
var responseText = await reader.ReadToEndAsync();


Console.WriteLine(responseText);


