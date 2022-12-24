
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable 0108
namespace mq
{

    public class msedefaults
    {
        // if planeswalker/land is asked for but not set, use card
        private string _planeswalker = "";
        public string planeswalker
        {
            get
            {
                if(_planeswalker == "" )
                {
                    return this.card;
                }
                return this._planeswalker;
            }
            set { this._planeswalker = value; }
        }
        private string _land = "";
        public string land
        {
            get
            {
                if(_land == "" )
                {
                    return this.card;
                }
                return this._land;
            }
            set { this._land = value; }
        }

        public string top = @"mse version: 0.3.8
game: magic
stylesheet: fwipho-hires-m15-promo
set info:
|symbol: symbol1.mse-symbol
|automatic card numbers: no
|automatic reminder text: 
styling:
|magic-future:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-future-artbg:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-future-clear:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-future-promo:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-future-split:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-future-textless: overlay: 
|magic-future-textless-clear: overlay: 
|magic-fwipho-hires-8ed:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-fwipho-hires-future-promo:
||font color: rgb(255,255,255)
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-fwipho-hires-future-textless:
||overlay: 
|magic-fwipho-hires-m15-promo:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-fwipho-hires-m15-token:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-new-token:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
";

        public string bottom =
@"version control:
|type: none
apprentice code: 
";

        // <<frame_options>> = red, artifact, horizontal 
        public string card =
@"card:
|stylesheet: fwipho-hires-future-promo
|has styling: false
|notes: <<note_text>>
|time created: 2017-01-14 01:35:32
|time modified: 2017-07-21 23:33:57
|card color: <<frame_options>>
|name: <<card_name>>
|casting cost: <<casting_cost>>
|card symbol: none
|type symbol: <<type_symbol>>
|image: <<image_file>>
|super type: <word-list-type><<high_type>></word-list-type>
|sub type: <word-list-race><<sub_type_race>></word-list-race> <word-list-class><<sub_type_class>></word-list-class><soft> </soft><word-list-class></word-list-class>
|rarity: <<rarity>>
|rule text: <<rules_text>>
|flavor text:
||<i-flavor><<flavour_text>></i-flavor>
|power: <<power>>
|toughness: <<toughness>>
|card code text: 
|illustrator: <<illustrator>>
|copyright: <<copyright>>
|image 2: 
|super type 2: <word-list-type></word-list-type>
|sub type 2: 
|rule text 2: 
|flavor text 2: <i-flavor></i-flavor>
|copyright 2: 
|draft value: <<draft_value>>
";

    }

    public class fwipho_hires_future_promo : msedefaults
    {


    }


        public class fwipho_hires_future_textless : fwipho_hires_future_promo
    {

public string top = @"mse version: 0.3.8
game: magic
stylesheet: fwipho-hires-m15-promo
set info:
|symbol: symbol1.mse-symbol
|automatic card numbers: no
|automatic reminder text: 
styling:
|magic-future:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-future-artbg:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-future-clear:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-future-promo:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-future-split:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-future-textless: overlay: 
|magic-future-textless-clear: overlay: 
|magic-fwipho-hires-8ed:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-fwipho-hires-future-promo:
||font color: rgb(255,255,255)
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-fwipho-hires-future-textless:
||overlay: 
|magic-fwipho-hires-m15-promo:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-fwipho-hires-m15-token:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-new-token:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
";

        public string bottom =
@"version control:
|type: none
apprentice code: 
";

        // <<frame_options>> = red, artifact, horizontal 
        public string card =
@"card:
|stylesheet: fwipho-hires-future-textless
|has styling: false
|notes: <<note_text>>
|time created: 2017-01-14 01:35:32
|time modified: 2017-07-21 23:33:57
|card color: <<frame_options>>
|name: <<card_name>>
|casting cost: <<casting_cost>>
|card symbol: none
|type symbol: <<type_symbol>>
|image: <<image_file>>
|super type: <word-list-type><<high_type>></word-list-type>
|sub type: <word-list-race><<sub_type_race>></word-list-race> <word-list-class><<sub_type_class>></word-list-class><soft> </soft><word-list-class></word-list-class>
|rarity: <<rarity>>
|rule text: <<rules_text>>
|flavor text:
||<i-flavor><<flavour_text>></i-flavor>
|power: <<power>>
|toughness: <<toughness>>
|card code text: 
|illustrator: <<illustrator>>
|copyright: <<copyright>>
|image 2: 
|super type 2: <word-list-type></word-list-type>
|sub type 2: 
|rule text 2: 
|flavor text 2: <i-flavor></i-flavor>
|copyright 2: 
|draft value: <<draft_value>>
";



    }



        public class fwipho_hires_future_m15 : fwipho_hires_future_promo
    {

public string top = @"mse version: 0.3.8
game: magic
stylesheet: fwipho-m15-future
set info:
|symbol: 
|masterpiece symbol: 
|automatic card numbers: yes
|automatic copyright: no
|rarity codes: no
styling:
|magic-fwipho-hires-8ed:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-fwipho-hires-8ed-extended:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-fwipho-hires-future-promo:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-fwipho-hires-m15-promo:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-fwipho-hires-m15-token:
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
|magic-fwipho-m15-future:
||chop top: 1
||chop bottom: 1
||flavor bar offset: 1
||use holofoil stamps: no
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
";

        public string bottom =
@"version control:
|type: none
apprentice code: 
";

