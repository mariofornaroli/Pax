// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.controllers' is found in controllers.js
var app = angular.module('starter', ['ionic', 'ionic-material', 'ionMdInput', 'ion-floating-menu']);

app.run(function ($ionicPlatform, $state, $ionicPopup, ionicMaterialInk, $timeout) {

    /* When platform is ready, then add notification plug in registration */
    window.ionic.Platform.ready(function () {

        /*  --------------------- PUSH NOTIFICATION --------------------- */

      //  //FCMPlugin.onTokenRefresh( onTokenRefreshCallback(token) );
      //  //Note that this callback will be fired everytime a new token is generated, including the first time.
      //  FCMPlugin.onTokenRefresh(function (token) {
      //      //console.log('onTokenRefresh:');
      //      //console.log(token);
      //      //alert(token);
      //  });
      //
      //  //FCMPlugin.getToken( successCallback(token), errorCallback(err) );
      //  //Keep in mind the function will return null if the token has not been established yet.
      //  FCMPlugin.getToken(function (token) {
      //      //console.log('getToken:');
      //      //console.log(token);
      //      //alert(token);
      //  });

        //FCMPlugin.subscribeToTopic( topic, successCallback(msg), errorCallback(err) );
        //All devices are subscribed automatically to 'all' and 'ios' or 'android' topic respectively.
        //Must match the following regular expression: "[a-zA-Z0-9-_.~%]{1,900}".
        //FCMPlugin.subscribeToTopic('paxNewHeratBooks');
        //FCMPlugin.subscribeToTopic('testPaxNewHeratBooks');
        window.FirebasePlugin.subscribe("paxNewHeratBooks");
        window.FirebasePlugin.subscribe("testPaxNewHeratBooks");
        window.FirebasePlugin.subscribe("testPaxNewHeratBooks2");

        /* Notification popup */
        var showNotificationPopup = function () {
            var alertPopup = $ionicPopup.alert({
                title: 'Nouveaux livres apparu'
            });

            $timeout(function () {
                ionicMaterialInk.displayEffect();
            }, 0);
        };

        /* Then redirect the app to Main page */
        var refreshPaxProfile = function refreshPaxProfile(forceGoToProfile) {
            if ($state.current.name == 'app.profile') {
                $state.reload();
            } else if (forceGoToProfile === true) {
                $state.go('app.profile');
            };
        }

     //   //FCMPlugin.onNotification( onNotificationCallback(data), successCallback(msg), errorCallback(err) )
     //   //Here you define your application behaviour based on the notification data.
     //   FCMPlugin.onNotification(function (data) {
     //       var forceGoToProfile = false;
     //       if (data.tap) {
     //           //Notification was received on device tray and tapped by the user.
     //           forceGoToProfile = true;
     //           //console.log("Was tapped:");
     //           //console.log(JSON.stringify(data));
     //       } else {
     //           //Notification was received in foreground. Maybe the user needs to be notified.
     //           showNotificationPopup();
     //           ///* A notification occurred, thus force the app to reload all it's data */
     //           //paxGlobal.NotificationOccurred = true;
     //           ///* Then redirect the app to Main page */
     //           //$state.go('app.profile', {}, { reload: true });
     //       };
     //       /* A notification occurred, thus force the app to reload all it's data */
     //       paxGlobal.NotificationOccurred = true;
     //       /* Then redirect the app to Main page */
     //       refreshPaxProfile(forceGoToProfile);
     //
     //   }, function (data) {
     //       if (data.tap) {
     //           //Notification was received on device tray and tapped by the user.
     //           //console.log("Was tapped:");
     //           //console.log(JSON.stringify(data));
     //       } else {
     //           //Notification was received in foreground. Maybe the user needs to be notified.
     //           //console.log("Wasn't tapped:");
     //           //console.log(JSON.stringify(data));
     //       }
     //   });


        window.FirebasePlugin.onNotificationOpen(function (notification) {
            var forceGoToProfile = false;
            //if (notification.numberNewBooks) {
            //    window.FirebasePlugin.setBadgeNumber(notification.numberNewBooks);
            //}
            if (notification.tap) {
                //Notification was received on device tray and tapped by the user.
                forceGoToProfile = true;
                //console.log("Was tapped:");
                //console.log(JSON.stringify(data));
            } else {
                //Notification was received in foreground. Maybe the user needs to be notified.
                showNotificationPopup();
                ///* A notification occurred, thus force the app to reload all it's data */
                //paxGlobal.NotificationOccurred = true;
                ///* Then redirect the app to Main page */
                //$state.go('app.profile', {}, { reload: true });
            };
            /* A notification occurred, thus force the app to reload all it's data */
            paxGlobal.NotificationOccurred = true;
            /* Then redirect the app to Main page */
            refreshPaxProfile(forceGoToProfile);
        }, function (error) {
            console.error(error);
        });


    });

    $ionicPlatform.ready(function () {
        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
        // for form inputs)
        if (window.cordova && window.cordova.plugins.Keyboard) {
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
        }
        if (window.StatusBar) {
            // org.apache.cordova.statusbar required
            StatusBar.styleDefault();
        };
    });
})

