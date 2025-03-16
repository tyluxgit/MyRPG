
namespace Engine;

// Cette version utilise l'algorithme SplitMix64 pour générer des nombres aléatoires.
// Elle n'est pas cryptographiquement sécurisée, mais convient parfaitement pour un jeu.
public static class RandomNumberGenerator
{
    // État interne pour l'algorithme SplitMix64, initialisé avec l'heure actuelle.
    private static ulong _state = (ulong)DateTime.UtcNow.Ticks;

    /// <summary>
    /// Génère un entier aléatoire compris entre minimumValue et maximumValue (inclus),
    /// en utilisant l'algorithme SplitMix64.
    /// </summary>
    /// <param name="minimumValue">Valeur minimale (incluse).</param>
    /// <param name="maximumValue">Valeur maximale (incluse).</param>
    /// <returns>Un entier aléatoire dans l'intervalle [minimumValue, maximumValue].</returns>
    public static int NumberBetween(int minimumValue, int maximumValue)
    {
        if (minimumValue > maximumValue)
        {
            throw new ArgumentException("minimumValue doit être inférieur ou égal à maximumValue");
        }

        // Génère un nombre aléatoire 64 bits et le convertit en une fraction dans [0, 1)
        double fraction = NextULong() / (double)ulong.MaxValue;

        // Calcul de l'intervalle
        int range = maximumValue - minimumValue + 1;
        int randomValue = minimumValue + (int)(fraction * range);
        return randomValue;
    }

    /// <summary>
    /// Version simple utilisant System.Random.
    /// Si vous préférez cette version, vous pouvez renommer SimpleNumberBetween en NumberBetween.
    /// </summary>
    private static readonly Random _simpleGenerator = new();
    public static int SimpleNumberBetween(int minimumValue, int maximumValue)
    {
        return _simpleGenerator.Next(minimumValue, maximumValue + 1);
    }

    /// <summary>
    /// Génère un nombre aléatoire 64 bits en utilisant l'algorithme SplitMix64.
    /// </summary>
    /// <returns>Un ulong aléatoire.</returns>
    private static ulong NextULong()
    {
        _state += 0x9E3779B97F4A7C15UL;
        ulong z = _state;
        z = (z ^ (z >> 30)) * 0xBF58476D1CE4E5B9UL;
        z = (z ^ (z >> 27)) * 0x94D049BB133111EBUL;
        return z ^ (z >> 31);
    }
}
