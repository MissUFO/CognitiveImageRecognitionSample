using System.IO;
using System.Linq;
using ImgRecognition.BusinessLogic.DataObjects;
using ImgRecognition.BusinessLogic.DataServices.Interface;
using System.Net.Http;
using ImgRecognition.BusinessLogic.Properties;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

namespace ImgRecognition.BusinessLogic.DataServices.Implementation
{
    /// <summary>
    /// Image recognition using Microsoft Cognitive Service
    /// </summary>
    public class CognitiveImgRecognition : IBasicImgRecognition<ImgDescription>
    {
        /// <summary>
        /// Recognize image by stream
        /// </summary>
        public async Task<ImgDescription> RecognizeImage(Stream photo)
        {
            var result = new ImgDescription();

            using (HttpClient client = new HttpClient())
            {

                // Request headers.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Settings.Default.COMPUTER_VISION_API_KEY);

                // Request parameters. A third optional parameter is "details".
                string requestParameters = "visualFeatures=Categories,Description&language=en";

                // Assemble the URI for the REST API Call.
                string uri = Settings.Default.COMPUTER_VISION_API_ENDPOINT + "?" + requestParameters;

                HttpResponseMessage response;

                // Request body. Posts an image.
                BinaryReader binaryReader = new BinaryReader(photo);
                byte[] byteData = binaryReader.ReadBytes((int)photo.Length);

                using (ByteArrayContent content = new ByteArrayContent(byteData))
                {
                    // This example uses content type "application/octet-stream".
                    // The other content types you can use are "application/json" and "multipart/form-data".
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    try
                    {
                        // Execute the REST API call.
                        response = await client.PostAsync(uri, content);

                        // Get the JSON response.
                        string json = await response.Content.ReadAsStringAsync();

                        var obj = JsonConvert.DeserializeObject<dynamic>(json);
                        result.Description = obj.description != null && obj.description.captions != null ? obj.description.captions[0].text : "";
                        result.Tags.AddRange(obj.description.tags);
                    }
                    catch (Exception exc)
                    {
                        result.Errors.Add(exc.Message);
                    }
                    // Display the JSON response.
                    //Console.WriteLine("\nResponse:\n");
                    //Console.WriteLine(JsonPrettyPrint(contentString));
                }
            }

            return result;
        }

    }
}
