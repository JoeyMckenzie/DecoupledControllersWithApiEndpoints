namespace DecoupledControllersWithApiEndpoints.Features.Beers.Models
{
    public class CreateBeerDto
    {
        public string? Name { get; set; }

        public string? Style { get; set; }

        public decimal Abv { get; set; }

        public int Ibu { get; set; }
    }
}
