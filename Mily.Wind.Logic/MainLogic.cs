
using Mily.Wind.SugarContext;
using Mily.Wind.SugarEntity.System;
using System;
using System.Collections.Generic;

namespace Mily.Wind.Logic
{
    public class MainLogic : MilyContext, IMainLogic
    {
        public List<MilyUser> GetUserList()
        {
           return Context().Queryable<MilyUser>().ToList();
        }

        public MilyUser GetUser(int id)
        {
            return Context().Queryable<MilyUser>().Where(t => t.Id == id).First();
        }
    }
}
