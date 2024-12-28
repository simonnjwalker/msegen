using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

public class xlsxhandler
{
    public List<CardInfo> GetCardInfoList(string filepath)
    {
        List<CardInfo> output = new List<CardInfo>();
        var xl = new Seamlex.Utilities.ExcelToData();
        var ds = xl.ToDataSet(filepath);
        var dt = ds.Tables[0];

        foreach(System.Data.DataRow row in ds.Tables[0].Rows)
        {
            if((row["multiverseid"] ?? "").ToString() != "" || (string)(row["name"] ?? "") != ""  || (string)(row["cardjson"] ?? "") != "" )
            {
                var item = new CardInfo(){
                    cardjson = (row["cardjson"] ?? "").ToString(),
                    imagepath = (row["imagepath"] ?? "").ToString(),
                    artdesc = (row["artdesc"] ?? "").ToString(),
                    name = (row["name"] ?? "").ToString(),
                    };
                
                if((row["multiverseid"] ?? "").ToString() != "")
                    item.multiverseid = Int32.Parse(row["multiverseid"].ToString());
                if((row["qty"] ?? "").ToString() != "")
                    item.qty = Int32.Parse(row["qty"].ToString());
                output.Add(item);
            }
        }
        return output;
    }

    public void SaveCardInfoList(List<CardInfo> cardlist, string filepath)
    {
        var ds = new System.Data.DataSet();
        var dt = ds.Tables.Add();
        dt.Columns.Add("multiverseid");
        dt.Columns.Add("qty");
        dt.Columns.Add("name");
        dt.Columns.Add("cardjson");
        dt.Columns.Add("imagepath");
        dt.Columns.Add("artdesc");
        foreach(var card in cardlist)
        {
            dt.Rows.Add(
                card.multiverseid,
                card.qty,
                card.name,
                card.cardjson,
                card.imagepath,
                card.artdesc
                );
        }

        var xl = new Seamlex.Utilities.ExcelToData();
        xl.ToExcelFile(ds,filepath);

    }

}