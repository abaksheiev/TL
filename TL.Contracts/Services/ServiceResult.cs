namespace TL.Contracts.Services
{
    /// <summary>
    /// Communication data transfer object for services
    /// </summary>
    /// <typeparam name="TEntity">Type of data object</typeparam>
    public class ServiceResult<TEntity>
    {
        public bool Success { get; private set; }
        public string ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }
        public TEntity Data { get; private set; }

        private ServiceResult(bool success, string errorCode, string errorMessage, TEntity data)
        {
            Success = success;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Data = data;
        }

        public static ServiceResult<TEntity> BuildSuccess(TEntity data)
        {
            return new ServiceResult<TEntity>(true, null, null, data);
        }

        public static ServiceResult<TEntity> BuildError(string errorMessage)
        {
            return new ServiceResult<TEntity>(false, null, errorMessage, default);
        }

        public static ServiceResult<TEntity> BuildError(string errorCode, string errorMessage)
        {
            return new ServiceResult<TEntity>(false, errorCode, errorMessage, default);
        }
    }
}
