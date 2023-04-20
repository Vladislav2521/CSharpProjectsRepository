namespace Astro.Domain.Common
{
    /// <summary>
    /// Интерфейся для сущности - Entity, содержащий идентификатор
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey>: IBaseEntity
    {
        public TKey Id { get; set; }
    }
}