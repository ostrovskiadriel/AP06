public class CursoOnline : IEntidade
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Instrutor { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public TimeSpan Duracao { get; set; }
}