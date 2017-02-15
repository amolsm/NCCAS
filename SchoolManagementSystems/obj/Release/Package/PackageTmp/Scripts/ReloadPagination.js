function ReloadPaging() {
    $(function () {
        $("table.tablesorter").tablesorter({ widthFixed: true, sortList: [[0, 0]] })
            .tablesorterPager({ container: $("#pager"), size: $(".pagesize option:selected").val() });
    });
}