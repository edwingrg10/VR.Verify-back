namespace VF.Verify.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CompanyCountry> CompanyCountries { get; set; } = new List<CompanyCountry>(); // relacion uno a muchos con CompanyCountry
    }
}
