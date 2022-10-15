document.addEventListener("DOMContentLoaded", function (event) {
    getData();
});

function getData() {
    $("#result").hide();
    $("#loading").show();

    $.ajax({
        url: _API_GET_SCHOLARSHIP ,
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
    $("#count").html(list.length);
}

function printData(item) {
    var html = template(item.id, item.code, item.name, item.email, item.phone, item.point, item.roleName, item.activities, item.class);
    $("#dataTable").append(html);
}

function template(id, code, name, email, phone, point, role, activities, sclass) {
    return `<tr>
                <td>${code}</td>
                <td>${name}</td>
                <td>${sclass}</td>
                <td>${email}</td>
                <td>${phone}</td>
                <td>${point}</td>
                <td>${role}</td>
                <td>${activities}</td>
                <td>
                    <a role="button" class="btn btn-block btn-sm btn-primary" target="_blank" href="/admin/scholarshipcv/${id}">CV</a>
                    <a role="button" class="btn btn-block btn-sm btn-primary" target="_blank" href="/admin/scholarshippoint/${id}">Điểm</a>
                    <button type="button" class="btn btn-block btn-sm btn-danger" onclick="deleteClicked(${id})">Xóa</button>
                </td>
            </tr>`;
}

// https://github.com/rainabba/jquery-table2excel
function exportExcel() {
    $("#exportTable").table2excel({
        name: "List Student",
        filename: "Scholarship.xls", // do include extension
        preserveColors: true // set to true if you want background colors and font colors preserved
    });
}

function toggleAllowReg() {
    $.ajax({
        url: _API_TOGGLE_STATUS_SCHOLARSHIP,
        type: 'GET',
    }).done(function (result) {
        if (result.success == false) {
            getData();
            alert(result.message);
        }
    });
}

function deleteClicked(id) {
    if (!confirm("Bạn có thực sự muốn xóa thông tin đăng ký của sinh viên này?")) {
        return;
    }

    deleteReg(id);
}

function deleteReg(id) {
    $.ajax({
        url: _API_DELETE_SCHOLARSHIP + "/" + id,
        type: 'POST',
    }).done(function (result) {
        alert(result.message);

        if (result.success) {
            getData();
        }
    });
}