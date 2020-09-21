namespace DecoupledControllersWithApiEndpoints.Features.Beers.Models
{
    public class CreateBeerDto
    {
        public string? Name { get; set; }

        public BeerStyle Style { get; set; }

        public decimal Abv { get; set; }

        public int Ibu { get; set; }
    }
}
