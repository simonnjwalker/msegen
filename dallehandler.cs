using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class dallehandler
{
    private readonly string _apiKey;
    private readonly string _baseUrl;
    private readonly HttpClient _httpClient;

    public dallehandler(string apiKey)
    {
        _apiKey = apiKey;
        _baseUrl = "https://api.openai.com/v1/images/generations";
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
    }

    public async Task<HttpResponseMessage> MakeRequestAsync(string query, string size = "256x256")
    {

        // 256x256, 512x512, or 1024x1024
        if(size != "256x256" && size != "512x512" && size != "1024x1024" )
            size = "256x256";

        // Set up the request body
        var requestBody = new StringContent($"{{\"prompt\": \"{query}\", \"model\": \"image-alpha-001\"}}");
        requestBody.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        // Make the request and return the response
        return await _httpClient.PostAsync(_baseUrl, requestBody);
    }

    public async Task DownloadImageAsync(string outputfile, string imagedesc, string size = "256x256")
    {
        // Make the request to the Dall-E API
        var response = await MakeRequestAsync(imagedesc,size);

        // Get the image URL from the response
        dynamic responseJson = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
        string imageUrl = "";
        try
        {
            imageUrl = responseJson.data[0].url;
        }
        catch (System.Exception e)
        {
            this.Message($"Cannot create Dall-E image for {System.IO.Path.GetFileName(outputfile)}");
            this.Message($"Error: {e.InnerException}:");
            // throw e.InnerException;
        }
        if(imageUrl == "")
            return;

        // Download the image
        using (HttpClient httpClient = new HttpClient())
        using (HttpResponseMessage responseMessage = await httpClient.GetAsync(imageUrl))
        using (Stream streamToReadFrom = await responseMessage.Content.ReadAsStreamAsync())
        {
            Stream streamToWriteTo = File.Open(outputfile, FileMode.Create);
            await streamToReadFrom.CopyToAsync(streamToWriteTo);
            streamToWriteTo.Close();
        }
    }

    public async Task<bool>  UpdateImgPathFromArtDesc(List<CardInfo> cards, string outputpath, string size = "256x256")
    {
        // include
        foreach(var cardinfo in cards)
        {
            if((cardinfo?.artdesc ?? "") != "" && (cardinfo?.imagepath ?? "") == "")
            {
                string cardname = (cardinfo?.cleanname ?? "");
                if(cardname == "")
                    cardname = (cardinfo?.name ?? "");
                if(cardname == "")
                    cardname = (cardinfo?.card?.name ?? "");
                if(cardname == "")
                    cardname = System.Guid.NewGuid().ToString().ToLower().Replace("-","");
                string outputfile = outputpath.TrimEnd('\\') + '\\' + cardname
                    .Replace(" ","_")
                    .Replace(",","")
                    .Replace("/","")
                    .Replace(":","")
                    .Replace(";","")
                    + ".png";

                string artdesc = (cardinfo?.artdesc ?? "");
                if(artdesc == "")
                {
                    artdesc = "Cute anime " + cardname;
                    if(cardinfo?.card?.colors?.Count > 1)
                        artdesc += " with a gold background";
                    if(cardinfo?.card?.colors?.Count == 1)
                        artdesc += " with a " + cardinfo?.card?.colors[0] + " background";
                }

                this.Message($"Attempting to render '{cardname}' with description '{artdesc}'");

                await this.DownloadImageAsync(outputfile,artdesc,size);
                GC.Collect();
                if(File.Exists(outputfile))
                    cardinfo.imagepath = outputfile;
            }
        }
        return true;
    }

    public void Message(string text)
    {
        Console.WriteLine(text);
    }

}