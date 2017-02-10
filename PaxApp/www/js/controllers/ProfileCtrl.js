(function () {

    app.controller('ProfileCtrl', ProfileCtrl);
    ProfileCtrl.$inject = ['$scope', '$stateParams', '$timeout', 'ionicMaterialInk', 'ionicMaterialMotion', '$controller', 'Books'];

    function ProfileCtrl($scope, $stateParams, $timeout, ionicMaterialInk, ionicMaterialMotion, $controller, Books) {
        
        var vm = this;

        vm.booksLoaded = false;
        vm.booksDetailsLoaded = false;

        /* all document table data */
        vm.heartBooks = [];

        /* Link to pax global object to allow binding to the view */
        vm.paxGlobal = paxGlobal;

        /* Load all heart books */
        vm.loadHeartBooks = function () {
            // If data has not been loaded yet, then load it from server
            if (Books.booksLoaded === false) {
                vm.loadBooks();
            } else {
                vm.heartBooks = Books.heartBooks;
            };
        };

        /* Load all books data from server */
        vm.loadBooks = function () {
            Books.GetHeartBooks().then(
                function (result) {
                    if (result.operationResult === true) {
                        Books.heartBooks = result.resultData;
                        Books.booksLoaded = true;

                    } else {
                        // handle error here
                        ErrorMng.showSystemError(result.msg);
                    };
                    /* Save vm state */
                    vm.booksLoaded = Books.booksLoaded;
                    vm.heartBooks = result.resultData;
                },
                function (error) {
                    // handle error here
                    ErrorMng.showSystemError(error.msg);
                });
        };

        vm.goToBookDetails = function (book) {
            /* First set current book */
            Books.currentBook = book;
            /* Call server to get book details */
            Books.GetHeartBooks().then(
                function (result) {
                    if (result.operationResult === true) {

                    }
                });


            alert("Go to book details: " + book.title);
        };

        /* Load all heart books */
        vm.loadDetailsOfHeartBooks = function () {
            // If data has not been loaded yet, then load it from server
            if (Books.booksLoaded === false) {
                vm.loadBooks();
            } else {
                vm.heartBooks = Books.heartBooks;
            };
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