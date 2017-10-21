window.onload = function() {
    localize();
};

function localize() {
    var lang = localStorage['Lang'];
    switch (lang) {
        case 'Ru':
            setRussian();
            break;
        case 'By':
            setBelarusian();
            break;
        case 'En':
            setEnglish();
            break;
    }
}

function setRussian() {
    document.getElementById('header-navbar').innerText = 'Портал VTS';
}

function setBelarusian() {
    document.getElementById('header-navbar').innerText = 'Партал VTS';
}

function setEnglish() {
    document.getElementById('header-navbar').innerText = 'VTS Portal';
}