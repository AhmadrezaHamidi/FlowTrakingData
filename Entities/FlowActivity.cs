namespace Houshmand.Framework.WorkFlow.Entities
{
    #region FlowActivity

    public class FlowActivity
    {
        public int Id { get; set; }

        public string UniqeId { get; set; }

        public int PervioseId { get; set; }

        public string PervioseUniqeId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string FlowId { get; set; }

        public string FlowUniqeId { get; set; }

        public string FlowActivityTypeId { get; set; }

        public string FlowActivityTypeName { get; set; }

        public string FlowActivityTypeTitle { get; set; }

        public int Priority { get; set; }
    }

    #endregion FlowActivity

    #region FlowActivityType

    public class FlowActivityType
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeTitle { get; set; }
        public string FlowTypeId { get; set; }

        public FlowActivityType(int typeId, string typeName, string typeTitle)
        {
            TypeId = typeId;
            TypeName = typeName;
            TypeTitle = typeTitle;
        }
    }

    #endregion FlowActivityType

    #region FlowTypeActivityCollection

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
            var defultStep = new FlowActivityType(0, "step0", "مقدار پیش فرض ");
            _flowTypectivities.Add("0", defultStep);
            var stepOne = new FlowActivityType(10, "step1", "استعلام کارت ");
            _flowTypectivities.Add("1", stepOne);
            var stepTwo = new FlowActivityType(20, "step2", "دریافت کارت مقضد ");
            _flowTypectivities.Add("2", stepTwo);
            var stepThree = new FlowActivityType(30, "step3", "دریافت");
            _flowTypectivities.Add("3", stepTwo);
            var stepFour = new FlowActivityType(40, "step4", "دریافت");
            _flowTypectivities.Add("4", stepTwo);
        }

        private readonly Dictionary<string, FlowActivityType> _flowTypectivities = new();
        private static readonly Lazy<FlowTypeCollection> lazy = new(() => new FlowTypeCollection());

        public int Count
        { get { return _flowTypectivities.Count; } }

        public FlowActivityType GetById(int typeId)
        {
            var flowType = _flowTypectivities.Values.FirstOrDefault(x => x.TypeId == typeId);
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

        public FlowActivityType this[int typeId]
        {
            get
            {
                var flowType = _flowTypectivities.Values.FirstOrDefault(x => x.TypeId == typeId);
                return flowType;
            }
        }
    }

    #endregion FlowTypeActivityCollection
}