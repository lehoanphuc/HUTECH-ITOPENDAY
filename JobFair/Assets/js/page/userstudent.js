var currentPage = 1;
document.addEventListener("DOMContentLoaded", function (event) {
    getData();
});

function getData() {
    $("#result").hide();
    $("#loading").show();

    $.ajax({
        url: _API_GET_USERSTUDENT + "?page=" + currentPage,
        type: 'GET',
    }).done(function (result) {
        printTable(result.data);

        printPageNavigation(result.totalPage, currentPage);
        $("#count").html(result.totalRecord);

        $("#loading").hide();
        $("#result").show();
    });
}

function printPageNavigation(totalPage, currentPage) {
    let _dataPage = $("#dataPage");
    let _listPage = $("#listPage");
    
    _dataPage.hide();
    _listPage.html("");
    if (totalPage < 2) {
        return;
    }

    let _html = "";
    for (let i = 1; i <= totalPage; i++) {
        if (i > 3 && i < totalPage - 3) {
            i = totalPage - 3;

            if (currentPage > 3 && currentPage <= totalPage - 3) {
                _html += printPage("...", currentPage - 1);
                _html += printPage(currentPage, currentPage);
            }
            _html += printPage("...", currentPage + 1);

            continue;
        }

        _html += printPage(i, i);
    }

    _listPage.html(`
                ${printPage("Trước", currentPage - 1)}
                ${_html}
                ${printPage("Sau", (currentPage + 1 > totalPage ? "" : currentPage + 1))}`);

    _dataPage.show();
}

function goPage(page) {
    if (page == undefined
        || page == ""
        || page < 1) {
        return;
    }
    currentPage = page;
    getData();
}

function printPage(name, page) {
    return `<li class="page-item ${(currentPage == name ? "active" : "")}">
                    <a class="page-link" href="javascript:void(0);" onclick="goPage(${page})">${name}</a>
                </li>`;
}

function printTable(list) {
    $("#dataTable").html("");
    list.forEach(printData);
}

function printData(item) {
    var html = template(item.id, item.username, item.fullname, item.email, item.phone, item.class);
    $("#dataTable").append(html);
}

function template(id, username, name, email, phone, sclass) {
    return `<tr>
                <td>${username}</td>
                <td>${name}</td>
                <td>${sclass}</td>
                <td>${email}</td>
                <td>${phone}</td>
                <td class="text-right">
                    <a role="button" href="${_ADMIN_CANDIDATE_CV}${id}" class="btn btn-primary">
                        Xem CV
                    </a>
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