var filterJobTitleIDs = [];

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
    var html = template(item.idCompany, item.idJobTitle, item.companyName, item.companyLogo, item.jobTitle, item.linkJD, item.isApplied);
    $("#dataTable").append(html);
}

function template(idCompany, idJobTitle, companyName, companyLogo, jobTitle, linkJD, isApplied) {
    var buttonTemplate = `<button type="button" class="btn btn-sm btn-block btn-primary" onclick="apply(${idCompany}, ${idJobTitle})"><i class="fa-solid fa-paper-plane"></i> Nộp CV</button>`;
    if (isApplied) {
        buttonTemplate = `<button type="button" class="btn btn-sm btn-block btn-warning" onclick="removeCV(${idCompany}, ${idJobTitle})"><i class="fa-solid fa-plane-slash"></i> Hủy ứng tuyển</button>`;
    }

    return `
<div class="card mt-4   ">
    <div class="card-body">
        <div class="row">
            <div class="col-md-3 d-none d-md-block">
                <img style="width: 80%" src="/Assets/img/logo_company/${companyLogo}" />
            </div>
            <div class="col-9 col-md-6 mb-1">
                <h5 class="card-title">Tuyển dụng ${jobTitle}</h5>
            </div>
            <div class="col-12 col-md-3">
                ${buttonTemplate}
            </div>
        </div>
    </div>
</div>
`;
}