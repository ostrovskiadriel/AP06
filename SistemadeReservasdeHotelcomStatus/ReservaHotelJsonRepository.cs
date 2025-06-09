public class ReservaHotelJsonRepository : GenericJsonRepository<ReservaHotel>, IReservaHotelRepository
{
    public ReservaHotelJsonRepository() : base("reservas.json") { }

    public IEnumerable<ReservaHotel> ObterPorStatus(StatusReserva status)
    {
        return ObterTodos().Where(r => r.Status == status);
    }
}