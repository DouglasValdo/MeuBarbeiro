using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("Users", Schema = "Data")]
[Index("Id", Name = "UQ__Users__3214EC066A06C23C", IsUnique = true)]
public partial class User
{
    [Key]
    public Guid Id { get; set; }

    public string FullName { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public int PhoneNumber { get; set; }
    
    [InverseProperty("User")]
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
