function saveResetPassword() {
    let password = $("#resetPassword").val();
    let repassword = $("#resetRePassword").val();

    if (password == "" || repassword == "") {
        alert("Vui lòng điền đủ thông tin bên trên");
        return;
    }

    if (password != repassword) {
        alert("Mật khẩu và xác nhận lại mật khẩu không trùng khớp");
        return;
    }
    $('#savePassSubmit').prop('disabled', true);

    grecaptcha.ready(function () {
        grecaptcha.execute('6Le2hPsUAAAAALzwDvu8vWcd7ImuBl_l49f927gN', { action: 'submit' }).then(function (token) {
            $.ajax({
                url: _API_RESETPASSWORD,
                type: 'POST',
                data: {
                    password,
                    repassword,
                    token,
                    UserToken: _USER_TOKEN
                }
            }).done(function (result) {
                $('#savePassSubmit').prop('disabled', false);
                alert(result.message);

                if (result.success) {
                    location.reload();
                }
            });
        });
    });
}

function saveChangePassword() {
    let currentPassword = $("#regCurrentPassword").val();
    let repassword = $("#resetRePassword").val();
    let password = $("#resetPassword").val();

    if (password == "" || repassword == "" || currentPassword == "") {
        alert("Vui lòng điền đủ thông tin bên trên");
        return;
    }

    if (password != repassword) {
        alert("Mật khẩu và xác nhận lại mật khẩu không trùng khớp");
        return;
    }
    $('#savePassSubmit').prop('disabled', true);

    grecaptcha.ready(function () {
        grecaptcha.execute('6Le2hPsUAAAAALzwDvu8vWcd7ImuBl_l49f927gN', { action: 'submit' }).then(function (token) {
            $.ajax({
                url: _API_CHANGEPASSWORD,
                type: 'POST',
                data: {
                    currentPassword,
                    password,
                    repassword,
                    token
                }
            }).done(function (result) {
                $('#savePassSubmit').prop('disabled', false);
                alert(result.message);

                if (result.success) {
                    location.reload();
                }
            });
        });
    });
}