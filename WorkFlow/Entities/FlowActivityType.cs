namespace WorkFlow.Entities
{
  

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


}
