using FluentAssertions;
using RondesSuisse;

namespace RondesSuisseTests;

public class DurantLaPremiereRonde
{
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