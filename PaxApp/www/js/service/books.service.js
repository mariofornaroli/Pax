(function () {
    'use strict';

    app.service('Books', Books);

    Books.$inject = ['$q', '$http'];

    function Books($q, $http) {
        var self = this;

        /* jshint validthis:true */
        self.getItems = getItems;
        self.getDetails = _getDetails;
        self.heartBooks = [];
        /* Current document selected for item loading */
        self.currentBook;
        /* Books loaded */
        self.booksLoaded = false;
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
        function _getDetails(book) {
            var req = {
                method: 'POST',
                url: paxGlobal.getAppUrl() + 'Books/GetDetails',
                data: {
                    currentBook: book
                }
            };
            return $http(req).then(function (response) {
                return response.data;
            }, function (response) {
                return response.data;
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
