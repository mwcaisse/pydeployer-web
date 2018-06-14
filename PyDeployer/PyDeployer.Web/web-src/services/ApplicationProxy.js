import Proxy from "services/Proxy.js"

var application = {
    get: function (id) {
        return Proxy.get("application/" + id);
    },
    getByUuid: function (uuid) {
        return Proxy.get("application/" + uuid);
    },
    getAll: function () {
        return Proxy.get("application/");
    },
    create: function (application) {
        return Proxy.post("application/", application);
    },
    update: function (application) {
        return Proxy.put("application/" + application.applicationId, application);
    },
    delete: function (id) {
        return Proxy.delete("application/" + id);
    }
}

var applicationEnvironment = {
    get: function(applicationId) {
        return Proxy.get("application/" + applicationId + "/environment");
    },
    create: function(applicationId, environmentId) {
        return Proxy.post("application/" + applicationId + "/environment/" + environmentId);
    },
    delete: function(applicationId, environmentId) {
        return Proxy.delete("application/" + applicationId + "/environment/" + environmentId);
    }
}

var applicationToken = {
    get: function(applicationId, id) {
        return Proxy.get("application/" + applicationId + "/token/" + id);
    },
    getForApplication: function(applicationId) {
        return Proxy.get("application/" + applicationId + "/token/");
    },
    create: function(applicationId, token) {
        return Proxy.post("application/" + applicationId + "/token/", token);
    },
    update: function(applicationId, token) {
        return Proxy.put("application/" + applicationId + "/token/" + token.applicationTokenId, token);
    },
    delete: function(applicationId, id) {
        return Proxy.delete("application/" + applicationId + "/token/" + id);
    }
}

var environment = {
    get: function(id) {
        return Proxy.get("environment/" + id);
    },
    getAll: function() {
        return Proxy.get("environment/");
    },
    create: function(environment) {
        return Proxy.post("environment/", environment);
    },
    update: function (environment) {
        return Proxy.put("environment/", environment);
    },
    delete: function(id) {
        return Proxy.delete("environment/" + id);
    }
}

export {
    application as ApplicationService,
    applicationEnvironment as ApplicationEnvironmentService,
    applicationToken as ApplicationTokenService,
    environment as EnvironmnetService
}