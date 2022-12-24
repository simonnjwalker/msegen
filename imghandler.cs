using System.IO;
using System.Net.Http;
using Microsoft.Maui.Graphics;
using SkiaSharp;

public class imghandler
{
    public void Message(string text)
    {
        Console.WriteLine(text);
    }

    public void ResizeImageFromFile(string imagefile, ImageFormat imageFormat, int intH, int intW, string output)
    {
        using (FileStream imageStream = File.Open(imagefile, FileMode.Open))
        {
            // Load the image from the memory stream
            using (SKManagedStream skStream = new SKManagedStream(imageStream))
            using (SKBitmap bitmap = SKBitmap.Decode(skStream))
            {
                // Calculate the dimensions of the cropped image
                int croppedWidth = (int)(bitmap.Width * ((float)intW / 100));
                int croppedHeight = (int)(bitmap.Height * ((float)intH / 100));
                int x = (bitmap.Width - croppedWidth) / 2;
                int y = (bitmap.Height - croppedHeight) / 2;

                // Crop the image to the specified dimensions
                using (SKBitmap croppedBitmap = new SKBitmap(croppedWidth, croppedHeight))
                {
                    bool success = bitmap.ExtractSubset(croppedBitmap,new SKRectI(x, y, x + croppedWidth, y + croppedHeight));
//                        croppedBitmap.Copy(bitmap, new SKRectI(x, y, x + croppedWidth, y + croppedHeight), new SKPointI(0, 0));

                    using (SKFileWStream stream = new SKFileWStream(output))
                    {
                        croppedBitmap.Encode(stream, SKEncodedImageFormat.Png, 100);
                    }
                }
            }
        }
    }


    //         string imageUrl = "https://cards.scryfall.io/art_crop/front/2/3/2317dab2-e412-43c5-9f8c-2fb832176cf5.jpg?156261029";
//         ImageFormat imageFormat = ImageFormat.Jpeg;
//         int intH = 50;
//         int intW = 50;

//         var test = new imghandler();
//         test.ResizeImage(imageUrl, imageFormat, intH, intW,@"c:\SNJW\code\mq\output.tst1.png");


    public void ResizeImagesToHeight(List<CardInfo> cards, string outputfolder, int outputHeight)
    {

        foreach(var cardinfo in cards)
        {
            if((cardinfo?.imagepath ?? "") != "")
            {
                this.Message($"From: {cardinfo.imagepath}");

                // Get the file name and extension
                string fileNameWithoutPath = Path.GetFileNameWithoutExtension(cardinfo.imagepath);
                string fileExtension = Path.GetExtension(cardinfo.imagepath);

                // Replace the file name and extension with the original file name and extension
                string newfile = Path.Combine(outputfolder,fileNameWithoutPath +fileExtension);

                this.ResizeImageToHeight(cardinfo.imagepath,newfile,outputHeight);

                cardinfo.imagepath = newfile;
                this.Message($"To: {cardinfo.imagepath}");

            }
        }
    }

    public void CropImagesToWidth(List<CardInfo> cards, string outputfolder, int outputWidth)
    {

        foreach(var cardinfo in cards)
        {
            if((cardinfo?.imagepath ?? "") != "")
            {
                this.Message($"From: {cardinfo.imagepath}");

                // Get the file name and extension
                string fileNameWithoutPath = Path.GetFileNameWithoutExtension(cardinfo.imagepath);
                string fileExtension = Path.GetExtension(cardinfo.imagepath);

                // Replace the file name and extension with the original file name and extension
                string newfile = Path.Combine(outputfolder,fileNameWithoutPath +fileExtension);

                this.CropImageToWidth(cardinfo.imagepath,newfile,outputWidth);

                cardinfo.imagepath = newfile;
                this.Message($"To: {cardinfo.imagepath}");

            }
        }
    }
    public void CropImageToWidth(string inputFile, string outputFile, int outputWidth)
    {

        // Load the image from the file
        using (SKBitmap original = SKBitmap.Decode(inputFile))
        {
            int offset = (int)Math.Round(((original.Width - outputWidth) / 2.0));

            // Define the rectangle that represents the region of the image to crop
            SKRectI cropRect = new SKRectI(0+offset, 0, original.Width-offset, original.Height);

            // Create a new bitmap with the same pixel format as the original image
            SKBitmap cropped = new SKBitmap(cropRect.Width, cropRect.Height, original.ColorType, original.AlphaType);

            // Create a canvas for the cropped image
            using (SKCanvas canvas = new SKCanvas(cropped))
            {
                // Draw the desired region of the original image onto the canvas
                canvas.DrawBitmap(original, cropRect, new SKRect(0, 0, cropRect.Width, cropRect.Height));
            }

            // Save the resized image to a new file
            using (FileStream fileStream = File.OpenWrite(outputFile))
            {
                using (SKData data = cropped.Encode(SKEncodedImageFormat.Png, 100))
                {
                    data.SaveTo(fileStream);
                }
            }
        }
    }

