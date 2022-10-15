document.addEventListener("DOMContentLoaded", function (event) {
    getData();
});

function getData() {
    $("#result").hide();
    $("#loading").show();

    $.ajax({
        url: _API_GET_EVENT,
        type: 'GET',
    }).done(function (result) {
        printTable(result);
        $("#loading").hide();
        $("#result").show();
    });
}

function printTable(list) {
    $("#dataTable").html("");
    list.forEach(printData);
}

function printData(item) {
    var html = template(item.id, item.name, item.time, item.place, item.allowReg, item.countStudent);
    $("#dataTable").append(html);
}

function template(id, name, time, room, status, count) {
    return `<tr>
                <td>${name}</td>
                <td>${time}</td>
                <td>${room}</td>
                <td>
                    <div class="custom-control custom-switch">
                        <input type="checkbox" class="custom-control-input" id="customSwitch${id}" onclick="toggleStatus(${id})" ${(status ? "checked" : "")}>
                        <label class="custom-control-label" for="customSwitch${id}"></label>
                    </div>
                </td>
                <td>${count}</td>
                <td class="text-right">
                    <div class="btn-group" role="group">
                        <a role="button" href="${_ADMIN_EVENT_INFO}/${id}" class="btn btn-outline-primary">
                            <i class="fa-solid fa-pen"></i>
                        </a>
                        <a role="button" href="${_ADMIN_EVENT_VIEWLISTSTUDENT}/${id}" class="btn btn-outline-primary">
                            <i class="fa-solid fa-eye"></i>
                        </a>
                        <button type="button" class="btn btn-block btn-danger" onclick="deleteClicked(${id})">Xóa</button>

                    </div>
                </td>
            </tr>`;
}

function toggleStatus(id) {
    $.ajax({
        url: _API_TOGGLE_STATUS_EVENT + "?id=" + id,
        type: 'GET',
    }).done(function (result) {
        if (result.success == false) {
            getData();
            alert(result.message);
        }
    });
}

function deleteClicked(id) {
    if (!confirm("Bạn có thực sự muốn xóa hội thảo này?")) {
        return;
    }

    deleteEvent(id);
}

function deleteEvent(id) {
    $.ajax({
        url: _API_DELETE_EVENT + "/" + id,
        type: 'POST',
    }).done(function (result) {
        alert(result.message);

        if (result.success) {
            getData();
        }
    });
}