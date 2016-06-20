window.onload = function () {
    document.getElementById('btnAdd').onclick = function () {
        addCategoryRequest();
    }
}

function addCategoryRequest() {
    sendRequest('/Categories/AddCategory', 'nameCategory='+document.getElementById('input').value, function (id,name) {
        addCategory(id,name);
    });
}

function deleteCategoryRequest(id) {
    sendRequest('/Categories/DeleteCategory', 'id=' + id, function () {
        deleteCategory(id)
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
                    successfulCallback(response.id, document.getElementById('input').value);
                }
            }
        }
    }
    xhr.send(data);
}

function addCategory(id, name) {
    var ul = document.getElementById("list");
    var li = document.createElement('li');
    var span = document.createElement('span');
    span.setAttribute('class', "glyphicon glyphicon-trash deleteIcon");
    span.id = id;
    span.onclick = function (e) {
        deleteCategoryRequest(e.target.id);
    }
    li.textContent = name;
    li.appendChild(span);
    li.id = id;
    li.style.position = 'relative';
    li.style.cursor = 'pointer';
    var firstChild = ul.firstChild;
    ul.insertBefore(li, firstChild);
}

function deleteCategory(id) {
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

