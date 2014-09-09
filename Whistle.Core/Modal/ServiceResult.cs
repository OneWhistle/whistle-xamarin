
namespace Whistle.Core.Modal
{
    public class ServiceResult<TEntity>
    {
        public TEntity Result { get; private set; }
        public bool HasError { get; private set; }
        public ErrorResponse Error { get; private set; }

        internal ServiceResult(TEntity result)
        {
            this.Result = result;
        }

        internal ServiceResult(ErrorResponse error)
        {
            HasError = true;
            this.Error = error;
        }
    }
}
