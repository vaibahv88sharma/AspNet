﻿using System.Collections.Generic;

namespace Library.API.Services
{   //  Dynamic Property Mapping from AuthorDTO to Entity.Author for sorting
    public interface IPropertyMappingService
    {
        bool ValidMappingExistsFor<TSource, TDestination>(string fields);
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
    }
}
