
using Mily.Wind.Extens.AOPUtity;
using Mily.Wind.Extens.DependencyInjection;
using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.SugarContext;
using Mily.Wind.SugarEntity.System;
using Mily.Wind.VMod.DataTransferObj;
using Mily.Wind.VMod.Enums;
using System;
using System.Collections.Generic;
using XExten.Advance.CacheFramework;
using XExten.Advance.LinqFramework;

namespace Mily.Wind.Logic.Main
{
    [Interceptor]
    public class MainLogic : MilyContext, IMainLogic
    {
        public ILog LogClient => IocManager.GetService<ILog>();

        [Actions]
        public virtual MilyMapperResult GetUserList()
        {
            return MilyMapperResult.Success<MilyUser, MilyUserVM>(MapperEnum.Collection, Context().Queryable<MilyUser>().ToList());
        }

        [Actions]
        public virtual MilyMapperResult GetUser(long id)
        {
            var User = Context().Queryable<MilyUser>().Where(t => t.Id == id).First();
            if (User != null) Caches.RedisCacheSet($"{User.Id}{User.Name}", User, 120);
            return MilyMapperResult.Success<MilyUser, MilyUserVM>(MapperEnum.Class, User);
        }

        [Actions]
        public virtual MilyMapperResult CreateUser(MilyUserVM input)
        {
            var User = base.Insert(input.ToMapper<MilyUser>().SetEncryptPassword());
            return MilyMapperResult.Success<MilyUser, MilyUserVM>(MapperEnum.Class, User);
        }
    }
}
