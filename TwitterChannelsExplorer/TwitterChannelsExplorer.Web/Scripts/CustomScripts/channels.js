window.onload = function () {
    document.getElementById('btnAdd').onclick = function () {
        addChannelRequest();
    }
}

function addChannelRequest() {
    sendRequest("/Channels/AddChannel/",'channel='+document.getElementById("input").value, function (id,name) {
        addChannel(id,name);
    });
}

function deleteChannelRequest(id) {
    sendRequest("/Channels/DeleteChannel/",'id='+id, function () {
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
    span1.textContent = 'uncategorized';
    span1.id = id;
    span1.onclik = function (e) {
        chooseCategory(e.target.id);
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

function chooseCategory(id) {
    var li = document.getElementById(id);
    var input = document.createElement('input');
    input.name = "q";
    input.id = "query";
    li.appendChild(input);

    $('#query').autocomplete({
        serviceUrl: '/Channels/AutoComplete', // Страница для обработки запросов автозаполнения
        minChars: 2, // Минимальная длина запроса для срабатывания автозаполнения
        delimiter: /(,|;)\s*/, // Разделитель для нескольких запросов, символ или регулярное выражение
        maxHeight: 400, // Максимальная высота списка подсказок, в пикселях
        width: 300, // Ширина списка
        zIndex: 9999, // z-index списка
        deferRequestBy: 0, // Задержка запроса (мсек), на случай, если мы не хотим слать миллион запросов, пока пользователь печатает. Я обычно ставлю 300.
        params: { country: 'Yes' }, // Дополнительные параметры
        onSelect: function (data, value) { }, // Callback функция, срабатывающая на выбор одного из предложенных вариантов,
        lookup: ['January', 'February', 'March'] // Список вариантов для локального автозаполнения
    });
}