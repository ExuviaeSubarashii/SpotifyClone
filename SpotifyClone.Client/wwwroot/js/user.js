var baseUrl = "http://localhost:5128/api";
function Login() {
    var userEmailInput = document.getElementById('loginEmail');
    var userPasswordInput = document.getElementById('loginPassword');
    var user = {
        userEmail: userEmailInput.value.trim(),
        password: userPasswordInput.value.trim()
    };
    var requestOptions = {
        method: 'POST',
        body: JSON.stringify(user),
        headers: {
            'Content-Type': 'application/json'
        }
    };
    fetch("".concat(baseUrl, "/User/Login"), requestOptions)
        .then(function (response) {
        if (response.ok) {
            return response.json();
        }
        else {
            throw new Error(response.statusText);
        }
    })
        .then(function (data) {
        localStorage.setItem('userId', data.id);
        localStorage.setItem('usertoken', data.userToken);
        localStorage.setItem('userEmail', data.userEmail);
        localStorage.setItem('userName', data.userName);
        localStorage.setItem('isLoggedIn', "true");
        window.location.href = '/';
    })
        .catch(function (error) {
        console.error('Error occurred while sending the request:', error);
    });
}
function Register() {
    var userEmail = document.getElementById('registerUserEmail');
    var userName = document.getElementById('registerUserName');
    var userPassword = document.getElementById('registerUserPassword');
    var userpasswordAgain = document.getElementById('registerUserpasswordagain');
    var passwordAgain = userpasswordAgain.value;
    var registration = {
        userEmail: userEmail.value,
        userName: userName.value,
        userPassword: userPassword.value,
    };
    if (registration.userEmail !== null && registration.userName !== null && registration.userPassword !== null && registration.userPassword == passwordAgain) {
        var requestOptions = {
            method: 'POST',
            body: JSON.stringify(registration),
            headers: {
                'Content-Type': 'application/json'
            }
        };
        fetch("".concat(baseUrl, "/User/Register"), requestOptions)
            .then(function (response) {
            if (response.ok) {
                return response.json();
            }
            else {
                throw new Error(response.statusText);
            }
        });
    }
    else {
        GoToThatPage('Login');
    }
}
function ShowOptions(buttonelement) {
    var option = buttonelement.getAttribute("data-option");
    var logindiv = document.getElementById("logindiv");
    var registerdiv = document.getElementById("registerdiv");
    if (option === "Login") {
        logindiv.style.display = "block";
        registerdiv.style.display = "none";
        console.log(option);
    }
    if (option === "Register") {
        logindiv.style.display = "none";
        registerdiv.style.display = "block";
        console.log(option);
    }
}
//# sourceMappingURL=user.js.map