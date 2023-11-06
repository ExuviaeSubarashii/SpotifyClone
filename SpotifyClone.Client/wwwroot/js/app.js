function GetToday() {
    var currentTime = new Date();
    var currentHour = currentTime.getHours();
    var howgood = document.getElementById("howgood");
    if (currentHour >= 0 && currentHour <= 11) {
        howgood.textContent = "Good Morning";
        console.log("Good Morning");
    }
    else if (currentHour >= 12 && currentHour <= 18) {
        howgood.textContent = "Good Afternoon";
        console.log("afternoon");
    }
    else if (currentHour >= 19 && currentHour <= 24) {
        howgood.textContent = "Good Evening";
        console.log("evening");
    }
}
function UnDisplay() {
    var elements = document.getElementById("searchpl");
    if (elements.style.display === "none") {
        elements.style.display = "block";
    }
    else {
        elements.style.display = "none";
    }
}
function GoToThatPage(pageName) {
    window.location.href = "/Home/".concat(pageName);
}
window.onload = GetToday;
//# sourceMappingURL=app.js.map