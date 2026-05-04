using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Tutorial4.Data;
using Tutorial4.Models;

namespace Tutorial4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        IEnumerable<Reservation> reservations = CurrMemory.Reservations;

        if (date.HasValue)
        
            reservations = reservations.Where(r => r.Date == date.Value);
        
        if (!string.IsNullOrEmpty(status))
            reservations = reservations.Where(r => r.Status == status);
        if(roomId.HasValue)
            reservations = reservations.Where(r => r.RoomId == roomId.Value);
        
        return Ok(reservations);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> GetById(int id)
    {
        var reservation = CurrMemory.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult<Reservation> Create([FromBody] Reservation reservation)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var room = CurrMemory.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        if (room is null)
        {
            return NotFound($"Sala o Id={reservation.RoomId} nie istnieje.");
        }

        if (!room.IsActive)
        {
            return BadRequest($"Sala o Id={reservation.RoomId} jest nieaktywna.");
        }
        bool hasCol = CurrMemory.Reservations.Any(r => r.RoomId == reservation.RoomId&&
                                                       r.Date == reservation.Date &&
                                                       r.StartTime < reservation.EndTime&&
                                                       r.EndTime > reservation.StartTime);

        if (hasCol)
        {
            return Conflict("Termin koliduje z inną rezerwacja");
        }

        reservation.Id = CurrMemory.Reservations.Any()
            ? CurrMemory.Reservations.Max(r => r.Id) + 1
            : 1;
        
        CurrMemory.Reservations.Add(reservation);
        
        return CreatedAtAction(nameof(GetById), new {id = reservation.Id}, reservation);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Reservation> Update(int id, [FromBody] Reservation updatedReservation)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var reservation = CurrMemory.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation is null)
        {
            return NotFound();
        }
        
        var room = CurrMemory.Rooms.FirstOrDefault(r => r.Id == updatedReservation.RoomId);
        if (room is null)
        {
            return NotFound($"Sala o Id={updatedReservation.RoomId} nie istnieje.");
        }

        if (!room.IsActive)
        {
            return BadRequest($"Sala o Id={updatedReservation.RoomId} jest nieaktywna.");
        }
        bool hasCOl = CurrMemory.Reservations.Any(rr => rr.Id != id &&
                                                       rr.RoomId == updatedReservation.RoomId &&
                                                       rr.Date == updatedReservation.Date &&
                                                       rr.StartTime < updatedReservation.EndTime &&
                                                       rr.EndTime > updatedReservation.StartTime);

        if (hasCOl)
        {
            return Conflict("Termin koliduje z inną rezerwacja");
        }
        reservation.RoomId = updatedReservation.RoomId;
        reservation.OrganizerName = updatedReservation.OrganizerName;
        reservation.Topic = updatedReservation.Topic;
        reservation.Date = updatedReservation.Date;
        reservation.StartTime = updatedReservation.StartTime;
        reservation.EndTime = updatedReservation.EndTime;
        reservation.Status = updatedReservation.Status;
        
        return Ok(reservation);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var reservation = CurrMemory.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation is null)
        {
            return NotFound();
        }
        CurrMemory.Reservations.Remove(reservation);
        
        return NoContent();
    }
}