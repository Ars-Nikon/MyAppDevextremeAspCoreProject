var guidEmployee = null;
function SelectionChanged(e) {
    if (e.value) {
        guidEmployee = e.value;
        var filialsSelectBox = $("#FilialsSelectBox").dxSelectBox('instance');
        filialsSelectBox.option('disabled', false);
        var filialdataSource = $("#FilialsSelectBox").dxSelectBox('getDataSource');
    }
}
function OnBeforeSend(e, s) {
    if (guidEmployee) {
        s.url += "?guid=".concat(guidEmployee);
    }
}
//# sourceMappingURL=Schedule.js.map