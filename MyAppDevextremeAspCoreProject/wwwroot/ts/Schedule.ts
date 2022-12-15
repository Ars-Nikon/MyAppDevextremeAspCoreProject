let guidEmployee: string | null = null;

function SelectionChanged(e: DevExpress.ui.dxSelectBox.ValueChangedEvent) {
    if (e.value) {
        guidEmployee = e.value;
        let filialsSelectBox = $("#FilialsSelectBox").dxSelectBox('instance');
        filialsSelectBox.option('disabled', false);
        let filialdataSource = <DevExpress.data.DataSource>$("#FilialsSelectBox").dxSelectBox('getDataSource');
    }
}

function OnBeforeSend(e, s) {
    if (guidEmployee) {
        s.url += `?guid=${guidEmployee}`
    }
}