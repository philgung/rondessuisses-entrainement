using FluentAssertions;

namespace RondesSuisseTests;

public class Tests
{
    // Pour la première ronde, les joueurs sont divisés en deux sous-groupes :
    // un sous-groupe S1 composé des joueurs 1 à n/2, et un sous-groupe
    // S2 composé des joueurs (n/2)+1 à n.
    // Le premier de S1 joue contre le premier de S2, le deuxième de S1
    // contre le deuxième de S2, et ainsi de suite de manière que le dernier
    // joueur de S1 joue contre le dernier joueur de S2
    
    // S'il y a 8 joueurs dans un groupe de même score, le numéro 1 affronte
    // le numéro 5, le numéro 2 affronte le numéro 6 et ainsi de suite
    
    [Test]
    public void Devrait_appairer_lors_de_la_premiere_ronde_pour_8_joueurs()
    {
        var rondesSuisse = new OrganisateurRondesSuisse()
            .AjouterJoueur("Numéro 1")
            .AjouterJoueur("Numéro 2")
            .AjouterJoueur("Numéro 3")
            .AjouterJoueur("Numéro 4")
            .AjouterJoueur("Numéro 5")
            .AjouterJoueur("Numéro 6")
            .AjouterJoueur("Numéro 7")
            .AjouterJoueur("Numéro 8")
            .Creer();

        var matchs = rondesSuisse.Appairer();
        matchs.Should().HaveCount(4);
        DevraitOpposer(matchs.First(), "Numéro 1", "Numéro 5");
        DevraitOpposer(matchs.ElementAt(1), "Numéro 2", "Numéro 6");
        DevraitOpposer(matchs.ElementAt(2), "Numéro 3", "Numéro 7");
        DevraitOpposer(matchs.Last(), "Numéro 4", "Numéro 8");
    }

    private static void DevraitOpposer(Match match, string nomJoueurA, string nomJoueurB)
    {
        match.JoueurA.Nom.Should().Be(nomJoueurA);
        match.JoueurB.Nom.Should().Be(nomJoueurB);
    }

    [Test]
    public void Appairer_lors_de_la_premier_ronde_sans_joueurs_ne_renvoie_aucun_matchs()
    {
        var rondesSuisse = new OrganisateurRondesSuisse().Creer();
        
        var matchs = rondesSuisse.Appairer();
        
        matchs.Should().BeEmpty();
    }
    // Les joueurs qui gagnent reçoivent un point et les perdants ne reçoivent aucun point
}

public class OrganisateurRondesSuisse
{
    private readonly IList<Joueur> _joueurs = new List<Joueur>();

    public OrganisateurRondesSuisse AjouterJoueur(string nom)
    {
        _joueurs.Add(new Joueur(nom));
        return this;
    }

    public RondesSuisse Creer()
    {
        return new RondesSuisse(_joueurs);
    }
}

public record Joueur(string Nom)
{
}

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
        return new[]
        {
            new Match {JoueurA = new Joueur("Numéro 1"), JoueurB = new Joueur("Numéro 5")},
            new Match {JoueurA = new Joueur("Numéro 2"), JoueurB = new Joueur("Numéro 6")},
            new Match {JoueurA = new Joueur("Numéro 3"), JoueurB = new Joueur("Numéro 7")},
            new Match {JoueurA = new Joueur("Numéro 4"), JoueurB = new Joueur("Numéro 8")},
        };
    }
}

public class Match
{
    public Joueur JoueurA { get; set; }
    public Joueur JoueurB { get; set; }
}