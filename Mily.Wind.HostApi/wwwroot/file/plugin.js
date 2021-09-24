$(document).ready(() => {
    option.Init();
});
var option = {
    CurrentFileArray: {},
    CurrentIndexArray: [],
    Init: () => {
        option.InitDom();
        option.InitAction();
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
        }
    },
    InitAction: () => {
        $(".input-group-addon").on("click", () => {
            $("#file").trigger('click');
        });
        $("#file").on("change", (e) => {
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
            if (option.CurrentIndexArray.length!=0) {
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
                  var newarr =  res.Result.filter((e) => {
                        if (e.success) return e.FileName;
                    });
                    debugger;
                }
            })

        });
    }
};