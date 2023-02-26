using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entities
{
    #region FlowActivity

    public class FlowActivity
    {
        public FlowActivity(string uniqeId, int pervioseId,
            string pervioseUniqeId, DateTime createdAt,
            string flowUniqeId, int flowActivityTypeId, string flowActivityTypeName,
            string flowActivityTypeTitle, int priority)
        {
            UniqeId = uniqeId;
            PervioseId = pervioseId;
            PervioseUniqeId = pervioseUniqeId;
            CreatedAt = createdAt;
            FlowUniqeId = flowUniqeId;
            FlowActivityTypeId = flowActivityTypeId;
            FlowActivityTypeName = flowActivityTypeName;
            FlowActivityTypeTitle = flowActivityTypeTitle;
            Priority = priority;
        }

        public int Id { get; set; }

        public string UniqeId { get; set; }

        public int PervioseId { get; set; }

        public string PervioseUniqeId { get; set; }

        public DateTime CreatedAt { get; set; }

        public int FlowId { get; set; }

        public string FlowUniqeId { get; set; }

        public int FlowActivityTypeId { get; set; }

        public string FlowActivityTypeName { get; set; }

        public string FlowActivityTypeTitle { get; set; }

        public int Priority { get; set; }
    }

    #endregion FlowActivity

    #region FlowActivityType

    public class FlowActivityType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }

        public FlowActivityType(int id, string name, string title, int priority)
        {
            Id = id;
            Name = name;
            Title = title;
            Priority = priority;
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
            var stepOne = new FlowActivityType(20, "OriginCard", "مبدا  استعلام کارت ", 2);
            _flowTypectivities.Add("1", stepOne);
            var stepTwo = new FlowActivityType(30, "Destination Card inquiry", "دریافت کارت مقضد ", 3);
            _flowTypectivities.Add("2", stepTwo);
            var stepThree = new FlowActivityType(40, "CartToCart", "عملیات کارت به کارت ", 4);
            _flowTypectivities.Add("3", stepTwo);
            var stepFour = new FlowActivityType(10, "Start", "شروع", 1);
            _flowTypectivities.Add("4", stepTwo);
            var stepfifth = new FlowActivityType(50, "End", "پایان ", 5);
            _flowTypectivities.Add("5", stepTwo);
        }

        private readonly Dictionary<string, FlowActivityType> _flowTypectivities = new();
        private static readonly Lazy<FlowTypeCollection> lazy = new(() => new FlowTypeCollection());

        public int Count
        { get { return _flowTypectivities.Count; } }

        public FlowActivityType GetById(int typeId)
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

        public FlowActivityType this[int typeId]
        {
            get
            {
                var flowType = _flowTypectivities.Values.FirstOrDefault(x => x.Id == typeId) ?? _flowTypectivities["0"];
                return flowType;
            }
        }
    }

    #endregion FlowTypeActivityCollection

}
