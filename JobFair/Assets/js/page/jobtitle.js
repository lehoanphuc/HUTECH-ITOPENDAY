document.addEventListener("DOMContentLoaded", function (event) {
    getData();
});

function getData() {
    $("#result").hide();
    $("#new").hide();
    $("#loading").show();

    $.ajax({
        url: _API_GET_JOBTITLE,
        type: 'GET',
    }).done(function (result) {
        printTable(result);
        $("#loading").hide();
        $("#result").show();
        $("#new").show();
    });
}

function printTable(list) {
    $("#dataTable").html("");
    list.forEach(printData);
}

function printData(item) {
    var html = template(item.id, item.title);
    $("#dataTable").append(html);
}

function template(id, title) {
    return `<tr>
                <td name="editForm">${title}</td>
                <td class="text-right">
                    <button type="button" onclick="edit($(this), ${id}, '${title}')" class="btn btn-outline-primary">
                        <i class="fa-solid fa-pen"></i>
                    </button>
                </td>
            </tr>`;
}

function templateInput(id, title) {
    return `<div class="input-group mb-3">
                <input name="id" value="${id}" hidden/>
                <input type="text" class="form-control" name="title" value="${title}" placeholder="Tên đầu việc" aria-label="Tên đầu việc">
                <div class="input-group-append">
                    <button class="btn btn-outline-secondary" type="button" onclick="saveData($(this))">Lưu lại</button>
                </div>
            </div>`;
}

function edit(_form, id, title) {
    let _root = _form.parent().parent();
    let _editForm = _root.find("[name=editForm]");
    _editForm.html(templateInput(id, title));
}

function saveData(_form) {
    let _root = _form.parent().parent();
    let _inputID = _root.find("[name=id]");
    let _inputTitle = _root.find("[name=title]");


    let id = _inputID.val();
    let title = _inputTitle.val();
    _inputID.prop('disabled', true);
    _inputTitle.prop('disabled', true);

    $.ajax({
        url: _API_SAVE_JOBTITLE,
        type: 'POST',
        data: {
            id,
            title
        }
    }).done(function (result) {
        _inputID.prop('disabled', false);
        _inputTitle.prop('disabled', false);

        if (result.success) {
            alert("Lưu dữ liệu thành công");
            _inputTitle.val('');
            getData();
        }
        else {
            alert(result.message);
        }
    });
}