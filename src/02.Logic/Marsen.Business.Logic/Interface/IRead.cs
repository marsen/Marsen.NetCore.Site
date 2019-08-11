namespace Marsen.Business.Logic.Interface
{
    public interface IRead<T>
    {
        T Read(long id);
    }
}
