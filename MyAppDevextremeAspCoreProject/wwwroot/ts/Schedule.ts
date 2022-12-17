let guidEmployee: string | null = null;
let guidFilial: string | null = null;

function EmployeesSelectionChanged(e: DevExpress.ui.dxSelectBox.ValueChangedEvent) {
    let filialsSelectBox = $("#FilialsSelectBox").dxSelectBox('instance');
    let filialsdataSource = <DevExpress.data.DataSource>$("#FilialsSelectBox").dxSelectBox('getDataSource');
    let scheduler = $("#IdTimeTable").dxScheduler('instance');

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

function FilialsSelectionChanged(e: DevExpress.ui.dxSelectBox.ValueChangedEvent) {
    if (e.value) {
        guidFilial = e.value;
        let scheduler = $("#IdTimeTable").dxScheduler('instance');
        scheduler.option('visible', true);
        let TimeTabledataSource = <DevExpress.data.DataSource>$("#IdTimeTable").dxScheduler('getDataSource');
        TimeTabledataSource.reload();
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
