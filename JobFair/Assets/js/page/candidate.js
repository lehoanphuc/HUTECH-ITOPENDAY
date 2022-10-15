document.addEventListener("DOMContentLoaded", function (event) {
    changeCompany();
    getData();
});

function getData() {
    $("#result").hide();
    $("#loading").show();

    let idCompany = $("#idCompany").val();
    let idJobTitle = $("#idJobTitle").val();

    $.ajax({
        url: _API_GET_CANDIDATE + "?idCompany=" + idCompany + "&idJobTitle=" + idJobTitle,
        type: 'GET'
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
    var html = template(item.id, item.index, item.studentCode, item.fullname, item.email, item.phone, item.class, item.jobTitle);
    $("#dataTable").append(html);
}

function template(id, index, code, name, email, phone, sclass, jobTitle) {
    if (!_ALLOW_CV) {
        return `<tr>
                <td>${index}</td>
                <td>${code}</td>
                <td>${name}</td>
                <td>${sclass}</td>
                <td>${jobTitle}</td>
            </tr>`;
    }

    return `<tr>
                <td>${index}</td>
                <td>${code}</td>
                <td>${name}</td>
                <td>${sclass}</td>
                <td>${email}</td>
                <td>${phone}</td>
                <td>${jobTitle}</td>
                <td class="text-right">
                    <a role="button" href="${_ADMIN_CANDIDATE_CV}${id}" class="btn btn-primary">
                        Xem CV
                    </a>
                </td>
            </tr>`;
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

// https://github.com/rainabba/jquery-table2excel
function exportExcel() {
    $("#exportTable").table2excel({
        name: "List",
        filename: "List.xls", // do include extension
        preserveColors: true // set to true if you want background colors and font colors preserved
    });
}


function changeCompany() {
    let idCompany = $("#idCompany").val();
    let _option = $("#idCompany").find("option[value = " + idCompany + "]")
    let supportJobTitles = _option.attr("support-jobtitle");
    let arrJobTitle = supportJobTitles.split(",");

    // Set default
    $("#idJobTitle").val("");

    // Hidden all value
    $(".jobTitle").each(function (index) {
        $(this).hide();
    });

    // Set new value
    for (let i = 0; i < arrJobTitle.length; i++) {
        $(".jobTitle[value = '" + arrJobTitle[i] + "']").show()
    }
}