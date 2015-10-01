namespace Depmon.Server.Domain
{
    public class Style
    {
        public int Id { get; set; }

        public ItemType Type { get; set; }

        public string Code { get; set; }

        public string DisplayName { get; set; }

        public string DisplayIcon { get; set; }

        public bool Hidden { get; set; }
    }
}