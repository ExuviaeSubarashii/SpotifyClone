function Login(email, password) {
    var user = {
        email: email,
        password: password
    };
    fetch("".concat(baseUrl, "/User/Login"), {
        method: "POST",
        body: JSON.stringify(user)
    })
        .then(function (response) {
        if (!response.ok) {
            throw new Error(response.statusText);
        }
        return response.json();
    });
}
function Register() {
}
//# sourceMappingURL=user.js.map