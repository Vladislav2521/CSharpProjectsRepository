using System;

namespace Astro.Infrasructure.Implementations
{
  internal class DateTimeProvider : IDateTimeProvider
  {
    public DateTime Now => DateTime.Now;

    public DateTime UtcNow => DateTime.UtcNow;
  }
}
