namespace OneCatalog.ElasticEngine.Sample.Models.Test
{
    public class Company : Document
    {
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
