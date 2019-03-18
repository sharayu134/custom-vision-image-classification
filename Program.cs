using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft;



namespace ChequePredictions
{
    static class Program
    {
        static void Main()
        {
            Console.Write("Enter image file path: ");
            string imageFilePath = Console.ReadLine();

            MakePredictionRequest(imageFilePath).Wait();

            Console.WriteLine("\n\n\nHit ENTER to exit...");
            Console.ReadLine();
        }

        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

        static async Task MakePredictionRequest(string imageFilePath)
        {
            var client = new HttpClient();

            // Request headers - replace this example key with your valid subscription key.
            client.DefaultRequestHeaders.Add("Prediction-Key", "226f12ea329149718db1847640d07a04");

            // Prediction URL - replace this example URL with your valid prediction URL.
            string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v2.0/Prediction/a536dc35-0205-47ef-a66d-8ee6b12e2491/image";

            HttpResponseMessage response;

            // Request body. Try this sample with a locally stored image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(url, content);

                string cons = await response.Content.ReadAsStringAsync();

                System.IO.File.WriteAllText(@"F:\name.json", cons);

                //
                var oMycustomclassname = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(cons);

                // oMycustomclassname.ToString;
                // Console.WriteLine(oMycustomclassname.predictions[0].probability);
                //Console.WriteLine(oMycustomclassname.predictions[0].tagName);
                //  Console.WriteLine(oMycustomclassname.predictions[0].tagId);



                for (var i = 0; i <= 6; i++)
                {

                    Console.WriteLine(oMycustomclassname.predictions[i].probability);
                    Console.WriteLine(oMycustomclassname.predictions[i].tagName);
                    Console.WriteLine(oMycustomclassname.predictions[i].tagId);


                }

                //  Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }
    }

 


}