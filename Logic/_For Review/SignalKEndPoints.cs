

using System.Text.Json;

namespace Logic.Review
{
    public class SignalKEndPoints
    {
        public static async Task<SignalKEndPoints> CreateAsync(string ip)
        {
            var _httpClient = new HttpClient();
            var url = $"http://{ip}:3000/signalk";

            var response = await _httpClient.GetAsync(url);
            var responseContent = response.Content;
            using var streamReader = new StreamReader(await responseContent.ReadAsStreamAsync());
            var responseText = await streamReader.ReadToEndAsync();


            //var options = new JsonReaderOptions
            //{
            //    AllowTrailingCommas = true,
            //    CommentHandling = JsonCommentHandling.Skip
            //};
            //var reader = new Utf8JsonReader(responseText, options);

            //while (reader.Read())
            //{
            //    Console.Write(reader.TokenType);

            //    switch (reader.TokenType)
            //    {
            //        case JsonTokenType.PropertyName:
            //        case JsonTokenType.String:
            //            {
            //                string? text = reader.GetString();
            //                Console.Write(" ");
            //                Console.Write(text);
            //                break;
            //            }

            //        case JsonTokenType.Number:
            //            {
            //                int intValue = reader.GetInt32();
            //                Console.Write(" ");
            //                Console.Write(intValue);
            //                break;
            //            }

            //            // Other token types elided for brevity
            //    }
            //    Console.WriteLine();
            //}


            return new SignalKEndPoints();
        }
    }
}
