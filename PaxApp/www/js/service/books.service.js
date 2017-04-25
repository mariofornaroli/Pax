(function () {
    'use strict';

    app.service('Books', Books);

    Books.$inject = ['$q', '$http', '$timeout', 'ParserSearchBook'];

    function Books($q, $http, $timeout, ParserSearchBook) {
        var self = this;

        /* jshint validthis:true */
        self.getItems = getItems;
        self.getBookDetails = _getBookDetails;
        /* search books */
        self.getSearchBookResults = _getSearchBookResults;
        self.oldSearchBookKey = '';
        self.searchBookKey = '';
        self.currentSearchResults = [];

        self.heartBooks = [];
        self.detailsForHeartBooks = [];
        /* Current document selected for item loading */
        self.currentBook;
        /* Books loaded */
        self.booksLoaded = false; 
        self.bestSellersBooksLoaded = false; 
        self.detailsForHeartBooksLoaded = false;
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

        /* Get the list of searched books */
        function _getSearchBookResults(currentSearchKey) {
            /* Mock input filefile */
            //ParserSearchBook.getMockData();

            /* If search key have not changed, then return the last search book result */
            if (self.oldSearchBookKey === self.searchBookKey) {
                var deferred = $q.defer();
                setTimeout(function () {
                    deferred.resolve({ resultData: self.currentSearchResults, operationResult: true });
                }, 500);
                return deferred.promise;
            };
            /* update old search key */
            self.oldSearchBookKey = self.searchBookKey;
            /* add '+' instead of spaces in the search key */
            /* Mock */
            //self.searchBookKey = 'Un paradis trompeur';
            /* END Mock */
            var _searchBookKey = self.searchBookKey.replace(/ /g, "+");

            /* search execution */
            var searchKey = self.searchBookKey;
            var req = {
                method: 'GET',
                url: 'http://www.librairiepax.be/listeliv.php?RECHERCHE=simple&LIVREANCIEN=2&MOTS=' + _searchBookKey + '&x=11&y=13'
            };
            return $http(req).then(function (response) {

                if (response.data) {
                    var retParsing = ParserSearchBook.parseResults(response.data);
                    if (retParsing.operationResult === true) {
                        return retParsing;
                    }
                }
                return response.data;
            }, function (response) {
                return response.data;
            });
        };

        /* Get Details */
        function _getBookDetails(book) {
            /* First find the book details in the loaded details list (detailsForHeartBooks),
               based on the 'completeHref' property.
               If the book is not found there, then search it in the server ('_getBookDetailsFromServer' function)
            */
            var bookItem = book !== undefined ? book : self.currentBook;
            var foundDetailsArray = self.detailsForHeartBooks.filter(function (book) {
                return bookItem.completeHref == book.completeHref;
            });
            if (foundDetailsArray && foundDetailsArray.length > 0) {
                var deferredDetailsItem = $q.defer();
                $timeout(function () {
                    deferredDetailsItem.resolve({ operationResult: true, resultData: foundDetailsArray[0] });
                }, 700);
                return deferredDetailsItem.promise;

                //return foundDetailsArray[0];
            } else {
                return _getBookDetailsFromServer(book);
            };
        };

        /* Get Details from server */
        function _getBookDetailsFromServer(book) {
            var bookItem = book !== undefined ? book : self.currentBook;
            self.currentBook = bookItem;
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
        self.GetBooks = function (bookTypeEnumValue) {
            var deferredLoadItems = $q.defer(); 

            var urlToCall = 'api/HeartBooks';
            if (bookTypeEnumValue === paxGlobal.BookListTypeEnum.HEART) {
                urlToCall = 'api/HeartBooks';
            } else if (bookTypeEnumValue === paxGlobal.BookListTypeEnum.BEST_SELLERS) {
                urlToCall = 'api/BestSellers';
            };
            var req = {
                method: 'GET',
                url: paxGlobal.getAppUrl() + urlToCall

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

        /* Get Details for Heart Books */
        self.getDetailsForHeartBooks = function (bookTypeEnumValue) {
            var deferredLoadItems = $q.defer();

            var urlToCall = 'api/DetailsHeartBooksList';
            var req = {
                method: 'GET',
                url: paxGlobal.getAppUrl() + urlToCall

            };
            return $http(req).then(function (response) {
                self.notifyObservers();
                /* Data correctly received, then fill heart books details array */
                if (response.data.operationResult) {
                    self.detailsForHeartBooksLoaded = true;
                    self.detailsForHeartBooks = response.data.resultData.detailsBooks;
                };

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
