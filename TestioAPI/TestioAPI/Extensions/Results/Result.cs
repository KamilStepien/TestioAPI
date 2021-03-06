using TestioAPI.Extensions.Logger;

namespace TestioAPI.Extensions.Results
{
    public class Result
    {
        internal string _message;
        private ResultStatus _status;

        internal Result(ResultStatus status , string message = "")
        {
            _status = status;
            _message = message;
        }

        public static Result Succes()
        {
            return new Result(ResultStatus.Succes);
        }

        public static Result Error(string message)
        {
            return new Result(ResultStatus.Error, message);
        }

        public static Result Warning(string message)
        {
            return new Result(ResultStatus.Warning, message);
        }


        public ResultStatus Status => _status;
        public virtual bool IsSucces => Status == ResultStatus.Succes;
        public virtual bool IsNotSucces => Status != ResultStatus.Succes;

        public bool IsError => Status == ResultStatus.Error;

        public override string ToString()
        {
            return _message;
        }

        internal void LoggerLog()
        {
            switch (Status)
            {
                case ResultStatus.Warning:
                    TLogger.Log().Msc(_message).Warning();
                    break;
                case ResultStatus.Error:
                    TLogger.Log().Msc(_message).Error();
                    break;
            }
        }

        public virtual Result Log()
        {
            LoggerLog();
            return this;
        }

    }
}
