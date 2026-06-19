namespace ScrynDomain.Entities;

public class Paiement
{
    public long id_paiement { get; set; }
    
    public float montant { get; set; }
    
    public string methode { get; set; }
    
    public DateTime date_paiement { get; set; }
    
    public Reservation Reservation { get; set; }
    
    public long fk_reservation { get; set; }
    
}