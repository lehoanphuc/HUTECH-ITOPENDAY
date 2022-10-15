// element
var _name, _code, _email, _phone, _activities, _point, _class;


document.addEventListener("DOMContentLoaded", function (event) {
    _name = $("#studentName");
    _code = $("#studentCode");
    _email = $("#studentEmail");
    _phone = $("#studentPhone");
    _activities = $("#studentActivities");
    _point = $("#studentPoint");
    _class = $("#studentClass");

    loadEvent();
    loadData();
});

function loadEvent() {
    document.getElementById('file1').addEventListener('change', function (e) {
        var fileName = document.getElementById("file1").files[0].name;
        var nextSibling = e.target.nextElementSibling
        nextSibling.innerText = fileName
    })

    document.getElementById('file2').addEventListener('change', function (e) {
        var fileName = document.getElementById("file2").files[0].name;
        var nextSibling = e.target.nextElementSibling
        nextSibling.innerText = fileName
    })
}

function loadData() {
    _name.val(getCookie("sname"));
    _code.val(getCookie("scode"));
    _email.val(getCookie("semail"));
    _phone.val(getCookie("sphone"));
    _class.val(getCookie("sclass"));
}

function saveData() {
    setCookie("sname", _name.val(), 7);
    setCookie("scode", _code.val(), 7);
    setCookie("semail", _email.val(), 7);
    setCookie("sphone", _phone.val(), 7);
    setCookie("sclass", _class.val(), 7);
}

// https://www.tutorialspoint.com/how-to-capitalize-the-first-letter-of-each-word-in-a-string-using-javascript#:~:text=Courses-,How%20to%20capitalize%20the%20first%20letter%20of,in%20a%20string%20using%20JavaScript%3F&text=At%20first%2C%20you%20need%20to,()%20for%20the%20extracted%20character.
function autoBeautyFullname() {
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
    console.log("Debug beauty name");
}

function submitData() {
    grecaptcha.ready(function () {
        grecaptcha.execute('6Le2hPsUAAAAALzwDvu8vWcd7ImuBl_l49f927gN', { action: 'submit' }).then(function (token) {
            callAPI(token);
        });
    });
}

function callAPI(token) {
    if (!validateInput()) {
        return;
    }

    $('#submit').prop('disabled', true);

    //let roles = [];
    //$("[name='studentRole']:checked").each(function (i, obj) {
    //    roles.push($(this).val());
    //});

    var form = $("#form");
    var formData = new FormData(form[0]);

    $.ajax({
        url: _API_SUBMIT_STUDENT_SCHOLARSHIP + "?token=" + token,
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false
        //data: {
        //    Code: _code.val(),
        //    Email: _email.val(),
        //    Phone: _phone.val(),
        //    Name: _name.val(),
        //    Activities: _activities.val(),
        //    Class: _class.val(),
        //    Point: _point.val(),
        //    Role: roles
        //}
    }).done(function (result) {
        $('#submit').prop('disabled', false);

        if (result.success) {
            alert(result.message);
            location.href = "/";
        }
        else {
            alert(result.message);
        }
    });
}

function validateInput() {
    if (_email.val() == "" || _code.val() == "" ||
        _name.val() == "" || _phone.val() == "" ||
        _class.val() == "") {
        alert("Vui lòng điền đầy đủ thông tin của bạn");
        return false;
    }

    if (!validateEmail(_email.val())) {
        alert("Vui lòng điền đúng định dạng email của bạn");
        return false;
    }

    if (!validatePhone(_phone.val())) {
        alert("Vui lòng điền đúng định dạng số điện thoại của bạn");
        return false;
    }

    let roles = [];
    $("[name='studentRole']:checked").each(function (i, obj) {
        roles.push($(this).val());
    });

    if (roles.length < 1) {
        alert("Vui lòng chọn đối tượng xét tuyển");
        return false;
    }

    return true;
}

// https://www.w3resource.com/javascript/form/email-validation.php
function validateEmail(mail) {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(mail)) {
        return (true)
    }
    return (false)
}

// https://stackoverflow.com/questions/4338267/validate-phone-number-with-javascript
function validatePhone(phone) {
    return phone.match(/\d/g).length === 10;
}