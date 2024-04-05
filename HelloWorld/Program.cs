﻿using Logic;

Console.WriteLine("Hello, World!");

var streamingUrl = await new SignalKEndPointRetriever().RetrieveStreamingEndpoint("192.168.1.87");
var dataSource = SignalKLibrary.CreateDataSource(streamingUrl);
var value = dataSource.CreateValue<double>("home.temperature");
value.Set(3.41);


var _httpClient = new HttpClient();
var url = "http://192.168.1.87:3000/signalk";

var response = await _httpClient.GetAsync(url);
var responseContent = response.Content;
using var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
var responseText = await reader.ReadToEndAsync();

Console.WriteLine(responseText);

await DataSetter.SetData();
await DataGetter.GetData();
