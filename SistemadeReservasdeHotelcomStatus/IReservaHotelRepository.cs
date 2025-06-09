public interface IReservaHotelRepository : IRepository<ReservaHotel>
{
    IEnumerable<ReservaHotel> ObterPorStatus(StatusReserva status);
}