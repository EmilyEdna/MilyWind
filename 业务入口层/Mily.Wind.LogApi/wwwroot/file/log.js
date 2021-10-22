$(document).ready(() => {
    option.Init();
});
var option = {
    SearchData: {
        KeyWord: null,
        SystemService: null,
        Start: null,
        End: null,
        LogLv: null,
        LogEnv:0,
        PageIndex: 1,
        PageSize: 10
    },
    Init: () => {
        option.InitDom();
        option.InitService();
        option.InitEvent.Search(option.SearchData);
    },
    InitService: () => {
        var html = '<option value="" selected>全部</option>';
        option.InitEvent.Ajax({
            url: "/Log/GetSystemService",
            type: "get"
        }).forEach(item => {
            html += `<option value="${item}">${item}服务</option>`;
        });
        $("#NameSpace").html(html);
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
            option.SearchData.SystemService = $("#NameSpace").find("option:selected").val();
            option.SearchData.LogEnv = $("#SystemEnv").find("option:selected").val();
            option.SearchData.PageIndex = 1;
            option.InitEvent.Search(option.SearchData);
        });
        $("#Repect").on("click", () => {
            option.SearchData.KeyWord = null;
            option.SearchData.Start = null;
            option.SearchData.End = null;
            option.SearchData.LogLV = null;
            option.SearchData.SystemService = null;
            option.SearchData.LogEnv = 0;
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
            var val = res.Detail;
            let total = Math.ceil(res.Total / e.PageSize);
            let page = total == 0 ? 0 : e.PageIndex;
            $("#TotalPage").text(total);
            $("#CurrentPage").val(page)
            $.each(val, (_, item) => {
                $("tbody").append(option.Template.replace("{TraceId}", option.InitEvent.Check(item.TraceId))
                    .replace("{CreatedTime}", option.InitEvent.Check(item.CreatedTime))
                    .replace("{ErrorMsg}", option.InitEvent.Check(item.ErrorMsg))
                    .replace("{LogLv}", option.InitEvent.Check(option.InitEvent.Enum(item.LogLv)))
                    .replace("{Id}", option.InitEvent.Check(item.Id))
                    .replace("{SystemService}", option.InitEvent.Check(item.SystemService))
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
            option.SearchData.LogEnv = $("#SystemEnv").find("option:selected").val();
            option.SearchData.LogLV = $("#LogLV").find("option:selected").val();
            option.SearchData.SystemService = $("#NameSpace").find("option:selected").val();
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
            option.SearchData.LogEnv = $("#SystemEnv").find("option:selected").val();
            option.SearchData.LogLV = $("#LogLV").find("option:selected").val();
            option.SearchData.SystemService = $("#NameSpace").find("option:selected").val();
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
            option.SearchData.LogEnv = $("#SystemEnv").find("option:selected").val();
            option.SearchData.LogLV = $("#LogLV").find("option:selected").val();
            option.SearchData.SystemService = $("#NameSpace").find("option:selected").val();
            option.SearchData.PageIndex = $("#CurrentPage").val();
            option.InitEvent.Search(option.SearchData);
        }
    },
    Template: `<tr>
                        <td class="text-center">{TraceId}</td>
                        <td class="text-center" style="color:red;" data-toggle="tooltip" title="{Stack}" data-placement="bottom">{ErrorMsg}</td>
                        <td class="text-center" style="color:blue;">{LogLv}</td>
                        <td class="text-center">{SystemService}</td>
                        <td class="text-center">{CreatedTime}</td>
                        <td class="text-center">
                            <button class="btn btn-sm btn-success" onclick="option.InitEvent.Copy(this)" data-stack="{Stack}" style="outline: none;">复制堆栈</button>
                            <button class="btn btn-sm btn-warning" onclick="option.InitEvent.Delete('{Id}')" style="outline: none;">删除</button>
                        </td>
                   </tr>`
};