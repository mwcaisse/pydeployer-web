import Proxy from ".Proxy"

module.exports = {
    application: {
        get: function(id) {
            return Proxy.get("application/" + id);
        },
        getByUuid: function(uuid) {
            return Proxy.get("application/" + uuid);
        },
        getAll: function() {
            return Proxy.get("application/");
        },
        create: function(application) {
            return Proxy.post("application/", application);
        },
        update: function(application) {
            return Proxy.put("application/" + application.applicationId, application);
        },
        delete: function(id) {
            return Proxy.delete("application/" + id);
        }
    }
}