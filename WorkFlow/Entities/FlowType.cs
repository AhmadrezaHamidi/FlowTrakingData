namespace WorkFlow.Entities
{


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

        public int Period { get; set; }
    }

}
