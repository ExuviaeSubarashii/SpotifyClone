async function GetSuggestedPlayLists(userToken = localStorage.getItem("userToken")||null) {
    if (userToken !== null || userToken !== undefined) {

        const lastplayedlist = document.getElementById("lastplayedlist");

        await fetch(`${baseUrl}/Playlist/GetSuggestedPlayLists`, {
            method: "POST",
            body: JSON.stringify(userToken)
        })
            .then(function (response) {
                if (!response.ok) {
                    throw new Error(response.statusText);
                }
                return response.json();
            })
            .then(function (data) {
                if (data !== null) {
                    const individualBoxes = data.map(item => {
                        const individualBox = document.createElement("div");
                        individualBox.id = "individualBox";

                        const plName = document.createElement("h1");
                        plName.id = "plName";
                        plName.textContent = item.plName;

                        individualBox.appendChild(plName);

                        return individualBox;
                    });
                    individualBoxes.forEach(box => {
                        lastplayedlist.appendChild(box);
                    });
                }
            });
    }
}
async function GetUserPlayLists(userToken = localStorage.getItem("userToken") || null) {
    if (userToken !== null || userToken !== undefined) {
        const playlists = document.getElementById("playlists");

        await fetch(`${baseUrl}/Playlist/GetUserPlayLists`, {
            method: "POST",
            body: JSON.stringify(userToken)
        })
            .then(function (response) {
                if (!response.ok) {
                    throw new Error(response.statusText);
                }
                return response.json();
            })
            .then(function (data) {
                if (data !== null) {
                    const individualPlayListBox = data.map(item => {
                        const individualPlayListBox = document.createElement("div");
                        individualPlayListBox.id = "individualPlayListBox";
                        const plName = document.createElement("h1");
                        plName.id = "plName";
                        plName.textContent = item.plName;
                        //type
                        const plType = document.createElement("h1");
                        plType.id = "plType";
                        plType.textContent = item.plType;
                        //owner
                        const plOwner = document.createElement("h1");
                        plOwner.id = "plOwner";
                        plOwner.textContent = item.plOwner;

                        individualPlayListBox.appendChild(plName);
                        individualPlayListBox.appendChild(plType);
                        individualPlayListBox.appendChild(plOwner);

                        return individualPlayListBox;
                    });
                    individualPlayListBox.forEach(box => {
                        playlists.appendChild(box);
                    });
                }
            });
    }
}