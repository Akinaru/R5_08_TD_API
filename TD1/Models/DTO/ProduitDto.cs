namespace TD1.Models.DTO
{
    public class ProduitDto
    {
        public int IdProduit { get; set; }
        public string? NomProduit { get; set; }
        public string? Type { get; set; }
        public string? Marque { get; set; }
        public string? Description { get; set; }

        public string? UriPhoto { get; set; }
    }
}
