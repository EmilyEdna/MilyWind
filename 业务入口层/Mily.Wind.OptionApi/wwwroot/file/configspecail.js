const CallSpan = `<div class="panel-group" id="{0}"><div class="panel panel-default"><div class="panel-heading"><h4 class="panel-title"><a data-toggle="collapse" data-parent="#{0}" href="#{1}">{2}</a></h4></div><div id="{1}" class="panel-collapse collapse in"><div class="panel-body">{3}</div></div></div></div>`;
const Label = `<button class="btn btn-warning" style ="width:100%;margin:3px;" onclick="option.InitEvent.Show('{1}')">{0}</button>`;
$(document).ready(() => {
    option.Init();
});
var option = {
    JEditor: {},
    Init: () => {
        option.InitDom();
        option.JEditor = new JSONEditor(document.getElementById("jsonviwer"), { mode: 'code' });
    },
    InitDom: () => {
        option.InitEvent.InitGet();
    },
    InitEvent: {
        InitGet: () => {
            var res = option.InitEvent.Ajax({
                url: "/OptionConf/SearchOptionConf",
                type: "get"
            });
            var html = '';
            let index = 0;
            $.each(res, (env, item) => {
                html += CallSpan.replace("{0}", env).replace("{0}", env).replace("{1}", `${env}_${index}`)
                    .replace("{1}", `${env}_${index}`).replace("{2}", `${env}环境`);
                let arr = [];
                item.forEach((obj) => {
                    arr.push(Label.replace("{0}", obj.NameSpace).replace("{1}", obj.Id));
                });
                html = html.replace('{3}', arr.join(""));
                arr = [];
                index += 1;
            });
            $("#coll").html(html);
        },
        Ajax: (e) => {
            var ret = {};
            $.ajax({
                url: e.url,
                data: e.param == undefined ? undefined : JSON.stringify(e.param),
                type: e.type,
                async: false,
                contentType: "application/json",
                success: res => { ret = res; },
                error: (e) => {
                    if (e.status == 500)
                        alert(e.responseText);
                }
            });
            return ret;
        },
        Save: () => {
            var param = { "NameSpace": $("#group").val(), "OptionJson": JSON.stringify(option.JEditor.get()), "Env": $("#env option:selected").val() };
           var res = option.InitEvent.Ajax({
                url: "/OptionConf/WriteOptionConf",
                param: param,
                type: "post"
           });
           option.InitEvent.InitGet();
            option.JEditor.set(JSON.parse(res.OptionJson));
            $("#_id").val(res.Id);
            $("#env").val(res.Env);
            $("#group").val(res.NameSpace);
        },
        Alter: () => {
            var param = { "Id": $("#_id").val(), "NameSpace": $("#group").val(), "OptionJson": JSON.stringify(option.JEditor.get()), "Env": $("#env option:selected").val() };
            if (param.Id == null || param.Id == "")
                throw Error("更新操作Id不能为空");
            option.InitEvent.Ajax({
                url: "/OptionConf/AlterOptionConf",
                param: param,
                type: "post"
            });
            option.InitEvent.Show(param.Id);
        },
        Show: (e) => {
            var res = option.InitEvent.Ajax({
                url: `/OptionConf/GetOptionConfFirst?Id=${e}`,
                type: "get"
            });
            $("#_id").val(e);
            $("#env").val(res.Env);
            $("#group").val(res.NameSpace);
            option.JEditor.set(JSON.parse(res.OptionJson));
        },
        History: (e) => {
            if ($("#_id").val() != null && $("#_id").val() != "")
            {
                var href = $(e).attr("href");
                href = "record.html?CId=" + $("#_id").val();
                $(e).attr("href", href);
            }
        }
    }
};