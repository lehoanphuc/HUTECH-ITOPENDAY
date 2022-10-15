// Empty its mean select all =]] i'm lazy
var filterJobTitleIDs = [];
var filterCompanyIDs = [];

document.addEventListener("DOMContentLoaded", function (event) {
    getData();
});

// Get data base on filter
function getData() {
    $("#result").hide();
    $("#loading").show();

    $.ajax({
        url: _API_FINDJOB_GET,
        type: 'POST',
        data: {
            FilterJobTitleIDs: filterJobTitleIDs,
            FilterCompanyIDs: filterCompanyIDs
        }
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

    if (list.length == 0) {
        $("#applyAllButton").hide();
    }
    else {
        $("#applyAllButton").show();
    }
}

function printData(item) {
    var html = template(item.idCompany, item.idJobTitle, item.companyName, item.companyLogo, item.jobTitle, item.linkJD, item.isApplied, item.linkInterview);
    $("#dataTable").append(html);
}

// Some unless event
function selectJobTitleID(id) {
    let _button = $("[jobTitle=" + id + "]");

    if (isSelectedJobTitle(id)) {
        _button.removeClass("btn-primary");
        _button.addClass("btn-outline-primary");

        let index = filterJobTitleIDs.indexOf(id);
        if (index > -1) {
            filterJobTitleIDs.splice(index, 1);
        }
    }
    else {
        _button.addClass("btn-primary");
        _button.removeClass("btn-outline-primary");
        filterJobTitleIDs.push(id);
    }

    getData();
}

function isSelectedJobTitle(id) {
    return filterJobTitleIDs.includes(id);
}

function selectCompanyID(id) {
    let _button = $("[company=" + id + "]");

    if (isSelectedCompany(id)) {
        _button.removeClass("btn-primary");
        _button.addClass("btn-outline-primary");

        let index = filterCompanyIDs.indexOf(id);
        if (index > -1) {
            filterCompanyIDs.splice(index, 1);
        }
    }
    else {
        _button.addClass("btn-primary");
        _button.removeClass("btn-outline-primary");
        filterCompanyIDs.push(id);
    }

    getData();
}

function isSelectedCompany(id) {
    return filterCompanyIDs.includes(id);
}

function clearFilter() {
    filterJobTitleIDs = [];
    filterCompanyIDs = [];

    $('.filter-button').each(function (i, obj) {
        $(this).removeClass("btn-primary");
        $(this).addClass("btn-outline-primary");
    });

    getData();
}

function template(idCompany, idJobTitle, companyName, companyLogo, jobTitle, linkJD, isApplied, linkInterview) {
    var buttonTemplate = `<button type="button" class="btn btn-sm btn-block btn-primary" onclick="apply(${idCompany}, ${idJobTitle})"><i class="fa-solid fa-paper-plane"></i> Nộp CV</button>`;
    if (isApplied) {
        buttonTemplate = `<button type="button" class="btn btn-sm btn-block btn-warning" onclick="removeCV(${idCompany}, ${idJobTitle})"><i class="fa-solid fa-plane-slash"></i> Hủy ứng tuyển</button>`;
    }

    var buttonInterview = `<button type="button" class="btn btn-sm btn-block btn-danger" disabled><i class="fa-solid fa-circle fa-beat-fade"></i> Interview Online</button>`;
    if (linkInterview != null && linkInterview != "") {
        buttonInterview = `<a role="button" class="btn btn-sm btn-block btn-danger" href="${linkInterview}" target="_blank"><i class="fa-solid fa-circle fa-beat-fade"></i> Interview Online</a>`;
    }

    return `
<div class="card mt-4   ">
    <div class="card-body">
        <div class="row">
            <div class="col-md-3 d-none d-md-block">
                <img class="logo-company" src="/Assets/img/logo_company/${companyLogo}" onclick="location.href = '/company/${companyName}'"/>
            </div>
            <div class="col-9 col-md-6 mb-1">
                <h5 class="card-title">${companyName}</h5>
                <p class="card-text">
                    ${companyName} tuyển dụng vị trí ${jobTitle}<br />
                    <a href="${linkJD}" target="_blank">Xem tin tuyển dụng</a>
                </p>
            </div>
            <div class="col-12 col-md-3">
                ${buttonInterview}
                ${buttonTemplate}
            </div>
        </div>
    </div>
</div>
`;
}