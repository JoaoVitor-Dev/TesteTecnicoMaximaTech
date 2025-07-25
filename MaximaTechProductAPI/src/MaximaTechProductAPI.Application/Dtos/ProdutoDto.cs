namespace MaximaTechProductAPI.Application.Dtos
{
    public class ProdutoDto
    {
        public string Codigo { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public Guid DepartamentoId { get; set; }
        public decimal Preco { get; set; }
    }
}
