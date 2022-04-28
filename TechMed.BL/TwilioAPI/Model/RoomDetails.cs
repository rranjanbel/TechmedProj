namespace TechMed.BL.TwilioAPI.Model
{
    public record RoomDetails(
    string Id,
    string Name,
    int ParticipantCount,
    int MaxParticipants);
}
