namespace Characters
{
    public interface IHealth
    {
        void TakeDamage(float amount);
        void Heal(float amount);
    }
}