using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositoryEfCore.Entities;

namespace WorkFlow.Entities
{
    public class StatusType : EntityBase
    {
        public StatusType( string name, string title)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Title = title;
        }

        public string Name { get; set; }
        public string Title { get; set; }
    }

    public class StatusTypeEntityTypeConfiguration : BaseEntityTypeConfiguration<StatusType>
    {

        public void Configure(EntityTypeBuilder<StatusType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).HasDefaultValue("getDate()");
            builder.Property(x => x.UpdatedAt).HasDefaultValue(null);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Title).IsRequired();

            builder.ToTable($"tbl{nameof(StatusType)}");
        }
    }


    public class StatusTypeCollection
    {
        public StatusTypeCollection()
        {
            CreateFlowTypeCollection();
        }


        public static StatusTypeCollection Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public void CreateFlowTypeCollection()
        {
            var DefaltInstance = new StatusType("DefaltInstance", "پیش فرض ");
            _flowTypes.Add("0", DefaltInstance);
            var successُStatus = new StatusType("Success", "موفقیت امیز ");
            _flowTypes.Add("10", successُStatus);
            var retryStatus = new StatusType("Retry", "امتحان مجدد");
            _flowTypes.Add("20", retryStatus);
            var failedInstance = new StatusType("Failed", "ناموفق");
            _flowTypes.Add("30", failedInstance);
            var finalnstance = new StatusType("Final", "مرحله پایانی ");
            _flowTypes.Add("50", failedInstance);
            var initnstance = new StatusType("Init", "امادکی  ");
            _flowTypes.Add("40", failedInstance);
        }

        private readonly Dictionary<string, StatusType> _flowTypes = new();
        private static readonly Lazy<StatusTypeCollection> lazy = new(() => new StatusTypeCollection());

        public int Count
        { get { return _flowTypes.Count; } }


        public StatusType GetById(string id)
        {
            var flowType = _flowTypes.Values.FirstOrDefault(x => x.Id.Equals(id));
            return flowType;
        }
        public StatusType GetByName(string name)
        {
            var flowType = _flowTypes.Values.FirstOrDefault(x => x.Name == name);
            return flowType;
        }


        public StatusType this[string FlowType]
        {
            get
            {
                try
                {
                    return _flowTypes[FlowType];
                }
                catch
                {
                    return _flowTypes["0"];
                }
            }
        }

        


    }
}
