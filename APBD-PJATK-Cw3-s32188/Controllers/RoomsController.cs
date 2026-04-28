using APBD_PJATK_Cw3_s32188.Data;
using APBD_PJATK_Cw3_s32188.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_PJATK_Cw3_s32188.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        IEnumerable<Room> rooms = AppData.Rooms;

        if (minCapacity.HasValue)
            rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);

        if (hasProjector.HasValue)
            rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);

        if (activeOnly == true)
            rooms = rooms.Where(r => r.IsActive);

        return Ok(rooms);
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var room = AppData.Rooms.FirstOrDefault(x => x.Id == id);

        return room is null 
            ? NotFound("Room not found") 
            : Ok(room);
    }
    
    [HttpGet("{buildingCode}")]
    public IActionResult GetById([FromRoute] string buildingCode)
    {
        var rooms = AppData.Rooms
            .Where(r => r.BuildingCode == buildingCode);

        return Ok(rooms);
    }
    
    [HttpPost]
    public IActionResult Add([FromBody] Room room)
    {
        room.Id = AppData.Rooms.Any()
            ? AppData.Rooms.Max(r => r.Id) + 1
            : 1;
        
        AppData.Rooms.Add(room);

        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }
    
    [HttpPut("{id:int}")]
    public IActionResult Update(
        [FromRoute] int id, 
        [FromBody] Room updated)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
            return NotFound();
        
        room.Name = updated.Name;
        room.BuildingCode = updated.BuildingCode;
        room.Floor = updated.Floor;
        room.Capacity = updated.Capacity;
        room.HasProjector = updated.HasProjector;
        room.IsActive = updated.IsActive;

        return Ok(room);
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
            return NotFound();
        
        if (AppData.Reservations.Any(r => r.RoomId == id))
            return Conflict("Cannot delete room with reservations");

        AppData.Rooms.Remove(room);

        return NoContent();
    }
}