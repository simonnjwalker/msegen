// using MtgApiManager.Lib.Core;
// using MtgApiManager.Lib.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MtgApiManager.Lib.Model;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using OpenXmlDrawing = DocumentFormat.OpenXml.Drawing;
using OpenXmlWordprocessing = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using OpenXmlPictures = DocumentFormat.OpenXml.Drawing.Pictures;
using System.Drawing;
using mq;

#pragma warning disable SYSLIB0014

// public class mycard
// {
//     public string cardname = "";
//     public int multiverseid = 0;
//     public string filename = "";

//     public ScryfallCard sfcard = new ScryfallCard();

//     public string sfjson = "";
// }

public class msehandler
{
    public string msesetdest = @"C:\SNJW\code\mqwip\output\test.mse-set";
    public string docxdest = @"C:\SNJW\code\mqwip\output\test.docx";
    // public string temp = @"C:\SNJW\tools\mqwip\output\temp";
    public string gallery = @"C:\SNJW\code\mqwip\source\art";
    public string imagefile = @"C:\SNJW\code\mqwip\output\gallery\<<multiverseid>>_<<width>>x<<height>>_<<cardname>>.png";
    public string insetfile = @"C:\SNJW\code\mqwip\source\inset.png";
    public string symbolfile = @"C:\SNJW\code\mqwip\source\mse-symbol\townsville-angel.mse-symbol";
    public string xlsxsource = @"C:\SNJW\code\mqwip\source\source.xlsx";
    public string xlsxdest = @"C:\SNJW\code\mqwip\source\dest.xlsx";

    public bool fetchmultiverseid = true;
    public bool fetchsfjson = true;
    public bool convertsfjson = true;
    public bool setfilename = true;
    public string dfcmode = "inset"; // inset = use the inset, front = front side only, back = back side only, checklist = front with generic text
    public bool rulestextstripreminder = true;
    public bool rulestextfillsymbols = true;
    public int height = 1672;
    public int width = 1288;
    public string copyright = "Townsville Community Cube";
    List<string> supertypes = new List<string>(){"Legendary","Snow","Basic"};
    List<string> types = new List<string>(){"Tribal","Kindred","Artifact","Enchantment","Instant","Sorcery","Land","Planeswalker", "Creature","Battle"};
    List<string> spelltypes = new List<string>(){"Arcane","Adventure","Trap","Lesson","Chorus"};
    List<string> enchantmenttypes = new List<string>(){"Aura","Saga","Cartouche","Curse","Rune","Shard","Shrine","Role","Class","Case","Room"};
    List<string> artifacttypes = new List<string>(){"Attraction", "Blood", "Bobblehead", "Clue", "Contraption", "Equipment", "Food", "Fortification", "Gold", "Incubator", "Junk", "Map", "Powerstone", "Treasure", "Vehicle"};
    List<string> landtypes = new List<string>(){"Plains","Island","Swamp","Mountain","Forest","Desert","Gate","Lair","Locus","Urza's Mine","Urza's Power-Plant","Urza's Tower","Sphere"};
    List<string> battletypes = new List<string>(){"Siege"};

    // public List<mycard> LoadXlsx()
    // {
    //     // populate from XLSX file
    //     List<mycard> output = new List<mycard>();

    //     omni.omnifile loader = new omni.omnifile();

    //     this.Message($"Trying to load from {this.xlsxsource}...");
    //     System.Data.DataTable dt = new System.Data.DataTable();
    //     bool success = true;
    //     try
    //     {
    //         dt = loader.GetDataSetFromFile(this.xlsxsource).Tables["cards"];
    //     }
    //     catch
    //     {
    //         success = false;
    //     }
    //     if (success == false || dt == null || dt.Columns.Count == 0)
    //     {
    //         this.Message($"Could not load from {this.xlsxsource}.");
    //         return output;
    //     }
    //     foreach(string requiredfield in new List<string>(){"filename","multiverseid","cardname","sfjson"})
    //     {
    //         if(!dt.Columns.Contains(requiredfield))
    //         {
    //             this.Message($"File {this.xlsxsource} does not contain field '{requiredfield}'");
    //             return output;
    //         }
    //     }

    //     int rowcount = 0;
    //     foreach (System.Data.DataRow thisrow in dt.Rows)
    //     {
    //         mycard newitem = new mycard();
    //         newitem.filename = (thisrow["filename"] ?? "").ToString().Trim();
    //         try{newitem.multiverseid = Int32.Parse('0' + (thisrow["multiverseid"] ?? "").ToString().Trim());} catch{newitem.multiverseid=0;}
    //         newitem.cardname = (thisrow["cardname"] ?? "").ToString().Trim();
    //         newitem.sfjson = (thisrow["sfjson"] ?? "").ToString().Trim();
    //         if(newitem.cardname == "")
    //             continue;

    //         rowcount++;
    //         this.Message($"Loading card {rowcount.ToString().PadLeft(6,'0')}: '{newitem.cardname}'");

    //         if(fetchmultiverseid && newitem.cardname != "" )
    //         {
    //             newitem.multiverseid = this.FetchMultiverseid(newitem.cardname);
    //         }
    //         if(fetchsfjson && newitem.multiverseid > 0 && newitem.sfjson == "")
    //         {
    //             newitem.sfjson = this.FetchSfjson(newitem.multiverseid);
    //         }
    //         if(convertsfjson && newitem.sfjson != "")
    //         {
    //             newitem.sfcard = this.ConvertSfjson(newitem.sfjson);
    //         }
    //         if(setfilename && newitem.filename == "")
    //         {
    //             newitem.filename = this.imagefile
    //                 .Replace("<<cardname>>",newitem.cardname.Replace("/","-").Replace(" ","_").Replace(".","-").Replace("?","-").Replace("!","-"))
    //                 .Replace("<<multiverseid>>",newitem.multiverseid.ToString())
    //                 .Replace("<<multiverseid>>",newitem.multiverseid.ToString())
    //                 .Replace("<<height>>",this.height.ToString())
    //                 .Replace("<<width>>",this.width.ToString());
    //         }
            
