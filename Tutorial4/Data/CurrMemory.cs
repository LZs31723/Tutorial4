using Tutorial4.Models;

namespace Tutorial4.Data;

public static class CurrMemory
{
    public static List<Room> Rooms { get; } = new()
    {
        new Room()
        {
            Id = 1, Name = "Room A", BuildingCode = "B1", Floor = 1, Capacity = 10, HasProjector = true, IsActive = true
        },
        new Room()
        {
            Id = 2, Name = "Room B", BuildingCode = "B1", Floor = 2, Capacity = 20, HasProjector = false,
            IsActive = true
        },
        new Room()
        {
            Id = 3, Name = "Room C", BuildingCode = "B2", Floor = 1, Capacity = 15, HasProjector = true,
            IsActive = false
        },
        new Room()
        {
            Id = 4, Name = "Room D", BuildingCode = "C1", Floor = 1, Capacity = 20, HasProjector = true,
            IsActive = false
        }

    };

    public static List<Reservation> Reservations { get; } = new()
    {
        new Reservation
        {
            Id = 1, RoomId = 1, OrganizerName = "Alice", Topic = "Project Update",
            Date = new DateOnly(2024, 7, 1),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(10, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 2, RoomId = 2, OrganizerName = "Bob", Topic = "Team Meeting",
            Date = new DateOnly(2024, 7, 1),
            StartTime = new TimeOnly(11, 0),
            EndTime = new TimeOnly(12, 0),
            Status = "planned"
        },
        new Reservation
        {
            Id = 3, RoomId = 3, OrganizerName = "Andrew", Topic = "Team Meeting 2",
            Date = new DateOnly(2025, 7, 1),
            StartTime = new TimeOnly(11, 0),
            EndTime = new TimeOnly(12, 0),
            Status = "planned"
        },
        new Reservation
        {
            Id = 4, RoomId = 4, OrganizerName = "Andrew", Topic = "Team Meeting 3",
            Date = new DateOnly(2025, 7, 1),
            StartTime = new TimeOnly(11, 0),
            EndTime = new TimeOnly(12, 0),
            Status = "planned"
        }
    };
}