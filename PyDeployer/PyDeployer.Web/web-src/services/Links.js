import Vue from "vue"
import VueConfig from "@app/services/VueCommon.js"

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

    get buildToken() {
        return this._rootPathPrefix + "build-token/";
    }

    get logout() {
        return this._rootPathPrefix + "logout";
    }

    get userAuthenticationTokens() {
        return this._rootPathPrefix + "user/tokens";
    }

    get login() {
        return this._rootPathPrefix + "login";
    }

    application(id) {
        return `${this._rootPathPrefix}application/${id}`;
    }

    environment(id) {
        return `${this._rootPathPrefix}environment/${id}`;
    }
}

export default new Links();