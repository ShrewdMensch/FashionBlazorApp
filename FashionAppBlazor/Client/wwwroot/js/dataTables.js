function AddAccessoriesDataTable(table) {
    if ($(table).length > 0) {
        if ($.fn.dataTable.isDataTable(table)) {
            $(table).addClass('nowrap').DataTable().destroy();
        }
        $(table).addClass('nowrap').DataTable(
            {
                responsive: true,
                columnDefs: [{ orderable: false, targets: -1 }],
                buttons: [
                    {
                        extend: "excelHtml5",
                        text: '<i class="fa fa-file-o mr-2"></i>Export to excel',
                        exportOptions: {
                            columns: ':not(.not-export-col)'
                        },
                        title: "List of Accessories"
                    },
                    {
                        extend: "copyHtml5",
                        text: '<i class="fa fa-copy mr-2"></i>Copy to clipboard',
                        exportOptions: {
                            columns: ':not(.not-export-col)'
                        },
                        title: "List of Accessories"

                    },
                    {
                        extend: "pdfHtml5",
                        text: '<i class="fa fa-file-o mr-2"></i>Export to PDF',
                        exportOptions: {
                            columns: ':visible :not(.not-export-col)'
                        },
                        title: "List of Accessories"
                    },
                ]
            });
    }
}