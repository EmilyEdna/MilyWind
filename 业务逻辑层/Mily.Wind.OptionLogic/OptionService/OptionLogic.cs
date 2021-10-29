using Mily.Wind.VMod.Mogo;
using Mily.Wind.VMod.Mogo.Input;
using Mily.Wind.VMod.Mogo.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.CacheFramework;
using XExten.Advance.CacheFramework.MongoDbCache;
using XExten.Advance.LinqFramework;

namespace Mily.Wind.OptionLogic.OptionService
{
    public class OptionLogic : IOptionLogic
    {
        public virtual OptionConfMogoViewModel WriteOptionConf(OptionConfInput input)
        {
            var vm = input.ToMapest<OptionConfMogoViewModel>();
            var res = Caches.MongoDBCacheGet<OptionConfMogoViewModel>(t => t.NameSpace == input.NameSpace);
            if (res != null) return null;
            vm.Version = 1;
            vm.Id = Guid.NewGuid();
            Caches.MongoDBCacheSet(vm);
            OptionConfVerMogoViewModel ver = new OptionConfVerMogoViewModel
            {
                AlterTime = DateTime.Now,
                AlterJson = input.OptionJson,
                Id = Guid.NewGuid(),
                OptionConfId = vm.Id.ToString(),
                Version = vm.Version
            };
            Caches.MongoDBCacheSet(ver);
            return vm;
        }
        public virtual OptionConfMogoViewModel GetOptionConfFirst(Guid Id)
        {
            return Caches.MongoDBCacheGet<OptionConfMogoViewModel>(t => t.Id == Id);
        }
        public virtual Dictionary<string, List<OptionConfMogoViewModel>> SearchOptionConf()
        {
            List<OptionConfMogoViewModel> query = MongoDbCaches.Query<OptionConfMogoViewModel>().ToList();
            return query.GroupBy(t => t.Env).ToDictionary(t => t.Key.ToString(), t => t.ToList());
        }
        public virtual OptionConfMogoViewModel AlterOptionConf(OptionConfInput input)
        {
            OptionConfMogoViewModel vm = Caches.MongoDBCacheGet<OptionConfMogoViewModel>(t => t.Id == input.Id);
            var vers = MongoDbCaches.Query<OptionConfVerMogoViewModel>().Where(t => t.OptionConfId == input.Id.ToString()).Max(t => t.Version);
            vm.NameSpace = input.NameSpace;
            vm.OptionJson = input.OptionJson;
            vm.Version = vers + 1;
            vm.Env = input.Env;
            MongoDbCaches.UpdateMany(t => t.Id == input.Id, vm);
            OptionConfVerMogoViewModel ver = new OptionConfVerMogoViewModel
            {
                AlterTime = DateTime.Now,
                AlterJson = vm.OptionJson,
                Id = Guid.NewGuid(),
                OptionConfId = vm.Id.ToString(),
                Version = vm.Version
            };
            Caches.MongoDBCacheSet(ver);
            return vm;
        }
        public virtual OptionConfVerPageOutput SearchOptionConfVer(OptionConfVerPageInput input)
        {
            var query = MongoDbCaches.Query<OptionConfVerMogoViewModel>()
                 .Where(t => t.OptionConfId == input.CId)
                 .OrderByDescending(t => t.AlterTime);

            return new OptionConfVerPageOutput
            {
                Data = query.Skip((input.PageIndex - 1) * input.PageSize)
                 .Take(input.PageSize).ToList(),
                Total = query.Count()
            };
        }
        public virtual bool RemoveAndSearchOptionConfVer(Guid Id)
        {
            Caches.MongoDBCacheRemove<OptionConfVerMogoViewModel>(t => t.Id == Id);
            return true;
        }
        public virtual bool RestoreOptionConf(Guid Id)
        {
            OptionConfVerMogoViewModel ver = Caches.MongoDBCacheGet<OptionConfVerMogoViewModel>(t => t.Id == Id);
            var Cid = Guid.Parse(ver.OptionConfId);
            var res = Caches.MongoDBCacheGet<OptionConfMogoViewModel>(t => t.Id == Cid);
            res.OptionJson = ver.AlterJson;
            res.Version = ver.Version;
            MongoDbCaches.UpdateMany(t => t.Id == Cid, res);
            return true;
        }
    }
}
