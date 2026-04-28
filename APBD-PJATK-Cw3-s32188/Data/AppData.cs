using APBD_PJATK_Cw3_s32188.Models;

namespace APBD_PJATK_Cw3_s32188.Data;

public class AppData
{
    public static List<Room> Rooms { get; set; } = new List<Room>
    {
        new Room
        {
            Id = 1,
            Name = "Lab 101",
            BuildingCode = "A",
            Floor = 1,
            Capacity = 20,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 2,
            Name = "Lab 101",
            BuildingCode = "A",
            Floor = 1,
            Capacity = 20,
            HasProjector = false,
            IsActive = false
        },
        new Room
        {
            Id = 3,
            Name = "Lab 201",
            BuildingCode = "B",
            Floor = 2,
            Capacity = 24,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 4,
            Name = "Lab 202",
            BuildingCode = "B",
            Floor = 2,
            Capacity = 24,
            HasProjector = true,
            IsActive = true
        }
    };

    public static List<Reservation> Reservations { get; set; } = new List<Reservation>
    {
        new Reservation
        {
            Id = 1,
            RoomId = 2,
            OrganizerName = "Jan Kowalski",
            Topic = "Warsztaty z C#",
            Date = new DateOnly(2026, 5, 8),
            StartTime = new TimeOnly(10, 0),
            EndTime = new TimeOnly(12, 30),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 2,
            RoomId = 2,
            OrganizerName = "Jan Kowalski",
            Topic = "Warsztaty z ASP.NET",
            Date = new DateOnly(2026, 5, 9),
            StartTime = new TimeOnly(10, 0),
            EndTime = new TimeOnly(12, 30),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 3,
            RoomId = 1,
            OrganizerName = "Anna Nowak",
            Topic = "Warsztaty o zarządzaniu projektem",
            Date = new DateOnly(2026, 5, 11),
            StartTime = new TimeOnly(8, 30),
            EndTime = new TimeOnly(10, 00),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 4,
            RoomId = 1,
            OrganizerName = "Anna Nowak",
            Topic = "Warsztaty o zarządzaniu zespołem",
            Date = new DateOnly(2026, 5, 12),
            StartTime = new TimeOnly(8, 30),
            EndTime = new TimeOnly(10, 00),
            Status = "planned"
        }
    };
}