(function () {
    'use strict';

    app.service('Books', Books);

    Books.$inject = ['$q', '$http'];

    function Books($q, $http) {
        var self = this;

        /* jshint validthis:true */
        self.getItems = getItems;
        self.getBookDetails = _getBookDetails;
        self.heartBooks = [];
        /* Current document selected for item loading */
        self.currentBook;
        /* Books loaded */
        self.booksLoaded = false;
        self.singleBookDetailsLoaded = false;
        /* callbacks to be called on documents status changes */
        self.observerCallbacks = [];
        //register an observer
        self.registerObserverCallback = function (callback) {
            /* If callback is already existent, then remove it and add the new one, 
             * otherwise add the new one */
            paxGlobal.addOrReplaceFinctionInFunctionArray(self.observerCallbacks, callback);
        };
        // clean all observer callbacks
        self.cleanObserverCallback = function () {
            self.observerCallbacks = [];
        };
        //call this when you know 'foo' has been changed
        self.notifyObservers = function () {
            angular.forEach(self.observerCallbacks, function (callback) {
                callback();
            });
        };

        ////////////////

        /* Get loaded documents */
        function getItems() {
            return self.heartBooks;
        };

        /* Get Details */
        function _getBookDetails(book) {
            var bookItem = book !== undefined ? book : self.currentBook;
            var req = {
                method: 'POST',
                url: paxGlobal.getAppUrl() + 'api/BookDetails',
                data: bookItem
            };
            return $http(req).then(function (response) {
                return response.data;
            }, function (response) {
                return response.data;
            });
        };

        function _loadDetailsOfHeartBooks() {

            var promises = [];

            self.heartBooks.forEach(function (book) {
                promises.push(_getBookDetails(book));
            });

            $q.all(promises).then((values) => {
                console.log(values[0]); // value alpha
                console.log(values[1]); // value beta
                console.log(values[2]); // value gamma

                complete();
            });
        };
        
        /* Get Heart Books */
        self.GetHeartBooks = function () {
            var deferredLoadItems = $q.defer();

            var req = {
                method: 'GET',
                url: paxGlobal.getAppUrl() + 'api/HeartBooks'

            };
            return $http(req).then(function (response) {
                self.notifyObservers();
                // promise is fulfilled
                deferredLoadItems.resolve(response.data);
                // promise is returned
                return deferredLoadItems.promise;
            }, function (response) {
                // the following line rejects the promise 
                deferredLoadItems.reject(response.data);
                // promise is returned
                return deferredLoadItems.promise;
            });
        };

    }
})();
