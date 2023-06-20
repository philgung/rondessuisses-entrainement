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
        matchs.First().JoueurA.Nom.Should().Be("Numéro 1");
        matchs.First().JoueurB.Nom.Should().Be("Numéro 5");
        matchs.ElementAt(1).JoueurA.Nom.Should().Be("Numéro 2");
        matchs.ElementAt(1).JoueurB.Nom.Should().Be("Numéro 6");
        matchs.ElementAt(2).JoueurA.Nom.Should().Be("Numéro 3");
        matchs.ElementAt(2).JoueurB.Nom.Should().Be("Numéro 7");
        matchs.Last().JoueurA.Nom.Should().Be("Numéro 4");
        matchs.Last().JoueurB.Nom.Should().Be("Numéro 8");
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