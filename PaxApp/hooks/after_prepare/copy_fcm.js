#!/usr/bin/env node

// Add Platform Class
// v1.0
// Automatically adds the platform class to the body tag
// after the `prepare` command. By placing the platform CSS classes
// directly in the HTML built for the platform, it speeds up
// rendering the correct layout/style for the specific platform
// instead of waiting for the JS to figure out the correct classes.

var filestocopy = [
        { "resources/android/notif-icons/drawable-hdpi/fcm_push_icon.png": "platforms/android/res/drawable-hdpi/fcm_push_icon.png"},
        { "resources/android/notif-icons/drawable-mdpi/fcm_push_icon.png": "platforms/android/res/drawable-mdpi/fcm_push_icon.png"},
        { "resources/android/notif-icons/drawable-xhdpi/fcm_push_icon.png": "platforms/android/res/drawable-xhdpi/fcm_push_icon.png"},
];

var fs = require('fs');
var path = require('path');

var rootdir = process.argv[2];


if (rootdir) {
    filestocopy.forEach(function(obj) {
        Object.keys(obj).forEach(function(key) {
            var val = obj[key];
            var srcfile = path.join(rootdir, key);
            var destfile = path.join(rootdir, val);
            console.log("copying "+srcfile+" to "+destfile);
            var destdir = path.dirname(destfile);
            if (fs.existsSync(srcfile) && fs.existsSync(destdir)) {
                fs.createReadStream(srcfile).pipe(fs.createWriteStream(destfile));
            }
        });
    });
}
