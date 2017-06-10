var app = angular.module("myApp", []);
app.controller("myCtrl", function ($scope, $http) {
    $scope.GetAllData = function () {
       
        $http({
            method: "get",
            url: "/MyProfile/Get_ProfileDetails"
        }).then(function (response) {
            $scope.profiledetails = response.data;
        }, function () {
            alert("Error Occur");
        })
    };
})