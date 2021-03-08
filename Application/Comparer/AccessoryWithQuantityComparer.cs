using Application.DTOs;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Application.Comparer
{
    public class AccessoryWithQuantityComparer: IEqualityComparer<AccessoryWithQuantityDto>
    {
        public bool Equals([AllowNull] AccessoryWithQuantityDto x, [AllowNull] AccessoryWithQuantityDto y)
        {
            if (x.Id == y.Id)
                return true;


            return false;
        }

        public int GetHashCode([DisallowNull] AccessoryWithQuantityDto user)
        {
            int hashId = user.Id.GetHashCode();

            return hashId;
        }
    }
}
