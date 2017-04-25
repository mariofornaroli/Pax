(function () {
    'use strict';

    app.service('ParserSearchBook', ParserSearchBook);

    ParserSearchBook.$inject = ['$q', '$http', '$timeout'];

    function ParserSearchBook($q, $http, $timeout) {
        var self = this;

        /* jshint validthis:true */
        self.parseResults = _parseResults;
        self.getMockData = _getMockData;
        self.PAX_WEBSITE = "http://www.librairiepax.be/";
        self.mockData;

        function _getMockData() {
            /* Mock input filefile */
            $http.get('js/service/test_search.html').success(function (data) {
                self.mockData = data;
            },
            function (err) {
                self.mockData = err;
            });
        };
        _getMockData();

        /* Main function */
        function _parseResults(data) {

            /* Mock input filefile */
            //data = self.mockData;

            /* Initialize parse data variables.
               Create a dummy DOM element and add the string to it. */
            self.domEl = document.createElement('html');
            self.domEl.innerHTML = data;
            self.parsedList = [];

            /* Call parsing method */
            var ret = _fillSearchedItems();

            return { resultData: self.parsedList, operationResult: true };
        };

        /* Main parser computatoion method */
        function _fillSearchedItems() {
            var resultItems = $('table.tab_listlivre tr', self.domEl);
            var itemsCount = resultItems.length;
            for (var i = 0; i < itemsCount; i++) {
                var bookToAdd = fillBookItem(resultItems[i]);
                /* Add book to list */
                self.parsedList.push(bookToAdd);
            }
        };

        /* Fill each book details */
        function fillBookItem(bookToParse) {
            var retBook = {};
            /* Fill title and href */
            var titleNode = $('td.metabook ul.listeliv_metabook li.titre_commentaire span.titre a', bookToParse);
            if (titleNode) {
                retBook.title = $(titleNode).text();
                retBook.href = $(titleNode).attr('href');
                retBook.href = self.PAX_WEBSITE + retBook.href;
                retBook.completeHref = retBook.href;
            }
            /* Fill Autheur */
            var autheurNode = $('td.metabook ul.listeliv_metabook li.auteurs a', bookToParse);
            if (autheurNode) {
                retBook.author = $(autheurNode).text();
                retBook.authorHref = $(autheurNode).attr('href');
                retBook.authorHref = self.PAX_WEBSITE + retBook.authorHref;
            }
            /* Fill img */
            var imgNode = $('td.visu img', bookToParse);
            if (imgNode) {
                retBook.imgSrc = $(imgNode).attr('src');
            }
            /* Fill editor */
            var editorNode = $('td.metabook ul.listeliv_metabook li.editeur', bookToParse);
            if (editorNode) {
                retBook.editor = $(editorNode).text();
            }
            /* Fill genre */
            var genreNode = $('td.metabook ul.listeliv_metabook li.genre', bookToParse);
            if (genreNode) {
                retBook.genre = $(genreNode).text();
            }            
            
            return retBook;
        };


        /* -------------------------------------------------------------------------------- */
        /* ------------------------------- Parsing functions ------------------------------ */
        /* -------------------------------------------------------------------------------- */



    }
})();
