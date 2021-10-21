using Mily.Wind.Extens.AOPUtity;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.SugarContext;
using Mily.Wind.SugarEntity.System;
using Mily.Wind.VMod.DataTransferObj.Input;
using Mily.Wind.VMod.DataTransferObj.Output;
using System.Collections.Generic;
using XExten.Advance.CacheFramework;
using XExten.Advance.LinqFramework;

namespace Mily.Wind.Logic.Main
{
    [Interceptor]
    public class MainLogic : MilyContext, IMainLogic
    {
        [Actions]
        public virtual MilyMapperResult GetUserList()
        {
            return MilyMapperResult.Success<List<MilyUserVMOutput>>(Context().Queryable<MilyUser>().ToList());
        }

        [Actions]
        public virtual MilyMapperResult GetUser(long id)
        {
            var User = Context().Queryable<MilyUser>().Where(t => t.Id == id).First();
            if (User != null) Caches.RedisCacheSet($"{User.Id}{User.Name}", User, 120);
            return MilyMapperResult.Success<MilyUserVMOutput>(User);
        }

        [Actions]
        public virtual MilyMapperResult CreateUser(MilyUserVMInput input)
        {
            var User =  base.InsertTrans(input.ToMapper<MilyUser>().SetEncryptPassword());
            return MilyMapperResult.Success<MilyUserVMOutput>(User);
        }
    }
}
