(function () {

    app.controller('EventCtrl', EventCtrl);
    EventCtrl.$inject = ['$scope', '$stateParams', '$timeout', 'ionicMaterialInk', 'ionicMaterialMotion', '$controller', '$state', 'Events', 'ErrorMng'];

function EventCtrl($scope, $stateParams, $timeout, ionicMaterialInk, ionicMaterialMotion, $controller, $state, Events, ErrorMng) {

        var vm = this;

        vm.eventsLoaded = false;
        vm.eventsDetailsLoaded = false;

        /* all document table data */
        vm.heartEvents = [];
        vm.eventsLoaded = false;
        vm.currentEvent = {};
        /* Mock Events */
        vm.mockEvents = [
            {
                title: "Hommage à François Jacqmin",
                imgSrc: "http://www.librairiepax.be/imagesmagasins/agendaevents/2862357437.JPG",
                shortDescription: "La librairie Pax a le plaisir de vous inviter à une soirée d'hommage à François Jacqmin le me [...]",
                date: "22/02/17"
            },
            {
                title: "Rencontre avec Alain Berenboom",
                imgSrc: "http://www.librairiepax.be/imagesmagasins/agendaevents/2862916448.jpg",
                shortDescription: "La librairie Pax a le plaisir de vous inviter le jeudi 16 mars 2017 à une rencontre avec Al [...]",
                date: "16/03/16"
            },
            {
                title: "Rencontre avec C. Lamarche et L. Demoulin",
                imgSrc: "http://www.librairiepax.be/imagesmagasins/agendaevents/2862781838.JPG",
                shortDescription: "La librairie Pax a le plaisir de vous inviter le jeudi 23 mars 2017 à une rencontre croisée av [...]",
                date: "23/03/17"
            },
            {
                title: "Les enjeux du revenu universel",
                imgSrc: "http://www.librairiepax.be/imagesmagasins/agendaevents/2862547143.JPG",
                shortDescription: "La librairie Pax a le plaisir de vous inviter le mercredi 26 avril 2017 à 18h30 à une soirée d [...]",
                date: "26/04/17"
            }
        ];

        vm.sanitizeMe = function (text) {
            return $sce.trustAsHtml(text)
        };


        /* Link to pax global object to allow binding to the view */
        vm.paxGlobal = paxGlobal;

        vm.setMotion = function () {

            $timeout(function () {
                ionicMaterialMotion.fadeSlideIn({
                        selector: '.animate-fade-slide-in .item'
                    });
                }, 200);
        };

        /* Load all heart events */
        vm.loadHeartEvents = function () {
            // If data has not been loaded yet, then load it from server
            if (Events.eventsLoaded === false) {
                vm.loadEventsFromServer();
            } else {
                vm.heartEvents = Events.heartEvents;
                vm.eventsLoaded = Events.eventsLoaded;
                vm.setMotion();
            };
        };

        /* Load all events data from server */
        vm.loadEventsFromServer = function () {
            Events.GetEvents().then(
                function (result) {
                    if (result.operationResult === true) {
                        Events.heartEvents = result.resultData.events;
                        Events.eventsLoaded = true;

                    } else {
                        // handle error here
                        ErrorMng.showSystemError(result.msg);
                    };
                    /* Save vm state */ 
                    vm.eventsLoaded = Events.eventsLoaded;
                    vm.heartEvents = Events.heartEvents;
                    vm.setMotion();
                },
                function (error) {
                    // handle error here
                    ErrorMng.showSystemError(error.msg);
                });
        };

        vm.goToEventDetails = function (event) {
            /* First set current event */
            Events.currentEvent = event;

            var newState = 'app.event-details';
            $state.go(newState);
        };

        vm.setVmCurrentEvent = function () {
            vm.currentEvent = Events.currentEvent;
        };

        /* Load all heart events */
        vm.loadDetailsOfHeartEvents = function () {
            // If data has not been loaded yet, then load it from server
            if (Events.eventsLoaded === false) {
                vm.loadEventsFromServer();
            } else {
                vm.heartEvents = Events.heartEvents;
            };
        };

        /* Init controller function */
        vm.initController = function () {
            vm.loadHeartEvents();
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


        // Set Ink
        ionicMaterialInk.displayEffect();

        /*  --------------------------------------------------------------------------------------------------------------------------------------------*/
        /*  ------------------------------------------------------  STYLE - Animations - Headers  ------------------------------------------------------*/
        /*  --------------------------------------------------------------------------------------------------------------------------------------------*/

    };

})();