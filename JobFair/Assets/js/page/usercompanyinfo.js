function saveData() {
    let id = $("#id").val();
    let username = $("#username").val();
    let password = $("#password").val();
    let fullname = $("#fullname").val();
    let email = $("#email").val();
    let phone = $("#phone").val();
    let idCompany = $("#idCompany").val();

    if (username == "" ||
        fullname == "") {
        alert("Vui lòng nhập họ tên và tài khoản");
        return;
    }

    if (idCompany == "" || idCompany == 0) {
        alert("Vui lòng chọn một công ty cho tài khoản này");
        return;
    }

    $('#submit').prop('disabled', true);

    $.ajax({
        url: _API_SAVE_USERCOMPANY,
        type: 'POST',
        data: {
            id,
            username,
            password,
            fullname,
            email,
            phone,
            idCompany
        }
    }).done(function (result) {
        $('#submit').prop('disabled', false);

        if (result.success) {
            alert("Lưu dữ liệu thành công");
            location.href = _ADMIN_USERCOMPANY_LIST;
        }
        else {
            alert(result.message);
        }
    });
}


// https://www.tutorialspoint.com/how-to-capitalize-the-first-letter-of-each-word-in-a-string-using-javascript#:~:text=Courses-,How%20to%20capitalize%20the%20first%20letter%20of,in%20a%20string%20using%20JavaScript%3F&text=At%20first%2C%20you%20need%20to,()%20for%20the%20extracted%20character.
function autoBeautyFullname() {
    let _name = $("#fullname");
    var words = _name.val();

    if (words == "") {
        return;
    }

    var separateWord = words.toLowerCase().split(' ');
    for (var i = 0; i < separateWord.length; i++) {
        separateWord[i] = separateWord[i].charAt(0).toUpperCase() +
            separateWord[i].substring(1);
    }

    _name.val(separateWord.join(' '));
}