    public void ResizeImageToHeight(string inputFile, string outputFile, int outputHeight)
    {
    
        // Load the image from the file
        using (SKBitmap bitmap = SKBitmap.Decode(inputFile))
        {
            // Calculate the output width based on the aspect ratio
            float aspectRatio = (float)bitmap.Width / bitmap.Height;
            int outputWidth = (int)(outputHeight * aspectRatio);

            // Create a new bitmap with the desired size
            using (SKBitmap resizedBitmap = new SKBitmap(outputWidth, outputHeight))
            {
                // Create a canvas to draw the scaled image on
                using (SKCanvas canvas = new SKCanvas(resizedBitmap))
                {
                    // Create a matrix to scale the image
                    SKMatrix matrix = SKMatrix.CreateScale((float)outputWidth / bitmap.Width, (float)outputHeight / bitmap.Height);

                    // Create a paint object with the matrix as an image filter
                    using (SKPaint paint = new SKPaint())
                    {
                        paint.ImageFilter = SKImageFilter.CreateMatrix(matrix, SKFilterQuality.High, null);

                        // Draw the scaled image on the canvas
                        canvas.DrawBitmap(bitmap, 0, 0, paint);
                    }
                }

                // Create an image from the resized bitmap
                using (SKImage image = SKImage.FromBitmap(resizedBitmap))
                {
                    // Save the resized image to a new file
                    using (FileStream fileStream = File.OpenWrite(outputFile))
                    {
                        using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
                        {
                            data.SaveTo(fileStream);
                        }
                    }
                }
            }
        }
    
    }

    public void ResizeImageFromUrl(string imageUrl, ImageFormat imageFormat, int intH, int intW, string output)
    {
        // Create an HttpClient to download the image
        using (HttpClient httpClient = new HttpClient())
        {
            // Download the image and save it to a memory stream
            using (HttpResponseMessage response = httpClient.GetAsync(imageUrl).Result)
            using (Stream imageStream = response.Content.ReadAsStreamAsync().Result)
            {
                // Load the image from the memory stream
                using (SKManagedStream skStream = new SKManagedStream(imageStream))
                using (SKBitmap bitmap = SKBitmap.Decode(skStream))
                {
                    // Calculate the dimensions of the cropped image
                    int croppedWidth = (int)(bitmap.Width * ((float)intW / 100));
                    int croppedHeight = (int)(bitmap.Height * ((float)intH / 100));
                    int x = (bitmap.Width - croppedWidth) / 2;
                    int y = (bitmap.Height - croppedHeight) / 2;

                    // Crop the image to the specified dimensions
                    using (SKBitmap croppedBitmap = new SKBitmap(croppedWidth, croppedHeight))
                    {
                        bool success = bitmap.ExtractSubset(croppedBitmap,new SKRectI(x, y, x + croppedWidth, y + croppedHeight));
//                        croppedBitmap.Copy(bitmap, new SKRectI(x, y, x + croppedWidth, y + croppedHeight), new SKPointI(0, 0));

                        using (SKFileWStream stream = new SKFileWStream(output))
                        {
                            croppedBitmap.Encode(stream, SKEncodedImageFormat.Png, 100);
                        }

                        // // Save the cropped image to a file
                        // using (SKData data = SKImage.FromBitmap(croppedBitmap).Encode(imageFormat == ImageFormat.Jpeg ? SKEncodedImageFormat.Jpeg : SKEncodedImageFormat.Png, 100))
                        // using (SKImage image = SKImage.FromData(data))
                        // using (FileStream fileStream = File.OpenWrite("cropped_image." + imageFormat.ToString().ToLower()))
                        // {
                        //     image.SaveTo(fileStream);
                        // }
                    }
                }
            }
        }
    }


//     public static void Test()
//     {
//         string imageUrl = "https://cards.scryfall.io/art_crop/front/2/3/2317dab2-e412-43c5-9f8c-2fb832176cf5.jpg?156261029";
//         ImageFormat imageFormat = ImageFormat.Jpeg;
//         int intH = 50;
//         int intW = 50;

//         var test = new imghandler();
//         test.ResizeImage(imageUrl, imageFormat, intH, intW,@"c:\SNJW\code\mq\output.tst1.png");
//     }
}