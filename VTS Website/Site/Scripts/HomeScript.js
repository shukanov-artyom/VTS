var manualSelected = false;
var currentBannerNumber = 0;

$(function() {
    String.prototype.trim = function() {
        return this.replace(/^\s+|\s+$/g, "");
    };

    String.prototype.ltrim = function() {
        return this.replace(/^\s+/, "");
    };

    String.prototype.rtrim = function() {
        return this.replace(/\s+$/, "");
    };
});

function markBanner1() {
    manualSelected = true;
    mark(1);
}

function markBanner2() {
    manualSelected = true;
    mark(2);
}

function markBanner3() {
    manualSelected = true;
    mark(3);
}

function markBanner4() {
    manualSelected = true;
    mark(4);
}

function mark(bannerId) {

    currentBannerNumber = bannerId;

    $("#banner-selector-button1").css("background-image", "url(../../Content/Images/banner_unactive.png)");
    $("#banner-selector-button2").css("background-image", "url(../../Content/Images/banner_unactive.png)");
    $("#banner-selector-button3").css("background-image", "url(../../Content/Images/banner_unactive.png)");
    $("#banner-selector-button4").css("background-image", "url(../../Content/Images/banner_unactive.png)");
    
    $(getFullTargetName(bannerId)).css("background-image", "url(../../Content/Images/banner_active.png)");

    $.ajax({
        url: "/Home/GetBanner?number=" + bannerId,
        type: 'POST',
        success: function (data) {
            document.getElementById("banner-content").innerHTML = data;
            $("#banner-content").hide().fadeIn();
        }
    });
}

function getFullTargetName(bannerNumber) {
    var fullBannerName = '#banner-selector-button' + bannerNumber;
    return fullBannerName;
}

function bannersRefresher() {
    if (!manualSelected) {
        var nextNumber = getNextNumber(currentBannerNumber);
        mark(nextNumber);
    }
}

function onVinSubmit() {
    document.getElementById('button-vin-submit').disabled = true;
    document.getElementById("vin-check-result").innerHTML = "<p>...</p>";
    var vin = document.getElementById('input-vin').value;
    var firstThreeUppercaseLetters = vin.substr(0, 3).toUpperCase();
    vin = vin.trim();
    if (vin === "") {
        $.ajax({
            url: "/VinCheck/VinLengthErrorText",
            type: 'POST',
            success: function (data) { setVinCheckResult(data); }
        });
    }
    else if (vin.length != 17) {
        $.ajax({
            url: "/VinCheck/VinLengthErrorText",
            type: 'POST',
            success: function (data) { setVinCheckResult(data); }
        });
    }
    else if (firstThreeUppercaseLetters === 'VF3' || // Citroen
        firstThreeUppercaseLetters === 'VF7' || // Peugeot
        firstThreeUppercaseLetters === 'W0L') { // Opel
        $.ajax({
            url: "/VinCheck/VinCheck?vin=" + vin.trim(),
            type: 'POST',
            success: function(data) { setVinCheckResult(data); }
        });
    }
    else {
        $.ajax({
            url: "/VinCheck/VinCheckUnsupportedManufacturer",
            type: 'POST',
            success: function (data) { setVinCheckResult(data); }
        });
    }
}

function setVinCheckResult(part) {
    document.getElementById("vin-check-result").innerHTML = part;
    document.getElementById('button-vin-submit').disabled = false;
}

function getNextNumber(num) {
    if (num < 4) {
        return num + 1;
    }
    return 1;
}

function preload(arrayOfImages) {
    var callback = function() {
        $('<img/>')[0].src = this;
    };
    arrayOfImages.forEach(callback);
}
