function Login(email: string, password: string) {
    const user = {
        email: email,
        password: password
    };
    fetch(`${baseUrl}/User/Login`, {
        method: "POST",
        body: JSON.stringify(user)
    })
        .then(function (response) {
            if (!response.ok) {
                throw new Error(response.statusText);
            }
            return response.json();
        })
}
function Register() {

}