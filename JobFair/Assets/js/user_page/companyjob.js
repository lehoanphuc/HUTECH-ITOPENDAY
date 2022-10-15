// _IS_LOGIN variable was set in _LayoutUser
// Apply CV and Interview Online
function apply(idCompany, idJobTitle) {
    if (!_IS_LOGIN) {
        needLogin();
        return;
    }

    $.ajax({
        url: _API_FINDJOB_SUBMIT_CV + "?IdCompany=" + idCompany + "&IdJobTitle=" + idJobTitle,
        type: 'POST'
    }).done(function (result) {
        if (result.success) {
            // Alert
            alert(result.message);
            getData();
        }
        else {
            alert(result.message);
        }
    });
}

function applyAll() {
    if (!_IS_LOGIN) {
        needLogin();
        return;
    }

    if (!confirm("Bạn có chắc chắn muốn nộp CV vào toàn bộ các công ty và vị trí tuyển dụng bên dưới hay không?\nSau khi nộp CV, bạn vẫn có thể hủy.")) {
        return;
    }

    $.ajax({
        url: _API_FINDJOB_SUBMIT_CV_ALL,
        type: 'POST',
        data: {
            FilterJobTitleIDs: filterJobTitleIDs,
            FilterCompanyIDs: filterCompanyIDs
        }
    }).done(function (result) {
        if (result.success) {
            // Alert
            alert(result.message);
            getData();
        }
        else {
            alert(result.message);
        }
    });
}

function removeCV(idCompany, idJobTitle) {
    if (!_IS_LOGIN) {
        needLogin();
        return;
    }

    $.ajax({
        url: _API_FINDJOB_CANCEL_CV + "?IdCompany=" + idCompany + "&IdJobTitle=" + idJobTitle,
        type: 'POST'
    }).done(function (result) {
        if (result.success) {
            // Alert
            alert(result.message);
            getData();
        }
        else {
            alert(result.message);
        }
    });
}

function meet(idCompany) {
    if (!_IS_LOGIN) {
        needLogin();
        return;
    }

    $.ajax({
        url: _API_FINDJOB_INTERVIEW + "?IdCompany=" + idCompany,
        type: 'POST'
    }).done(function (result) {
        if (result.success) {
            if (confirm("Vui lòng ấn OK để được chuyển đến phòng phỏng vấn Online")) {
                window.open(
                    result.data,
                    '_blank'
                );
            }
        }
        else {
            alert(result.message);
        }
    });
}

function needLogin() {
    alert("Vui lòng đăng nhập trước khi thực hiện thao tác này\nBấm OK để bắt đầu đăng nhập");
    $("#modalLogin").modal("show");
}