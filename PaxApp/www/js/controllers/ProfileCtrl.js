﻿(function () {

    app.controller('ProfileCtrl', ProfileCtrl);
    ProfileCtrl.$inject = ['$scope', '$stateParams', '$timeout', 'ionicMaterialInk', 'ionicMaterialMotion', '$controller', 'Books', '$state', 'ErrorMng', '$sce'];

    function ProfileCtrl($scope, $stateParams, $timeout, ionicMaterialInk, ionicMaterialMotion, $controller, Books, $state, ErrorMng, $sce) {
        
        var vm = this;

        vm.booksLoaded = false;
        vm.bestSellersBooksLoaded = false;
        vm.booksDetailsLoaded = false;

        /* all document table data */
        vm.heartBooks = []; 
        vm.bestSellersBooks = [];
        vm.pocheDuMois = {};

        /* Link to pax global object to allow binding to the view */
        vm.paxGlobal = paxGlobal;

        vm.setMotion = function () {
            if (navigator && navigator.splashscreen) {
                navigator.splashscreen.hide();
            };

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
        };

        /* Load all heart books */ 
        vm.loadHeartBooks = function () {
            // If data has not been loaded yet, then load it from server
            if (Books.booksLoaded === false) {
                vm.loadBooks(); 
            } else { 
                vm.heartBooks = Books.heartBooks;
                vm.pocheDuMois = Books.pocheDuMois;
                vm.setMotion();

            };
        }; 

        /* Load all books data from server */
        vm.loadBooks = function () {
            Books.GetBooks(paxGlobal.BookListTypeEnum.HEART).then(
                function (result) {
                    if (result.operationResult === true) {
                        /* service state */
                        Books.heartBooks = result.resultData.heartBooks;
                        Books.pocheDuMois = result.resultData.monthBook;
                        Books.booksLoaded = true;
                        /* vm state */
                        vm.booksLoaded = Books.booksLoaded;
                        vm.heartBooks = Books.heartBooks;
                        vm.pocheDuMois = Books.pocheDuMois;
                        vm.setMotion();

                    } else {
                        // handle error here
                        ErrorMng.showSystemError(result.msg);
                    };
                },
                function (error) { 
                    // handle error here
                    ErrorMng.showSystemError(error.msg);
                });
        };

        /* Load all heart books */
        vm.loadBestSellers = function () {
            // If data has not been loaded yet, then load it from server
            if (Books.bestSellersBooksLoaded === false) {
                vm.loadBestSellersBooks();
            } else {
                vm.bestSellersBooks = Books.bestSellersBooks;
                vm.setMotion();

            };
        };

        /* Load all books data from server */ 
        vm.loadBestSellersBooks = function () {
            Books.GetBooks(paxGlobal.BookListTypeEnum.BEST_SELLERS).then(
                function (result) {
                    if (result.operationResult === true) {
                        /* service state */
                        Books.bestSellersBooks = result.resultData.bestSellers;
                        Books.bestSellersBooksLoaded = true;
                        /* vm state */
                        vm.bestSellersBooksLoaded = Books.bestSellersBooksLoaded;
                        vm.bestSellersBooks = Books.bestSellersBooks;
                        vm.setMotion();

                    } else {
                        // handle error here
                        ErrorMng.showSystemError(result.msg);
                    };
                },
                function (error) {
                    // handle error here
                    ErrorMng.showSystemError(error.msg);
                });
        };

        /* Go to details book */
        vm.goToBookDetails = function (book) {
            /* First set current book */
            Books.currentBook = book;
            $state.go('app.book-details');
        };

        vm.goToPocheDuMois = function () { 
            /* First set current book */
            Books.currentBook = vm.pocheDuMois;
            Books.currentBook.isPocheDuMois = true;
            $state.go('app.book-details');
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

        /* Init controller function */
        vm.initController = function () { 
            vm.loadHeartBooks();
            vm.loadBestSellers();
        };

        /* Call init controller */
        vm.initController();

        /*  --------------------------------------------------------------------------------------------------------------------------------------------*/
        /*  ------------------------------------------------------  STYLE - Animations - Headers  ------------------------------------------------------*/
        /*  --------------------------------------------------------------------------------------------------------------------------------------------*/

        // Set Header
        $scope.$parent.showHeader();
        $scope.$parent.clearFabs();
        $scope.isExpanded = false;
        $scope.$parent.setExpanded(false);
        $scope.$parent.setHeaderFab(false);

//        // Set Motion
//        $timeout(function () {
//            ionicMaterialMotion.slideUp({
//                selector: '.slide-up'
//            });
//        }, 10000);
//
//        $timeout(function () {
//            ionicMaterialMotion.fadeSlideInRight({
//                startVelocity: 3000
//            });
//        }, 10000);

        // Set Ink
        ionicMaterialInk.displayEffect();

        /*  --------------------------------------------------------------------------------------------------------------------------------------------*/
        /*  ------------------------------------------------------  STYLE - Animations - Headers  ------------------------------------------------------*/
        /*  --------------------------------------------------------------------------------------------------------------------------------------------*/

    };

})();