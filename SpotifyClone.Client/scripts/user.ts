const baseUrl = "http://localhost:5128/api";
function Login() {
    const userEmailInput = document.getElementById('loginEmail') as HTMLInputElement;
    const userPasswordInput = document.getElementById('loginPassword') as HTMLInputElement;

    const user = {
        userEmail: userEmailInput.value.trim(),
        password: userPasswordInput.value.trim()
    };

    const requestOptions = {
        method: 'POST',
        body: JSON.stringify(user),
        headers: {
            'Content-Type': 'application/json'
        }
    };
    fetch(`${baseUrl}/User/Login`, requestOptions)
        .then(response => {
            if (response.ok) {
                return response.json();
            }
            else {
                throw new Error(response.statusText);
            }
        })
        .then(data => {
            localStorage.setItem('userId',data.id);
            localStorage.setItem('usertoken', data.userToken);
            localStorage.setItem('userEmail', data.userEmail);
            localStorage.setItem('userName', data.userName);
            localStorage.setItem('isLoggedIn', "true");
            window.location.href = '/';
        })
        .catch(error => {
            console.error('Error occurred while sending the request:', error);
        });
}

function Register() {
    const userEmail = document.getElementById('registerUserEmail') as HTMLInputElement;
    const userName = document.getElementById('registerUserName') as HTMLInputElement;
    const userPassword = document.getElementById('registerUserPassword') as HTMLInputElement;
    const userpasswordAgain = document.getElementById('registerUserpasswordagain') as HTMLInputElement;
    var passwordAgain = userpasswordAgain.value;
    const registration = {
        userEmail: userEmail.value,
        userName: userName.value,
        userPassword: userPassword.value,
    };
    if (registration.userEmail !== null && registration.userName !== null && registration.userPassword !== null && registration.userPassword == passwordAgain) {

    const requestOptions = {
        method: 'POST',
        body: JSON.stringify(registration),
        headers: {
            'Content-Type': 'application/json'
        }
    };
    fetch(`${baseUrl}/User/Register`, requestOptions)
        .then(response => {
            if (response.ok) {
                return response.json();
            }
            else {
                throw new Error(response.statusText);
            }
        })
    }
    else {
        GoToThatPage('Login');
    }
}
function ShowOptions(buttonelement) {
    var option = buttonelement.getAttribute("data-option");
    const logindiv = document.getElementById("logindiv");
    const registerdiv = document.getElementById("registerdiv");
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
