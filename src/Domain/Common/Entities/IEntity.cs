namespace Domain.Common.Entities;

public interface IEntity<TPrimaryKey> where TPrimaryKey : IEquatable<TPrimaryKey> {
    TPrimaryKey Id { get;  }
    
}