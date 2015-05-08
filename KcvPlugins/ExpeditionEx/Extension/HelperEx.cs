using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.ExpeditionEx.Extension
{
    public static class HelperEx
    {
        /// <summary>
        /// 所以条件是否满足
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static bool ClaimsIsAccord<T>(this List<T> claims)
            where T : Model.Claim
        {
            if (claims == null) return false;

            foreach (var item in claims)
            {
                if (!item.IsAccord) return false;
            }
            return true;
        }
    }
}
