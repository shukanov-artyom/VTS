window.onload = function() {
    var lang = localStorage['Lang'];
    if (lang) {
        switch (lang) {
            case "Ru":
                translateRu();
                break;
            case "En":
                translateEn();
                break;
            case "By":
            case "Be":
                translateBy();
                break;
        }
    }
};

function translateRu() {
    document.getElementById('link-vehicles').innerText = 'Машины';
    document.getElementById('link-data').innerText = 'Данные';
    document.getElementById('link-analysis').innerText = 'Анализ';
}

function translateEn() {
    document.getElementById('link-vehicles').innerText = 'Cars';
    document.getElementById('link-data').innerText = 'Data';
    document.getElementById('link-analysis').innerText = 'Analysis';
}

function translateBy() {
    document.getElementById('link-vehicles').innerText = 'Машыны';
    document.getElementById('link-data').innerText = 'Дадзеныя';
    document.getElementById('link-analysis').innerText = 'Аналіз';
}