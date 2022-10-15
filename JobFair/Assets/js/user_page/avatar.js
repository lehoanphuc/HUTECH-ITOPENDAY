$("#rotat").slider({
    value: 0,
    min: -180,
    max: 180,
    step: 1,
    slide: function () {
        makeImg();
    }

});

$("#resize").slider({
    value: 100,
    min: 0,
    max: 200,
    step: 1,
    slide: function (event, ui) {
        $('#img_resize').val(ui.value);
        makeImg();
    }
});

var isTouch = ('ontouchstart' in document.documentElement) ? true : false;
var IMG_OW = 4167 / 2;
var IMG_OH = 4167 / 2;
var IMG_W = $("#imgDiv").innerWidth() * 0.91;
var IMG_H = parseInt(IMG_W * IMG_OH / IMG_OW);
var pic_image = new Image();
pic_image.setAttribute('crossorigin', 'anonymous');
var stcanvas = document.getElementById('stcanvas');
var stcanvas_review = document.getElementById('stcanvas_review');

var ctx = stcanvas.getContext('2d');
var ctx_review = stcanvas_review.getContext('2d');

var imageLoader = document.getElementById('imageLoader');
imageLoader.addEventListener('change', handleImage, false);
var mask_image = new Image();
mask_image.onload = function () {
    makeImg();
    $("#loadingDiv").hide();
    $("#controlDiv").show();
}
mask_image.src = 'https://i.imgur.com/ADBQew4.png';
mask_image.setAttribute('crossorigin', 'anonymous');
var moveXAmount = 0;
var moveYAmount = 0;
var isDragging = false;
var prevX = 0;
var prevY = 0;

$("#posX").slider({
    value: 0,
    min: -IMG_W,
    max: IMG_W,
    step: 1,
    slide: function (event, ui) {
        moveXAmount = ui.value;
        makeImg();
    }
});

$("#posY").slider({
    value: 0,
    min: -IMG_H,
    max: IMG_H,
    step: 1,
    slide: function (event, ui) {
        moveYAmount = ui.value;
        makeImg();
    }
});

function makeImg() {
    try {
        buildcanvas();
        //buildLoiChuc();
        //buildTen();
    }
    catch (e) {
        alert("Có lỗi xảy ra vui lòng thử lại sau\nMessage: " + e);
        location.reload(true);
    }
}

function textGetLines(ctx, text, maxWidth) {
    var words = text.split(" ");
    var lines = [];
    var currentLine = words[0];

    for (var i = 1; i < words.length; i++) {
        var word = words[i];
        var width = ctx.measureText(currentLine + " " + word).width;
        if (width < maxWidth) {
            currentLine += " " + word;
        } else {
            lines.push(currentLine);
            currentLine = word;
        }
    }
    lines.push(currentLine);
    return lines;
}

function setDownload() {
    let btnDownload = $("#btnDownload");
    let oldHtml = btnDownload.html();
    btnDownload.html('<i class="fa fa-spinner fa-pulse fa-fw"></i> Vui lòng đợi');
    btnDownload.attr("disabled", true);

    try {
        ctx_review.canvas.width = IMG_OW;
        ctx_review.canvas.height = IMG_OH;
        make_pic_review(ctx_review);

        var link = document.createElement('a');
        link.download = 'avatar.png';
        link.href = stcanvas_review.toDataURL()
        link.click();
    }
    catch {

    }

    btnDownload.html(oldHtml);
    btnDownload.attr("disabled", false);
}

function buildcanvas() {
    ctx.canvas.width = IMG_W;
    ctx.canvas.height = IMG_H;
    make_pic(ctx);
}

function handleImage(e) {
    var reader = new FileReader();
    reader.onload = function (event) {
        pic_image = new Image();
        pic_image.src = event.target.result;
        makeImg();
    }

    reader.readAsDataURL(e.target.files[0]);

    setTimeout(function () {
        makeImg();
    }, 500);
}

// prepare image to fit canvas;

function prep_image() {
    pic_i = 400;
    xfact = pic_i / 400;
    return xfact;
}

function make_pic_review(ctx) {
    ctx.clearRect(0, 0, IMG_OW, IMG_OH);
    ctx.save();
    xfact = prep_image();

    var resizeValue = $('#resize').slider('value') / 100;
    var im_width = parseInt(pic_image.width * resizeValue * (IMG_OW / IMG_W));
    var im_height = parseInt(pic_image.height * resizeValue * (IMG_OH / IMG_H));
    ctx.translate(parseInt(IMG_OW / 2), parseInt(IMG_OH / 2));
    ctx.rotate($('#rotat').slider('value') * Math.PI / 180);
    ctx.drawImage(pic_image, (-IMG_OW / 2 + moveXAmount * (IMG_OH / IMG_H)), (-IMG_OH / 2 + moveYAmount * (IMG_OH / IMG_H)), im_width, im_height);

    // Mask for clipping
    ctx.restore();
    ctx.drawImage(mask_image, 0, 0, IMG_OW, IMG_OH);
}

function make_pic(ctx) {
    ctx.clearRect(0, 0, IMG_W, IMG_H);
    ctx.save();
    xfact = prep_image();

    var resizeValue = $('#resize').slider('value') / 100;
    var im_width = parseInt(pic_image.width * resizeValue);
    var im_height = parseInt(pic_image.height * resizeValue);
    ctx.translate(parseInt(IMG_W / 2), parseInt(IMG_H / 2));
    ctx.rotate($('#rotat').slider('value') * Math.PI / 180);
    ctx.drawImage(pic_image, -IMG_W / 2 + moveXAmount, -IMG_H / 2 + moveYAmount, im_width, im_height);

    // Mask for clipping
    ctx.restore();
    ctx.drawImage(mask_image, 0, 0, IMG_W, IMG_H);
}

function shareFB() {
    let base64 = stcanvas.toDataURL("image/png");
    base64 = base64.replace("data:image/png;base64,", "");
    let btn = $("#btnShare");
    let html = btn.html();
    btn.prop('disabled', true);
    btn.html("Vui lòng đợi");

    // Gọi ajax
    $.ajax({
        url: '/Event/ShareFacebookAsync',
        type: 'POST',
        data: {
            base64
        },
    }).done(function (result) {
        if (result != null && result != "null" && result != "") {
            let contentShare = "HUTECH CNTT";
            let url = 'http://www.facebook.com/sharer.php?u=' + encodeURIComponent("https://i.imgur.com/" + result + ".png") + '&t=' + encodeURIComponent(contentShare);
            window.open(url, "Chia sẻ facebook", 'width=500, height=300, scrollbars=yes, resizable=no');
        }
        else {
            alert("Có lỗi xảy ra khi server xử lý hình ảnh, vui lòng dùng tạm chức năng tải hình ảnh");
        }
    }).fail(function () {
        alert("Có lỗi xảy ra khi server xử lý thông tin");
    }).always(function () {
        btn.prop('disabled', false);
        btn.html(html);
    });
}

$("#stcanvas").mousedown(function () {
    isDragging = true;
    prevX = 0;
    prevY = 0;
});


$(window).mouseup(function () {
    isDragging = false;
    prevX = 0;
    prevY = 0;
});

$(window).mousemove(function (event) {
    if (isDragging == true) {
        if (prevX > 0 || prevY > 0) {
            moveXAmount += event.pageX - prevX;
            moveYAmount += event.pageY - prevY;
            makeImg();
        }
        prevX = event.pageX;
        prevY = event.pageY;
    }
});