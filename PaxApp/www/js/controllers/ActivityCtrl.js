(function () {

    app.controller('ActivityCtrl', ActivityCtrl);
    ActivityCtrl.$inject = ['$scope', '$stateParams', '$timeout', 'ionicMaterialMotion', 'ionicMaterialInk'];


    function ActivityCtrl($scope, $stateParams, $timeout, ionicMaterialMotion, ionicMaterialInk) {

        $scope.$parent.showHeader();
        $scope.$parent.clearFabs();
        $scope.isExpanded = true;
        $scope.$parent.setExpanded(true);
        $scope.$parent.setHeaderFab('right');

        $timeout(function () {
            ionicMaterialMotion.fadeSlideIn({
                selector: '.animate-fade-slide-in .item'
            });
        }, 200);

        // Activate ink for controller
        ionicMaterialInk.displayEffect();
    };

})();
