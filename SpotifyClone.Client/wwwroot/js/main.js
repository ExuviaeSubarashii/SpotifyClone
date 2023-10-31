var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (g && (g = 0, op[0] && (_ = 0)), _) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
function GetSuggestedPlayLists(userToken) {
    if (userToken === void 0) { userToken = localStorage.getItem("userToken") || null; }
    return __awaiter(this, void 0, void 0, function () {
        var lastplayedlist_1;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    if (!(userToken !== null || userToken !== undefined)) return [3 /*break*/, 2];
                    lastplayedlist_1 = document.getElementById("lastplayedlist");
                    return [4 /*yield*/, fetch("".concat(baseUrl, "/Playlist/GetSuggestedPlayLists"), {
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
                                var individualBoxes = data.map(function (item) {
                                    var individualBox = document.createElement("div");
                                    individualBox.id = "individualBox";
                                    var plName = document.createElement("h1");
                                    plName.id = "plName";
                                    plName.textContent = item.plName;
                                    individualBox.appendChild(plName);
                                    return individualBox;
                                });
                                individualBoxes.forEach(function (box) {
                                    lastplayedlist_1.appendChild(box);
                                });
                            }
                        })];
                case 1:
                    _a.sent();
                    _a.label = 2;
                case 2: return [2 /*return*/];
            }
        });
    });
}
function GetUserPlayLists(userToken) {
    if (userToken === void 0) { userToken = localStorage.getItem("userToken") || null; }
    return __awaiter(this, void 0, void 0, function () {
        var playlists_1;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    if (!(userToken !== null || userToken !== undefined)) return [3 /*break*/, 2];
                    playlists_1 = document.getElementById("playlists");
                    return [4 /*yield*/, fetch("".concat(baseUrl, "/Playlist/GetUserPlayLists"), {
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
                                var individualPlayListBox = data.map(function (item) {
                                    var individualPlayListBox = document.createElement("div");
                                    individualPlayListBox.id = "individualPlayListBox";
                                    var plName = document.createElement("h1");
                                    plName.id = "plName";
                                    plName.textContent = item.plName;
                                    //type
                                    var plType = document.createElement("h1");
                                    plType.id = "plType";
                                    plType.textContent = item.plType;
                                    //owner
                                    var plOwner = document.createElement("h1");
                                    plOwner.id = "plOwner";
                                    plOwner.textContent = item.plOwner;
                                    individualPlayListBox.appendChild(plName);
                                    individualPlayListBox.appendChild(plType);
                                    individualPlayListBox.appendChild(plOwner);
                                    return individualPlayListBox;
                                });
                                individualPlayListBox.forEach(function (box) {
                                    playlists_1.appendChild(box);
                                });
                            }
                        })];
                case 1:
                    _a.sent();
                    _a.label = 2;
                case 2: return [2 /*return*/];
            }
        });
    });
}
//# sourceMappingURL=main.js.map