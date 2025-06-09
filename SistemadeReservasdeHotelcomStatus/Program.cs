var repo = new ReservaHotelJsonRepository();

var reserva1 = new ReservaHotel
{
    Id = Guid.NewGuid(),
    NomeHospede = "João da Silva",
    DataCheckIn = DateTime.Today,
    DataCheckOut = DateTime.Today.AddDays(3),
    Status = StatusReserva.Confirmada
};

var reserva2 = new ReservaHotel
{
    Id = Guid.NewGuid(),
    NomeHospede = "Maria Oliveira",
    DataCheckIn = DateTime.Today.AddDays(5),
    DataCheckOut = DateTime.Today.AddDays(7),
    Status = StatusReserva.Pendente
};

repo.Adicionar(reserva1);
repo.Adicionar(reserva2);

Console.WriteLine("Reservas Confirmadas:");
foreach (var r in repo.ObterPorStatus(StatusReserva.Confirmada))
{
    Console.WriteLine($"- {r.NomeHospede} | {r.DataCheckIn:dd/MM/yyyy} até {r.DataCheckOut:dd/MM/yyyy}");
}