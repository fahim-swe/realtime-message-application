namespace repository.Entities
{
    public interface IEntity
    {
        public Guid Id {get; set;}
        DateTime CreatedAt {get;set;}
    }
}