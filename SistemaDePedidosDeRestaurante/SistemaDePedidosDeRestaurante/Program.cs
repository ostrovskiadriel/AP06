using System;

var repo = new ItemCardapioJsonRepository();

var prato = new Prato
{
    Id = Guid.NewGuid(),
    NomeItem = "Risoto de Cogumelos",
    Preco = 32.90m,
    DescricaoDetalhada = "Risoto cremoso com shitake, shimeji e parmesão",
    Vegetariano = true
};

var bebida = new Bebida
{
    Id = Guid.NewGuid(),
    NomeItem = "Água com Gás",
    Preco = 5.00m,
    VolumeMl = 500,
    Alcoolica = false
};

repo.Adicionar(prato);
repo.Adicionar(bebida);

Console.WriteLine("Itens do cardápio:");
foreach (var item in repo.ObterTodos())
{
    Console.WriteLine($"- {item.NomeItem} ({item.GetType().Name}) - R$ {item.Preco}");
}