
using Mily.Wind.SugarContext;
using Mily.Wind.SugarEntity.System;
using System;

namespace Mily.Wind.Logic
{
    public class MainLogic : MilyContext, IMainLogic
    {
        public void Test()
        {
            var u = Context().Queryable<MilyUser>().First();

            XExten.Advance.CacheFramework.Caches.RedisCacheSet(u.Id.ToString(), u);

            MilyUser user = new MilyUser();
            user.EncryptPassword = "2";
            user.Password = "2";
            user.Name = "2";
            user.TenantId = 2;

            base.Insert(user);
        }
    }
}
