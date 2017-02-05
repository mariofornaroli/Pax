app.controller('LoginCtrl', function ($scope, $stateParams, $ionicActionSheet, $timeout, $ionicLoading, $ionicModal, $ionicPopup, ionicMaterialInk, $state) {
    
    $scope.$parent.clearFabs();
    $timeout(function () {
        $scope.$parent.hideHeader();
    }, 0);
    ionicMaterialInk.displayEffect();

    $scope.login = function () {
        $state.go('app.components');

        /* Test */
        //$scope.$parent.clearFabs();
        //$scope.$parent.hideHeader();
    };

});