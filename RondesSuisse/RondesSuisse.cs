namespace RondesSuisse;

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
            var tailleGroupe = joueurs.Length/2;
            var sousGroupeA = joueurs[..tailleGroupe];
            var sousGroupeB = joueurs[tailleGroupe..];

            return sousGroupeB.Select((joueurB, index) => 
                new Match {JoueurA = sousGroupeA[index], JoueurB = joueurB});
        }
        else
        {
            var tailleGroupe = joueurs.Length/2 + 1;
            var sousGroupeA = joueurs[..tailleGroupe];
            var sousGroupeB = joueurs[tailleGroupe..];
            var matchs = sousGroupeB.Select((joueurB, index) => 
                    new Match {JoueurA = sousGroupeA[index], JoueurB = joueurB})
                .ToList();
            matchs.Add(new Match{ JoueurA = sousGroupeA[^1], JoueurB = Joueur.Anonyme});
            return matchs;
        }
    }
}