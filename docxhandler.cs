using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using Wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using Pic = DocumentFormat.OpenXml.Drawing.Pictures;
using A14 = DocumentFormat.OpenXml.Office2010.Drawing;
// using Ap = DocumentFormat.OpenXml.ExtendedProperties;
// using Thm15 = DocumentFormat.OpenXml.Office2013.Theme;
// using M = DocumentFormat.OpenXml.Math;
// using Ovml = DocumentFormat.OpenXml.Vml.Office;
// using V = DocumentFormat.OpenXml.Vml;
// using W14 = DocumentFormat.OpenXml.Office2010.Word;
// using W15 = DocumentFormat.OpenXml.Office2013.Word;

public class docxhandler
{

    public void SaveMultiPageImgDocument(string imgpath, string outputtemplate)
    {
        var mygen = new GeneratedClass();
        var allfiles = Directory.GetFiles(imgpath).ToList();
        int imagesperdocument = mygen.pagesperdocument * mygen.columnsperrow * mygen.rowsperpage;
        var ratio = ((decimal)allfiles.Count / (decimal)imagesperdocument);
        int numdocuments = (int)System.Math.Ceiling(ratio);
        if(outputtemplate.ToLower().EndsWith(".docx"))
            outputtemplate = outputtemplate.Substring(0,outputtemplate.Length-5);

        for(int i = 0; i < numdocuments; i++)
        {
            mygen.files.Clear();
            int start = (i * imagesperdocument);
            int end = System.Math.Min(imagesperdocument, allfiles.Count-start);
            for(int j = 0; j < end; j++)
                mygen.files.Add(allfiles[start + j]);
            mygen.CreatePackage(@$"{outputtemplate}{(i+1).ToString().PadLeft(2,'0')}.docx");
        }
    }
}


public class GeneratedClass
{

    public List<string> files = new List<string>();
    public int columnsperrow = 3;
    public int rowsperpage = 3;
    public int pagesperdocument = 4;
    
