document.addEventListener("DOMContentLoaded", function (event) {
    $('#datetimepicker1').datetimepicker({
        format: 'DD/MM/YYYY HH:mm'
    });
});

function saveData() {
    $('#submit').prop('disabled', true);

    let dateObj = $('#datetimepicker1').datetimepicker('viewDate');

    $.ajax({
        url: _API_SAVE_EVENT,
        type: 'POST',
        data: {
            Id: $("[name=id]").val(),
            Name: $("[name=name]").val(),
            Description: $("[name=description]").val(),
            Place: $("[name=place]").val(),
            AllowReg: $('[name=allowReg]').is(':checked'),
            DateTimeType: dateObj.format('MM-DD-YYYY HH:mm:00')
        }
    }).done(function (result) {
        $('#submit').prop('disabled', false);

        if (result.success) {
            alert("Lưu dữ liệu thành công");
            location.href = _ADMIN_EVENT_LIST;
        }
        else {
            alert(result.message);
        }
    });
}
