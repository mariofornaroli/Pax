(function () {

    app.controller('AboutPaxCtrl', AboutPaxCtrl);
    AboutPaxCtrl.$inject = ['$scope', '$stateParams', '$window', 'ionicMaterialInk', 'ionicMaterialMotion', '$controller', 'Books', '$state', 'ErrorMng', '$sce', '$ionicPopup', 'Events'];

    function AboutPaxCtrl($scope, $stateParams, $window, ionicMaterialInk, ionicMaterialMotion, $controller, Books, $state, ErrorMng, $sce, $ionicPopup, Events) {
        
        var vm = this;

        vm.goTwitter = function () {
            $window.open('https://twitter.com/librairiepax');
        };

        vm.goFacebook = function () {
            $window.open('https://www.facebook.com/Librairie-Pax-180592962073215');
        };

        vm.goInstagram = function () {
            $window.open('https://www.instagram.com/librairie_pax/');
        };
    };

})();