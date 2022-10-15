var selectedEventIds = [];

// element
var _name, _code, _email, _phone, _class;

document.addEventListener("DOMContentLoaded", function (event) {
    _name = $("#studentName");
    _code = $("#studentCode");
    _email = $("#studentEmail");
    _phone = $("#studentPhone");
    _class = $("#studentClass");

    loadData();
});

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

function selectEvent(id) {
    let _card = $("[eventId=" + id + "]");

    if (isSelected(id)) {
        _card.removeClass("text-white bg-primary");
        removeEventId(id);
    }
    else {
        _card.addClass("text-white bg-primary");
        selectedEventIds.push(id);
    }
}

function isSelected(id) {
    return selectedEventIds.includes(id);
}

function removeEventId(id) {
    let index = selectedEventIds.indexOf(id);
    if (index > -1) {
        selectedEventIds.splice(index, 1);
    }
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
    if (!validateInput()) {
        return;
    }

    $('#submit').prop('disabled', true);

    $.ajax({
        url: _API_SUBMIT_STUDENT_EVENT,
        type: 'POST',
        data: {
            Code: _code.val(),
            Email: _email.val(),
            Phone: _phone.val(),
            Name: _name.val(),
            Class: _class.val(),
            EventIDs: selectedEventIds
        }
    }).done(function (result) {
        $('#submit').prop('disabled', false);

        if (result.success) {
            // Save cookie
            saveData();

            // Alert
            alert("Đăng ký thành công, ban tổ chức sẽ sớm liên hệ với bạn qua Email hoặc Số điện thoại\nVui lòng theo dõi các thông tin mới nhất trên fanpage của Khoa CNTT HUTECH");
            location.href = "/";
        }
        else {
            alert(result.message);
        }
    });
}

function validateInput() {
    if (selectedEventIds.length < 1) {
        alert("Vui lòng chọn ít nhất 1 hội thảo mà bạn muốn tham gia");
        return false;
    }

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