import Proxy from "@app/services/Proxy.js"

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
    getEnvironmentsForApplication: function(applicationId) {
        return Proxy.get("application/" + applicationId + "/environment");
    },
    getApplicationsForEnvironment: function (environmentId) {
        return Proxy.get("environment/" + environmentId + "/application");
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

var applicationEnvironmentToken = {
    getAll: function(applicationId, environmentId) {
        return Proxy.get("application/" + applicationId + "/environment/" + environmentId + "/token/");
    },
    save: function(applicationId, environmentId, token) {
        return Proxy.post("application/" + applicationId + "/environment/" + environmentId +"/token/", token);
    }
}

var buildToken = {
    get: function(id) {
        return Proxy.get("build/token/" + id);
    },
    getAll: function() {
        return Proxy.get("build/token/");
    },
    create: function(token) {
        return Proxy.post("build/token/", token);
    },
    update: function (token) {
        var id = token.buildTokenId;
        return Proxy.put("build/token/" + id, token);
    },
    delete: function(id) {
        return Proxy.delete("build/token/" + id);
    }
}

var user = {
    register: function(registration) {
        return Proxy.post("user/register", registration);
    },
    usernameAvailable: function(username) {
        return Proxy.get("user/available?username=" + username);
    },
    me: function() {
        return Proxy.get("user/me");
    }
}

var userAuthenticationToken = {
    getActive: function() {
        return Proxy.get("user/token/active?take=100");
    },
    create: function(description) {
        return Proxy.post("user/token", description);
    },
    delete: function(id) {
        return Proxy.delete("user/token/" + id);
    }
};

export {
    application as ApplicationService,
    applicationEnvironment as ApplicationEnvironmentService,
    applicationToken as ApplicationTokenService,
    environment as EnvironmentService,
    applicationEnvironmentToken as ApplicationEnvironmentTokenService,
    buildToken as BuildTokenService,
    user as UserService,
    userAuthenticationToken as UserAuthenticationTokenService
}