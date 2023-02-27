namespace WorkFlow.Entities
{
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
            var DefaltInstance = new FlowType("DefaltInstance", "پیش فرض ", 0);
            _flowTypes.Add("0", DefaltInstance);
            var CardTransferActionInstance = new FlowType( "CardTransferAction", "کارت به کارت", 10);
            _flowTypes.Add("1", CardTransferActionInstance);
            var CardBalanceInstance = new FlowType("CardBalance", "مانده کارت", 2);
            _flowTypes.Add("2", CardBalanceInstance);
            var CardCirculationInstance = new FlowType("CardCirculation", "گردش", 3);
            _flowTypes.Add("3", CardCirculationInstance);
        }

        private readonly Dictionary<string, FlowType> _flowTypes = new();

        private static readonly Lazy<FlowTypeCollection> lazy = new(() => new FlowTypeCollection());

        public int Count
        { get { return _flowTypes.Count; } }

        public FlowType GetById(string id)
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

    }

}
