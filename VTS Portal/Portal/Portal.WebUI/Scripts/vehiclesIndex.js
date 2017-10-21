window.onload = function() {
    var langString = localStorage['Lang'];
    if (langString) {
        switch (langString) {
        case "Ru":
            setRussian();
            break;
        case "By":
            setBelarusian();
            break;
        case "En":
            setEnglish();
            break;
        }
    }
};

function setRussian() {
    var buttons = document.getElementsByClassName('btn-characteristics');
    for (var i = 0; i < buttons.length; i++) {
        buttons[i].innerText = 'Характеристики';
    }
    var buttonsData = document.getElementsByClassName('btn-data');
    for (i = 0; i < buttonsData.length; i++) {
        buttonsData[i].innerText = 'Данные';
    }
}

function setBelarusian() {
    var buttonsChars = document.getElementsByClassName('btn-characteristics');
    for (var i = 0; i < buttonsChars.length; i++) {
        buttonsChars[i].innerText = 'Характарыстыкі';
    }
    var buttonsData = document.getElementsByClassName('btn-data');
    for (i = 0; i < buttonsData.length; i++) {
        buttonsData[i].innerText = 'Дадзеныя';
    }
}

function setEnglish() {
    var buttons = document.getElementsByClassName('btn-characteristics');
    for (var i = 0; i < buttons.length; i++) {
        buttons[i].innerText = 'Characteristics';
    }
    var buttonsData = document.getElementsByClassName('btn-data');
    for (i = 0; i < buttonsData.length; i++) {
        buttonsData[i].innerText = 'Data';
    }
}