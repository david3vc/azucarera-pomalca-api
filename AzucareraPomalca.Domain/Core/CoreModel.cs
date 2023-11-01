namespace AzucareraPomalca.Domain.Core
{
    public abstract class CoreModel<ID>
    {
        public ID Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }
    }
}
