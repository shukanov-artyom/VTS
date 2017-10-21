window.onload = function () {
    raiseLang();
    document.getElementById('dropdown-language').onchange = onLanguageChange;
    document.getElementById('text-box-email').onkeydown = validateInput;
    document.getElementById('button-logon').isEnabled = false;
};

function onLanguageChange() {
    var dropdown = this;
    var langString = dropdown.value;
    localStorage['Lang'] = langString;
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

function raiseLang() {
    var lang = localStorage['Lang'];
    if (lang) {
        switch (lang) {
            case "Ru":
                document.getElementById('dropdown-language').value = "Ru";
                break;
            case "By":
                document.getElementById('dropdown-language').value = "By";
                break;
            case "En":
                document.getElementById('dropdown-language').value = "En";
                break;
        }
    }
}

function setRussian() {
    document.getElementById('document-title').innerText =
        'Портал системы VTS Автомониторинг – Вход';
    document.getElementById('text-box-email').placeholder = 'Email или Логин';
    document.getElementById('text-box-password').placeholder = 'Пароль';
    document.getElementById('button-logon').value = 'Войти';
    var userNotFound = document.getElementById('label-usernotfound');
    if (userNotFound) {
        userNotFound.innerText = 'Такая комбинация не найдена';
    }
}

function setBelarusian() {
    document.getElementById('document-title').innerText =
        'Партал сістэмы VTS Аўтаманіторынг – Уваход';
    document.getElementById('text-box-email').placeholder = 'Email альбо Логін';
    document.getElementById('text-box-password').placeholder = 'Пароль';
    document.getElementById('button-logon').value = 'Увайсці';
    var userNotFound = document.getElementById('label-usernotfound');
    if (userNotFound) {
        userNotFound.innerText = 'Гэтая камбінацыя не знойдзена';
    }
}

function setEnglish() {
    document.getElementById('document-title').innerText =
        'VTS Automonitoring system Portal – Logon';
    document.getElementById('text-box-email').placeholder = 'Email or Login';
    document.getElementById('text-box-password').placeholder = 'Password';
    document.getElementById('button-logon').value = 'Log on';
    var userNotFound = document.getElementById('label-usernotfound');
    if (userNotFound) {
        userNotFound.innerText = 'This combination not found';
    }
}

function validateInput() {
    var login = document.getElementById('text-box-email').value;
    var password = document.getElementById('text-box-password').value;
    var buttonLogon = document.getElementById('button-logon');
    buttonLogon.isEnabled = password && login;
}