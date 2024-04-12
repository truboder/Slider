using System;

public interface IHealth
{
    float Health { get;}
    event Action<float> OnHealthChange;
    public void Hit(float value);
    public void Heal(float value);
}
