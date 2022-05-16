namespace Experian.CreditCards.Repo;

using Experian.CreditCards.Models;
using Microsoft.EntityFrameworkCore;

public class CreditCardContext : DbContext
{
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<AssessedApplicant> AssesssedApplicants { get; set; }
    public DbSet<Card> Cards { get; set; }

    public CreditCardContext(DbContextOptions<CreditCardContext> options)
       : base(options)
    { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>().HasData(
                    new Card { Id=1, Name = "Barclaycard", Apr = 10, Promo = "Super Barclaycard promo", RuleName = "Barclaycard" },
                    new Card { Id=2, Name = "Vanquis", Apr = 20, Promo = "Ace Vanquis promo", RuleName = "Vanquis" }
                );
    }
}
