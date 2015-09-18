namespace Depmon.Server.Domain.Model
{
    public class Association
    {
        public int Id { get; set; }

        public AssociationType Type { get; set; }

        public string Code { get; set; }

        public string DisplayName { get; set; }

        public string DisplayIcon { get; set; }

        public bool Hidden { get; set; }
    }
}