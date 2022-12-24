
    // This class represents a card from the Scryfall API
    public class Card
    {
        public string id { get; set; }
        public string oracle_id { get; set; }
        public List<int> multiverse_ids { get; set; }
        public string name { get; set; }
        public string lang { get; set; }
        public string released_at { get; set; }
        public string uri { get; set; }
        public string scryfall_uri { get; set; }
        public string layout { get; set; }
        public bool highres_image { get; set; }

        public ImageUris image_uris { get; set; }
        public string mana_cost { get; set; }
        public double cmc { get; set; }
        public string type_line { get; set; }
        public string oracle_text { get; set; }
        public string power { get; set; }
        public string toughness { get; set; }
        public string loyalty { get; set; }
        public List<string> colors { get; set; }
        public List<string> color_identity { get; set; }
        public List<string> keywords { get; set; }
        // public List<string> all_parts { get; set; }
        public List<CardFace> card_faces { get; set; }
        public bool reserved { get; set; }
        public bool foil { get; set; }
        public bool nonfoil { get; set; }
        public bool oversized { get; set; }
        public bool promo { get; set; }
        public bool reprint { get; set; }
        public bool variation { get; set; }
        public string set { get; set; }
        public string set_search_uri { get; set; }
        public string scryfall_set_uri { get; set; }
        public string rulings_uri { get; set; }
        public string prints_search_uri { get; set; }
        public string collector_number { get; set; }
        public bool digital { get; set; }
        public string rarity { get; set; }
        public string illustration_id { get; set; }
        public string artist { get; set; }
        public string frame { get; set; }
        public bool full_art { get; set; }
        public string border_color { get; set; }
        public string timeshifted { get; set; }
        public string colorshifted { get; set; }
        public string futurish { get; set; }
        public bool story_spotlight { get; set; }
        public int edhrec_rank { get; set; }
        public string usd { get; set; }
        public string tix { get; set; }
        public string eur { get; set; }
        // public string related_uris { get; set; }
        // public string purchase_uris { get; set; }
        public string[] games { get; set; }

        public Legalities legalities { get; set; }
        // public string[] prices { get; set; }
        // public string[] prices_foil { get; set; }
        // public string[] price_high { get; set; }
        // public string[] price_mid { get; set; }
        // public string[] price_low { get; set; }
        // public float price_normal { get; set; }
        // public float price_foil { get; set; }

    }