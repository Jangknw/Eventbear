$(document).ready(function () {
    DorpDownList();
});

function DorpDownList() {
    var data = new FormData();
    data.append("ProvinID", $("#Place_Province").val());
    GenDropDownList("Place_District", "http://localhost:56400/AddLo/DropDownDistrict", data);

    $("#Place_Province").on("change", function () {
        data = new FormData();
        data.append("provinID", $("#Place_Province").val());
        GenDropDownList("Place_District", "http://localhost:56400/AddLo/DropDownDistrict", data);
    });

    $("#Place_District").on("change", function () {
        data = new FormData();
        data.append("DistrictID", $("#Place_District").val());
        GenDropDownList("Place_SubDistrict", "http://localhost:56400/AddLo/DropDownSubDistrict", data);
    });
}