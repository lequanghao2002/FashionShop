/// <reference path="../../../assets/admin/libs/angular-1.8.2/angular.js" />

(function (app) {
    app.controller('orderDetailController', orderDetailController);

    orderDetailController.$inject = ['$scope', 'apiService', 'notificationService', '$stateParams']

    function orderDetailController($scope, apiService, notificationService, $stateParams) {
        $scope.order = [];

        $scope.loadOrderById = function () {
            apiService.get('/api/Orders/get-order-by-id/' + $stateParams.id, null, (result) => {
                console.log(result.data);
                $scope.order = result.data;

                $scope.totalMoney = 0;
                $scope.order.orderDetails.forEach((item, index) => {
                    $scope.quantity = item.quantity;
                    if (item.product.discount > 0) {
                        $scope.price = item.product.price - (item.product.price * item.product.discount / 100);
                        $scope.totalMoney += $scope.price * $scope.quantity;
                    }
                    else {
                        $scope.price = item.product.price;
                        $scope.totalMoney += $scope.price * $scope.quantity;
                    }
                });

                $scope.voucherValue = 0;
                if ($scope.order.voucher != null) {
                    if (order.voucher.discountAmount == true) {
                        $scope.voucherValue = order.voucher.discountValue;
                    }
                    else {
                        $scope.voucherValue = totalMoney * order.voucher.discountValue / 100;
                    }
                }

                $scope.totalPayment = $scope.totalMoney + $scope.order.deliveryFee - $scope.voucherValue;

                console.log($scope.order.orderDetails);

            }, (error) => {
                notificationService.displayError('Không tải đơn hàng được');
            });
        };
        $scope.loadOrderById();
    };


})(angular.module('FashionShopOrder'));