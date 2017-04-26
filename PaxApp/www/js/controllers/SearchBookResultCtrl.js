(function () {

    app.controller('SearchBookResultsCtrl', SearchBookResultsCtrl);
    SearchBookResultsCtrl.$inject = ['$scope', '$stateParams', '$timeout', 'ionicMaterialInk', 'ionicMaterialMotion', 'Books', '$ionicLoading', 'ErrorMng', '$state'];

    function SearchBookResultsCtrl($scope, $stateParams, $timeout, ionicMaterialInk, ionicMaterialMotion, Books, $ionicLoading, ErrorMng, $state) {
        
        var vm = this;

        vm.searchResultsLoaded = false;
        vm.searchResultsList = {};
        
        /* Link to pax global object to allow binding to the view */
        vm.paxGlobal = paxGlobal;
        
        /* Set motion when search results arrives */
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
            }, 100);

            //$timeout(function () { 
            //    ionicMaterialMotion.fadeSlideInRight({
            //        startVelocity: 3000
            //    });
            //}, 700);
        };

        /* Execute search books and go to results list */
        vm.executeSearchBookResults = function () {
            /* First set service searchBook key */
            Books.searchBookKey = vm.searchBookKey;
            vm.loadSearchResults();
        };

        /* Compute and load search results */
        vm.loadSearchResults = function () {
            vm.showLoading();
            /* Call server to get book details */
            Books.getSearchBookResults().then(
                function (result) {
                    if (result.operationResult === true) {
                    } else {
                        // handle error here
                        ErrorMng.showSystemError(result.msg);
                    };
                    /* Save vm state */
                    vm.searchResultsList = result.resultData;
                    Books.currentSearchResults = vm.searchResultsList;
                    vm.setMotion();
                    $ionicLoading.hide();
                },
                function (error) {
                    // handle error here
                    ErrorMng.showSystemError(error.msg);
                    $ionicLoading.hide();
                });
        };

        /* Go to details book */
        vm.goToBookDetails = function (book) {
            /* First set current book */
            Books.currentBook = book;
            $state.go('app.book-details');
        };

        vm.showLoading = function () {
            $ionicLoading.show({
                template: '<div class="loader"><svg class="circular"><circle class="path" cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10"/></svg></div>'
            });
        };

        /* Init controller function */
        vm.initController = function () {
            /* First set current book */
            vm.searchBookKey = Books.searchBookKey;
            vm.loadSearchResults();
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