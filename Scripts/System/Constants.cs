public class Constants
{
    public static readonly string VERSION_GAME = "0.0.1";
    public static readonly int VERSION_BUNDLE = 1;
    public static readonly int DEFAULT_FRAME = 60;

    public static class Game
    {
        public static readonly string STRING_PLAYER = "Player";
        public static readonly string STRING_MONSTER = "Monster";
        public static readonly string STRING_ZERO = "0";
        public static readonly string STRING_MASTER = "Master";
        public static readonly string STRING_WINDOWSETTING = "WindowSetting";
        public static readonly string STRING_RESOLUTION = "ResolutionSetting";

        public static readonly int INVENTORY_SLOT_COLUMN = 6;
        public static readonly int INVENTORY_SLOT_ROW = 8;
    }

    public static class Item
    {
        public static readonly int EQUIPMENT_DEFAULT_SWORD = 200;
        public static readonly int EQUIPMENT_BOSS_SWORD = 201;

        public static readonly int CONSUMABLE_POTION = 100;
    }

    public static class Save
    {
        public static readonly string DIRECTORY_NAME = "save";
        public static readonly string FILE_NAME = "save";
        public static readonly int SLOT_COUNT = 3;
    }

    public static class Animation
    {
        public static readonly string NAME_HORIZONTAL = "Horizontal";
        public static readonly string NAME_VERTICAL = "Vertical";
    }

    public static class Prefab
    {
        public static readonly string NAME_UI_POPUP_DEATH = "UI_Popup_Death";
        public static readonly string NAME_UI_POPUP_GAME = "UI_Popup_Game";
        public static readonly string NAME_UI_POPUP_KEYSIGN = "UI_Popup_KeySign";
        public static readonly string NAME_UI_POPUP_REST = "UI_Popup_Rest";
        public static readonly string NAME_UI_POPUP_SLOTS = "UI_Popup_Slots";
        public static readonly string NAME_UI_POPUP_UPGRADE = "UI_Popup_Upgrade";
        public static readonly string NAME_UI_POPUP_INVENTORY = "UI_Popup_Inventory";
        public static readonly string NAME_UI_POPUP_SETTING = "UI_Popup_Setting";
        public static readonly string NAME_UI_POPUP_EXIT = "UI_Popup_Exit";
        public static readonly string NAME_UI_POPUP_DESTROYED = "UI_Popup_Destroyed";
        public static readonly string NAME_UI_POPUP_DROPITEMINFO = "UI_Popup_DropItemInfo";
        public static readonly string NAME_UI_POPUP_Update = "UI_Popup_Update";

        public static readonly string NAME_GAME_OBJECTS_SOUL = "Soul";

        public static readonly string NAME_GAME_MONSTERS_HUMAN = "HumanMonster";

        public static readonly string NAME_EFFECT_HIT = "HitEffect";
        public static readonly string NAME_EFFECT_UPGRADE = "Upgrade";
        public static readonly string NAME_EFFECT_DRINK_POTION = "DrinkPotion";
        public static readonly string NAME_EFFECT_BLOCK = "Block";
        public static readonly string NAME_EFFECT_PARRY = "Parry";
    }

    public static class Addressable
    {
        public static readonly string LABEL_ATLAS = "Atlas";
        public static readonly string LABEL_CSV = "Csv";
        public static readonly string LABEL_PREFAB = "Prefab";
        public static readonly string LABEL_AUDIO = "Audio";
        public static readonly string LABEL_MATERIAL = "Mat";

        public static readonly string ATLAS_CONSUMABLE = "Consumable";
        public static readonly string ATLAS_EQUIPMENT = "Equipment";

        public static readonly string CSV_CONSUMABLEITEMS = "ConsumableItems";
        public static readonly string CSV_EQUIMENTITEMS = "EquimentItems";
        public static readonly string CSV_STRING = "String";
        public static readonly string CSV_UPGRADE = "Upgrade";
        public static readonly string CSV_MONSTER = "Monster";
    }

    public static class StringIndex
    {
        public static readonly int INFO_TITLE_EQUIPMENT = 3001;
        public static readonly int INFO_TITLE_CONSUMABLE = 3002;
        public static readonly int ITEM_TYPE_EQUIPMENT = 3003;
        public static readonly int ITEM_TYPE_CONSUMABLE = 3004;
        public static readonly int UPGRADE_MAX_LEVEL = 3007;
        public static readonly int BONFIRE_ENABLE = 3008;
        public static readonly int BONFIRE_REST = 3009;
    }

    public static class CommonSoundVolume
    {
        public static readonly float HALF = 0.5f;
        public static readonly float FOOT_STEP = 0.1f;
    }
}
