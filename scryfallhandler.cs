using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using SkiaSharp;

public class scryfallhandler
{
    private readonly HttpClient client = new HttpClient();

    public bool donotdownload = false;
    public void Message(string text)
    {
        Console.WriteLine(text);
    }
    public int GetBasicLandMultiverseId(string landtype)
    {
        landtype = ToProperCase(landtype);
        if(landtype=="Forest")
            return 289;
        if(landtype=="Mountain")
            return 291;
        if(landtype=="Plains")
            return 295;
        if(landtype=="Swamp")
            return 278;
        if(landtype=="Island")
            return 293;
        return 0;
    }
    public string ToProperCase(string str)
    {
        return string.IsNullOrEmpty(str)
            ? string.Empty
            : System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
    }    
    public async Task<bool> UpdateCardJson(List<CardInfo> cards)
    {

        foreach(var cardinfo in cards)
        {
            this.Message(cardinfo.multiverseid.ToString().PadLeft(7,'0')+' ' + cardinfo?.name);

            // Send a GET request to the Scryfall API to search for the card
            string cardjson = "";
            string url = $"https://api.scryfall.com/cards/multiverse/{cardinfo.multiverseid}";
            if(cardinfo.multiverseid == 0)
            {
                // 289=Forest, 291=Mountain, 295=Plains, 278 Swamp, 293 Island 
                // Replace spaces with "+" for the API query
                string queryCardName = cardinfo.name.Replace(" ", "+");
                url = "https://api.scryfall.com/cards/named?fuzzy=" + queryCardName;
            }

            client.DefaultRequestHeaders.Add("User-Agent", "msegen/1.0.3");
            client.DefaultRequestHeaders.Add("Accept", "application/json;q=0.9,*/*;q=0.8");

            HttpResponseMessage responseid = 
                await client.GetAsync(url);

            // If the request is successful, parse the response
            if (responseid.IsSuccessStatusCode)
                cardjson = await responseid.Content.ReadAsStringAsync();

 //           string bob2 = @"C:\SNJW\code\mq\source\lastout.json";
            if(cardjson != "" && (cardinfo?.cardjson ?? "") == "")
            {
                cardinfo.cardjson = JsonConvert.SerializeObject(cardjson)
                    .Replace(@"\\""",@"'")
                    .Replace(@"\""",@"""")
                    .Replace(@"\\",@"\")
                    .Trim('"');
//              File.WriteAllText(bob2,cardinfo.cardjson);

            }

            if(cardinfo.multiverseid == 0)
            {
                // Card card = JsonConvert.DeserializeObject<Card>(File.ReadAllText(bob2));
                Card card = JsonConvert.DeserializeObject<Card>(cardinfo.cardjson);

                if(card?.name == null)
                {
                    this.Message("Cannot find '" + cardinfo?.name + "' on Scryfall.");
                }
                else if(GetBasicLandMultiverseId(card.name)>0)
                {
                    cardinfo.multiverseid = GetBasicLandMultiverseId(card.name);
                    // 

                    // 
                }
                else if(card.multiverse_ids.Count>0)
                {
                    cardinfo.multiverseid = card.multiverse_ids.Min();
                }
            }

        }

        return true;
    }

    public void UpdateFromCardJson(List<CardInfo> cards)
    {
        foreach(var cardinfo in cards)
        {
            if(cardinfo.cardjson != "")
                cardinfo.card = JsonConvert.DeserializeObject<Card>(cardinfo.cardjson);
            if(cardinfo.cleanname != "")
                cardinfo.cleanname = cardinfo?.card?.name ?? "";
        }
    }

    public async Task<bool>  UpdateImgPathFromCardJson(List<CardInfo> cards, string outputpath)
    {
        // include
        foreach(var cardinfo in cards)
        {
            if((cardinfo?.cardjson ?? "") != "" && (cardinfo?.imagepath ?? "") == "")
            {
                Card card = JsonConvert.DeserializeObject<Card>(cardinfo.cardjson);
                string imageUrl = card?.image_uris?.art_crop ?? "";
                string outputfile = outputpath + '\\' + card.name
                    .Replace(" ","_")
                    .Replace(",","")
                    .Replace("/","")
                    .Replace(":","")
                    .Replace(";","")
                    + ".png";

                // if double-sided, set the URL of the front face if this is the correct one
                if(imageUrl == "" && (card?.multiverse_ids?.Count ?? 0) > 1  && (card?.card_faces?.Count ?? 0) > 1 )
                {
                    // the name in cardinfo can be checked against each card face
                    foreach(var card_face in card.card_faces)
                    {
                        if(card_face?.name == cardinfo.name)
                        {
                            imageUrl = card_face?.image_uris?.art_crop ?? "";
                            outputfile = outputpath + '\\' + cardinfo.name
                                .Replace(" ","_")
                                .Replace(",","")
                                .Replace("/","")
                                .Replace(":","")
                                .Replace(";","")
                                + ".png";
                        }
                    }
                }


                if(imageUrl != "")
                {
                    if( donotdownload == false)
                        await this.DownloadImageAsPng(imageUrl,outputfile);
                    cardinfo.imagepath = outputfile;
                }
            }
        }
        return true;
    }

    public async Task<bool> DownloadImageAsPng(string imageUrl, string filename)
    {
        // Create an HttpClient to download the image
        using (HttpClient httpClient = new HttpClient())
        {
            // Download the image and save it to a memory stream
            using (HttpResponseMessage response = await httpClient.GetAsync(imageUrl))
            {
                using (Stream imageStream = await response.Content.ReadAsStreamAsync())
                {
                    // Load the image from the memory stream
                    using (SKManagedStream skStream = new SKManagedStream(imageStream))
                    {
                        using (SKBitmap bitmap = SKBitmap.Decode(skStream))
                        {
                            // Save the image to a file in PNG format
                            using (SKData data = SKImage.FromBitmap(bitmap).Encode(SKEncodedImageFormat.Png, 100))
                            using (SKImage pngImage = SKImage.FromEncodedData(data))
                            using (FileStream fileStream = File.OpenWrite(filename))
                            {
                                data.SaveTo(fileStream);
                            }
                        }
                    }
                }
            }
        }
        return true;
    }

    public string GetCardJson(int multiverseId)
    {

        // Replace this with the desired multiverse ID
        // int multiverseId = 123456;

        // Construct the URL for the Scryfall API
        string url = $"https://api.scryfall.com/cards/multiverse/{multiverseId}";

        // Send a request to the Scryfall API using HttpClient
        HttpClient client = new HttpClient();
        HttpResponseMessage response = client.GetAsync(url).Result;
        string jsonString = response.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string into a card object
        Card card = JsonConvert.DeserializeObject<Card>(jsonString);

        return JsonConvert.SerializeObject(card, Formatting.Indented);

        // // Save the card object as a JSON file
        // string filePath = $@"C:\SNJW\code\mq\output\{card.name}.json";
        // File.WriteAllText(filePath, JsonConvert.SerializeObject(card, Formatting.Indented));
    }
}