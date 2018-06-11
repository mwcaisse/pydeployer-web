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

export {
    application as ApplicationService,
    applicationEnvironment as ApplicationEnvironmentService
}