document.addEventListener("DOMContentLoaded", function (event) {
    getData();
});

// Need define _ID_EVENT in view
function getData() {
    $("#result").hide();
    $("#loading").show();

    $.ajax({
        url: _API_STUDENT_EVENT + "?id=" + _ID_EVENT,
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
    var html = template(item.code, item.name, item.email, item.phone, item.class);
    $("#dataTable").append(html);
}

function template(code, name, email, phone, sclass) {
    return `<tr>
                <td>${code}</td>
                <td>${name}</td>
                <td>${sclass}</td>
                <td>${email}</td>
                <td>${phone}</td>
            </tr>`;
}

// https://github.com/rainabba/jquery-table2excel
function exportExcel() {
    $("#exportTable").table2excel({
        name: "List Student",
        filename: "Event" + _ID_EVENT + ".xls", // do include extension
        preserveColors: true // set to true if you want background colors and font colors preserved
    });
}