app.config(function ($stateProvider, $urlRouterProvider, $ionicConfigProvider) {

    // Turn off caching for demo simplicity's sake
    $ionicConfigProvider.views.maxCache(0);

    /*
    // Turn off back button text
    $ionicConfigProvider.backButton.previousTitleText(false);
    */

    $stateProvider.state('app', {
        url: '/app',
        abstract: true,
        templateUrl: 'templates/menu.html',
        controller: 'AppCtrl'
    })

        .state('app.events', {
            url: '/events',
            views: {
                'menuContent': {
                    templateUrl: 'templates/events.html'
                    //,controller: 'EventsOldCtrl'
                }
                //,'fabContent': {
                //    template: '<button id="fab-activity" class="button button-fab button-fab-top-right expanded button-energized-900 flap"><i class="icon ion-paper-airplane"></i></button>',
                //    controller: function ($timeout) {
                //        $timeout(function () {
                //            document.getElementById('fab-activity').classList.toggle('on');
                //        }, 200);
                //    }
                //}
            }
        })

        .state('app.event-details', {
            url: '/event-details',
            views: {
                'menuContent': {
                    templateUrl: 'templates/event-Details.html'
                }
            }
        })

    .state('app.best-sellers', {
        url: '/best-sellers',
        views: {
            'menuContent': {
                templateUrl: 'templates/best-sellers-books.html'
            }
        }
    })

    .state('app.books', {
        url: '/books',
        views: {
            'menuContent': {
                templateUrl: 'templates/books.html'
                //, controller: 'BooksCtrl'
            }
            //, 'fabContent': {
            //    template: '<button id="fab-books" class="button button-fab button-fab-bottom-right expanded button-energized-900 fade"><i class="icon ion-heart"></i></button>',
            //    controller: function ($timeout) {
            //        $timeout(function () {
            //            document.getElementById('fab-books').classList.toggle('on');
            //        }, 900);
            //    }
            //}
        }
    })

        .state('app.search-book-results', {
            url: '/search-book-results',
            views: {
                'menuContent': {
                    templateUrl: 'templates/search-book-results.html'
                }
            }
        })

        .state('app.book-details', {
            url: '/book-details',
            views: {
                'menuContent': {
                    //templateUrl: 'templates/books.html',
                    //controller: 'BooksCtrl'
                    templateUrl: 'templates/book-details.html',
                    //controller: 'BookDetailsCtrl'
                }
                //, 'fabContent': {
                //    template: '<button id="fab-book-details" data-to-href="test" class="button button-fab button-fab-bottom-right expanded button-energized-900 fade" ng-click="gePaxLink()"><i class="icon ion-forward"></i></button>',
                //    controller: function ($timeout, $scope, Books, $window) {
                //        $timeout(function () {
                //            document.getElementById('fab-book-details').classList.toggle('fade');
                //        }, 900);
                //        $scope.gePaxLink = function () {
                //            dataToHref = Books.currentBook.completeHref;
                //            $window.open(dataToHref);
                //            //console.log(Books.currentBook.completeHref);
                //            //var dataToHref = $('#fab-book-details').attr("title");
                //            //alert(dataToHref);
                //            //$window.open(dataToHref);
                //        };
                //    }
                //}
            }
        })

    .state('app.gallery', {
        url: '/gallery',
        views: {
            'menuContent': {
                templateUrl: 'templates/gallery.html',
                controller: 'GalleryCtrl'
            },
            'fabContent': {
                template: '<button id="fab-gallery" class="button button-fab button-fab-top-right expanded button-energized-900 drop"><i class="icon ion-heart"></i></button>',
                controller: function ($timeout) {
                    $timeout(function () {
                        document.getElementById('fab-gallery').classList.toggle('on');
                    }, 600);
                }
            }
        }
    })

    .state('app.login', {
        url: '/login',
        views: {
            'menuContent': {
                templateUrl: 'templates/login.html',
                controller: 'LoginCtrl'
            },
            'fabContent': {
                template: ''
            }
        }
    })

        .state('externalGoTwitter', {
            url: 'https://twitter.com/librairiepax',
            external: true
        })

    .state('app.profile', {
        url: '/profile',
        views: {
            'menuContent': {
                templateUrl: 'templates/profile.html'
                //,controller: 'ProfileCtrl'
            },
            'fabContent': {
                template: ''
            }
            //'fabContent': {
            //    template: '<button id="fab-profile" class="button button-positive button-fab button-fab-bottom-right" ng-click="goTest()"><i class="icon ion-social-twitter"></i></button>',
            //    controller: function ($timeout, $window, $scope, $state, $location) {
            //        $timeout(function () {
            //            document.getElementById('fab-profile').classList.toggle('on');
            //        }, 800);
            //        $scope.goTest = function () {
            //            //$state.go('externalGoTwitter');
            //            $window.open('https://twitter.com/librairiepax');
            //            //$location.url('https://twitter.com/librairiepax');
            //        };
            //    }
            //}
        }
    })

    .state('app.about-pax', {
        url: '/about-pax',
        views: {
            'menuContent': {
                templateUrl: 'templates/about-pax.html'
                //,controller: 'ProfileCtrl'
            }
            //, 'fabContent': {
            //    template: '<button id="fab-profile" class="button button-positive button-fab button-fab-bottom-right" ng-click="goTest()"><i class="icon ion-social-twitter"></i></button>',
            //    controller: function ($timeout, $window, $scope) {
            //        $timeout(function () {
            //            document.getElementById('fab-profile').classList.toggle('on');
            //        }, 800);
            //        $scope.goTest = function () {
            //            $window.open('https://twitter.com/librairiepax');
            //        };
            //    }
            //}
        }
    })
    ;

    // if none of the above states are matched, use this as the fallback
    //$urlRouterProvider.otherwise('/app/login');
    $urlRouterProvider.otherwise('/app/profile');
});
