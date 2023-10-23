/// <reference path="../../../assets/admin/libs/angular-1.8.2/angular.js" />

(function (app) {
    app.controller('revenueStatisticController', revenueStatisticController);

    revenueStatisticController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter']

    function revenueStatisticController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.tableData = [];

        $scope.labels = [];
        $scope.series = ['Doanh thu', 'Lợi nhuận'];

        $scope.chartdata = [];

        $scope.getStatistic = getStatistic;
        function getStatistic() {
            var config = {
                params: {
                    fromDate: '01/01/2016',
                    toDate: '01/01/2025',
                }
            };

            apiService.get('/api/Statistics/get-revenueStatistics', config, (result) => {
                $scope.tableData = result.data;

                var labels = [];
                var chartData = [];
                var revenues = [];
                var benefits = [];
                $.each(result.data, function (i, item) {
                    labels.push($filter('date')(item.date, 'dd/MM/yyyy'));
                    revenues.push(item.revenues);
                    benefits.push(item.benefit);
                });
                chartData.push(revenues);
                chartData.push(benefits);

                $scope.chartdata = chartData;
                $scope.labels = labels;

                console.log($scope.labels);
                console.log($scope.chartdata);

            }, () => {
                alert('Get data failed');
            });
        }
        $scope.getStatistic();


    };
})(angular.module('FashionShopStatistic'));
