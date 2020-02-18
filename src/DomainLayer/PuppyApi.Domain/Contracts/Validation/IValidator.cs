namespace PuppyApi.Domain.Contracts.Validation
{
    public interface IValidator<TEntity>
    {
        bool IsValid(TEntity entity);
    }
}
