(function () {
    'use strict';

    app.service('ParserSearchBook', ParserSearchBook);

    ParserSearchBook.$inject = ['$q', '$http', '$timeout'];

    function ParserSearchBook($q, $http, $timeout) {
        var self = this;

        /* jshint validthis:true */
        self.parseResults = _parseResults;

        function _parseResults(data) {
            /* Create a dummy DOM element and add the string to it */
            var el = document.createElement( 'html' );
            el.innerHTML = data;


            var b = $('li.compte_pro', el);

            return { data: '', operationResult: true };
        };
        
    }
})();
