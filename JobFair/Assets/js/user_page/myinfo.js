var _userInfoLoading, _userInfoContent;

document.addEventListener("DOMContentLoaded", function (event) {
    loadEventInfo();
    loadDataInfo();
});

function loadDataInfo() {
    _userInfoLoading = $("#infoLoading");
    _userInfoContent = $("#infoContent");
}

function loadEventInfo() {
    document.getElementById('infoCV').addEventListener('change', function (e) {
        var fileName = document.getElementById("infoCV").files[0].name;
        var nextSibling = e.target.nextElementSibling
        nextSibling.innerText = fileName
    })
}

function updateCV() {
    $('#changepassContentTab').prop('disabled', true);
    _userInfoLoading.show();
    _userInfoContent.hide();

    let form = document.getElementById('cvForm');
    let formData = new FormData(form);

    grecaptcha.ready(function () {
        grecaptcha.execute('6Le2hPsUAAAAALzwDvu8vWcd7ImuBl_l49f927gN', { action: 'submit' }).then(function (token) {
            $.ajax({
                url: _API_UPDATECV + "?token=" + token,
                type: 'POST',
                contentType: false,
                processData: false,
                data: formData
            }).done(function (result) {
                $('#changepassContentTab').prop('disabled', false);
                _userInfoLoading.hide();
                _userInfoContent.show();

                alert(result.message);

                if (result.success) {
                    location.reload();
                }
            });
        });
    });
}