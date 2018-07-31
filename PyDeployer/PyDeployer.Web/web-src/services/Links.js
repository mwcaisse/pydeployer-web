import Vue from "vue"
import VueConfig from "services/VueCommon.js"

class Links {
    constructor() {
        this._rootPathPrefix = ($("#rootPathPrefix").val() || "") + "/";

        VueConfig();
    }

    get rootPathPrefix() {
        return this._rootPathPrefix;
    }

    get home() {
        return this._rootPathPrefix;
    }

    get logout() {
        return this._rootPathPrefix + "logout";
    }

    get login() {
        return this._rootPathPrefix + "login";
    }
}

export default new Links();