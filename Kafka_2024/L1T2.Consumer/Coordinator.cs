using L1T2.Signal;

namespace L1T2.Consumer
{
    public static class Coordinator
    {
        private static readonly Dictionary<string, TaxiData> _dict;

        static Coordinator()
        {
            _dict = new Dictionary<string, TaxiData>();
        }

        public static double Push(SignalDto dto)
        {
            double tmpRange = 0;

            if (_dict.TryGetValue(dto.Id.ToString(), out TaxiData value))
            {
                value.X = dto.X;
                value.Y = dto.Y;
                value.Range += Math.Sqrt(Math.Pow(value.X - dto.X, 2) + Math.Pow(value.Y - dto.Y, 2));
                tmpRange = value.Range;
            }
            else
            {
                tmpRange = Math.Sqrt(dto.X * dto.X + dto.Y * dto.Y);

                _dict.Add(dto.Id.ToString(), new TaxiData()
                {
                    Id = dto.Id,
                    X = dto.X,
                    Y = dto.Y,
                    Range = tmpRange,
                });
            }

            return tmpRange;
        }
    }
}
