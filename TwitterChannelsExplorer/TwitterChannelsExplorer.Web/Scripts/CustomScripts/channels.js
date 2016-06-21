window.onload = function () {
    document.getElementById('btnAdd').onclick = function () {
        addChannelRequest();
    }
}


function addChannelRequest() {
    sendRequest("/Channels/AddChannel/", 'channel=' + document.getElementById("input").value, function (id, name) {
        addChannel(id, name);
    });
}

function deleteChannelRequest(id) {
    sendRequest("/Channels/DeleteChannel/", 'id=' + id, function () {
        deleteChannel(id);
    });
}

function sendRequest(url, data, successfulCallback) {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", url, true);
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    xhr.onreadystatechange = function (e) {
        if (xhr.readyState === 4 && xhr.status === 200) {
            var response = JSON.parse(xhr.responseText);
            if (response.status.toUpperCase() === 'OK') {
                if (successfulCallback) {
                    successfulCallback(response.id, document.getElementById("input").value);
                }
            }
        }
    }
    xhr.send(data);
}

function addChannel(id, name) {
    var ul = document.getElementById("list");
    var li = document.createElement('li');
    var span = document.createElement('span');
    var span1 = document.createElement('span');
    var a = document.createElement('a');
    span.setAttribute('class', "glyphicon glyphicon-trash deleteIcon");
    span.id = id;
    span.onclick = function (e) {
        deleteChannelRequest(e.target.id);
    }
    span1.id = id;
    span1.textContent = 'uncategorized';
    span1.onclick = function (e) {
        chooseCategory(e);
    }
    a.setAttribute('href', "/Tweets/GetTweets/" + id);
    a.style.color = "white";
    a.textContent = name;
    li.appendChild(a);
    li.appendChild(span);
    li.appendChild(span1);
    li.id = id;
    li.style.position = 'relative';
    li.style.cursor = 'pointer';
    var firstChild = ul.firstChild;
    ul.insertBefore(li, firstChild);
}

function deleteChannel(id) {
    var li = document.getElementById(id);
    var start = Date.now();
    var timer = setInterval(function () {
        var timePassed = Date.now() - start;
        if (timePassed >= 2000) {
            clearInterval(timer);
            var ul = document.getElementById('list');
            ul.removeChild(li);
            return;
        }
        li.style.left = timePassed / 2.5 + 'px';
    }, 10);
}

function drawDeleteAnimation(timePassed, right) {
    right = timePassed / 5 + 'px';
}

function chooseCategory(e) {
    var li = document.getElementById(e.id);
    var input = document.createElement('input');
    input.type = 'text';
    li.appendChild(input);

    $(input).autocomplete({
        source: 'Categories/GetCategoryAutoComplete/' + input.value,
        select: function (ui) {
            e.textContent = ui.currentTarget.innerText
            li.removeChild(input);
        }
    });
}