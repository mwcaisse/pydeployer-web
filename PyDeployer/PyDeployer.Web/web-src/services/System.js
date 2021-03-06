import Vue from "vue"
import VueConfig from "@app/services/VueCommon.js"
import "@app/services/CustomDirectives.js"

class System {
    constructor() {
        this._baesUrl = ($("#rootPathPrefix").val() || "/") + "api/";
        this._events = new Vue();

        VueConfig();
    }

    get events() {
        return this._events;
    }

    get baseUrl() {
        return this._baseUrl;
    }
}

export default new System();