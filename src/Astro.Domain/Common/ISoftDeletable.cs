using System;

namespace Astro.Domain.Common
{
    /// <summary>
    /// Интерфейс для сущности с мягким удалением
    /// </summary>
    public interface ISoftDeletable
    {
        DateTime? DeletedDate { get; set; }
    }
}