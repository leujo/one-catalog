namespace OneCatalog.ElasticEngine.Sample.Models
{
    public sealed class Product
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = default!;

        public IEnumerable<Category> Categories { get; init; } = default!;

        public IDictionary<string, string> Properties { get; init; } = default!;
    }

    public sealed class Category
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = default!;

        public IEnumerable<Category> Childs { get; init; } = default!;
    }

    public sealed class FilterGroup
    {
        public Guid Id { get; init; }

        // public IEnumerable<Filter> Filters { get; init; }
        public IDictionary<string, string> Filters { get; init; } = default!;
    }

    public sealed record Filter
    {
        public string Key { get; init; } = default!;

        public string Value { get; init; } = default!;
    }
}
