$(document).ready(() => {
    option.Init();
});
var option = {
    SearchData: {
        KeyWord: null,
        NameSpace: null,
        Start: null,
        End: null,
        LogLv: null,
        PageIndex: 1,
        PageSize: 10
    },
    Init: () => {
        option.InitDom();
        option.InitEvent.Search(option.SearchData);
    },
    InitDom: () => {
        laydate.render({
            elem: '#Star',
            type: 'datetime',
            theme: '#393D49'
        });
        laydate.render({
            elem: '#End',
            type: 'datetime',
            theme: '#393D49'
        });
        $("#Search").on("click", (e) => {
            option.SearchData.KeyWord = $("#KeyWord").val();
            option.SearchData.Start = $("#Star").val();
            option.SearchData.End = $("#End").val();
            option.SearchData.LogLV = $("#LogLV").find("option:selected").val();
            option.SearchData.NameSpace = $("#NameSpace").find("option:selected").val();
            option.SearchData.PageIndex = 1;
            option.InitEvent.Search(option.SearchData);
        });
        $("#Repect").on("click", () => {
            option.SearchData.KeyWord = null;
            option.SearchData.Start = null;
            option.SearchData.End = null;
            option.SearchData.LogLV = null;
            option.SearchData.NameSpace = null;
            option.SearchData.PageIndex = 1;

            $("#KeyWord").val("");
            $("#Star").val("")
            $("#End").val("")
        })
    },
    InitEvent: {
        Check: (e) => {
            return e == undefined ? "" : e;
        },
        Enum: (e) => {
            switch (e) {
                case 0: return "Debug";
                case 1: return "Info";
                case 2: return "Warning";
                case 3: return "Error";
                default: return "Info";
            }
        },
        Delete: (e) => {
            option.InitEvent.Ajax({
                url: `/Log/DeleteLog?Id=${e}`,
                type: "delete"
            });
            option.InitEvent.Search(option.SearchData);
        },
        Copy: (e) => {
            $("#copy").attr("type", "text").val($(e).data().stack).select();
            document.execCommand("copy");
            $("#copy").val("").attr("type", "hidden");
        },
        Search: (e) => {
            $("tbody").html("");
            var res = option.InitEvent.Ajax({
                url: "/Log/GetLogPage",
                param: e,
                type: "post"
            });
            if (res.HttpCode != 200)
                throw new Error("服务器异常");
            var val = res.Result.Detail;
            let total = Math.ceil(res.Result.Total / e.PageSize);
            let page = total == 0 ? 0 : e.PageIndex;
            $("#TotalPage").text(total);
            $("#CurrentPage").val(page)
            $.each(val, (_, item) => {
                $("tbody").append(option.Template.replace("{Trace}", option.InitEvent.Check(item.TraceId))
                    .replace("{Method}", option.InitEvent.Check(item.Invoken))
                    .replace("{Entity}", option.InitEvent.Check(item.EntityName))
                    .replace("{Time}", option.InitEvent.Check(item.CreatedTime))
                    .replace("{Msg}", option.InitEvent.Check(item.ErrorMsg))
                    .replace("{Param}", option.InitEvent.Check(item.Param.replace('\\', '')))
                    .replace("{Lv}", option.InitEvent.Check(option.InitEvent.Enum(item.LogLv)))
                    .replace("{Id}", option.InitEvent.Check(item.Id))
                    .replace("{Service}", option.InitEvent.Check(item.SystemService))
                    .replace("{Stack}", option.InitEvent.Check(item.StackTrace))
                    .replace("{Stack}", option.InitEvent.Check(item.StackTrace)));
            });
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
        Next: () => {
            let page = Number($("#CurrentPage").val()) + 1;
            if (page > Number($("#TotalPage").text()))
                throw new Error("页码不能大于总页数");
            $("#CurrentPage").val(page);
            option.SearchData.KeyWord = $("#KeyWord").val();
            option.SearchData.Start = $("#Star").val();
            option.SearchData.End = $("#End").val();
            option.SearchData.LogLV = $("#LogLV").find("option:selected").val();
            option.SearchData.NameSpace = $("#NameSpace").find("option:selected").val();
            option.SearchData.PageIndex = $("#CurrentPage").val();
            option.InitEvent.Search(option.SearchData);
        },
        Last: () => {
            let page = Number($("#CurrentPage").val()) - 1;
            if (page <= 0)
                throw new Error("页码不能小于等于0");
            $("#CurrentPage").val(page);
            option.SearchData.KeyWord = $("#KeyWord").val();
            option.SearchData.Start = $("#Star").val();
            option.SearchData.End = $("#End").val();
            option.SearchData.LogLV = $("#LogLV").find("option:selected").val();
            option.SearchData.NameSpace = $("#NameSpace").find("option:selected").val();
            option.SearchData.PageIndex = $("#CurrentPage").val();
            option.InitEvent.Search(option.SearchData);
        },
        Ret: () => {
            let page = Number($("#CurrentPage").val());
            if (page - 1 < 0)
                throw new Error("页码不能小于0");
            if (page + 1 > Number($("#TotalPage").text()))
                throw new Error("页码不能大于总页数");
            option.SearchData.KeyWord = $("#KeyWord").val();
            option.SearchData.Start = $("#Star").val();
            option.SearchData.End = $("#End").val();
            option.SearchData.LogLV = $("#LogLV").find("option:selected").val();
            option.SearchData.NameSpace = $("#NameSpace").find("option:selected").val();
            option.SearchData.PageIndex = $("#CurrentPage").val();
            option.InitEvent.Search(option.SearchData);
        }
    },
    Template: `<tr>
                        <td class="text-center">{Trace}</td>
                        <td class="text-center">{Service}</td>
                        <td class="text-center">{Method}</td>
                        <td class="text-center">{Entity}</td>
                        <td class="text-center">{Time}</td>
                        <td class="text-center" style="color:red;" data-toggle="tooltip" title="{Stack}" data-placement="bottom">{Msg}</td>
                        <td class="text-center" style="color:blue;">{Lv}</td>
                        <td class="text-center" style="color:darkorange;">{Param}</td>
                        <td class="text-center">
                            <button class="btn btn-sm btn-success" onclick="option.InitEvent.Copy(this)" data-stack="{Stack}" style="outline: none;">复制堆栈</button>
                            <button class="btn btn-sm btn-warning" onclick="option.InitEvent.Delete('{Id}')" style="outline: none;">删除</button>
                        </td>
                   </tr>`
};