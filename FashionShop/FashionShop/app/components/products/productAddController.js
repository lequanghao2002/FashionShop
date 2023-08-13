/// <reference path="../../../assets/admin/libs/angular-1.8.2/angular.js" />

(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state']

    function productAddController($scope, apiService, notificationService, $state) {
        $scope.product = {
            createdBy: "Quang Hào",
            status: true,
            discount: 0,
        };
        
        $scope.chooseImage = function() {
            window.fileSelected = function (data) {
                $scope.$apply(() => {
                    $scope.product.image = data.path;
                })
            };
            window.open('/file-manager-elfinder', "Select", "menubar:0;");
        };

        $scope.listImages = [];
        $scope.chooseListImage = function () {
            window.fileSelected = function (data) {
                $scope.$apply(() => {
                    if ($scope.listImages.includes(data.path) == false) {
                        $scope.listImages.push(data.path);
                    }
                    else {
                        notificationService.displayWarning('Hình ảnh này đã tồn tại');
                    }
                })
            };
            window.open('/file-manager-elfinder', "Select", "menubar:0;");
        }

        $scope.removeImage = function (img) {
            $scope.temp = $scope.listImages.filter(item => item !== img);
            $scope.listImages = $scope.temp
        }

        $scope.listAllCategory = [];
        $scope.getCategory = function() {
            apiService.get('/api/Categories/get-all-category', null, (result) => {
                $scope.listAllCategory = result.data;
            }, () => {
                alert('Get all list categories failed');
            });
        };
        $scope.getCategory();

        $scope.addProduct = function() {
            $scope.product.listImages = JSON.stringify($scope.listImages);

            apiService.post('/api/products/create-product', $scope.product, (result) => {
                console.log(result);
                notificationService.displaySuccess('Thêm sản phẩm thành công');
                $state.go('product_list');

            }, (error) => {
                notificationService.displayError('Không thêm được sản phẩm');
            });
        };
    };
})(angular.module('FashionShopProduct'));