namespace RThomaz.Application.Financeiro.Helpers
{
    public class CountModel<T>
    {
        private readonly T _model;
        private readonly long _count;

        public CountModel(T model, long count)
        {
            _model = model;
            _count = count;
        }

        public T Model { get { return _model; } }
        public long Count { get { return _count; } }
    }
}
