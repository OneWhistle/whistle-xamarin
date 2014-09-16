
namespace Whistle.Core.Modal
{
    public class ServiceResult<TEntity>
    {
        public string RawJsonResponse { get; private set; }
        public TEntity Result { get; private set; }
        public bool HasError { get; private set; }
        public ErrorResponse Error { get; private set; }

        internal ServiceResult(TEntity result, string RawJson)
        {
            this.Result = result;
            this.RawJsonResponse = RawJson;
        }

        internal ServiceResult(ErrorResponse error, string rawJson)
        {
            HasError = true;
            this.Error = error;
            this.RawJsonResponse = rawJson;
        }
    }
}
