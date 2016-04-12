$(document).ready(function () {   
    $('#contacts-list tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('<input type="text" class="form-control" placeholder="Search ' + title + '" />');
    });

    var table = $('#contacts-list').DataTable({
        "columnDefs": [ {//ID
            "targets": 0,
            "visible": false,
            "orderable": false,
        },
        {//Actions
            "targets": [3, 4, 5],
            "orderable": false
        },
       ],
        "columns": [
            { "title": "Id", "data": "Id" },
            { "title": "Name", "data": "Name" },
            { "title": "Surname", "data": "Surname" },
            {
                "title": "Emails",
                "data": "Emails",
                "render": function (data, type, row) {
                    var emailsListHtml = '';
                    for (var i = 0; i < data.length; i++) {
                        emailsListHtml += data[i].Name + '</br>';
                    }
                    return emailsListHtml;
                }
            },
            {
                "title": "Addresses",
                "data": "Addresses",
                "render": function (data, type, row) {                                  
                    var addressesListHtml = '';
                    for (var i = 0; i < data.length; i++) {
                        addressesListHtml += data[i].Name + '</br>';
                    }
                    return addressesListHtml;
                }
            },
            {
                "data": null,                
                "render": function (data, type, row) {
                    return '<a class="glyphicon glyphicon-edit" href=/Contacts/Edit/' + data.Id + '>Edit</a>' +
                           '<a class="margin-left-25 glyphicon glyphicon-trash" data_toggle="modal" data_target="#modal-container" href=/Contacts/Delete/' + data.Id + '>Delete</a>';
                }
            }
        ],
        "processing": true,
        "serverSide": true,
        "ajax": "/Contacts/GetFilteredContacts"
    });

    table.columns().every(function () {
        var that = this;
        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });
});