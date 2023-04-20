using System;

namespace Astro.Infrasructure
{
  /// <summary>
  /// Интерфейс для подмены в тестах <see cref="DateTime.Now"/> и <see cref="DateTime.UtcNow"/>
  /// </summary>
  public interface IDateTimeProvider
  {
    DateTime Now { get; }

    DateTime UtcNow { get; }
  }
}
