// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var dataTable;
var pop;
$(function () {
    

    dataTable = $("#jobTable").DataTable({
        "ajax": {
            "url": "/api/jobs",
            "type": "GET",
            "datatype": "json",
            "dataSrc": ""
        },
        "columns": [
            { "data": "title" },
            { "data": "description" },
            { "data": "createdAt", "render": function (data) { return new Date(data).toLocaleDateString(); } },
            { "data": "expiresAt", "render": function (data) { return new Date(data).toLocaleDateString(); } },
            {
                "data": "id",
                "render": function (data) {
                    return "<a class='btn btn-secondary' onclick='EditJob(" + data + ")'>Edit</a> <a class='btn btn-secondary' onclick='DeleteJob(" + data + ")'>Delete</a>";
                }
            }
        ]
    });


    //
    $("#btnCreateJob").click(function () {
        //if ($("#CreateModal form .expire-date").val() == "") {
        //    $("#CreateModal form .expire-date").val("0001-01-01");
        //}
        var data = ConvertFormToJSON($("#CreateModal form"));
        CreateJob(data);
    });

    $("#btnUpdateJob").click(function () {
        //if ($("#UpdateModal form .expire-date").val() == "") {
        //    $("#UpdateModal form .expire-date").val("0001-01-01");
        //}
        var data = ConvertFormToJSON($("#UpdateModal form"));
        SendEditJob(data);
    });

});

function CreateJob(data) {
    $.ajax({
        type: "POST",
        url: "/api/jobs",
        dataType: "json",
        contentType: "application/json",
        data: data,
        success: function(result) {
            dataTable.ajax.reload();
            $('#CreateModal').modal('hide');
            ResetModal();
        }
    })
}

function DeleteJob(id) {
    if (confirm("Are you sure you want to delete this job?")) {
        $.ajax({
            type: "DELETE",
            url: "/api/jobs/" + id
        }).done(function () {
            dataTable.ajax.reload();
        })
    }
}

function EditJob(id) {
    $.ajax({
        type: "GET",
        url: "/api/jobs/" + id
    }).done(function (data) {

        var expiresAt = new Date(data.expiresAt);
        var exDate = expiresAt.getFullYear() + "-" + ("0" + (expiresAt.getMonth() + 1)).slice(-2) + "-" + ("0" + (expiresAt.getDate())).slice(-2);
        $('#UpdateModal #txtTitle').val(data.title);
        $("#UpdateModal #txtDescription").val(data.description);
        $("#UpdateModal #txtExpires").val(exDate);
        $("#UpdateModal #idx").val(data.id);
        $('#UpdateModal').modal('show');

    });   
}

function SendEditJob(data) {
    var idx = $("#UpdateModal #idx").val();
    $.ajax({
        type: "PUT",
        url: "/api/jobs/" + idx,
        dataType: "json",
        contentType: "application/json",
        data: data,
        success: function (response) {
            dataTable.ajax.reload();
            $('#UpdateModal').modal('hide');
            ResetModal();
        }
    });
}

function ConvertFormToJSON(form) {
    var array = jQuery(form).serializeArray();
    var json = {};

    jQuery.each(array, function () {
        json[this.name] = this.value || '';
    });

    return JSON.stringify(json);
}

function ResetModal() {
    $("#CreateModal input, #UpdateModal input").val("");
}