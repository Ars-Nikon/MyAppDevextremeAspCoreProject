let guidEmployee: string | null = null;
let guidFilial: string | null = null;
let guidScheduleTime: string | null = null;

let TimeTableUrl: string | null = null;
let CreateTimeTableUrl: string | null = null;

function EmployeesSelectionChanged(e: DevExpress.ui.dxSelectBox.ValueChangedEvent) {
    let filialsSelectBox = $("#FilialsSelectBox").dxSelectBox('instance');
    let filialsdataSource = <DevExpress.data.DataSource>$("#FilialsSelectBox").dxSelectBox('getDataSource');
    let scheduler = $("#IdTimeTable").dxScheduler('instance');

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

function FilialsSelectionChanged(e: DevExpress.ui.dxSelectBox.ValueChangedEvent) {
    let scheduler = $("#IdTimeTable").dxScheduler('instance');
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
        s.url += `?guid=${guidEmployee}`
    }
}

function ScheduleOnBeforeSend(e, s) {
    if (guidEmployee) {
        s.url += `?guidEmployee=${guidEmployee}&guidFilial=${guidFilial}`
    }
}

function ScheduleOnCellClick(e: DevExpress.ui.dxScheduler.CellClickEvent) {
    let choseDate = new Date(e.cellData.startDate).toDateString();
    $.ajax({
        url: TimeTableUrl,
        dataType: 'json',
        data: { guidEmployee, guidFilial, date: choseDate },
        success: function (data: string | null) {
            if (data) {
                RenderTimes(data);
            }
            else {
                RenderCreateTimesButton(choseDate);
            }
        },
        error: function (ex) {
            alert(ex)
        }
    });

}

function VisibleScheduleTime(data: boolean) {
    let scheduleTime = $("#ScheduleTime").dxDataGrid('instance');
    scheduleTime.option('visible', data);
}

function ClearCreateSchedule() {
    let createScheduleElement = $('#CreateSchedule');
    createScheduleElement.empty();
}

function UpdateScheduleTime() {
    let scheduleTimedataSource = <DevExpress.data.DataSource>$("#ScheduleTime").dxDataGrid('getDataSource');
    scheduleTimedataSource.reload();
}

function UpdateScheduler() {
    let TimeTabledataSource = <DevExpress.data.DataSource>$("#IdTimeTable").dxScheduler('getDataSource');
    TimeTabledataSource.reload();
}

function RenderTimes(data: string) {
    guidScheduleTime = data;
    ClearCreateSchedule();
    UpdateScheduleTime();
    VisibleScheduleTime(true);
}

function RenderCreateTimesButton(date: string) {
    let createScheduleElement = $('#CreateSchedule');
    ClearCreateSchedule();
    VisibleScheduleTime(false);
    let button = `<button class="btn btn-success" onClick="CreateTimeTable('${date}')">Создать расписание</button>`;
    createScheduleElement.append(button);
}

function OnBeforeSendScheduleTime(e, s) {
    if (guidScheduleTime) {
        s.url += `?TimeTableId=${guidScheduleTime}`
    }
}

function CreateTimeTable(date: string) {
    $.ajax({
        url: CreateTimeTableUrl,
        type: "post",
        dataType: 'json',
        data: { guidEmployee, guidFilial, date: date },
        success: function (data: string) {
            RenderTimes(data);
        },
        error: function (ex) {
            alert(ex)
        }
    });
}