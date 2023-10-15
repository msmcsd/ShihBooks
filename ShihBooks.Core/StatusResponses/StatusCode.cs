namespace ShihBooks.Core.StatusResponses
{
    public enum StatusCode
    {
        Success,
        EntityInUse,
        EntityExists,
        EntityNotFound,
        InvalidEntityName,
        InvalidEntity,
        IncomeNotFound,
        ExpenseNotFound,
        Error
    }

    public class StatusResponse
    {
        private readonly StatusCode _status;
        private readonly string _error;

        public StatusCode Status => _status;

        public string Error => _error;

        public bool IsSuccess => _status != StatusCode.Error && 
                                 _status != StatusCode.InvalidEntityName &&
                                 _status != StatusCode.InvalidEntity;

        public StatusResponse(StatusCode status)
        {
            _status = status;
            _error = null;
        }

        public StatusResponse(StatusCode status, string error)
        {
            _status = status;
            _error = error;
        }
    }
}
