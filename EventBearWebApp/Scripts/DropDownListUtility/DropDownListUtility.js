//Api 
//[HttpPost]
//public ActionResult DropDownWarehose(string branchCode)
//{
            
//    return Json(new { Status = true, data = warehoseList });
//}



function HttpPostFormData(uri, formData, callbackFunc) {

    $.ajax({
        type: "POST",
        url: uri,
        contentType: false,
        processData: false,
        data: formData,
        success: function (msg) {
            if (msg.Status) {
                if (typeof (callbackFunc) === "function") {
                    setTimeout(callbackFunc(msg), 50);
                }
            }
            else {
                alert(msg.ErrorMessage);

            }
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            //alert(err);
        }
    });
}

function GenDropDownList(idDproDown, dataSouce, formData) {
    HttpPostFormData(dataSouce, formData, function (msg) {
        $("#" + idDproDown).html("");
        var htmlOption = "";
        var selectedValue = "";
        $.each(msg.data, function (i, val) {
            if (i === 0)
                selectedValue = val.Value;
            htmlOption += "<option value = '" + val.Value + "'>" + val.Text + "</option>";
        });
        $("#" + idDproDown).html(htmlOption).val(selectedValue);
        $("#" + idDproDown).change();
    });
}

