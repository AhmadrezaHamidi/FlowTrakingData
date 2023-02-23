
namespace Houshmand.Framework.WorkFlow.Entities
{
    #region flow

    [Table("Flow")]
    public class Flow
    {
        private Version _version = new("0.0.1");

        public Flow(string uniqeId, DateTime createdAt,
            Version version, int userId,
            int flowTypeId, string flowTypeName,
            string flowTypeTitle)
        {
            UniqeId = uniqeId;
            CreatedAt = createdAt;
            Version = version;
            UserId = userId;
            FlowTypeId = flowTypeId;
            FlowTypeName = flowTypeName;
            FlowTypeTitle = flowTypeTitle;
        }

        [Column("Id")]
        public int Id { get; set; }

        public string UniqeId { get; set; } 


        public DateTime CreatedAt { get; set; }

        [Column("Version")]
        public string VersionName { get; private set; } = "0.0.1";

        [NotMapped]
        public Version Version
        {
            get { return _version; }
            set
            {
                _version = value;
                VersionName = _version.ToString();
            }
        }

        public int UserId { get; set; }

        public int FlowTypeId { get; set; }

        public string FlowTypeName { get; set; }

        public string FlowTypeTitle { get; set; }
    }

    #endregion flow

    #region FlowType

    public class FlowType
    {
        public FlowType(int id, string name, string title, int period)
        {
            Id = id;
            Name = name;
            Title = title;
            Period = period;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// minutes
        /// </summary>
        public int Period { get; set; }
    }

    #endregion FlowType

    #region FlowTypeCollection

    public class FlowTypeCollection
    {
        public FlowTypeCollection()
        {
            CreateFlowTypeCollection();
        }

        public static FlowTypeCollection Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public void CreateFlowTypeCollection()
        {
            var DefaltInstance = new FlowType(0, "DefaltInstance", "پیش فرض ", 0);
            _flowTypes.Add("0", DefaltInstance);
            var CardTransferActionInstance = new FlowType(10, "CardTransferAction", "کارت به کارت", 10);
            _flowTypes.Add("1", CardTransferActionInstance);
            var CardBalanceInstance = new FlowType(20, "CardBalance", "مانده کارت", 2);
            _flowTypes.Add("2", CardBalanceInstance);
            var CardCirculationInstance = new FlowType(30, "CardCirculation", "گردش", 3);
            _flowTypes.Add("3", CardCirculationInstance);
        }

        private readonly Dictionary<string, FlowType> _flowTypes = new();

        private static readonly Lazy<FlowTypeCollection> lazy = new(() => new FlowTypeCollection());

        public int Count
        { get { return _flowTypes.Count; } }

        public FlowType GetById(int id)
        {
            var flowType = _flowTypes.Values.FirstOrDefault(x => x.Id == id) ?? _flowTypes["0"];

            return flowType;
        }

        public FlowType this[string FlowType]
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

        public FlowType this[int typeId]
        {
            get
            {
                var flowType = _flowTypes.Values.FirstOrDefault(x => x.Id == typeId) ?? _flowTypes["0"];
                return flowType;
            }
        }
    }

    #endregion FlowTypeCollection
}