    //         output.Add(newitem);
    //     }

    //     return output;
    // }

    public string GetImgFileName(CardInfo card)
    {
        return this.imagefile
            .Replace("<<cardname>>",card.cleanname.Replace("/","-").Replace(" ","_").Replace(".","-").Replace("?","-").Replace("!","-"))
            .Replace("<<multiverseid>>",card.multiverseid.ToString())
            .Replace("<<height>>",this.height.ToString())
            .Replace("<<width>>",this.width.ToString());    
    }
    public Card ConvertSfjson(string sfjson)
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<Card>(sfjson);
    }

    public int FetchMultiverseid(string cardname)
    {

// https://api.scryfall.com/cards/named?fuzzy=aust+com

        int lowestmultiverseid = 0;
        string url = $"https://api.scryfall.com/cards/named?fuzzy=" + cardname.ToLower().Replace(" ","+");
        string sfjson = "";
        try
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            System.Net.WebResponse response = request.GetResponse();
            using (var sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                sfjson = sr.ReadToEnd();
                sr.Close();
            }            
            response.Close();
            GC.Collect();
        }
        catch
        {
            this.Message($"Could not load from {url}");
        }


        if(sfjson != "")
        {
            try
            {

                Card sfcard = this.ConvertSfjson(sfjson);
                foreach(var multiverseid in sfcard.multiverse_ids)
                {
                    if(lowestmultiverseid == 0 || (lowestmultiverseid > multiverseid))
                        lowestmultiverseid = multiverseid;
                }
            }
            catch
            {
                this.Message($"Could not find a multiverseid for {cardname}");
            }
        }
        else
        {
            this.Message($"Could not find data for {cardname}");
        }
        return lowestmultiverseid;
    }

    public string FetchSfjson(int multiverseid)
    {

        string url = $"https://api.scryfall.com/cards/multiverse/{multiverseid}";
        string sfjson = "";
        this.Message($"Attempting to load from {url}");
        try
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            System.Net.WebResponse response = request.GetResponse();
            using (var sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                sfjson = sr.ReadToEnd();
                sr.Close();
            }            
            response.Close();
            GC.Collect();
        }
        catch
        {
            this.Message($"Could not load from {url}");
        }
        return this.JsonPrettify(sfjson);
    }        

    public string JsonPrettify(string json)
    {
        using (var stringReader = new System.IO.StringReader(json))
        using (var stringWriter = new System.IO.StringWriter())
        {
            var jsonReader = new Newtonsoft.Json.JsonTextReader(stringReader);
            var jsonWriter = new Newtonsoft.Json.JsonTextWriter(stringWriter) { Formatting = Newtonsoft.Json.Formatting.Indented };
            jsonWriter.WriteToken(jsonReader);
            return stringWriter.ToString();
        }
    }

    public bool SaveMse(List<CardInfo> source, string outputfile = "")
    {
        bool success = true;

        this.Message("Beginning creation of MSE set ");
        string tempdir = Path.GetTempPath().TrimEnd('\\') + '\\' + System.Guid.NewGuid().ToString().ToLower().Replace("-","");
        System.IO.Directory.CreateDirectory(tempdir);

        StringBuilder sb = new StringBuilder();

        // 2018-03-19 SNJW load the defaults from text fileS
        // mtgdefaults template = new mtgdefaults();

        // 2021-01-25 SNJW update with fwipho_hires_future_m15
        // fwipho_hires_future_m15 template = new fwipho_hires_future_m15();
        fwipho_future_m15_tall template = new fwipho_future_m15_tall();
        //template.planeswalker = template.planeswalker.Replace(@"|", new string(new char[] { '\t' }));
        //template.land = template.land.Replace(@"|", new string(new char[] { '\t' }));
        template.card = template.card.Replace(@"|", new string(new char[] { '\t' }));
        template.bottom = template.bottom.Replace(@"|", new string(new char[] { '\t' }));
        template.top = template.top.Replace(@"|", new string(new char[] { '\t' }));

        sb.AppendLine(template.top);
        int imagenum = 0;
        int image2num = 1;
        foreach (var thiscard in source)
        {
            if (thiscard.multiverseid > 0 && thiscard?.card != null)
            {
                imagenum++;
                string image_file = "image" + imagenum.ToString();
                string image2_file = "image 2" + image2num.ToString();
                bool usefullfilename = false;  // does not work otherwise

                if(usefullfilename)
                {
                    image_file = System.IO.Path.GetFileName(thiscard.imagepath);
                }

                // copy the
                string image_file_fullpath = tempdir.TrimEnd(new char[] { '\\' }) + '\\' + image_file;
                if(thiscard.imagepath != "" )
                {
                    if(System.IO.File.Exists(image_file_fullpath))
                    {
                        System.IO.File.Delete(image_file_fullpath);
                        System.GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                    System.IO.File.Copy(thiscard.imagepath, tempdir.TrimEnd(new char[] { '\\' }) + '\\' + image_file);
                }

                Dictionary<string, string> fieldvalues = this.GetTemplateFieldDictionary(thiscard);
                string cardtemplate = template.card;
                fieldvalues["note_text"] = this.GetRulesText("multiverseid=" + thiscard.multiverseid + ", imagefile=" + image_file + ", filename=" + System.IO.Path.GetFileName(thiscard.imagepath).ToString());
                fieldvalues["image"] = image_file;
                if(fieldvalues["image_2"] != "")
                {
                    fieldvalues["image_2"] = image2_file;
                    System.IO.File.Copy(this.insetfile, tempdir.TrimEnd(new char[] { '\\' }) + '\\' + image2_file);
                    image2num++;
                }

                foreach(var field in fieldvalues)
                    cardtemplate = cardtemplate.Replace("<<"+field.Key+">>",field.Value);
                


                this.Message("Adding '" + thiscard.cleanname + "' to the output");
                sb.AppendLine(cardtemplate);

                // if this does not exist, use an HTTP request to save a raw file
                //string sourcefile

                // get files that are not present and put them into the 
                //  this.SaveWebImage()

                //   System.IO.File.Copy(images[thiscard.multiverseid], tempdir.TrimEnd(new char[] { '\\' }) + '\\' + "image" + imagenum.ToString());
            }
        }
        
        if(outputfile == "")
            outputfile = this.msesetdest;

        sb.AppendLine(template.bottom);

        this.Message("Trying to create " + outputfile);
        string set_file_fullpath = tempdir.TrimEnd(new char[] { '\\' }) + '\\' + "set";
        if (System.IO.File.Exists(set_file_fullpath))
        {
            System.IO.File.Delete(set_file_fullpath);
            System.GC.Collect();
        }
        System.IO.File.WriteAllText(set_file_fullpath, sb.ToString());

        string setsymbol_file_fullpath = tempdir.TrimEnd(new char[] { '\\' }) + '\\' + "symbol1.mse-symbol";
        if (System.IO.File.Exists(setsymbol_file_fullpath))
        {
            System.IO.File.Delete(setsymbol_file_fullpath);
            System.GC.Collect();
        }
        System.IO.File.Copy(symbolfile, setsymbol_file_fullpath);
        System.GC.Collect();

        if (System.IO.File.Exists(outputfile))
        {
            System.IO.File.Delete(outputfile);
            System.GC.Collect();
        }
        System.IO.Compression.ZipFile.CreateFromDirectory(tempdir, outputfile);
        GC.Collect();
        foreach(var file in System.IO.Directory.GetFiles(tempdir))
            System.IO.File.Delete(file);
        GC.Collect();
        System.IO.Directory.Delete(tempdir);
        GC.Collect();
        this.Message("Created MSE file '" + outputfile + "'");



        return success;
    }

    public Dictionary<string, string> GetTemplateFieldDictionary(CardInfo data)
    {
        Dictionary<string, string> output = this.GetTemplateFieldDictionary();
        var card = data.card;

        output["alternative_frame"] = "default";
        output["time_created"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        output["time_modified"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        output["casting_cost"] = this.GetCastingCost(card.mana_cost ?? "");
        output["image"] = "";

        output["sub_type"] = this.GetTypeLineText(card.type_line ?? "", "sub_type");
        output["super_type"] = this.GetTypeLineText(card.type_line ?? "", "super_type");
        output["rule_text"] = this.GetRulesText(card.oracle_text ?? "");
        output["flavor_text"] = "";

// |super type: <<super_type>> |sub type: <<sub_type>>

        string cardname = card.name ?? "";
        string oracletext = card.oracle_text ?? "";
        string power = card.power ?? "";
        string toughness = card.toughness ?? "";
        string rarity = card.rarity ?? "common";
        if(rarity.Contains("mythic"))
            rarity = "mythic rare";

        string layout = card.layout ?? "normal";
        if(layout == "normal" && output["super_type"].Contains("Planeswalker"))
            layout = "planeswalker";
        if(layout == "normal" && output["sub_type"].Contains("Saga"))
            layout = "saga";

        // 	card color: blue, red, multicolor, horizontal
        string wubrg = "";
        if(card.colors != null)
            wubrg = String.Join("",card.colors);
        string identity = "";
        if(card.color_identity != null)
            identity = String.Join("",card.color_identity);
        
        // fetchland colours
        if((card.type_line ?? "").Contains("Land") && identity == "")
        {
            identity += oracletext.Contains("Plains") ? "W" : "";
            identity += oracletext.Contains("Island") ? "U" : "";
            identity += oracletext.Contains("Swamp") ? "B" : "";
            identity += oracletext.Contains("Mountain") ? "R" : "";
            identity += oracletext.Contains("Forest") ? "G" : "";
        }
        string framecolour = (card.type_line ?? "").Contains("Land") ? identity : wubrg;
        string cardcolour = this.GetColourType((card.type_line ?? ""), framecolour );


        // default_typenamefontcolour
        string default_typenamefontcolour = "auto";
        if(cardcolour.Contains("multicolor") )
        {
            if(cardcolour.Contains("white"))
            {
                if(!cardcolour.Contains("black"))
                {
                    cardcolour += ", reversed";
                }
            }
            else
            {
                default_typenamefontcolour = "white";
            }
        }
        cardcolour = cardcolour.Trim(new char[]{' ',','});

        int draftvalue = 1;
        output["power"] = power;
        output["toughness"] = toughness;
        output["name"] = cardname;
        output["card_colour"] = cardcolour;

        for(int level = 1; level <= 4; level++)
        {
            if(layout == "saga")
            {
                output["alternative_frame"] = "saga";                    
                output[$"saga_level_{level}_a"] = this.GetSagaText(oracletext,$"saga_level_{level}_a");
                output[$"saga_level_{level}_b"] = this.GetSagaText(oracletext,$"saga_level_{level}_b");
                output[$"level_{level}_text"] = this.GetRulesText(this.GetSagaText(oracletext,$"level_{level}_text"));
            }
            else if(layout == "leveler")
            {
                output["alternative_frame"] = "leveler";                    
                output[$"leveler_level_{level}"] = this.GetLevelerText(oracletext,power,toughness,$"leveler_level_{level}");
                output[$"leveler_power_{level}"] = this.GetLevelerText(oracletext,power,toughness,$"leveler_power_{level}");
                output[$"leveler_toughness_{level}"] = this.GetLevelerText(oracletext,power,toughness,$"leveler_toughness_{level}");
                output[$"level_{level}_text"] = this.GetRulesText(this.GetLevelerText(oracletext,power,toughness,$"level_{level}_text"));
            }
            else if(layout == "planeswalker")
            {
                output["alternative_frame"] = "planeswalker";                  
                output[$"loyalty_cost_{level}"] = this.GetPlaneswalkerText(oracletext,$"loyalty_cost_{level}");
                output[$"level_{level}_text"] = this.GetRulesText(this.GetPlaneswalkerText(oracletext,$"level_{level}_text"));
                output[$"loyalty"] = card.loyalty;
            }
        }

        bool isdual = layout == "split" || layout == "adventure" || layout == "flip";
        bool isdfc = layout == "transform" || layout == "meld"  || layout == "modal_dfc";
        

        if(( isdfc || isdual ) && card.card_faces != null)  
        {
            output["alternative_frame"] = "inset";
            output["image_2"] = "transparent";
            if(card.card_faces.Count>1)
            {
                int frontface = 0;
                int backface = 1;

                if(this.dfcmode == "inset")
                {
                    output["alternative_frame"] = "inset";
                }
                else if(this.dfcmode == "front" && isdfc)
                {
                    output["alternative_frame"] = "default";
                    backface = -1;
                }
                else if(this.dfcmode == "back" && isdfc)
                {
                    output["alternative_frame"] = "default";
                    backface = -1;
                    frontface = 1;
                }
                else if(this.dfcmode == "checklist" && isdfc)
                {
                    output["alternative_frame"] = "default";
                }

                if(frontface >= 0)
                {
                    output["name"] = card.card_faces[frontface].name ?? "";
                    output["casting_cost"] = this.GetCastingCost(card.card_faces[frontface].mana_cost ?? "");
                    output["rule_text"] = this.GetRulesText(card.card_faces[frontface].oracle_text ?? "");
                    if(this.dfcmode == "checklist" && isdfc)
                        output["rule_text"] = $"Replace this card with the double-sided card {cardname} in all public zones.";
                    output["super_type"] = this.GetTypeLineText(card.card_faces[frontface].type_line ?? "" ?? "", "super_type");
                    output["sub_type"] = this.GetTypeLineText(card.card_faces[frontface].type_line ?? "" ?? "", "sub_type");
                    output["power"] = card.card_faces[frontface].power ?? "";
                    output["toughness"] = card.card_faces[frontface].toughness ?? "";
                    string frontfacecolour = "";
                    if(card.card_faces[frontface].colors != null)
                    {
                        frontfacecolour = string.Join("",card.card_faces[frontface].colors);
                    }
                    else
                    {
                        frontfacecolour = (card.card_faces[frontface].mana_cost ?? "")
                            .Replace("0","")
                            .Replace("1","")
                            .Replace("2","")
                            .Replace("3","")
                            .Replace("4","")
                            .Replace("5","")
                            .Replace("6","")
                            .Replace("7","")
                            .Replace("8","")
                            .Replace("9","")
                            .Replace("{","")
                            .Replace("}","")
                            .Replace("X","");
                    }

                    output["card_colour"] = this.GetColourType(card.card_faces[frontface].type_line ?? "", frontfacecolour);
                }

                if(backface >= 0)
                {
                    output["name_2"] = card.card_faces[backface].name ?? "";
                    output["casting_cost_2"] = this.GetCastingCost(card.card_faces[backface].mana_cost ?? "");
                    output["rule_text_2"] = this.GetRulesText(card.card_faces[backface].oracle_text ?? "");
                    output["super_type_2"] = this.GetTypeLineText(card.card_faces[backface].type_line ?? "" ?? "", "super_type");
                    output["sub_type_2"] = this.GetTypeLineText(card.card_faces[backface].type_line ?? "" ?? "", "sub_type");
                    output["power_2"] = card.card_faces[backface].power ?? "";
                    output["toughness_2"] = card.card_faces[backface].toughness ?? "";
                    string backfacecolour = "";
                    if(card.card_faces[backface].colors != null)
                    {
                        backfacecolour = string.Join("",card.card_faces[backface].colors);
                    }
                    else
                    {
                        backfacecolour = (card.card_faces[backface].mana_cost ?? "")
                            .Replace("0","")
                            .Replace("1","")
                            .Replace("2","")
                            .Replace("3","")
                            .Replace("4","")
                            .Replace("5","")
                            .Replace("6","")
                            .Replace("7","")
                            .Replace("8","")
                            .Replace("9","")
                            .Replace("{","")
                            .Replace("}","")
                            .Replace("X","");
                    }
                    output["card_colour_2"] = this.GetColourType(card.card_faces[backface].type_line ?? "", backfacecolour);
                }
            }
        }

// 	card color: blue, red, multicolor, horizontal

// blue, red, multicolor, horizontal


        output["card_code_text"] = "";
        output["illustrator"] = card.artist;
        output["copyright"] = this.copyright;
        output["rarity"] = rarity;
        output["default_typenamefontcolour"] = default_typenamefontcolour;

        if(output["super_type"].Contains("Creature"))
        {
            if(rarity == "uncommon")
                draftvalue = 1;
            if(rarity == "rare")
                draftvalue = 2;
            if(rarity.Contains("mythic"))
                draftvalue = 5;
        }
        else
        {
            if(rarity == "uncommon")
                draftvalue = 2;
            if(rarity == "rare")
                draftvalue = 5;
            if(rarity.Contains("mythic"))
                draftvalue = 10;
        }

        if(card.reserved == true)
        {
            if(card.legalities.vintage == "restricted" && ( output["draft_value"] == "5" || output["draft_value"] == "10" ) )
            {
                draftvalue = 50;
//"legalities":{"standard":"not_legal","future":"not_legal","historic":"not_legal","gladiator":"not_legal","pioneer":"not_legal","modern":"not_legal","legacy":"banned","pauper":"not_legal","vintage":"restricted"                
            }
            else if(output["super_type"].Contains("Land") || card.legalities.commander == "banned" )
            {
                draftvalue = 20;
            }
            else
            {
                draftvalue = 10;
            }
        }
        else if(output["super_type"].Contains("Planeswalker") && ( output["sub_type"].Contains("Oko") || output["name"].Contains("Mind Sculpter") || output["sub_type"].Contains("Dack")  ))
        {
                draftvalue = 20;
        }
        output["draft_value"] = draftvalue.ToString();
        output["note_text"] = "";

        // if the template supports it, put in values for chop top/bottom/left/right
        output["chop_top"] = "1";
        output["chop_left"] = "1";
        output["chop_right"] = "1";
        output["chop_bottom"] = "1";

        int wordcount = oracletext.Length - oracletext.Replace(" ","").Length + 1;
        int charcount = oracletext.Replace("{","").Replace("}","").Length;
        int linecount = oracletext.Length - oracletext.Replace(new string(new char[] { '\n' }),"").Length + 1;

        if(layout != "normal")
        {
            // leave these alone
        }
        else if(linecount == 1 && wordcount < 8 && charcount < 30)
        {
            // Sol Ring, Signets, French Vanilla creatures
            output["chop_top"] = "300";
            output["chop_left"] = "130";
        }
        else if(linecount == 1 && wordcount < 12 && charcount < 60)
        {
            // gilded Lotus
            output["chop_top"] = "300";
            output["chop_left"] = "110";
        }
        else if(linecount == 1 && wordcount < 30 && charcount < 200)
        {
            // Fetch lands
            output["chop_top"] = "170";
            output["chop_left"] = "40";
            output["chop_right"] = "70";
        }
        else if(linecount == 2 && wordcount < 30 && charcount < 200)
        {
            // Fetch lands
            output["chop_top"] = "110";
            output["chop_left"] = "40";
        }
        else if(linecount == 3 && wordcount < 30 && charcount < 200)
        {
            // Basalt Monolith
            output["chop_top"] = "40";
            output["chop_left"] = "40";
        }


// 25 words 149 characters
// {T}, Pay 1 life, Sacrifice Verdant Catacombs: Search your library for a Swamp or Forest card, put it onto the battlefield, then shuffle your library.
// 
// chop top: 170
// chop bottom: 1
// chop left: 40
// chop right: 70

// 3 words 16 characters
// {T}: Add {C}{C}.
// chop top: 300
// chop bottom: 1
// chop left: 130

// 24 words 117 characters
// At the beginning of your upkeep, flip a coin. If you lose the flip, Mana Crypt deals 3 damage to you.
// {T}: Add {C}{C}.
// chop top: 110
// chop bottom: 1
// chop left: 40

// 44 words 250 characters
// Multikicker {2} (You may pay an additional {2} any number of times as you cast this spell.)
// Everflowing Chalice enters the battlefield with a charge counter on it for each time it was kicked.
// {T}: Add {C} for each charge counter on Everflowing Chalice.
// no chop

// 8 words 35 characters
// T: Add three mana of any one color.
// chop top: 300
// chop bottom: 1
// chop left: 110


        


        return output;
    }


    public string GetColourType(string typeline, string framecolour)
    {
        string cardcolour = "";
        cardcolour += framecolour.Contains('W') ? "white, " : "";
        cardcolour += framecolour.Contains('U') ? "blue, " : "";
        cardcolour += framecolour.Contains('B') ? "black, " : "";
        cardcolour += framecolour.Contains('R') ? "red, " : "";
        cardcolour += framecolour.Contains('G') ? "green, " : "";
        cardcolour = cardcolour.TrimEnd(new char[]{' ',','});
        if(cardcolour.Contains(","))
            cardcolour += ", multicolor, horizontal";
        if((typeline ?? "").Contains("Land"))
            cardcolour += ", land";
        if((typeline ?? "").Contains("Artifact"))
            cardcolour += ", artifact";

        return cardcolour;
    }

    public string GetTypeLineText(string type_line, string typelinetype)
    {
        string output = "";
        string super_type = "";
        string sub_type = "";
        List<string> supertypes = this.GetSuperTypes(type_line ?? "");
        List<string> types = this.GetTypes(type_line ?? "");
        List<string> subtypes = this.GetSubTypes(type_line ?? "");
        foreach(var supertype in supertypes)
            super_type += "<word-list-type>"+supertype+"</word-list-type> ";
        foreach(var type in types)
            super_type += "<word-list-type>"+type+"</word-list-type> ";
        super_type = super_type.Trim();

        int typecount = 0;
        foreach(var subtype in subtypes)
        {
            if(artifacttypes.Contains(subtype))
            {
                sub_type += "<word-list-artifact>"+subtype+"</word-list-artifact> ";
            }
            else if(landtypes.Contains(subtype))
            {
                sub_type += "<word-list-land>"+subtype+"</word-list-land> ";
            }
            else if(spelltypes.Contains(subtype))
            {
                sub_type += "<word-list-spell>"+subtype+"</word-list-spell> ";
            }
            else if(enchantmenttypes.Contains(subtype))
            {
                sub_type += "<word-list-enchantment>"+subtype+"</word-list-enchantment> ";
            }
            else if(battletypes.Contains(subtype))
            {
                sub_type += "<word-list-battle>"+subtype+"</word-list-battle> ";
            }
            else if(types.Contains("Planeswalker"))
            {
                sub_type += "<word-list-planeswalker>"+subtype+"</word-list-planeswalker> ";
            }
            else
            {
                if(typecount == (subtypes.Count-1) && subtypes.Count>1 )
                {
                    sub_type += "<word-list-class>"+subtype+"</word-list-class> ";
                }
                else
                {
                    sub_type += "<word-list-race>"+subtype+"</word-list-race> ";
                }
            }
            typecount++;
        }
        sub_type = sub_type.Trim();

        if(typelinetype =="super_type")
            output = super_type;
        if(typelinetype =="sub_type")
            output = sub_type;
        return output;
    }

    public string GetRulesText(string oracletext)
    {
        string output = "";
        string lf = 
@"
";
        
        if(oracletext.Contains(new string(new char[] { '\n' })))
        {
            output += lf;
            foreach(string line in oracletext.Split(new string(new char[] { '\n' }),StringSplitOptions.RemoveEmptyEntries))
            {
                output += new string(new char[] { '\t', '\t' }) + "<nospellcheck>" + line + "</nospellcheck>" + lf;
            }
            output.TrimEnd(lf.ToCharArray());
        }
        else
        {
            output = oracletext;
        }

        output = this.FixRulesText(output);
        output = this.FixManaSymbols(output);

// (Damage and effects that say “destroy” don’t destroy it.)

        return output;
    }

    public string FixRulesText(string oracletext)
    {
        if(this.rulestextstripreminder == false)
            return oracletext;
        string[] brackets = oracletext.Split("()".ToCharArray(),StringSplitOptions.None);
        for(int i = 0; i<brackets.Length;i++)
        {
            if(i % 2 == 1)
                brackets[i] = ""; 
        }
        return string.Join("", brackets);
    }

    public string GetCastingCost(string oracletext)
    {
        oracletext = oracletext
            .Replace("{W/P}","{H/W}")
            .Replace("{U/P}","{H/U}")
            .Replace("{B/P}","{H/B}")
            .Replace("{R/P}","{H/R}")
            .Replace("{G/P}","{H/G}");
        return oracletext;
    }

    public string FixManaSymbols(string oracletext)
    {
        if(this.rulestextfillsymbols == false)
            return oracletext;
        oracletext = oracletext
            .Replace("{W/P}","<sym>H/W</sym>")
            .Replace("{U/P}","<sym>H/U</sym>")
            .Replace("{B/P}","<sym>H/B</sym>")
            .Replace("{R/P}","<sym>H/R</sym>")
            .Replace("{G/P}","<sym>H/G</sym>");

        return oracletext.Replace("{","<sym>").Replace("}","</sym>");
    }

    public List<string> GetSuperTypes(string typeline)
    {
        List<string> output = new List<string>();
        foreach(var supertype in this.supertypes)
        {
            if(typeline.Contains(supertype))
                output.Add(supertype);
        }
        return output;
    }

    public List<string> GetTypes(string typeline)
    {
        List<string> output = new List<string>();
        foreach(var type in this.types)
        {
            if(typeline.Contains(type))
                output.Add(type);
        }
        return output;
    }

    public List<string> GetSubTypes(string typeline)
    {
        List<string> output = new List<string>();
        string[] parts = typeline.Split(" — ",StringSplitOptions.None);
        if(parts.Length>1)
        {
            foreach(var subtype in  parts[1].Split(" ",StringSplitOptions.None))
                output.Add(subtype);
        }
        return output;
    }

    public void MoveLevelTextDown(Dictionary<string, string> allfields, string fieldtype = "saga")
    {
        // the default
        // leveler_level_1
        // loyalty_cost_1
        // saga_level_1_a
        int shiftdown = 0;
        for(int level = 4; level > 0; level--)
        {
            if(fieldtype == "saga" )
            {
                    if(allfields[$"saga_level_{level}_a"] != "" || allfields[$"level_{level}_text"] != "")
                    break;
                shiftdown++;
            }
            else  if(fieldtype == "leveler" )
            {
                    if(allfields[$"leveler_level_{level}"] != "")
                    break;
                shiftdown++;
            }
            else if(fieldtype == "planeswalker" )
            {
                    if(allfields[$"loyalty_cost_{level}"] != "" || allfields[$"level_{level}_text"] != "" )
                    break;
                shiftdown++;
            }
        }

        Dictionary<string, string> tempfields = this.GetTemplateFieldDictionary();;

        for(int level = 1; level <= 4; level++)
        {
            if(fieldtype == "saga" )
            {
                tempfields[$"saga_level_{level+shiftdown}_a"] = allfields[$"saga_level_{level}_a"];
                tempfields[$"saga_level_{level+shiftdown}_b"] = allfields[$"saga_level_{level}_b"];
            }
            else  if(fieldtype == "leveler" )
            {
                tempfields[$"leveler_level_{level+shiftdown}"] = allfields[$"leveler_level_{level}"];
                tempfields[$"leveler_power_{level+shiftdown}"] = allfields[$"leveler_power_{level}"];
                tempfields[$"leveler_toughness_{level+shiftdown}"] = allfields[$"leveler_toughness_{level}"];
            }
            else if(fieldtype == "planeswalker" )
            {
                tempfields[$"loyalty_cost_{level+shiftdown}"] = allfields[$"loyalty_cost_{level}"];
            }
            tempfields[$"level_{level+shiftdown}_text"] = allfields[$"level_{level}_text"];
        }

        for(int level = 1; level <= 4; level++)
        {
            allfields[$"saga_level_{level}_a"] = tempfields[$"saga_level_{level}_a"];
            allfields[$"saga_level_{level}_b"] = tempfields[$"saga_level_{level}_b"];
            allfields[$"leveler_level_{level}"] = tempfields[$"leveler_level_{level}"];
            allfields[$"leveler_power_{level}"] = tempfields[$"leveler_power_{level}"];
            allfields[$"leveler_toughness_{level}"] = tempfields[$"leveler_toughness_{level}"];
            allfields[$"loyalty_cost_{level}"] = tempfields[$"loyalty_cost_{level}"];
            allfields[$"level_{level}_text"] = tempfields[$"level_{level}_text"];
        }
    }

    public string GetLevelerText(string oracletext, string power, string toughness, string levelertexttype)
    {
        // the MSE template fills level-up cards from the BOTTOM
        // 
        // also we need the P/T as this goes in the level 1 P/T (before we call MoveLevelTextDown())
// Level up {1} ({1}: Put a level counter on this. Level up only as a sorcery.)\nLEVEL 3-7\n4/4\nProtection from instants\nLEVEL 8+\n6/6\nProtection from everything
// Level up {U} ({U}: Put a level counter on this. Level up only as a sorcery.)\nLEVEL 4-6\n2/4\nLEVEL 7+\n3/5\nAt the beginning of each end step, if it's not your turn, take an extra turn after this one.
        string[] levelerparts = oracletext.Split('\n',StringSplitOptions.None);
        Dictionary<string, string> allfields = this.GetTemplateFieldDictionary();

        int level = 1;
        string currentrange = "";
        string currentpower = "";
        string currenttoughness = "";
        string currenttext = "";
        
        for(int i = 0; i < levelerparts.Length;i++)
        {
            if(level == 1 && i == 0)
            {
                currentrange = "";
                currentpower = power;
                currenttoughness = toughness;
                currenttext = this.FixManaSymbols(this.FixRulesText(levelerparts[i]));
            }
            else if(i == levelerparts.Length-1)
            {
                allfields[$"leveler_level_{level}"] = currentrange;
                allfields[$"leveler_power_{level}"] = currentpower;
                allfields[$"leveler_toughness_{level}"] = currenttoughness;
                allfields[$"level_{level}_text"] = this.FixManaSymbols(this.FixRulesText(levelerparts[i]));
            }
            else if(levelerparts[i].StartsWith("LEVEL "))
            {
                allfields[$"leveler_level_{level}"] = currentrange;
                allfields[$"leveler_power_{level}"] = currentpower;
                allfields[$"leveler_toughness_{level}"] = currenttoughness;
                allfields[$"level_{level}_text"] = this.FixManaSymbols(this.FixRulesText(currenttext));
                currentrange = levelerparts[i].Replace("LEVEL ","");
                currentpower = "";
                currenttoughness = "";
                currenttext = "";
                level++;
            }
            else if(levelerparts[i].Contains('/'))
            {
                string[] pt = levelerparts[i].Split('/');
                currentpower = pt[0];
                currenttoughness = pt[1];
            }
            else
            {
                currenttext = this.FixManaSymbols(this.FixRulesText(levelerparts[i]));
            }
        }
        this.MoveLevelTextDown(allfields,"leveler");
        return allfields[levelertexttype];
    }

    public string GetPlaneswalkerText(string oracletext, string planeswalkertexttype)
    {
        // the MSE template fills sagas from the BOTTOM
        // 
        // thus we fill from the top normally, then move everything down at the end in a separate method

        List<string> planeswalkertextlist = this.GetPlaneswalkerTextList(oracletext);
        List<string> planeswalkerloyaltylist = this.GetPlaneswalkerLoyaltyList(oracletext);
        Dictionary<string, string> allfields = this.GetTemplateFieldDictionary();
        for(int i = 0; i < System.Math.Min(planeswalkertextlist.Count,4); i++)
        {
            allfields[$"loyalty_cost_{i+1}"] = planeswalkerloyaltylist[i];
            allfields[$"level_{i+1}_text"] = planeswalkertextlist[i];
        }
        this.MoveLevelTextDown(allfields,"planeswalker");
        return allfields[planeswalkertexttype];
    }

    public string GetSagaText(string oracletext, string sagatexttype)
    {
        // the MSE template fills sagas from the BOTTOM
        // 
        // thus we fill from the top normally, then move everything down at the end in a separate method

        List<string> sagatextlist = this.GetSagaTextList(oracletext);
        Dictionary<string, string> allfields = this.GetTemplateFieldDictionary();
        string previoussagalinetext = "";
        string currentsagalinetext = "";
        int sagalinecount = 0; // will be 1 - 4 - this is the box that the things go in
        for(int i = 0; i < sagatextlist.Count; i++)
        {
            // i is the line in the source data
            currentsagalinetext = this.FixManaSymbols(this.FixRulesText(sagatextlist[i]));
            // we only fill in "a" if the previ
            // we will fill this in for the BOTTOM - level 4 is filled-in first
            if(currentsagalinetext == previoussagalinetext)
            {
                // this is a double-up in the current saga part
                allfields[$"saga_level_{sagalinecount}_b"] = this.GetRomanNumeral(i+1);
            }
            else
            {
                sagalinecount++;
                allfields[$"saga_level_{sagalinecount}_a"] = this.GetRomanNumeral(i+1);
                allfields[$"level_{sagalinecount}_text"] = currentsagalinetext;
            }
            previoussagalinetext = currentsagalinetext;
        }
        this.MoveLevelTextDown(allfields,"saga");
        return allfields[sagatexttype];

/*
            output["saga_level_1_a"] = "";
            output["saga_level_1_b"] = "";
            output["saga_level_2_a"] = "";
            output["saga_level_2_b"] = "";
            output["saga_level_3_a"] = "";
            output["saga_level_3_b"] = "";
            output["saga_level_4_a"] = "";
            output["saga_level_4_b"] = "";
            output["level_1_text"] = "";
            output["level_2_text"] = "";
            output["level_3_text"] = "";
            output["level_4_text"] = "";

*/

    //  "oracle_text": "(As this Saga enters and after your draw step, add a lore counter. Sacrifice after IV.)\nI — Create a 1/1 white Human Soldier creature token.\nII — Put three +1/+1 counters on target creature you control.\nIII — If you control a creature with power 4 or greater, draw two cards.\nIV — Create a Gold token. (It's an artifact with \"Sacrifice this artifact: Add one mana of any color.\")",
    //  "oracle_text": "(As this Saga enters and after your draw step, add a lore counter. Sacrifice after III.)\nI, II — Create a 2/2 white Knight creature token with vigilance.\nIII — Knights you control get +2/+1 until end of turn.",

    }

    public string GetRomanNumeral(int numeral)
    {
        if(numeral == 1)
            return "I";
        if(numeral == 2)
            return "II";
        if(numeral == 3)
            return "III";
        if(numeral == 4)
            return "IV";
        if(numeral == 5)
            return "V";
        if(numeral == 6)
            return "VI";
        if(numeral == 7)
            return "VII";
        if(numeral == 8)
            return "VIII";
        return "";
    }

    public List<string> GetSagaTextList(string oracletext)
    {
        List<string> output = new List<string>();
        string[] lines = oracletext.Split('\n',StringSplitOptions.None);
        for(int i = 0; i < lines.Length; i++)
        {
            if(!lines[i].StartsWith("(As this Saga enters"))
            {
                string[] sagaparts = lines[i].Split(" — ",StringSplitOptions.None);
                if(sagaparts.Length>1)
                {
                    string[] sagalevels = sagaparts[0].Split(", ",StringSplitOptions.None);
                    for(int j = 0; j < sagalevels.Length; j++)
                    {
                        output.Add(sagaparts[1].Trim());
                    }
                }
            }
            // (As this Saga enters and after your draw step, add a lore counter.
        }
        return output;
    }

    public bool IsLoyaltyText(string planeswalkertext)
    {
        // −- --> note that the "−" is in the Json but it doesn't show in MSE so must change to -
        return planeswalkertext
            .Replace("-","")
            .Replace("−","")
            .Replace("+","")
            .Replace("X","")
            .Replace("0","")
            .Replace("1","")
            .Replace("2","")
            .Replace("3","")
            .Replace("4","")
            .Replace("5","")
            .Replace("6","")
            .Replace("7","")
            .Replace("8","")
            .Replace("9","") == "";
    }

    public List<string> GetPlaneswalkerTextList(string oracletext)
    {
        return this.GetPlaneswalkerList(oracletext,"leveltext");
    }

    public List<string> GetPlaneswalkerLoyaltyList(string oracletext)
    {
        return this.GetPlaneswalkerList(oracletext,"loyalty");
    }

    public List<string> GetPlaneswalkerList(string oracletext, string outputtype = "loyalty")
    {
        List<string> leveltext = new List<string>();
        List<string> loyalty = new List<string>();
        string[] lines = oracletext.Split('\n',StringSplitOptions.None);
        List<string> nonloyalty = new List<string>();
        for(int i = 0; i < lines.Length+1; i++)
        {
            if(i == lines.Length)
            {
                //  if there is still text in the non-loyalty, put that into the result
                if(nonloyalty.Count >0)
                {
                    // put the previous non-loyalty lines together in the result
                    leveltext.Add(String.Join(new string(new char[] { '\n' }),nonloyalty));
                    loyalty.Add("");
                    nonloyalty.Clear();
                }
            }
            else
            {
                string[] pwparts = lines[i].Split(": ",StringSplitOptions.None);
                if(this.IsLoyaltyText(pwparts[0]))
                {
                    if(nonloyalty.Count > 0)
                    {
                        // put the previous non-loyalty lines together in the result
                        leveltext.Add(String.Join(new string(new char[] { '\n' }),nonloyalty));
                        loyalty.Add("");
                        nonloyalty.Clear();
                    }
                    if(pwparts.Length>2)
                    {
                        string fullpwline = lines[i].Substring(pwparts[0].Length+2);

                        leveltext.Add( this.FixManaSymbols(this.FixRulesText(fullpwline  ) ));
                    }
                    else
                    {
                        leveltext.Add(pwparts.Length>1 ? this.FixManaSymbols(this.FixRulesText(pwparts[1])) : "");
                    }

                    // // −- --> note that the "−" is in the Json but it doesn't show in MSE so must change to -
                    loyalty.Add(pwparts[0].Replace("−","-"));
                }
                else
                {
                    nonloyalty.Add(this.FixManaSymbols(this.FixRulesText(lines[i])));
                }
            }
        }
        if(outputtype == "loyalty")
            return loyalty;
        return leveltext;
    }

    public Dictionary<string, string> GetTemplateFieldDictionary()
    {
        Dictionary<string, string> output = new Dictionary<string, string>();
        foreach(var field in this.GetTemplateFieldList())
            output.Add(field,"");
        return output;
    }

    public List<string> GetTemplateFieldList()
    {
        List<string> fields = new List<string>();
        fields.Add("alternative_frame");
        fields.Add("time_created");
        fields.Add("time_modified");
        fields.Add("default_typenamefontcolour");
        fields.Add("card_colour");
        fields.Add("name");
        fields.Add("casting_cost");
        fields.Add("image");
        fields.Add("super_type");
        fields.Add("sub_type");
        fields.Add("rarity");
        fields.Add("rule_text");
        fields.Add("flavor_text");
        fields.Add("loyalty");
        fields.Add("saga_level_1_a");
        fields.Add("saga_level_1_b");
        fields.Add("saga_level_2_a");
        fields.Add("saga_level_2_b");
        fields.Add("saga_level_3_a");
        fields.Add("saga_level_3_b");
        fields.Add("saga_level_4_a");
        fields.Add("saga_level_4_b");
        fields.Add("loyalty_cost_1");
        fields.Add("level_1_text");
        fields.Add("loyalty_cost_2");
        fields.Add("level_2_text");
        fields.Add("loyalty_cost_3");
        fields.Add("level_3_text");
        fields.Add("loyalty_cost_4");
        fields.Add("level_4_text");
        fields.Add("leveler_power_1");
        fields.Add("leveler_toughness_1");
        fields.Add("leveler_power_2");
        fields.Add("leveler_toughness_2");
        fields.Add("leveler_power_3");
        fields.Add("leveler_toughness_3");
        fields.Add("leveler_power_4");
        fields.Add("leveler_toughness_4");
        fields.Add("card_code_text");
        fields.Add("illustrator");
        fields.Add("copywrite");
        fields.Add("name_2");
        fields.Add("casting_cost_2");
        fields.Add("image_2");
        fields.Add("super_type_2");
        fields.Add("sub_type_2");
        fields.Add("rule_text_2");
        fields.Add("flavor_text_2");
        fields.Add("power");
        fields.Add("toughness");
        fields.Add("power_2");
        fields.Add("toughness_2");
        fields.Add("leveler_level_1");
        fields.Add("leveler_level_2");
        fields.Add("leveler_level_3");
        fields.Add("leveler_level_4");
        fields.Add("draft_value");
        fields.Add("chop_top");
        fields.Add("chop_bottom");
        fields.Add("chop_left");
        fields.Add("chop_right");
        return fields;


    }


    public void Message(string message)
    {
        Console.WriteLine(message);
    }

}
