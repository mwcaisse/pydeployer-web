function getURLParameter(name, def) {
    if (typeof def === "undefined") {
        def = "";
    }
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? def : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function isStringNullOrBlank(string) {
    return (typeof string === "undefined" ||
        string === null ||
        typeof string !== "string" ||
        string.length === 0 ||
        string.trim().length === 0);
};

export default {
    getURLParameter: getURLParameter,
    isStringNullOrBlank: isStringNullOrBlank
}