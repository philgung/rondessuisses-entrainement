namespace RondesSuisse;

public record Joueur(string Nom)
{
    public static Joueur Anonyme = new("Anonyme");
}