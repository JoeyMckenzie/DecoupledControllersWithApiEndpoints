using System;

namespace DecoupledControllersWithApiEndpoints.Features.Beers.Models
{
    public class Beer
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal Abv { get; set; }

        public int Ibu { get; set; }

        public BeerStyle Style { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
