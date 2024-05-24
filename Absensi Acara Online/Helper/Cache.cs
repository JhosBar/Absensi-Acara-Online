using Microsoft.Extensions.Caching.Memory;
using Absensi.Services;
using Absensi.Services.Interface;
using Absensi.Services.Base;


namespace Absensi.Helper
{
    public class Cache
    {
        //    public static async Task<List<PermissionGetRole>> CachePerms(HttpContext HttpContext)
        //    {
        //        var MemCache = HttpContext.RequestServices.GetService<IMemoryCache>();
        //        var CacheKey = "Permission";

        //        var Cache = (List<PermissionGetRole>)MemCache.Get(CacheKey);

        //        if (Cache == null)
        //        { 
        //            IPermissionService PermissionService = new PermissionService();
        //            var List = PermissionService.GetRolePermission(0);

        //            MemCache.Set(CacheKey, List, new MemoryCacheEntryOptions()
        //            {
        //                SlidingExpiration = TimeSpan.FromMinutes(60)
        //            });
        //            return List;
        //        }

        //        return Cache;
        //    }
    }
}
