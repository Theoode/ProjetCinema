using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using ScrynDomain.Entities;

namespace ScrynDataProvider.Entities;
public class ScrynUser: IdentityUser, IUtilisateur
{
    
}