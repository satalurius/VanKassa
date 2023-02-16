using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace VanKassa.Domain.Attributes;

public class MinimumCollectionLengthAttribute
{
    public class MinimumCollectionLength : ValidationAttribute
    {
        private readonly int _minimumCollectionLength;

        public MinimumCollectionLength(int minimumCollectionLength)
        {
            _minimumCollectionLength = minimumCollectionLength;
        }

        public override bool IsValid(object value)
        {
            var collection = value as ICollection;
            if (collection != null)
            {
                return collection.Count >= _minimumCollectionLength;
            }
            return false;
        }
    }
}