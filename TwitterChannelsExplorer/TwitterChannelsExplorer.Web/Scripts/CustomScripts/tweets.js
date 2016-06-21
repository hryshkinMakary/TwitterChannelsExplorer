window.onload = function () {
    var span = document.getElementById('refresh').onclick = function () {
        addNewTweetsRequest();
    }
}

function addNewTweetsRequest() {
    var span = document.getElementById('refresh');
    var src_Name = span.getAttribute("srcName");
    var xhr = new XMLHttpRequest();
    var href = window.location.toString();
    var id = href.substr(href.lastIndexOf('/') + 1);
    xhr.open('POST', '/Tweets/GetPartialModel/', true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            var response = JSON.parse(xhr.responseText);
            if (response.status.toUpperCase() === 'OK') {
                showNewTweets(response.model);
            }
        }
    }
    var channelInfo = {
        ChannelId: id,
        ChannelName: src_Name
    }
    xhr.send(JSON.stringify(channelInfo));
}

function showNewTweets(model) {
    var tweets = $.map(model, function (el) {
        return el
    });
    for (var i = 1; i<tweets.length; i++) {
        var img = document.createElement('img');
        img.setAttribute('src', tweets[i].Src_Image);
        img.setAttribute('class', 'img-rounded');
        var p = document.createElement('p');
        p.textContent = tweets[i].Text;
        p.setAttribute('class', 'blockquote twitter-tweet');
        var img1 = document.createElement('img');
        img1.setAttribute('class', 'img-thumbnail myImage');
        if (tweets[i].Image !== "false") {
            img1.setAttribute('src', tweets[i].Image);
            img1.setAttribute('class', 'myImage');
        }
        var ol = document.getElementById('listTweets');
        var li = document.createElement('li');
        li.appendChild(img);
        li.appendChild(p);
        li.id = model.Id;
        li.setAttribute('class', 'blockquote twitter-tweet');
        var firstChild = ol.firstChild;
        ol.insertBefore(li, firstChild);
    }

}