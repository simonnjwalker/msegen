using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
#pragma warning disable CS4014
namespace msegen
{
    
    class msegenmain
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Started ...");
            bool rundemo = false;
            if(rundemo)
            {
                var demo = new msegendemo();
                await demo.RunDemo();
                return;
            }

            // string deck = @"C:\SNJW\code\mq\source\dimir.txt";
            string xlsource = @"C:\SNJW\code\mqwip\source\main.xlsx";
            string xldest = @"C:\SNJW\code\mqwip\output\cards.xlsx";
            string imgoutput = @"C:\SNJW\code\mqwip\output\temp";
            string imgresized = @"C:\SNJW\code\mqwip\output\temp\resized";
            string imgcropped = @"C:\SNJW\code\mqwip\output\temp\cropped";
            string docximgsource = @"C:\snjw\code\mqwip\output\cardimages";
            string docximgoutput = @"C:\snjw\code\mqwip\output\docx\2024vc.docx";
            string msedest = @"C:\SNJW\code\mqwip\output\main.mse-set";
            string apikey = System.IO.File.ReadAllText(@"C:\SNJW\admin\info\apikeys\openai.apikey.txt");
            var xl = new xlsxhandler();
            var sf = new scryfallhandler();
            var tx = new txthandler();
            var ig = new imghandler();
            var ms = new msehandler();
            var dx = new docxhandler();
            var dl = new dallehandler(apikey);

            // extra bits: load the outputs into docx files
            // do this once you have used the MSE app to export PNG files
            //dx.SaveMultiPageImgDocument(docximgsource,docximgoutput);
            //return;


            // build an empty list of cards
            List<CardInfo> cards = new List<CardInfo>();

            // fill this list either from a text file or from an XLSX
            cards.AddRange(xl.GetCardInfoList(xlsource));


            // go to scryfall and download the cardjson field into the cards collection
            // the query is made from from the multiverseid (if known) or alternatively the name field
            // if the name field, it will try and grab a multiverseid
            // note that it is not always possible to get multiverseids from a name automatically
            //  so you should check all of them
            await sf.UpdateCardJson(cards);

            // update the card object from the Json field in the cards collection
            sf.UpdateFromCardJson(cards);

            // use Dall-E to generate images based on the art desc then download and update the file field "1024x1024" "256x256"
            // await dl.UpdateImgPathFromArtDesc(cards,imgoutput,"1024x1024");

            // alternatively, download the image from scryfall's art crop
            //sf.donotdownload = true;
            //await sf.UpdateImgPathFromCardJson(cards,imgoutput);

            // scale the images to the height required then crop to the width required
            //ig.ResizeImagesToHeight(cards,imgresized,1672);
            //ig.CropImagesToWidth(cards,imgcropped,1288);

            // create the msefile
            //ms.SaveMse(cards,msedest);

            // save the updated card info to a new XLSX file
            xl.SaveCardInfoList(cards,xldest);

            Console.WriteLine("Finished.");
        }
    }

    public class msegendemo
    {
        public async Task<bool> RunDemo()
        {
            Console.WriteLine("Deleting and recreating temporary folders.");
            string path = Path.GetTempPath().TrimEnd('\\') + '\\' + "mqdemo";
            string imgpath = path + '\\' +"images";
            string xldest = path + '\\' + "cards.xlsx";
            string msedest = path + '\\' + "output.mse-set";
            if(System.IO.Directory.Exists(path))
                System.IO.Directory.Delete(path,true);
            System.IO.Directory.CreateDirectory(path);
            System.IO.Directory.CreateDirectory(imgpath);
            var xl = new xlsxhandler();
            var sf = new scryfallhandler();
            var tx = new txthandler();
            var ig = new imghandler();
            var ms = new msehandler();

            Console.WriteLine("Creating a list of card names.");
            List<CardInfo> cards = new List<CardInfo>();
            cards.Add(new CardInfo(){name = "Sol Ring"});
            cards.Add(new CardInfo(){name = "Baneslayer Angel"});
            cards.Add(new CardInfo(){name = "History of Benalia"});

            // note that some cards you must specify a multiverseid
            // this links to a Gatherer entry 
            // eg: for Fire//Ice, it will not be found without onw
            // whereas for Liliana, it will be the wrong version
            cards.Add(new CardInfo(){name = "Fire", multiverseid=27165});
            cards.Add(new CardInfo(){name = "Liliana of the Veil", multiverseid=235597});
            cards.Add(new CardInfo(){name = "Island", multiverseid=293});
            cards.Add(new CardInfo(){name = "Mountain", multiverseid=73958});
            cards.Add(new CardInfo(){name = "Lighthouse Chronologist"});
            cards.Add(new CardInfo(){name = "Urza's Saga"});
            cards.Add(new CardInfo(){name = "Karakas", multiverseid=1701});
            cards.Add(new CardInfo(){name = "Jeska, Thrice Reborn"});
            cards.Add(new CardInfo(){name = "Dryad Arbor"});


            // cards.Add(new CardInfo(){name = "The First Iroan Games"});

            // note: we could load from Excel or a text fiile using either of these
            // cards.AddRange(xl.GetCardInfoList(xlsource));
            // cards.AddRange(tx.GetCardInfoList(deck));

            Console.WriteLine("Going to Scryfall and retrieving JSON information for each card.");

            // go to scryfall and update the cardjson field in the data
            // from the multiverseid (if known) or alternatively the rawname field
            await sf.UpdateCardJson(cards);
            sf.UpdateFromCardJson(cards);

            Console.WriteLine("Going to Scryfall and downloading the art crop for each card.");
            await sf.UpdateImgPathFromCardJson(cards,imgpath);

            // scale the images to the height required then crop to the width required
            Console.WriteLine("Resizing the images to the required height.");
            ig.ResizeImagesToHeight(cards,imgpath,1672);
            Console.WriteLine("Cropping the images to the required width.");
            ig.CropImagesToWidth(cards,imgpath,1288);

            // create the msefile
            Console.WriteLine("Creating the MSE (Magic Set Editor) file using the final images.");
            ms.SaveMse(cards,msedest);

            // save the updated card info to a new XLSX file
            Console.WriteLine("Creating the updated card info to a new XLSX file.");
            xl.SaveCardInfoList(cards,xldest);

            Console.WriteLine("Finished creating demo data.");
            Console.WriteLine("");
            Console.WriteLine("You can open the XLSX file here:");
            Console.WriteLine(xldest);
            Console.WriteLine("");
            Console.WriteLine("You can open the MSE file here:");
            Console.WriteLine(msedest);
            Console.WriteLine("");
            Console.WriteLine("You can also download the MSE application with the Future-Shifted Hi-Res cards here:");
            Console.WriteLine("https://www.dropbox.com/s/duuabd3zb5ws70p/mse-application-future-tall-hires-only.zip?dl=0");

            return true;
        }

    }
}