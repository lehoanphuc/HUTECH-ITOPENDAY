function saveData() {
    $('#submit').prop('disabled', true);

    let form = document.querySelector('form');
    let formData = new FormData(form);

    $.ajax({
        url: _API_SAVE_COMPANY,
        type: 'POST',
        contentType: false,
        processData: false,
        data: formData
    }).done(function (result) {
        $('#submit').prop('disabled', false);

        if (result.success) {
            alert("Lưu dữ liệu thành công");
            location.href = _ADMIN_COMPANY_LIST;
        }
        else {
            alert(result.message);
        }
    });
}
