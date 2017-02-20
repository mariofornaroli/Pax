paxGlobal = {};

/* ---- App url ---- */
//paxGlobal.appUrl = '';
// Debug
//paxGlobal.appUrl = 'http://localhost:51267/';
// IIS
//paxGlobal.appUrl = 'http://localhost:8091/';
// Azure
paxGlobal.appUrl = 'http://paxwebapi.azurewebsites.net/';

paxGlobal.getAppUrl = function getAppUrl() {
    if (paxGlobal.appUrl) {
        return paxGlobal.appUrl;
    } else {
        var url = location.protocol + '//' + location.host + '/';
        paxGlobal.appUrl = url;
        return url;
    };
};
/* ---- App url ---- */

/* Add replaceAll method */
String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
};