        // <<frame_options>> = red, artifact, horizontal 
        public string card =
@"card:
|stylesheet: fwipho-m15-future
|has styling: true
|styling data:
||chop top: <<chop_top>>
||chop bottom: <<chop_bottom>>
||chop left: <<chop_left>>
||chop right: <<chop_right>>
||flavor bar offset: 1
||use holofoil stamps: no
||original symbols: yes
||beleren: yes
||shifted sorting: no
||remove from autocount: no
||grey hybrid name: yes
||colored multicolor land name: yes
||use guild mana symbols: no
||text box mana symbols: magic-mana-small.mse-symbol-font
||center text: short text only
||overlay: 
|notes:  <<note_text>>
|time created: 2017-01-14 01:35:32
|time modified: 2017-07-21 23:33:57
|card color: <<frame_options>>
|name: <<card_name>>
|casting cost: <<casting_cost>>
|card symbol: none
|type symbol: <<type_symbol>>
|image: <<image_file>>
|super type: <word-list-type><<high_type>></word-list-type>
|sub type: <word-list-race><<sub_type_race>></word-list-race> <word-list-class><<sub_type_class>></word-list-class><soft> </soft><word-list-class></word-list-class>
|rarity: <<rarity>>
|rule text: <<rules_text>>
|flavor text:
||<i-flavor><<flavour_text>></i-flavor>
|power: <<power>>
|toughness: <<toughness>>
|card code text: 
|illustrator: <<illustrator>>
|copyright: <<copyright>>
|image 2: 
|super type 2: <word-list-type></word-list-type>
|sub type 2: 
|rule text 2: 
|flavor text 2: <i-flavor></i-flavor>
|copyright 2: 
|copyright 3: 
|mainframe image: 
|mainframe image 2: 
|draft value: <<draft_value>>
";



    }



        public class fwipho_future_m15_tall : msedefaults
    {

public string top = @"mse version: 0.3.8
game: magic
stylesheet: fwipho-m15-future-tall
set info:
|symbol: symbol1.mse-symbol
|masterpiece symbol: 
|automatic reminder text: 
|automatic copyright: no
|auto legends: yes
styling:
|magic-fwipho-m15-future-tall:
||original symbols: no
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: ";

        public string bottom =
@"version control:
|type: none
apprentice code: ";

        // <<frame_options>> = red, artifact, horizontal 
        public string card =
@"card:
|has styling: true
|styling data:
||chop top: <<chop_top>>
||chop bottom: <<chop_bottom>>
||chop left: <<chop_left>>
||chop right: <<chop_right>>
||use holofoil stamps: no
||beleren: yes
||original symbols: no
||shifted sorting: no
||remove from autocount: no
||text box mana symbols: magic-mana-small.mse-symbol-font
||overlay: 
||default typename font: <<default_typenamefontcolour>>
||default cardtext font: white
||alternative frame: <<alternative_frame>>
|notes: <<note_text>>
|time created: <<time_created>>
|time modified: <<time_modified>>
|card color: <<card_colour>>
|name: <<name>>
|casting cost: <<casting_cost>>
|image: <<image>>
|super type: <<super_type>>
|sub type: <<sub_type>>
|rarity: <<rarity>>
|rule text: <<rule_text>>
|flavor text: <<flavor_text>>
|loyalty cost 1: <<loyalty_cost_1>>
|saga level 1 a: <<saga_level_1_a>>
|saga level 1 b: <<saga_level_1_b>>
|saga level 2 a: <<saga_level_2_a>>
|saga level 2 b: <<saga_level_2_b>>
|saga level 3 a: <<saga_level_3_a>>
|saga level 3 b: <<saga_level_3_b>>
|saga level 4 a: <<saga_level_4_a>>
|saga level 4 b: <<saga_level_4_b>>
|level 1 text: <<level_1_text>>
|loyalty cost 2: <<loyalty_cost_2>>
|level 2 text: <<level_2_text>>
|loyalty cost 3: <<loyalty_cost_3>>
|level 3 text: <<level_3_text>>
|loyalty cost 4: <<loyalty_cost_4>>
|level 4 text: <<level_4_text>>
|loyalty: <<loyalty>>
|power: <<power>>
|toughness: <<toughness>>
|leveler power 1: <<leveler_power_1>>
|leveler toughness 1: <<leveler_toughness_1>>
|leveler power 2: <<leveler_power_2>>
|leveler toughness 2: <<leveler_toughness_2>>
|leveler power 3: <<leveler_power_3>>
|leveler toughness 3: <<leveler_toughness_3>>
|leveler power 4: <<leveler_power_4>>
|leveler toughness 4: <<leveler_toughness_4>>
|custom card number: 
|card code text: <<card_code_text>>
|illustrator: <<illustrator>>
|copyright: <<copyright>>
|name 2: <<name_2>>
|casting cost 2: <<casting_cost_2>>
|image 2: <<image_2>>
|super type 2: <<super_type_2>>
|sub type 2: <<sub_type_2>>
|rule text 2: <<rule_text_2>>
|flavor text 2: <<flavor_text_2>>
|power 2: <<power_2>>
|toughness 2: <<toughness_2>>
|copyright 2: 
|copyright 3: 
|leveler level 1: <<leveler_level_1>>
|leveler level 2: <<leveler_level_2>>
|leveler level 3: <<leveler_level_3>>
|leveler level 4: <<leveler_level_4>>
|mainframe image: 
|mainframe image 2: 
|draft value: <<draft_value>>";

    }


}
