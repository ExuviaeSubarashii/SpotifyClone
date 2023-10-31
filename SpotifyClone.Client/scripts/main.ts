async function GetSuggestedPlayLists(userToken: string) {
    if (userToken !== null || userToken !== undefined) {

        const lastplayedlist = document.getElementById("lastplayedlist");

        fetch(`${baseUrl}`, {
            method: "GET"
        })
            .then(function (response) {
                if (!response.ok) {
                    throw new Error(response.statusText);
                }
                return response.json();
            })
            .then(function (data) {
                if (data !== null) {
                    data.forEach(item => {
                        //div
                        const individualBox = Object.assign(document.createElement("div"), { id: "individualBox" });
                        //image
                        const plImage = Object.assign(document.createElement("img"), { id: "plImage", src: "" })
                        //playlist name
                        const plName = Object.assign(document.createElement("h1"), { id: "plName", textContent: `${item.plName}` });

                        individualBox.append(plImage);
                        individualBox.append(plName);
                        lastplayedlist.appendChild(individualBox);
                    })
                }
            })
    }
}