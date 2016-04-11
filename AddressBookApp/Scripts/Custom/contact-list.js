$(document).ready(function () {
    $('#contacts-list').dataTable({
        "columnDefs": [{//Actions
            "targets": 3,
            "orderable": false
        },
        {//ID
            "targets": 0,
            "visible": false,
            "orderable": false,
        }],
        "columns": [
            { "title": "Id", "data": "Id" },
            { "title": "Name", "data": "Name" },
            { "title": "Surname", "data": "Surname" },
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
});