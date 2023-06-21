using FluentAssertions;

namespace RondesSuisseTests;

public class DurantLaPremiereRonde
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
    public void Appairer_pour_8_joueurs_devrait_retourner_4_matchs()
    {
        var rondesSuisse = new OrganisateurRondesSuisse()
            .AjouterJoueur(_numéro1)
            .AjouterJoueur(_numéro2)
            .AjouterJoueur(_numéro3)
            .AjouterJoueur(_numéro4)
            .AjouterJoueur(_numéro5)
            .AjouterJoueur(_numéro6)
            .AjouterJoueur(_numéro7)
            .AjouterJoueur(_numéro8)
            .Creer();

        var matchs = rondesSuisse.Appairer();
        matchs.Should().HaveCount(4);
        matchs.First().DevraitOpposer(_numéro1, _numéro5);
        matchs.ElementAt(1).DevraitOpposer(_numéro2, _numéro6);
        matchs.ElementAt(2).DevraitOpposer(_numéro3, _numéro7);
        matchs.Last().DevraitOpposer(_numéro4, _numéro8);
    }

    [Test]
    public void Appairer_pour_7_joueurs_devrait_retourner_4_matchs_dont_un_exempte()
    {
        var rondesSuisse = new OrganisateurRondesSuisse()
            .AjouterJoueur(_numéro1)
            .AjouterJoueur(_numéro2)
            .AjouterJoueur(_numéro3)
            .AjouterJoueur(_numéro4)
            .AjouterJoueur(_numéro5)
            .AjouterJoueur(_numéro6)
            .AjouterJoueur(_numéro7)
            .Creer();

        var matchs = rondesSuisse.Appairer();

        matchs.Should().HaveCount(4);
        matchs.First().DevraitOpposer(_numéro1, _numéro5);
        matchs.ElementAt(1).DevraitOpposer(_numéro2, _numéro6);
        matchs.ElementAt(2).DevraitOpposer(_numéro3, _numéro7);
        matchs.Last().DevraitOpposer(_numéro4, Joueur.Anonyme);
    }

    [Test]
    public void Appairer_sans_joueurs_ne_renvoie_aucun_matchs()
    {
        var rondesSuisse = new OrganisateurRondesSuisse().Creer();
        
        var matchs = rondesSuisse.Appairer();
        
        matchs.Should().BeEmpty();
    }

    private static Joueur _numéro1 = new("Numéro 1");
    private static Joueur _numéro2 = new("Numéro 2");
    private static Joueur _numéro3 = new("Numéro 3");
    private static Joueur _numéro4 = new("Numéro 4");
    private static Joueur _numéro5 = new("Numéro 5");
    private static Joueur _numéro6 = new("Numéro 6");
    private static Joueur _numéro7 = new("Numéro 7");
    private static Joueur _numéro8 = new("Numéro 8");
    // Les joueurs qui gagnent reçoivent un point et les perdants ne reçoivent aucun point
}

public static class TestExtensions
{
    public static void DevraitOpposer(this Match match, Joueur joueurA, Joueur joueurB)
    {
        match.JoueurA.Should().Be(joueurA);
        match.JoueurB.Should().Be(joueurB);
    }    
}
public class OrganisateurRondesSuisse
{
    private readonly IList<Joueur> _joueurs = new List<Joueur>();

    public OrganisateurRondesSuisse AjouterJoueur(Joueur joueur)
    {
        _joueurs.Add(joueur);
        return this;
    }

    public RondesSuisse Creer()
    {
        return new RondesSuisse(_joueurs);
    }
}

public record Joueur(string Nom)
{
    public static Joueur Anonyme = new("Anonyme");
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