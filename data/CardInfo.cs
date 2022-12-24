
    // This class represents a card from the Scryfall API
    public class CardInfo
    {
        public int multiverseid { get; set; }
        public Card card { get; set; }
        public int qty { get; set; }
        public string name { get; set; }
        public string cleanname { get; set; }
        public string jsname { get; set; }
        public string cardjson { get; set; }
        public string imagepath { get; set; }
        public string artdesc { get; set; }

    }