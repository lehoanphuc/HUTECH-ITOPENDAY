document.addEventListener("DOMContentLoaded", function (event) {
    getData();
});

function getData() {
    $("#result").hide();
    $("#loading").show();

    $.ajax({
        url: _API_GET_COMPANY,
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
    var html = template(item.id, item.name, item.logo);
    $("#dataTable").append(html);
}

function template(id, name, logo) {
    return `<tr>
                <td><img src="/Assets/img/logo_company/${logo}.png" width="80%" alt="Logo"></td>
                <td>${name}</td>
                <td class="text-right">
                    <a role="button" href="${_ADMIN_COMPANY_INFO}${id}" class="btn btn-outline-primary">
                        <i class="fa-solid fa-pen"></i>
                    </a>
                </td>
            </tr>`;
}


function toggleAllowInterview() {
    $.ajax({
        url: _API_TOGGLE_STATUS_INTERVIEW,
        type: 'GET',
    }).done(function (result) {
        if (result.success == false) {
            alert(result.message);
        }
    });
}