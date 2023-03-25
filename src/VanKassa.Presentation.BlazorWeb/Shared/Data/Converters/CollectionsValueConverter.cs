using System.Linq.Expressions;

namespace VanKassa.Presentation.BlazorWeb.Shared.Data.Converters
{
    public static class CollectionsValueConverter
    {
        public static string ConvertStringCollectionToSingleString(IList<string> stringCollection)
            => string.Join(", ", stringCollection); 
    }
}
