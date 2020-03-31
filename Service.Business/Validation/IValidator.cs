namespace BusinessLayer.Validation
{
    public interface IValidator<T>
    {
        bool IsValid ( T entity );
    }
}
