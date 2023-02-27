namespace WorkFlow.Entities
{
    public class FlowTypeActivityCollection
    {
        public FlowTypeActivityCollection()
        {
            CreateFlowActivityTypeCollection();
        }

        public static FlowTypeCollection Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public void CreateFlowActivityTypeCollection()
        {
            var stepOne = new FlowActivityType("OriginCard", "مبدا  استعلام کارت ", 2);
            _flowTypectivities.Add("1", stepOne);
            var stepTwo = new FlowActivityType("Destination Card inquiry", "دریافت کارت مقضد ", 3);
            _flowTypectivities.Add("2", stepTwo);
            var stepThree = new FlowActivityType("CartToCart", "عملیات کارت به کارت ", 4);
            _flowTypectivities.Add("3", stepTwo);
            var stepFour = new FlowActivityType("Start", "شروع", 1);
            _flowTypectivities.Add("4", stepTwo);
            var stepfifth = new FlowActivityType("End", "پایان ", 5);
            _flowTypectivities.Add("5", stepTwo);
        }

        private readonly Dictionary<string, FlowActivityType> _flowTypectivities = new();
        private static readonly Lazy<FlowTypeCollection> lazy = new(() => new FlowTypeCollection());

        public int Count
        { get { return _flowTypectivities.Count; } }

        public FlowActivityType GetById(string typeId)
        {
            var flowType = _flowTypectivities.Values.FirstOrDefault(x => x.Id == typeId) ?? _flowTypectivities["0"];
            return flowType;
        }

        public FlowActivityType this[string flowActivity]
        {
            get
            {
                try
                {
                    return _flowTypectivities[flowActivity];
                }
                catch
                {
                    return _flowTypectivities["0"];
                }
            }
        }

    }

}
