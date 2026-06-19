using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScrynDataProvider.Entities;
using ScrynDomain.Entities;

namespace WebApplication1.Data;

public class ScrynDbContext: IdentityDbContext<ScrynUser>
{
    public static readonly ILoggerFactory consoleLogger = LoggerFactory.Create(builder => { builder.AddConsole(); });
    
    public ScrynDbContext(DbContextOptions<ScrynDbContext> options)
        : base(options)
    {
    }
 
    public ScrynDbContext():base()
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(consoleLogger)  //on lie le contexte avec le système de journalisation
            .EnableSensitiveDataLogging() 
            .EnableDetailedErrors();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Propriétés de la table Film
        // Clé primaire
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Film>()
            .HasKey(f => f.id_film);
        // ManyToMany vers Genres
        modelBuilder.Entity<Film>()
            .HasMany(f => f.FaitPartie)
            .WithMany(g => g.Appartient);
        // OneToMany vers Seance
        modelBuilder.Entity<Film>()
            .HasMany(f => f.Seances)
            .WithOne(s => s.Film);
        
        
        // Propriétés de la table Genre
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Genre>()
            .HasKey(g => g.id_genre);
        modelBuilder.Entity<Genre>()
            .HasMany(g => g.Appartient)
            .WithMany(f => f.FaitPartie);


        
        // Propriétés de la table Tarif
        // Clé primaire
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Tarif>()
            .HasKey(t => t.id_tarif);
        // ManyToMany vers Seances
        modelBuilder.Entity<Tarif>()
            .HasMany(t => t.AppliqueDans)
            .WithMany(s => s.AppliqueSur);
        
        
        

        // Propriétés de la table Seance
        // Clé primaire
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Seance>()
            .HasKey(s => s.id_seance);

        modelBuilder.Entity<Seance>()
            .HasOne(f => f.Film)
            .WithMany(s => s.Seances)
            .HasForeignKey(s => s.fk_film);
        // ManyToMany vers Tarif
        modelBuilder.Entity<Seance>()
            .HasMany(s=>s.AppliqueSur)
            .WithMany(t => t.AppliqueDans);
        // OneToMany vers Reservation
        modelBuilder.Entity<Seance>()
            .HasMany(s => s.ContenuDans)
            .WithOne(r => r.Seance);
        // OneToMany vers Reservation
        modelBuilder.Entity<Seance>()
            .HasOne(s => s.Salle)
            .WithMany(s => s.ContenuDans)
            .HasForeignKey(seance => seance.fk_salle );
    
        
        
        
        
        // Propriétés de la table Paiement
        // Clé primaire
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Paiement>()
            .HasKey(p => p.id_paiement);
        //One to one
        modelBuilder.Entity<Paiement>()
            .HasOne(p => p.Reservation)
            .WithOne(p =>p.fk_paiement)
            .HasForeignKey<Reservation>(r => r.id_reservation)
            .IsRequired();
        
            
            
            
            
        
        //Propriété de la table reservation
        //Clé primaire
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Reservation>()
            .HasKey(r => r.id_reservation);
        modelBuilder.Entity<Reservation>()
            .Ignore(r => r.Utilisateur);
        //One to one vers paiement
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.fk_paiement)
            .WithOne(r => r.Reservation);
            
        //Many to one
        modelBuilder.Entity<Reservation>()
            .HasMany(r=>r.ContientDans)
            .WithOne(p => p.Reservation);
        
        //One to Many vers Séances
        modelBuilder.Entity<Reservation>()
            .HasOne(r =>r.Seance)
            .WithMany(s => s.ContenuDans)
            .HasForeignKey(s =>s.fk_seance);
        
        
        
        //Propriété de la table salle
        //Clé primaire
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Salle>()
            .HasKey(s=>s.id_salle);
        //Many to one vers Place
        modelBuilder.Entity<Salle>()
            .HasMany(s =>s.PresenteDans)
            .WithOne(p => p.FaitPartie);
        //Many to one vers Seance 
        modelBuilder.Entity<Salle>()
            .HasMany(s =>s.ContenuDans)
            .WithOne(s =>s.Salle);
        
        
        //Propriété de la table place
        //Clé primaire
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Place>()
            .HasKey(p => p.id_place);
        //One to many vers Reservation
        modelBuilder.Entity<Place>()
            .HasOne(r => r.FaitPartie)
            .WithMany(r => r.PresenteDans)
            .HasForeignKey(s => s.fk_salle);

        modelBuilder.Entity<Place>()
            .HasOne(r => r.Reservation)
            .WithMany(p => p.ContientDans)
            .HasForeignKey(r => r.fk_reservation);
        //One to many vers Salle
        modelBuilder.Entity<Place>()
            .HasOne(p =>p.FaitPartie)
            .WithMany(s=>s.PresenteDans);
    }
        public DbSet<Film?> Films { get; set;  }
        public DbSet<Reservation?> Reservations { get; set; }
        
        public DbSet<Salle?> Salles { get; set; }
        
        public DbSet <ScrynUser>? Utilisateur { get; set; }
        public DbSet <Tarif>? Tarifs { get; set; }

        public DbSet<ScrynRole>? Roles { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Seance> Seance { get; set; }
        public DbSet<Paiement> Paiement { get; set; }
}
