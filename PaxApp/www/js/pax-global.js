﻿paxGlobal = {};

/* ---- App url ---- */
//paxGlobal.appUrl = '';
paxGlobal.appUrl = 'http://localhost:51267/';

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