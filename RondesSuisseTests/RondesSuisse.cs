namespace RondesSuisseTests;

public class RondesSuisse
{
    private readonly IEnumerable<Joueur> _joueurs;

    public RondesSuisse(IEnumerable<Joueur> joueurs)
    {
        _joueurs = joueurs;
    }

    public IEnumerable<Match> Appairer()
    {
        if (!_joueurs.Any())
            return Enumerable.Empty<Match>();

        var joueurs = _joueurs.ToArray();
        if (joueurs.EstPair())
        {
            var sousGroupeA = joueurs[..(joueurs.Length/2)];
            var sousGroupeB = joueurs[(joueurs.Length/2)..];

            return sousGroupeA.Select((joueurA, index) => 
                new Match {JoueurA = joueurA, JoueurB = sousGroupeB[index]});
        }
        else
        {
            var sousGroupeA = joueurs[..(joueurs.Length/2 + 1)];
            var sousGroupeB = joueurs[(joueurs.Length/2 + 1)..];
            var matchs = sousGroupeB.Select((joueurB, index) => 
                new Match {JoueurA = sousGroupeA[index], JoueurB = joueurB}).ToList();
            matchs.Add(new Match{ JoueurA = sousGroupeA[^1], JoueurB = Joueur.Anonyme});
            return matchs;
        }
    }
}

public static class EnumerableExtensions
{
    public static bool EstPair<T>(this IEnumerable<T> collection) => collection.Count() % 2 == 0;
}