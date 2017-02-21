(function () {

    app.controller('BaseBooksCtrl', BaseBooksCtrl);
    BaseBooksCtrl.$inject = ['$scope', '$stateParams', '$timeout', 'ionicMaterialInk', 'ionicMaterialMotion', 'Books', 'ErrorMng'];

    function BaseBooksCtrl($scope, $stateParams, $timeout, ionicMaterialInk, ionicMaterialMotion, Books, ErrorMng) {
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
            Books.GetBooks().then(
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
    };

})();