var guidEmployee = null;
var guidFilial = null;
var guidScheduleTime = null;
var TimeTableUrl = null;
var CreateTimeTableUrl = null;
function EmployeesSelectionChanged(e) {
    var filialsSelectBox = $("#FilialsSelectBox").dxSelectBox('instance');
    var filialsdataSource = $("#FilialsSelectBox").dxSelectBox('getDataSource');
    var scheduler = $("#IdTimeTable").dxScheduler('instance');
    if (e.value) {
        guidEmployee = e.value;
        filialsSelectBox.option('disabled', false);
        scheduler.option('visible', false);
        filialsSelectBox.reset();
        filialsdataSource.reload();
        VisibleScheduleTime(false);
        ClearCreateSchedule();
    }
    else {
        filialsSelectBox.reset();
        filialsSelectBox.option('disabled', true);
        scheduler.option('visible', false);
        VisibleScheduleTime(false);
        ClearCreateSchedule();
    }
}
function FilialsSelectionChanged(e) {
    var scheduler = $("#IdTimeTable").dxScheduler('instance');
    if (e.value) {
        guidFilial = e.value;
        UpdateScheduler();
        scheduler.option('visible', true);
        VisibleScheduleTime(false);
        ClearCreateSchedule();
    }
    else {
        scheduler.option('visible', false);
        VisibleScheduleTime(false);
        ClearCreateSchedule();
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
function ScheduleOnCellClick(e) {
    var choseDate = new Date(e.cellData.startDate).toDateString();
    $.ajax({
        url: TimeTableUrl,
        dataType: 'json',
        data: { guidEmployee: guidEmployee, guidFilial: guidFilial, date: choseDate },
        success: function (data) {
            if (data) {
                RenderTimes(data);
            }
            else {
                RenderCreateTimesButton(choseDate);
            }
        },
        error: function (ex) {
            alert(ex);
        }
    });
}
function VisibleScheduleTime(data) {
    var scheduleTime = $("#ScheduleTime").dxDataGrid('instance');
    scheduleTime.option('visible', data);
}
function ClearCreateSchedule() {
    var createScheduleElement = $('#CreateSchedule');
    createScheduleElement.empty();
}
function UpdateScheduleTime() {
    var scheduleTimedataSource = $("#ScheduleTime").dxDataGrid('getDataSource');
    scheduleTimedataSource.reload();
}
function UpdateScheduler() {
    var TimeTabledataSource = $("#IdTimeTable").dxScheduler('getDataSource');
    TimeTabledataSource.reload();
}
function RenderTimes(data) {
    guidScheduleTime = data;
    ClearCreateSchedule();
    UpdateScheduleTime();
    VisibleScheduleTime(true);
}
function RenderCreateTimesButton(date) {
    var createScheduleElement = $('#CreateSchedule');
    ClearCreateSchedule();
    VisibleScheduleTime(false);
    var button = "<button class=\"btn btn-success\" onClick=\"CreateTimeTable('".concat(date, "')\">\u0421\u043E\u0437\u0434\u0430\u0442\u044C \u0440\u0430\u0441\u043F\u0438\u0441\u0430\u043D\u0438\u0435</button>");
    createScheduleElement.append(button);
}
function OnBeforeSendScheduleTime(e, s) {
    if (guidScheduleTime) {
        s.url += "?TimeTableId=".concat(guidScheduleTime);
    }
}
function CreateTimeTable(date) {
    $.ajax({
        url: CreateTimeTableUrl,
        type: "post",
        dataType: 'json',
        data: { guidEmployee: guidEmployee, guidFilial: guidFilial, date: date },
        success: function (data) {
            RenderTimes(data);
        },
        error: function (ex) {
            alert(ex);
        }
    });
}
//# sourceMappingURL=Schedule.js.map