public class ReservaHotel : IEntidade
{
    public Guid Id { get; set; }
    public string NomeHospede { get; set; } = string.Empty;
    public DateTime DataCheckIn { get; set; }
    public DateTime DataCheckOut { get; set; }
    public StatusReserva Status { get; set; }
}