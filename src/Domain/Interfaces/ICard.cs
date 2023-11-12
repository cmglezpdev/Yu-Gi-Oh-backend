


using backend.Domain.Entities;

namespace backend.Domain.Interfaces{
    public interface ICard{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TypeCards TypeCard { get; set; }
        public string Type { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrlSmall { get; set; }
        public string ImageUrlCropped { get; set; }
        public ArchetypeDomain? Archetype { get; set; }
    }
}