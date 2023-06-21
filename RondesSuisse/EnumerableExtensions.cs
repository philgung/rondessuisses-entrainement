namespace RondesSuisse;

public static class EnumerableExtensions
{
    public static bool EstPair<T>(this IEnumerable<T> collection) => collection.Count() % 2 == 0;
}