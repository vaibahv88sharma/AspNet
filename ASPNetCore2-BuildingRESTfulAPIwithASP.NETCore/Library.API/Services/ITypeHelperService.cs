namespace Library.API.Services
{   //  Dynamically Mapping AuthorDTO properties to Entity.Author for Data Shaping on the Controller Actions that do not support Dynamic Sorting
    public interface ITypeHelperService
    {
        bool TypeHasProperties<T>(string fields);
    }
}
