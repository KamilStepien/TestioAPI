using TestioAPI.Extensions.Logger;

namespace TestioAPI.Extensions.Results
{
    public class DataResult<T> : Result 
    {
        private readonly T _data;

        private DataResult(T data, ResultStatus status, string message = "") :base(status, message)
        {
            _data = data;
        }

        public static DataResult<T> Succes(T data)
        {
            return new DataResult<T>(data, ResultStatus.Succes);
        }

        public new static DataResult<T> Error(string message)
        {
            return new DataResult<T>(default(T), ResultStatus.Error, message);
        }

        public new static DataResult<T> Warning(string message)
        {
            return new DataResult<T>(default(T), ResultStatus.Warning, message);
        }

        public T Data => _data;
        public override bool IsSucces => base.IsSucces && Data != null;
        public override bool IsNotSucces => !IsSucces;

        public override DataResult<T> Log()
        {
            LoggerLog();
            return this;
        }

    }
}
