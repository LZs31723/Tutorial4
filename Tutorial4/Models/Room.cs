using System.ComponentModel.DataAnnotations;

namespace Tutorial4.Models;

public class Room
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Nazwa sali jest wymagana")]
    [StringLength(100, ErrorMessage = "nazwa sali moze miec max 100 znakow")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "kod budynku jest wymagany")]
    [StringLength(10, ErrorMessage = "kod budynku może miec max 10 znakw")]
    public string BuildingCode { get; set; }
    [Range(0,50, ErrorMessage = "Piętro musi b yc od 0 do 50")]
    public int Floor { get; set;} 
    [Range(1, int.MaxValue, ErrorMessage = "pojemnosc musi byc wiekza od 0")]
    public int Capacity { get; set; }
    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }

}