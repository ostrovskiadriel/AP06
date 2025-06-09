public abstract class ItemCardapio : IEntidade
{
    public Guid Id { get; set; }
    public string NomeItem { get; set; } = string.Empty;
    public decimal Preco { get; set; }
}