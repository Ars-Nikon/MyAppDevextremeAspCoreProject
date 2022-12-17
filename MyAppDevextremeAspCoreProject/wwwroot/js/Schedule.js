var guidEmployee = null;
var guidFilial = null;
function EmployeesSelectionChanged(e) {
    var filialsSelectBox = $("#FilialsSelectBox").dxSelectBox('instance');
    var filialsdataSource = $("#FilialsSelectBox").dxSelectBox('getDataSource');
    var scheduler = $("#IdTimeTable").dxScheduler('instance');
    if (e.value) {
        guidEmployee = e.value;
        filialsSelectBox.option('disabled', false);
        filialsSelectBox.reset();
    }
    else {
        filialsSelectBox.option('disabled', true);
        scheduler.option('visible', false);
    }
}
function FilialsSelectionChanged(e) {
    if (e.value) {
        guidFilial = e.value;
        var scheduler = $("#IdTimeTable").dxScheduler('instance');
        scheduler.option('visible', true);
        var TimeTabledataSource = $("#IdTimeTable").dxScheduler('getDataSource');
        TimeTabledataSource.reload();
    }
}
function FilialsOnBeforeSend(e, s) {
    if (guidEmployee) {
        s.url += "?guid=".concat(guidEmployee);
    }
}
function ScheduleOnBeforeSend(e, s) {
    if (guidEmployee) {
        s.url += "?guidEmployee=".concat(guidEmployee, "&guidFilial=").concat(guidFilial);
    }
}
//# sourceMappingURL=Schedule.js.map