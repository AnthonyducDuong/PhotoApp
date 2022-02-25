using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Entities
{
    public class RoleEntity : IdentityRole<Guid>
    {
        /*[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "RoleId is invalid")]
        public int Id { get; init; }

        [Required(ErrorMessage = "Type is invalid")]
        public string? Type { get; set; }

        // Relationship
        public ICollection<UserEntity>? userEntities { get; set; }*/
    }
}
