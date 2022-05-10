using Nest;

namespace OneCatalog.ElasticEngine.Sample.Models.Test
{
    public abstract class Document
    {
        public JoinField Join { get; set; }
    }
}
