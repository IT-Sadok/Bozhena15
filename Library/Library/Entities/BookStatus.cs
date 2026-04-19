using System.Runtime.Serialization;

namespace Library.Entities;

public enum BookStatus
{
    [EnumMember(Value = "Booked")]
    Booked,
    
    [EnumMember(Value = "Free")]
    Free
} 