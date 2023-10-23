/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function () {
    angular.module('FashionShop',
        [
            'FashionShopCommon',
            'FashionShopProduct',
            'FashionShopCategory',
            'FashionShopContact',
            'FashionShopOrder',
            'FashionShopPost',
            'FashionShopRole',
            'FashionShopAdmin',
            'FashionShopMember',
            'FashionShopCustomer',
            'FashionShopVoucher',
            'FashionShopStatistic'
        ]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('base', {
                url: '',
                templateUrl: '/app/shared/views/baseView.html',
                abstract: true
            })
            .state('home', {
                // Đường dẫn vào view
                url: '/admin',
                // Kế thừa 'base'
                parent: 'base',
                // Vào view này nó tự nhận và sử dụng controller ở dưới
                templateUrl: '/app/components/home/homeView.html',
                // Không khai báo homeController thì bị lỗi
                controller: 'homeController'
            });

        $urlRouterProvider.otherwise('/admin');
    }
})();