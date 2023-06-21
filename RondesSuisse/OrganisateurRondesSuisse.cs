namespace RondesSuisse;

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