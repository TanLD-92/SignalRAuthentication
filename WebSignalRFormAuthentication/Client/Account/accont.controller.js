(function () {
    'use strict';

    angular
        .module('account.module')
        .controller('AccountController', AccountController);

    AccountController.$inject = ['$location', 'serviceAccount', '$rootScope','$scope'];

    function AccountController($location, serviceAccount, $rootScope, $scope) {
        /* jshint validthis:true */
        var seft = this;
        seft.register = register;
        seft.login = login;
        seft.listMemeber = null;
        seft.updateListUser = updateListUser;
        seft.getListUser = getListUser;
        activate();
        function activate() {
            serviceAccount.connectHub();
        }
        function updateListUser(listUser) {
            seft.listMemeber = listUser;
        }
        $rootScope.$on("getListMember", function (e, listUser) {
            $scope.$apply(function () {
                updateListUser(listUser);
            });
        });
        $rootScope.$on("doneConnectServer", function () {
            $rootScope.$on("getListUser", function () {
                serviceAccount.getListUser();
            });
        });
        //click button get list user
        function getListUser() {
            $rootScope.$emit("getListUser");
        }
        function register(form) {
            if (form.$valid) {
                var registerData = {
                    UserName: seft.userName,
                    Password: seft.passWord,
                    ConfirmPassword: seft.confirmPassword
                }
                serviceAccount.register(registerData).then(function (res) {
                    if (res.data) {
                        window.location.href = '/Home/Index';
                    }
                });
            }
        }
        function login(form) {
            if (form.$valid) {
                var loginData = {
                    UserName: seft.userName,
                    Password: seft.passWord
                }
                serviceAccount.login(loginData).then(function (res) {
                    if (res.data) {
                        window.location.href = '/Home/Index';
                    }
                });
            }
        }
    }
})();
