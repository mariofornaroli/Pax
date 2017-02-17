// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.controllers' is found in controllers.js
var app = angular.module('starter', ['ionic', 'ionic-material', 'ionMdInput'])

app.run(function ($ionicPlatform) {
    $ionicPlatform.ready(function () {
        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
        // for form inputs)
        if (window.cordova && window.cordova.plugins.Keyboard) {
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
        }
        if (window.StatusBar) {
            // org.apache.cordova.statusbar required
            StatusBar.styleDefault();
        }
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

        .state('app.book-details', {
            url: '/book-details',
            views: {
                'menuContent': {
                    //templateUrl: 'templates/books.html',
                    //controller: 'BooksCtrl'
                    templateUrl: 'templates/book-details.html',
                    //controller: 'BookDetailsCtrl'
                }
                , 'fabContent': {
                    template: '<button id="fab-book-details" class="button button-fab button-fab-bottom-right expanded button-energized-900 fade"><i class="icon ion-forward"></i></button>',
                    controller: function ($timeout) {
                        $timeout(function () {
                            document.getElementById('fab-book-details').classList.toggle('fade');
                        }, 900);
                    }
                }
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
                template: '<button id="fab-profile" class="button button-positive button-fab button-fab-bottom-right" ng-click="goTest()"><i class="icon ion-social-twitter"></i></button>',
                controller: function ($timeout, $window, $scope, $state, $location) {
                    $timeout(function () {
                        document.getElementById('fab-profile').classList.toggle('on');
                    }, 800);
                    $scope.goTest = function () {
                        //$state.go('externalGoTwitter');
                        $window.open('https://twitter.com/librairiepax');
                        //$location.url('https://twitter.com/librairiepax');
                    };
                }
            }
        }
    })

    .state('app.about-pax', {
        url: '/about-pax',
        views: {
            'menuContent': {
                templateUrl: 'templates/about-pax.html'
                //,controller: 'ProfileCtrl'
            },
            'fabContent': {
                template: '<button id="fab-profile" class="button button-positive button-fab button-fab-bottom-right" ng-click="goTest()"><i class="icon ion-social-twitter"></i></button>',
                controller: function ($timeout, $window, $scope, $state, $location) {
                    $timeout(function () {
                        document.getElementById('fab-profile').classList.toggle('on');
                    }, 800);
                    $scope.goTest = function () {
                        //$state.go('externalGoTwitter');
                        $window.open('https://twitter.com/librairiepax');
                        //$location.url('https://twitter.com/librairiepax');
                    };
                }
            }
        }
    })
    ;

    // if none of the above states are matched, use this as the fallback
    //$urlRouterProvider.otherwise('/app/login');
    $urlRouterProvider.otherwise('/app/profile');
});
