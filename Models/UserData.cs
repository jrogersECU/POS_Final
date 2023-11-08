using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace UserData;

[Table("Users")]
public class User
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Username { get; set; }
   
    [Required]
    [MaxLength(255)]
    public string Email { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
    
    [Required]
    public byte[] PasswordSalt { get; set; }

    [Required]
    [MaxLength(50)]
    public string Role { get; set; }
}

