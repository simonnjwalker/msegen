
    public class CardFace
    {
        public string @object { get; set; }
        public string @name { get; set; }
        public string[] names { get; set; }
        public string mana_cost { get; set; }
        public int cmc { get; set; }
        public string type_line { get; set; }
        public string oracle_text { get; set; }
        public string power { get; set; }
        public string toughness { get; set; }
        public string loyalty { get; set; }
        public string[] colors { get; set; }
        public string[] color_indicator { get; set; }
        public string[] color_identity { get; set; }
        public Legalities legalities { get; set; }
        public string[] games { get; set; }
        public string[] reserved { get; set; }
        public bool reprint { get; set; }
        public string[] @set { get; set; }
        public string scryfall_uri { get; set; }
        public string rulings_uri { get; set; }
        public string prints_search_uri { get; set; }
        public string[] variations { get; set; }
        public ImageUris image_uris { get; set; }
        public string[] watermark { get; set; }
        public string[] border_color { get; set; }
        public bool timeshifted { get; set; }
        public bool hand_modifier { get; set; }
        public bool life_modifier { get; set; }
    }
