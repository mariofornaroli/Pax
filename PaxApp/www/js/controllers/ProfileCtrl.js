(function () {

    app.controller('ProfileCtrl', ProfileCtrl);
    ProfileCtrl.$inject = ['$scope', '$stateParams', '$timeout', 'ionicMaterialInk', 'ionicMaterialMotion', '$controller', 'Books'];

    function ProfileCtrl($scope, $stateParams, $timeout, ionicMaterialInk, ionicMaterialMotion, $controller, Books) {
        
        var vm = this;

        vm.loading = false;

        /* all document table data */
        vm.heartBooks = [];

        /* Link to pax global object to allow binding to the view */
        vm.paxGlobal = paxGlobal;

        /* Load all heart books */
        vm.loadHeartBooks = function () {
            // If data has not been loaded yet, then load it from server
            if (Books.loading === false) {
                vm.loadBooks();
            };
        };

        /* Load all books data from server */
        vm.loadBooks = function () {
            Books.GetHeartBooks().then(
                function (result) {
                    if (result.error === false) {
                        vm.heartBooks = result.data;
                        vm.loading = false;

                    } else {
                        vm.loading = false;
                        // handle error here
                        ErrorMng.showSystemError(result.msg);
                    };

                },
                function (error) {
                    vm.loading = false;
                    // handle error here
                    ErrorMng.showSystemError(error.msg);
                });
        };

        /*  --------------------------------------------------------------------------------------------------------------------------------------------*/
        /*  ------------------------------------------------------  STYLE - Animations - Headers  ------------------------------------------------------*/
        /*  --------------------------------------------------------------------------------------------------------------------------------------------*/

        // Set Header
        $scope.$parent.showHeader();
        $scope.$parent.clearFabs();
        $scope.isExpanded = false;
        $scope.$parent.setExpanded(false);
        $scope.$parent.setHeaderFab(false);

        // Set Motion
        $timeout(function () {
            ionicMaterialMotion.slideUp({
                selector: '.slide-up'
            });
        }, 300);

        $timeout(function () {
            ionicMaterialMotion.fadeSlideInRight({
                startVelocity: 3000
            });
        }, 700);

        // Set Ink
        ionicMaterialInk.displayEffect();

        /*  --------------------------------------------------------------------------------------------------------------------------------------------*/
        /*  ------------------------------------------------------  STYLE - Animations - Headers  ------------------------------------------------------*/
        /*  --------------------------------------------------------------------------------------------------------------------------------------------*/


        /* Init controller function */
        vm.initController = function () {
            vm.loadHeartBooks();
        };

        /* Call init controller */
        vm.initController();
    };

})();