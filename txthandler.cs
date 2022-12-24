using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

public class txthandler
{
    public List<CardInfo> GetCardInfoList(string filepath)
    {
        List<CardInfo> output = new List<CardInfo>();

        string[] lines = File.ReadAllLines(filepath);
        foreach(string line in lines)
        {
            // Split the line into words
            string[] words = line.Split(' ');

            // If the first word is a number, store it as a variable
            string cardname = line.Trim();
            int qty = 0;
            if(cardname == "")
            {

            }
            else if (int.TryParse(words[0], out int number))
            {
                // Read the rest of the line as a string
                cardname = string.Join(" ", words[1..]);
                qty = number;
            }
            else
            {
                qty = 1;
            }

            if(qty>0)
            {
                output.Add(new CardInfo(){qty=qty,name=cardname});
            }
        }

        return output;
    }


    public void SaveCardInfoList(List<CardInfo> cardlist, string filepath)
    {
        StringBuilder sb = new StringBuilder();
        foreach(var card in cardlist)
        {
            sb.AppendLine($"{card.qty} {card.name}");
        }
        System.IO.File.WriteAllText(filepath,sb.ToString());
    }
}