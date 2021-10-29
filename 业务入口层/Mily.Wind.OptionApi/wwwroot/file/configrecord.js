const table = `<tr><td class="text-center" style="color:blue;">{0}</td><td class="text-center" style="color:red;">{1}</td><td class="text-center"><button class="btn btn-sm btn-success" onclick="option.InitEvent.ReStore('{2}')"  style="outline: none;">还原</button><button class="btn btn-sm btn-warning" onclick="option.InitEvent.Delete('{3}')" style="outline: none;">删除</button></td></tr >`;

$(document).ready(() => {
    option.Init();
});
var option = {
    Dto: {
        PageIndex: 1,
        PageSize: 20,
        CId: null
    },
    Init: () => {
        option.InitDom();
    },
    InitDom: () => {
        option.Dto.CId = option.InitEvent.GetUrlParam("CId");
        option.InitEvent.Search(option.Dto);
    },
    InitEvent: {
        GetUrlParam: (name) => {
            let reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            let r = window.location.search.substr(1).match(reg);
            if (r != null) {
                return decodeURIComponent(r[2]);
            };
            return null;
        },
        Ajax: (e) => {
            var ret = {};
            $.ajax({
                url: e.url,
                data: e.param == undefined ? undefined : JSON.stringify(e.param),
                type: e.type,
                async: false,
                contentType: "application/json",
                success: res => { ret = res; }
            });
            return ret;
        },
        Search: (e) => {
            $("tbody").html("");
            var res = option.InitEvent.Ajax({
                url:"/OptionConf/SearchOptionConfVer",
                param: e,
                type: "post"
            });
            let total = Math.ceil(res.Total / e.PageSize);
            let page = total == 0 ? 0 : e.PageIndex;
            $("#TotalPage").text(total);
            $("#CurrentPage").val(page)
            $.each(res.Data, (_, item) => {
                $("tbody").append(table.replace('{0}', item.AlterJson)
                    .replace("{1}", item.AlterTime)
                    .replace("{2}", item.Id)
                    .replace("{3}", item.Id));
            });
        },
        Next: () => {
            let page = Number($("#CurrentPage").val()) + 1;
            if (page > Number($("#TotalPage").text()))
                throw new Error("页码不能大于总页数");
            $("#CurrentPage").val(page);
            option.Dto.PageIndex = $("#CurrentPage").val();
            option.Dto.CId = option.InitEvent.GetUrlParam("CId");
            option.InitEvent.Search(option.Dto);
        },
        Last: () => {
            let page = Number($("#CurrentPage").val()) - 1;
            if (page <= 0)
                throw new Error("页码不能小于等于0");
            $("#CurrentPage").val(page);
            option.Dto.PageIndex = $("#CurrentPage").val();
            option.Dto.CId = option.InitEvent.GetUrlParam("CId");
            option.InitEvent.Search(option.Dto);
        },
        Ret: () => {
            let page = Number($("#CurrentPage").val());
            if (page - 1 < 0)
                throw new Error("页码不能小于0");
            if (page + 1 > Number($("#TotalPage").text()))
                throw new Error("页码不能大于总页数");
            option.Dto.PageIndex = $("#CurrentPage").val();
            option.Dto.CId = option.InitEvent.GetUrlParam("CId");
            option.InitEvent.Search(option.Dto);
        },
        ReStore: (e) => {
            option.InitEvent.Ajax({
                url: `/OptionConf/RestoreOptionConf?Id=${e}`,
                type: "get"
            });
        },
        Delete: (e) => {
            option.InitEvent.Ajax({
                url: `/OptionConf/RemoveAndSearchOptionConfVer?Id=${e}`,
                type: "delete"
            });
            option.Dto.PageIndex = 1;
            option.InitEvent.Search(option.Dto);
        }
    }
};