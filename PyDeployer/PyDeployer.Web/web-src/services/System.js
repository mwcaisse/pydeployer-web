import Vue from "vue"

class System {
    constructor() {
        this._baesUrl = ($("#rootPathPrefix").val() || "/") + "api/";
        this._events = new Vue();

        console.log("Created system");
    }

    get events() {
        return this._events;
    }

    get baseUrl() {
        return this._baseUrl;
    }
}

export default new System();