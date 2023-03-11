namespace Logging
{
    public class DeleteDateConfig
    {
        private readonly LogBuilder _builder;
        private readonly Action<int> _action;

        public DeleteDateConfig(LogBuilder builder, Action<int> action)
        {
            _builder = builder;
            _action = action;
        }

        public LogBuilder Day(int day)
        {
            _action(day);
            return _builder;
        }

        public LogBuilder Week(int count = 1)
        {
            _action(7 * count);
            return _builder;
        }

        public LogBuilder Mounth(int count = 1)
        {
            _action(30 * count);
            return _builder;
        }
    }
}