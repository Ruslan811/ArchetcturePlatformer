using CodeBase.Services.PlayerProgressService;
using System;

public interface IHealth : IProgressSaver
{
    event Action HealthChanged;
    float Current { get; set; }
    float Max { get; set; }
    void TakeDamage(float damage);
}
