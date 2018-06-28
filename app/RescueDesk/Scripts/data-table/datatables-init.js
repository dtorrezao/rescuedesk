(function ($) {
    //    "use strict";

    /*  Data Table
    -------------*/

    //https://datatables.net/examples/advanced_init/
    $('#bootstrap-data-table').DataTable({
        //dom: 'lBfrtip',
        lengthMenu: [[10, 20, 50, -1], [10, 20, 50, "Todos"]],
        pageLength: 20,
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.10.16/i18n/Portuguese.json"
        },
        columnDefs: [{
            "targets": 'no-sort',
            "orderable": false,
        }],
        //buttons: [
        //    'copy', 'csv', 'excel', 'pdf', 'print'
        //]
    });

    $('#bootstrap-data-table-pedidos').DataTable({
        //dom: 'lBfrtip',
        lengthMenu: [[10, 20, 50, -1], [10, 20, 50, "Todos"]],
        pageLength: 20,
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.10.16/i18n/Portuguese.json"
        },
        columnDefs: [{
            "targets": 'no-sort',
            "orderable": false,
        }],
        "ordering": false,
        //buttons: [
        //    'copy', 'csv', 'excel', 'pdf', 'print'
        //]
    });

    $('#bootstrap-data-table-export').DataTable({
        dom: 'lBfrtip',
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });

    $('#row-select').DataTable({
        initComplete: function () {
            this.api().columns().every(function () {
                var column = this;
                var select = $('<select class="form-control"><option value=""></option></select>')
                    .appendTo($(column.footer()).empty())
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );

                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });
        }
    });

    $('#bootstrap-data-table-ajax').DataTable({
        processing: true,
        serverSide: true,
        ajax: $("#bootstrap-data-table-ajax").data("url"),
        lengthMenu: [[10, 20, 50, -1], [10, 20, 50, "Todos"]],
        pageLength: 20,
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.10.16/i18n/Portuguese.json"
        },
        columnDefs: [{
            "targets": 'no-sort',
            "orderable": false,
        }],
    });
})(jQuery);