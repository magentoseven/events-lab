namespace Events.Web.Models
{
    public interface IViewModel<T> where T : class
    {
        void FromModel(T model);
        void ToModel(T model);
    }
}
