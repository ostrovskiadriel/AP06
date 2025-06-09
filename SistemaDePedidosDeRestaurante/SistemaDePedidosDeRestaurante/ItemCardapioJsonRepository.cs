using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

public class ItemCardapioJsonRepository : IRepository<ItemCardapio>
{
    private const string Arquivo = "cardapio.json";
    private List<ItemCardapio> itens;

    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers =
            {
                ti =>
                {
                    if (ti.Type == typeof(ItemCardapio))
                    {
                        ti.PolymorphismOptions = new JsonPolymorphismOptions
                        {
                            TypeDiscriminatorPropertyName = "$type",
                            IgnoreUnrecognizedTypeDiscriminators = true,
                            DerivedTypes =
                            {
                                new JsonDerivedType(typeof(Prato), "prato"),
                                new JsonDerivedType(typeof(Bebida), "bebida")
                            }
                        };
                    }
                }
            }
        }
    };

    public ItemCardapioJsonRepository()
    {
        itens = Carregar();
    }

    public void Adicionar(ItemCardapio item)
    {
        itens.Add(item);
        Salvar();
    }

    public ItemCardapio? ObterPorId(Guid id)
    {
        return itens.FirstOrDefault(i => i.Id == id);
    }

    public List<ItemCardapio> ObterTodos()
    {
        return itens;
    }

    public void Atualizar(ItemCardapio item)
    {
        var index = itens.FindIndex(i => i.Id == item.Id);
        if (index != -1)
        {
            itens[index] = item;
            Salvar();
        }
    }

    public bool Remover(Guid id)
    {
        var item = ObterPorId(id);
        if (item != null)
        {
            itens.Remove(item);
            Salvar();
            return true;
        }
        return false;
    }

    private List<ItemCardapio> Carregar()
    {
        if (!File.Exists(Arquivo)) return new List<ItemCardapio>();
        var json = File.ReadAllText(Arquivo);
        return JsonSerializer.Deserialize<List<ItemCardapio>>(json, _options) ?? new List<ItemCardapio>();
    }

    private void Salvar()
    {
        var json = JsonSerializer.Serialize(itens, _options);
        File.WriteAllText(Arquivo, json);
    }
}