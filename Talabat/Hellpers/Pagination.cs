namespace Talabat.api.Hellpers
{
    public class Pagination<T>
    {


        public int? PadgeSize { get; set; }
        public int? Padgenum { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }

        public Pagination(int? padgeSize, int? padgenum, int count, IEnumerable<T> data)
        {
            PadgeSize = padgeSize;
            Padgenum = padgenum;
            Count = count;
            Data = data;
        }
    }
}
