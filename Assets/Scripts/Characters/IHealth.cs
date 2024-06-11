namespace Characters
{
    // Interfejs definiujący metody związane ze zdrowiem postaci
    public interface IHealth
    {
        // Metoda do zadawania obrażeń postaci
        void TakeDamage(float amount);

        // Metoda do leczenia postaci
        void Heal(float amount);
    }
}