function FillPermission() {
    if ($("#Add").val() == "Allow") { $("#AddForm").show(); }
    else { $("#AddForm").hide(); }

    if ($("#Modify").val() == "Allow") { $(".btn_edit").show(); $(".edt").show(); }
    else { $(".btn_edit").hide(); $(".edt").hide(); }

    if ($("#View").val() == "Allow") { $("#result").show(); }
    else { $("#result").hide(); }

    if ($("#Delete").val() == "Allow") { $(".btn_delete").show(); }
    else { $(".btn_delete").hide(); }
}