const More = `<svg t="1632797373528" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="2413" width="20" height="20"><path d="M315.076923 275.692308h433.230769a39.384615 39.384615 0 0 1 39.384616 39.384615v433.230769a39.384615 39.384615 0 0 1-39.384616 39.384616H315.076923a39.384615 39.384615 0 0 1-39.384615-39.384616V315.076923a39.384615 39.384615 0 0 1 39.384615-39.384615z m0 39.384615v433.230769h433.230769V315.076923H315.076923z m196.923077 196.923077v-98.461538a19.692308 19.692308 0 1 1 39.384615 0V512h98.461539a19.692308 19.692308 0 1 1 0 39.384615H551.384615v98.461539a19.692308 19.692308 0 1 1-39.384615 0V551.384615h-98.461538a19.692308 19.692308 0 1 1 0-39.384615H512z" p-id="2414" fill="#d4237a"></path></svg>`;
const Less = `<svg t="1632797550960" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="3625" width="20" height="20"><path d="M315.076923 315.076923v433.230769h433.230769V315.076923H315.076923z m0-39.384615h433.230769a39.384615 39.384615 0 0 1 39.384616 39.384615v433.230769a39.384615 39.384615 0 0 1-39.384616 39.384616H315.076923a39.384615 39.384615 0 0 1-39.384615-39.384616V315.076923a39.384615 39.384615 0 0 1 39.384615-39.384615z" p-id="3626" fill="#d4237a"></path><path d="M413.538462 512h236.307692a19.692308 19.692308 0 0 1 0 39.384615h-236.307692a19.692308 19.692308 0 0 1 0-39.384615z" p-id="3627" fill="#d4237a"></path></svg>`;
$(document).ready(() => {
    option.Init();
});
var option = {
    CurrentFileArray: {},
    CurrentIndexArray: [],
    SearchData: {
        IsEable: null,
        PluginAlias: null,
        PageIndex: 0,
        PageSize: 10
    },
    Init: () => {
        option.InitDom();
        option.InitAction();
    },
    InitDom: () => {
        option.InitEvent.Search(option.SearchData);
        $("#Search").click(() => {
            option.SearchData.IsEable = $("#State").find("option:selected").val();
            option.SearchData.PluginAlias = $("#NickName").val();
            option.InitEvent.Search(option.SearchData);
        });
    },
    InitEvent: {
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
        RemoveLbl: (e) => {
            $("#lbl" + e).remove();
            for (var item in option.CurrentFileArray) {
                if (item == "length") continue;
                if (item != e) {
                    option.CurrentIndexArray.push(item);
                    option.CurrentIndexArray = option.CurrentIndexArray.filter((i) => { return i != e });
                }
            }
        },
        Check: (e) => {
            return e == undefined ? "" : e;
        },
        Search: (e) => {
            $("tbody").html("");
            var res = option.InitEvent.Ajax({
                url: "/Plugin/GetPluginPage",
                param: e,
                type: "post"
            });
            if (res.HttpCode != 200)
                throw new Error("服务器异常");
            var val = res.Result.Detail;
            let total = Math.ceil(res.Result.Total / e.PageSize);
            let page = total == 0 ? 0 : Math.ceil(e.PageIndex / e.PageSize) + 1;
            $("#TotalPage").text(total);
            $("#CurrentPage").val(page)
            $.each(val, (_, item) => {
                $("tbody").append(option.Template.replace("{Name}", option.InitEvent.Check(item.PluginName))
                    .replace("{NickName}", option.InitEvent.Check(item.PluginAlias))
                    .replace("{Size}", option.InitEvent.Check(item.PluginSize))
                    .replace("{Time}", option.InitEvent.Check(item.RegistTime))
                    .replace("{Version}", option.InitEvent.Check(item.PluginVersion))
                    .replace("{State}", item.IsEable ? "启用" : "禁用")
                    .replace("{Id}", option.InitEvent.Check(item.Id))
                    .replace("{Id}", option.InitEvent.Check(item.Id))
                    .replace("{Id}", option.InitEvent.Check(item.Id))
                    .replace("{Id}", option.InitEvent.Check(item.Id)));
            });
        },
        Handler: (e, t) => {
            var res = option.InitEvent.Ajax({
                url: "/Plugin/AlterPlugin",
                param: { Id: e, Type: t },
                type: "put"
            });
            if (res.HttpCode != 200)
                throw new Error("服务器异常");
            option.InitEvent.Search(option.SearchData);
        },
        Alter: (e) => {
            option.InitEvent.Ajax({
                url: "/Plugin/AlterPlugin",
                param: { Id: $(e).data().id, PluginAlias: $(e).val() },
                type: "put"
            });
        },
        Next: () => {
            let page = Number($("#CurrentPage").val()) + 1;
            if (page > Number($("#TotalPage").text()))
                throw new Error("页码不能大于总页数");
            $("#CurrentPage").val(page);
            option.SearchData.IsEable = $("#State").find("option:selected").val();
            option.SearchData.PluginAlias = $("#NickName").val();
            option.SearchData.PageIndex = $("#CurrentPage").val() - 1;
            option.InitEvent.Search(option.SearchData);
        },
        Last: () => {
            let page = Number($("#CurrentPage").val()) - 1;
            if (page <= 0)
                throw new Error("页码不能小于等于0");
            $("#CurrentPage").val(page);
            option.SearchData.IsEable = $("#State").find("option:selected").val();
            option.SearchData.PluginAlias = $("#NickName").val();
            option.SearchData.PageIndex = $("#CurrentPage").val() - 1;
            option.InitEvent.Search(option.SearchData);
        },
        Ret: () => {
            let page = Number($("#CurrentPage").val());
            if (page - 1 < 0)
                throw new Error("页码不能小于0");
            if (page + 1 > Number($("#TotalPage").text()))
                throw new Error("页码不能大于总页数");
            option.SearchData.IsEable = $("#State").find("option:selected").val();
            option.SearchData.PluginAlias = $("#NickName").val();
            option.SearchData.PageIndex = $("#CurrentPage").val() - 1;
            option.InitEvent.Search(option.SearchData);
        },
        OpenOrClose: (e) => {
            if ($(e).data().set == "1")
            {
                $(e).html(Less);
                $(e).data("set", "2");
                return;
            }
            if ($(e).data().set == "2") {
                $(e).html(More);
                $(e).data("set", "1");
                return;
            }
        }
    },
    InitAction: () => {
        $(".input-group-addon").on("click", () => {
            $("#file").trigger('click');
        });
        $("#file").change((e) => {
            var files = $(e.target)[0].files;
            if (files.length <= 3) {
                var template = [];
                for (var i = 0; i < files.length; i++) {
                    var name = files[i].name;
                    let reg = /(.*?).dll/g;
                    if (!reg.test(name)) {
                        $("#filecontent").attr("title", "文件类型错误，仅支持.dll的文件");
                        $("#filecontent").tooltip("show");
                        return;
                    }
                    if (files[i].name.length > 10) {
                        name = `${files[i].name.substring(0, 10)}..`;
                    }
                    template.push(`<lable class="label label-primary" style="margin-right:10px;font-size:15px;" onclick="option.InitEvent.RemoveLbl('${i}');" id='lbl${i}'>${name}</lable>`);
                }
                $("#filecontent").html(template);
                Array.prototype.push.apply(option.CurrentFileArray, files);
            }
            else {
                $("#filecontent").attr("title", "超出文件上传数量，目前只支持上传3个以下，为了样式好看。");
                $("#filecontent").tooltip("show");
            }
        });
        $("#Upload").click(() => {
            var data = new FormData();
            if (option.CurrentIndexArray.length != 0) {
                for (var item in option.CurrentIndexArray) {
                    var index = option.CurrentIndexArray[item];
                    data.append("files", option.CurrentFileArray[index]);
                }
            } else {
                for (var item in option.CurrentFileArray) {
                    if (item == "length") continue;
                    data.append("files", option.CurrentFileArray[item]);
                }
            }
            $.ajax({
                contentType: false,
                processData: false,
                url: "/Plugin/UploadPlugin",
                data: data,
                type: "post",
                async: false,
                success: (res) => {
                    $("lable").remove();
                    option.CurrentFileArray = {};
                    option.CurrentIndexArray = [];
                    $("#file").val("");
                    option.InitEvent.Search(option.SearchData);
                }
            });
        });
    },
    Template: `<tr>
                        <td class="text-center"><div class="row"><div class="col-md-4"></div><div class="col-md-1"><span onclick="option.InitEvent.OpenOrClose(this)" data-set="1">${More}</span></div><div class="col-md-1">{Name}</div></div></td>
                        <td class="text-center"><input type="text" style="border:none;outline:none;" value="{NickName}" data-id="{Id}" onchange="option.InitEvent.Alter(this)"/></td>
                        <td class="text-center">{Size}</td>
                        <td class="text-center">{Time}</td>
                        <td class="text-center">{Version}</td>
                        <td class="text-center">{State}</td>
                        <td class="text-center">
                            <button class="btn btn-sm btn-success" onclick="option.InitEvent.Handler('{Id}',1)" style="outline: none;">启用</button>
                            <button class="btn btn-sm btn-warning" onclick="option.InitEvent.Handler('{Id}',0)" style="outline: none;">禁用</button>
                            <button class="btn btn-sm btn-danger" onclick="option.InitEvent.Handler('{Id}',-1)" style="outline: none;">删除</button>
                        </td>
                   </tr>`
};