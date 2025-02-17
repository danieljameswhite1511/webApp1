namespace Domain.Entities;

public interface IEntity<TPrimaryKey> where TPrimaryKey : IEquatable<TPrimaryKey>
{
    TPrimaryKey Id { get; set; }
    string Name { get; set; }
    
    //todo add audit fields
}