function exporting(e) {
    var workbook = new ExcelJS.Workbook();
    var worksheet = workbook.addWorksheet('Employees');

    DevExpress.excelExporter.exportDataGrid({
        component: e.component,
        worksheet: worksheet,
        autoFilterEnabled: true
    }).then(function () {
        workbook.xlsx.writeBuffer().then(function (buffer) {
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Employees.xlsx');
        });
    });
    e.cancel = true;
}