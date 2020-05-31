$(function () {
    $.validator.methods["date"] = function (value, element) { return true; } 
    $('table[data-table="true"]').each(function (index) {
        $(this).DataTable({
            columnDefs: [{ type: 'date-euro', targets: 3 }],
            "order": [[3, "asc"]],
            "responsive": true,
            "autowidth": false
        });
    });
    $('.datetimepicker').datetimepicker({
        format:"d/m/Y H:i",
        minDate: 0,
        defaultTime: '12:00',
        step:"5"
    }
    );
    $.datetimepicker.setLocale("tr");

    $("body").on("click", "[data-delete-id]", function (event) {
        event.preventDefault();
        var button = $(this); // Button that triggered the modal
        var id = button.data('delete-id') // Extract info from data-* attributes
        var name = button.data('delete-name') // Extract info from data-* attributes
        var action = button.data('delete-action') // Extract info from data-* attributes
        $("#deleteModalForm").attr("action", action);
        $("#deleteModalItemName").text(name);
        $("#deleteModalItemId").val(id);
        $("#deleteModal").modal();
    });
})