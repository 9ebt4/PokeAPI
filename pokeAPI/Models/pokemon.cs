namespace pokeAPI.Models
{

    public class pokemon
    {
        public Description[] descriptions { get; set; }
        public int id { get; set; }
        public bool is_main_series { get; set; }
        public string name { get; set; }
        public Name[] names { get; set; }
        public Pokemon_Entries[] pokemon_entries { get; set; }
        public Region region { get; set; }
        public Version_Groups[] version_groups { get; set; }
    }

    public class Region
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Description
    {
        public string description { get; set; }
        public Language language { get; set; }
    }

    public class Language
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Name
    {
        public Language1 language { get; set; }
        public string name { get; set; }
    }

    public class Language1
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Pokemon_Entries
    {
        public int entry_number { get; set; }
        public Pokemon_Species pokemon_species { get; set; }
    }

    public class Pokemon_Species
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Version_Groups
    {
        public string name { get; set; }
        public string url { get; set; }
    }

}
