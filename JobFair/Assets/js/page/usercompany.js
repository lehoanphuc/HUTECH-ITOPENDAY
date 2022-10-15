document.addEventListener("DOMContentLoaded", function (event) {
    getData();
});

function getData() {
    $("#result").hide();
    $("#loading").show();

    $.ajax({
        url: _API_GET_USERCOMPANY,
        type: 'GET',
    }).done(function (result) {
        printTable(result);
        $("#loading").hide();
        $("#result").show();
    });
}

function printTable(list) {
    $("#dataTable").html("");
    list.forEach(printData);
}

function printData(item) {
    var html = template(item.id, item.username, item.fullname, item.email, item.phone, item.companyName);
    $("#dataTable").append(html);
}

function template(id, username, name, email, phone, company) {
    return `<tr>
                <td>${username}</td>
                <td>${name}</td>
                <td>${email}</td>
                <td>${phone}</td>
                <td>${company}</td>
                <td class="text-right">
                    <div class="btn-group" role="group">
                        <a role="button" class="btn btn-primary" target="_blank" href="/admin/viewusercompany/${id}">
                            <i class="fa-solid fa-pen"></i>
                        </a>
                        <button type="button" class="btn btn-primary" onclick="sendMail(${id}, $(this))">
                            <i class="fa-solid fa-paper-plane"></i>
                        </button>
                        <button type="button" class="btn btn-block btn-danger" onclick="deleteClicked(${id})">Xóa</button>
                    </div>
                </td>
            </tr>`;
}

function deleteClicked(id) {
    if (!confirm("Bạn có thực sự muốn xóa thông tin tài khoản này?")) {
        return;
    }

    deleteUserCompany(id);
}

function deleteUserCompany(id) {
    $.ajax({
        url: _API_DELETE_USER + "/" + id,
        type: 'POST',
    }).done(function (result) {
        alert(result.message);

        if (result.success) {
            getData();
        }
    });
}

function sendMail(id, _this) {
    if (!confirm("Tính năng này sẽ random password và send email với thông tin password mới cho user này, bạn có thực sự muốn thực hiện?")) {
        return;
    }

    _this.prop('disabled', true);

    $.ajax({
        url: _API_SENDPASS_USER + "/" + id,
        type: 'POST',
    }).done(function (result) {
        alert(result.message);
        _this.prop('disabled', false);
    });
}

function toggleAllowViewCV() {
    $.ajax({
        url: _API_TOGGLE_STATUS_CV,
        type: 'GET',
    }).done(function (result) {
        if (result.success == false) {
            alert(result.message);
        }
    });
}