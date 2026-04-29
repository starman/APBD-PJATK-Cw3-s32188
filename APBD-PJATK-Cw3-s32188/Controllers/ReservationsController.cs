using APBD_PJATK_Cw3_s32188.Data;
using APBD_PJATK_Cw3_s32188.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_PJATK_Cw3_s32188.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        IEnumerable<Reservation> reservations = AppData.Reservations;

        if (date.HasValue)
            reservations = reservations.Where(r => r.Date == date.Value);

        if (!string.IsNullOrEmpty(status))
            reservations = reservations.Where(r => r.Status == status);

        if (roomId.HasValue)
            reservations = reservations.Where(r => r.RoomId == roomId.Value);

        return Ok(reservations);
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(x => x.Id == id);

        return reservation is null 
            ? NotFound("Reservation not found") 
            : Ok(reservation);
    }
    
    [HttpPost]
    public IActionResult Add([FromBody] Reservation reservation)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);

        if (room == null)
            return NotFound("Room does not exist");

        if (!room.IsActive)
            return Conflict("Room is not active");

        if (reservation.EndTime <= reservation.StartTime)
            return BadRequest("EndTime must be later than StartTime");

        var conflict = AppData.Reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Date == reservation.Date &&
            reservation.StartTime < r.EndTime &&
            reservation.EndTime > r.StartTime);

        if (conflict)
            return Conflict("Reservation time conflict");

        reservation.Id = AppData.Reservations.Any()
            ? AppData.Reservations.Max(r => r.Id) + 1
            : 1;

        AppData.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }
    
    [HttpPut("{id:int}")]
    public IActionResult Update(
        [FromRoute] int id, 
        [FromBody] Reservation updated)
    {
        var res = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (res == null)
            return NotFound();

        if (updated.EndTime <= updated.StartTime)
            return BadRequest("EndTime must be later than StartTime");

        var room = AppData.Rooms.FirstOrDefault(r => r.Id == updated.RoomId);

        if (room == null)
            return NotFound("Room does not exist");

        if (!room.IsActive)
            return Conflict("Room is not active");

        var conflict = AppData.Reservations.Any(r =>
            r.Id != id &&
            r.RoomId == updated.RoomId &&
            r.Date == updated.Date &&
            updated.StartTime < r.EndTime &&
            updated.EndTime > r.StartTime);

        if (conflict)
            return Conflict("Reservation time conflict");

        res.RoomId = updated.RoomId;
        res.OrganizerName = updated.OrganizerName;
        res.Topic = updated.Topic;
        res.Date = updated.Date;
        res.StartTime = updated.StartTime;
        res.EndTime = updated.EndTime;
        res.Status = updated.Status;

        return Ok(res);
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var res = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (res == null)
            return NotFound();

        AppData.Reservations.Remove(res);

        return NoContent();
    }
}