    // Creates a WordprocessingDocument.
    public void CreatePackage(string filePath)
    {
        using(WordprocessingDocument package = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
        {
            CreateParts(package);
        }
    }

    // Adds child parts and generates content of the specified part.
    private void CreateParts(WordprocessingDocument document)
    {
        // ExtendedFilePropertiesPart extendedFilePropertiesPart1 = document.AddNewPart<ExtendedFilePropertiesPart>("rId3");
        // GenerateExtendedFilePropertiesPart1Content(extendedFilePropertiesPart1);

        MainDocumentPart mainDocumentPart1 = document.AddMainDocumentPart();
        GenerateMainDocumentPart1Content(mainDocumentPart1);

        int rdcount = 0;
        foreach(string file in files)
        {
            rdcount++;
            ImagePart imagePart = mainDocumentPart1.AddNewPart<ImagePart>("image/png", $"rId{rdcount}");
            GenerateImagePartContentFromFile(imagePart,file);
        }

        // SetPackageProperties(document);
    }

    // Generates content of mainDocumentPart1.
    private void GenerateMainDocumentPart1Content(MainDocumentPart mainDocumentPart1)
    {
        Document document1 = new Document();
        Body body1 = new Body();

        // loop through this - a row is essentially one paragraph
        var paragraphs = new List<Paragraph>();
        int rdcount = 0;
        for (int h = 0; h < pagesperdocument; h++)
        {
            for (int i = 0; i < rowsperpage; i++)
            {
                Paragraph paragraph1 = new Paragraph();
                List<Run> runs = new List<Run>();

                for (int j = 0; j < columnsperrow; j++)
                {
                    rdcount++;

                    Run run1 = new Run();
                    RunProperties runProperties1 = new RunProperties();
                    NoProof noProof1 = new NoProof();
                    runProperties1.Append(noProof1);
                    Drawing drawing1 = new Drawing();
                    Wp.Inline inline1 = new Wp.Inline(){ DistanceFromTop = (UInt32Value)0U, DistanceFromBottom = (UInt32Value)0U, DistanceFromLeft = (UInt32Value)0U, DistanceFromRight = (UInt32Value)0U, AnchorId = "34194BBA", EditId = "540BB012" };
                    Wp.Extent extent1 = new Wp.Extent(){ Cx = 2322000L, Cy = 3216542L };
                    Wp.EffectExtent effectExtent1 = new Wp.EffectExtent(){ LeftEdge = 0L, TopEdge = 0L, RightEdge = 2540L, BottomEdge = 3175L };
                    Wp.DocProperties docProperties1 = new Wp.DocProperties(){ Id = (UInt32Value)1U, Name = $"Picture rId{rdcount}" };

                    A.Graphic graphic1 = new A.Graphic();
                    graphic1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

                    A.GraphicData graphicData1 = new A.GraphicData(){ Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" };

                    Pic.Picture picture1 = new Pic.Picture();
                    picture1.AddNamespaceDeclaration("pic", "http://schemas.openxmlformats.org/drawingml/2006/picture");

                    Pic.NonVisualPictureProperties nonVisualPictureProperties1 = new Pic.NonVisualPictureProperties();
                    Pic.NonVisualDrawingProperties nonVisualDrawingProperties1 = new Pic.NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = $"Picture rId{rdcount}" };

                    Pic.NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties1 = new Pic.NonVisualPictureDrawingProperties();
                    A.PictureLocks pictureLocks1 = new A.PictureLocks(){ NoChangeAspect = true, NoChangeArrowheads = true };

                    nonVisualPictureDrawingProperties1.Append(pictureLocks1);

                    nonVisualPictureProperties1.Append(nonVisualDrawingProperties1);
                    nonVisualPictureProperties1.Append(nonVisualPictureDrawingProperties1);

                    Pic.BlipFill blipFill1 = new Pic.BlipFill();

                    A.Blip blip1 = new A.Blip(){ Embed = $"rId{rdcount}" };

                    A.BlipExtensionList blipExtensionList1 = new A.BlipExtensionList();

                    A.BlipExtension blipExtension1 = new A.BlipExtension(){ Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}" };

                    A14.UseLocalDpi useLocalDpi1 = new A14.UseLocalDpi(){ Val = false };
                    useLocalDpi1.AddNamespaceDeclaration("a14", "http://schemas.microsoft.com/office/drawing/2010/main");

                    blipExtension1.Append(useLocalDpi1);

                    blipExtensionList1.Append(blipExtension1);

                    blip1.Append(blipExtensionList1);

                    A.Stretch stretch1 = new A.Stretch();
                    A.FillRectangle fillRectangle1 = new A.FillRectangle();

                    stretch1.Append(fillRectangle1);

                    blipFill1.Append(blip1);
                    blipFill1.Append(stretch1);

                    Pic.ShapeProperties shapeProperties1 = new Pic.ShapeProperties(){ BlackWhiteMode = A.BlackWhiteModeValues.Auto };

                    A.Transform2D transform2D1 = new A.Transform2D();
                    A.Offset offset1 = new A.Offset(){ X = 0L, Y = 0L };
                    A.Extents extents1 = new A.Extents(){ Cx = 2322000L, Cy = 3216542L };

                    transform2D1.Append(offset1);
                    transform2D1.Append(extents1);

                    A.PresetGeometry presetGeometry1 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
                    A.AdjustValueList adjustValueList1 = new A.AdjustValueList();

                    presetGeometry1.Append(adjustValueList1);
                    A.NoFill noFill1 = new A.NoFill();

                    A.Outline outline1 = new A.Outline();
                    A.NoFill noFill2 = new A.NoFill();

                    outline1.Append(noFill2);

                    shapeProperties1.Append(transform2D1);
                    shapeProperties1.Append(presetGeometry1);
                    shapeProperties1.Append(noFill1);
                    shapeProperties1.Append(outline1);

                    picture1.Append(nonVisualPictureProperties1);
                    picture1.Append(blipFill1);
                    picture1.Append(shapeProperties1);

                    graphicData1.Append(picture1);

                    graphic1.Append(graphicData1);

                    inline1.Append(extent1);
                    inline1.Append(effectExtent1);
                    inline1.Append(docProperties1);
                    //inline1.Append(nonVisualGraphicFrameDrawingProperties1);
                    inline1.Append(graphic1);

                    drawing1.Append(inline1);

                    // run1.Append(runProperties1);
                    run1.Append(drawing1);

                    // if this is the end of the row, don't add spaces
                    runs.Add(run1);
                    if(j != (columnsperrow - 1))
                    {

                        Run run2 = new Run();
                        Text text1 = new Text(){ Space = SpaceProcessingModeValues.Preserve };
                        text1.Text = " ";
                        run2.Append(text1);
                        runs.Add(run2);
                    }
                    else if(i == (rowsperpage-1) && !(h == (pagesperdocument-1)))
                    {
                        Run run9 = new Run();
                        Break break1 = new Break(){ Type = BreakValues.Page };
                        run9.Append(break1);
                        runs.Add(run9);
                    }
                }

                foreach(var run in runs)
                    paragraph1.Append(run);
                paragraphs.Add(paragraph1);
            }
        }

        SectionProperties sectionProperties1 = new SectionProperties(){ RsidR = "00136A7F", RsidSect = "00C42993" };
        PageSize pageSize1 = new PageSize(){ Width = (UInt32Value)11906U, Height = (UInt32Value)16838U };
        PageMargin pageMargin1 = new PageMargin(){ Top = 378, Right = (UInt32Value)378U, Bottom = 378, Left = (UInt32Value)378U, Header = (UInt32Value)378U, Footer = (UInt32Value)708U, Gutter = (UInt32Value)0U };
        Columns columns1 = new Columns(){ Space = "708" };
        DocGrid docGrid1 = new DocGrid(){ LinePitch = 360 };

        sectionProperties1.Append(pageSize1);
        sectionProperties1.Append(pageMargin1);
        sectionProperties1.Append(columns1);
        sectionProperties1.Append(docGrid1);


        foreach(var paragraph in paragraphs)
            body1.Append(paragraph);

        body1.Append(sectionProperties1);

        document1.Append(body1);

        mainDocumentPart1.Document = document1;
    }

    // Generates content of imagePart1.
    // private void GenerateImagePart1Content(ImagePart imagePart1)
    // {
    //     System.IO.Stream data = GetBinaryDataStream(imagePart1Data);
    //     imagePart1.FeedData(data);
    //     data.Close();
    // }

    // Generates content of imagePart2.
    // private void GenerateImagePart2Content(ImagePart imagePart2)
    // {
    //     System.IO.Stream data = GetBinaryDataStream(imagePart2Data);
    //     imagePart2.FeedData(data);
    //     data.Close();
    // }
    private void GenerateImagePartContentFromFile(ImagePart imagePart, string filepath)
    {
        System.IO.Stream data = GetBinaryDataStreamFromFile(filepath);
        imagePart.FeedData(data);
        data.Close();
    }

    #region Binary Data
    // private string imagePart1Data = "iVBORw0KGgoAAAANSUhEUgAAAPQAAAFSCAYAAADFHfK5AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAPvSURBVHhe7dMxEcAwEMCwb3B0LH9mxZAsQeGTFiPw87/fHiBh3QIBhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0ZMwfHwwTQCJ5MngAAAABJRU5ErkJggg==";

    // private string imagePart2Data = "iVBORw0KGgoAAAANSUhEUgAAAPQAAAFSCAYAAADFHfK5AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAPuSURBVHhe7dMxEcAwEMCwb4h1LfhySpag8EmLEfh5v38PkLBugQBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhhBDQ4ihIcTQEGJoCDE0hBgaQgwNIYaGEENDiKEhxNAQYmgIMTSEGBpCDA0hhoYQQ0OIoSHE0BBiaAgxNIQYGkIMDSGGhoyZA0tNBPaHe32uAAAAAElFTkSuQmCC";

    // private System.IO.Stream GetBinaryDataStream(string base64String)
    // {
    //     return new System.IO.MemoryStream(System.Convert.FromBase64String(base64String));
    // }

    private System.IO.Stream GetBinaryDataStreamFromFile(string filepath)
    {
        return new System.IO.MemoryStream(File.ReadAllBytes(filepath));
    }
    #endregion

}
