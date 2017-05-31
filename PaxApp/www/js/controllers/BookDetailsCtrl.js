(function () {

    app.controller('BookDetailsCtrl', BookDetailsCtrl);
    BookDetailsCtrl.$inject = ['$scope', '$stateParams', '$timeout', 'ionicMaterialInk', 'ionicMaterialMotion', 'Books', '$ionicLoading', 'ErrorMng'];

    function BookDetailsCtrl($scope, $stateParams, $timeout, ionicMaterialInk, ionicMaterialMotion, Books, $ionicLoading, ErrorMng) {
        
        var vm = this;

        vm.booksLoaded = false;
        vm.singleBooksDetailLoaded = false;
        vm.currentBookDetail = {};
        
        /* Link to pax global object to allow binding to the view */
        vm.paxGlobal = paxGlobal;
         
        vm.getPageTitle = function () {
            return Books.currentBook && Books.currentBook.isPocheDuMois === true ? 'Poche du mois' : vm.currentBookDetail.title;
        };

        vm.setMotion = function () {
            // Set Motion
            $timeout(function () {
                //$scope.isExpanded = true;
                //$scope.$parent.setExpanded(true);
                ionicMaterialMotion.blinds();
                //// Set Ink
                ionicMaterialInk.displayEffect();

                //ionicMaterialMotion.slideUp({
                //    selector: '.slide-up'
                //});
            }, 300);

            //$timeout(function () { 
            //    ionicMaterialMotion.fadeSlideInRight({
            //        startVelocity: 3000
            //    });
            //}, 700);
        };

        /* Load all heart books */
        vm.loadBookDetails = function () { 
            vm.showLoading();
            /* Call server to get book details */
            Books.getBookDetails().then(
                function (result) {
                    if (result.operationResult === true) { 
                    } else {
                        // handle error here
                        ErrorMng.showSystemError(result.msg);
                    };
                    /* Save vm state */
                    vm.singleBooksDetailLoaded = true;
                    vm.currentBookDetail = result.resultData;
                    vm.setMotion();
                    $ionicLoading.hide();
                },
                function (error) {
                    // handle error here
                    ErrorMng.showSystemError(error.msg);
                    $ionicLoading.hide();
                });
        };

        vm.showLoading = function () {
            $ionicLoading.show({
                template: '<div class="loader"><svg class="circular"><circle class="path" cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10"/></svg></div>'
            });
        };

        /* Init controller function */
        vm.initController = function () {
            vm.loadBookDetails();
        };

        /* Call init controller */
        vm.initController();

        /*  --------------------------------------------------------------------------------------------------------------------------------------------*/
        /*  ------------------------------------------------------  STYLE - Animations - Headers  ------------------------------------------------------*/
        /*  --------------------------------------------------------------------------------------------------------------------------------------------*/

        // Set Header
        //$scope.$parent.showHeader();
        $scope.$parent.clearFabs();
        $scope.$parent.setHeaderFab('left');

        //// Delay expansion
        //$timeout(function () {
        //    $scope.isExpanded = true;
        //    $scope.$parent.setExpanded(true);
        //}, 300);

        //// Set Motion
        //ionicMaterialMotion.fadeSlideInRight();

        //// Set Ink
        //ionicMaterialInk.displayEffect();
    };

})();