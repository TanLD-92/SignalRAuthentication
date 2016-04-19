(function () {
    'use strict';

    angular
        .module('account.module')
        .service('serviceAccount', ServiceAccount);

    ServiceAccount.$inject = ['$http','$rootScope','$q'];

    function ServiceAccount($http, $rootScope, $q) {
        var seft = this;
        seft.login = login;
        seft.register = register;
        seft.serviceHub = null;
        seft.proxyHub = null;
        seft.connectHub = connectHub;
        seft.getListUser = getListUser;
        //connectHub 
        function connectHub() {
            seft.serviceHub = $.hubConnection();
            seft.proxyHub = seft.serviceHub.createHubProxy('serviceHub');
            //public an event server
            seft.proxyHub.on('getListMember', function (listUser) {
                $rootScope.$emit("getListMember", listUser);
            });
            seft.serviceHub.start().done(function () {
                $rootScope.$emit("doneConnectServer");
            });
        }
        //login
        function login(param) {
            var deferred = $q.defer();
            return $http.post('/account/signin', param).success(function (response) {
                deferred.resolve(response);
            }).error(function (err, status) {
                deferred.reject(err);
            });
        }
        //register
        function register(param) {
            var deferred = $q.defer();
            return $http.post('/account/signup', param).success(function (response) {
                deferred.resolve(response);
            }).error(function (err, status) {
                deferred.reject(err);
            });
        }
        //get message hub
        function getListUser() {
            seft.proxyHub.invoke('listUser');
        }
        return {
            login: login,
            register: register,
            connectHub: connectHub,
            getListUser: getListUser
        }
    }
})();