
using Mily.Wind.SugarContext;
using Mily.Wind.SugarEntity.System;
using System;
using System.Collections.Generic;
using XExten.Advance.CacheFramework;

namespace Mily.Wind.Logic
{
    public class MainLogic : MilyContext, IMainLogic
    {
        public List<MilyUser> GetUserList()
        {
            return Context().Queryable<MilyUser>().ToList();
        }

        public MilyUser GetUser(long id)
        {
            var User = Context().Queryable<MilyUser>().Where(t => t.Id == id).First();
            if (User != null) Caches.RedisCacheSet($"{User.Id}{User.Name}", User, 120);
            return User;
        }
        public MilyUser CreateUser()
        {
            MilyUser user = new MilyUser
            {
                Password = "1",
                Name = "lzh",
                EncryptPassword = "1"
            };
            return base.Insert(user,true);
        }
    }
}
