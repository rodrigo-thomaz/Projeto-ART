namespace RThomaz.Domain.Financeiro.Services.DTOs.Helpers
{
    public class CountDTO<T>
    {
        private readonly T _dto;
        private readonly long _count;

        public CountDTO(T dto, long count)
        {
            _dto = dto;
            _count = count;
        }

        public T DTO { get { return _dto; } }
        public long Count { get { return _count; } }
    }
}
