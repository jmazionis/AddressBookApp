$(document).ready(function () {
    $('#contacts-list').dataTable({
        "columnDefs": [{
            "targets": 2,
            "orderable": false
        }]
    });
});