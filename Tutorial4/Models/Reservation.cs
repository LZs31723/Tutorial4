using System.ComponentModel.DataAnnotations;

namespace Tutorial4.Models;

public class Reservation : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult("Czas zakończenia musi być późniejszy niż czas rozpoczęcia.",
                new[] { nameof(EndTime) });
        }
    }
    public int Id { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Id sali musi być większe od 0")]
    public int RoomId { get; set; }
    [Required(ErrorMessage = "imie i naziwksko organizatora musi byc podane")]
    [StringLength(100)]
    public string OrganizerName { get; set; }
    [Required(ErrorMessage = "Temat jest wymagany.")]
    [StringLength(200)]
    public string Topic { get; set; }
    [Required(ErrorMessage = "Data jest wymagana")]
    public DateOnly Date { get; set; }
    [Required(ErrorMessage = "Data startu jest wymagana")]
    public TimeOnly StartTime { get; set; }
    [Required(ErrorMessage = "Data startu jest wymagana")]
    public TimeOnly EndTime { get; set; }

    [Required(ErrorMessage = "Status jest wymagana")]
    [RegularExpression("^(planned|confirmed|cancelled)$",
        ErrorMessage = "Status musi mieć wartość: planned, confirmed lub cancelled.")]
    public string Status { get; set; } = "planned";
}