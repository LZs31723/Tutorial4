using Microsoft.AspNetCore.Mvc;
using Tutorial4.Data;
using Tutorial4.Models;

namespace Tutorial4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Room>> GetAll(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)

    {
        IEnumerable<Room> rooms = CurrMemory.Rooms;

        if (minCapacity.HasValue)
        
            rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);

            if (hasProjector.HasValue)            
                rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);

            if (activeOnly == true)                            
                rooms = rooms.Where(r => r.IsActive == true);
        
        return Ok(rooms.ToList());
    }

    [HttpGet("{id:int}")]
        public ActionResult<Room> GetById(int id)
        {
            var room = CurrMemory.Rooms.FirstOrDefault(r => r.Id == id);

            if (room is null)
            {
                return NotFound();
            }
            return Ok(room);
        }

    [HttpGet("building/{buildingCode}")]
    public ActionResult<IEnumerable<Room>> GetByBuildingCode(string buildingCode)
    {
        var room = CurrMemory.Rooms
            .Where(r => r.BuildingCode == buildingCode)
            .ToList();
        return Ok(room);
    }

    [HttpPost]
    public ActionResult<Room> Create([FromBody] Room room)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        room.Id = CurrMemory.Rooms.Any()
            ? CurrMemory.Rooms.Max(r => r.Id) + 1
            : 1;
        
        CurrMemory.Rooms.Add(room);
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Room> Update(int id, [FromBody] Room updateRoom)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var room  = CurrMemory.Rooms.FirstOrDefault(r => r.Id == id);
        if (room is null)
        {
            return NotFound();
        }
        room.Name = updateRoom.Name;
        room.BuildingCode = updateRoom.BuildingCode;
        room.Floor = updateRoom.Floor;
        room.Capacity = updateRoom.Capacity;
        room.HasProjector = updateRoom.HasProjector;
        room.IsActive = updateRoom.IsActive;

        return Ok(room);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var room = CurrMemory.Rooms.FirstOrDefault(r => r.Id == id);
        if (room is null)
        {
            return NotFound();
        }
        
        bool hasReservations = CurrMemory.Reservations.Any(r => r.RoomId == id);
        if (hasReservations)
        {
            return BadRequest("Nie można usunąć sali, która ma przypisane rezerwacje.");
        }

        CurrMemory.Rooms.Remove(room);
        return NoContent();
    }

}