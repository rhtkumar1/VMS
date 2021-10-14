var smm = (function (scope) {
    var listGrid;
    scope.listItems = [];
    scope.url = `/admin`;
    scope.eventId = 0;
    scope.Event = [];
    window.location.search.substring(1).split("&").forEach(function (qs) {
        var arr = qs.split("=");
    })

    scope.customerProfileOnLoad = function () {
        var formSubmitHandler = $.noop;
        IMSC.ajaxCall("GET", "/admin/customerprofilelists?type=list", {}, "text", function (d) {
            var result = JSON.parse(d);
            if (result.CustomerProfileLists !== "null" && result.CustomerProfileLists !== "") {
                var fields = [
                    { name: "UserId", css: "hidden", },
                    { name: "FirstName", title: "Customer Name", sorting: true, filtering: true, width: 50 },
                    { name: "MobileNo", title: "Mobile Number", sorting: true, filtering: true, width: 50 },
                    { name: "CompanyName", title: "Company Name", sorting: true, filtering: true, width: 50 },
                    { name: "MobileNo", title: "Mobile Number", sorting: true, filtering: true, width: 50 },
                    {
                        width: 20,
                        itemTemplate: function (value, args) {
                            var $customeEditButton = $('<a class="btn btn-primary"><i class="fa fa-edit"></i></a>')
                                .click(function (e) {
                                    editCustomerProfile('Edit', args);
                                    return false;
                                });
                            return $("<div class='display-flex'></div>").append($customeEditButton);
                        }
                    }
                ];
                var options = {
                    filtering: true,
                    editing: false,
                    sorting: true,
                    paging: true,
                    autoload: true,
                    controller: {
                        CustomerProfileLists: result.CustomerProfileLists,
                        loadData: function (filter) {
                            return $.grep(this.CustomerProfileLists, function (CustomerProfileList) {
                                return (!filter.FirstName || CustomerProfileList.FirstName.toLowerCase().indexOf(filter.FirstName.toLowerCase()) > -1);
                            });
                        }
                    },
                    fields: fields
                };
                $.extend(options, IMSC.grid_options);
                $("#listGrid").jsGrid(options);
                IMSC.filteOnKeyPress("#listGrid");
            }
            formSubmitHandler = function () {
                manageCustomerProfile()
            };
            var editCustomerProfile = function (dilogType, CustomerProfile) {
                if (CustomerProfile.UserId > 0) {
                    $("#UserId").val(CustomerProfile.UserId);
                    $("#FirstName").val(CustomerProfile.FirstName);
                    $("#MiddleName").val(CustomerProfile.MiddleName);
                    $("#LastName").val(CustomerProfile.LastName);
                    $("#MobileNo").val(CustomerProfile.MobileNo);
                    $("#EmailId").val(CustomerProfile.EmailId);
                    $("#CompanyName").val(CustomerProfile.CompanyName);
                    $("#CityName").val(CustomerProfile.City);
                    $("#StateName").val(CustomerProfile.State);
                    $("#CountryName").val(CustomerProfile.Country);
                    $("#Address").val(CustomerProfile.Address);
                    $("#PinCode").val(CustomerProfile.PinCode);
                    $("#AccountNumber").val(CustomerProfile.AccountNumber);
                    $("#IFSCCode").val(CustomerProfile.IFSCCode);
                    $("#AccountHolderName").val(CustomerProfile.AccountHolderName);
                    $("#CountryName").val(CustomerProfile.Country);
                    $("#btnSubmit").text("Update")
                }
                else {
                    $("#UserId").val("0");
                    $("#btnSubmit").text("Submit")
                }
            };
        });
    };

    scope.liabilitiesOnLoad = function () {
        var formSubmitHandler = $.noop;
        IMSC.ajaxCall("GET", "/admin/customerliabilitieslists?type=list", {}, "text", function (d) {
            var result = JSON.parse(d);
            if (result.CustomerLiablityLists !== "null" && result.CustomerLiablityLists !== "") {
                var fields = [
                    { name: "LiabilitiesId", css: "hidden", },
                    { name: "CustomerName", title: "Customer Name", sorting: true, filtering: true, width: 50 },
                    { name: "Date", title: "Date", sorting: true, filtering: true, width: 50 },
                    { name: "Amount", title: "Amount", sorting: true, filtering: true, width: 50 },
                    {
                        width: 20,
                        itemTemplate: function (value, args) {
                            var $customeEditButton = $('<a class="btn btn-primary"><i class="fa fa-edit"></i></a>')
                                .click(function (e) {
                                    editCustomerLiabilities('Edit', args);
                                    return false;
                                });
                            return $("<div class='display-flex'></div>").append($customeEditButton);
                        }
                    }
                ];
                var options = {
                    filtering: true,
                    editing: false,
                    sorting: true,
                    paging: true,
                    autoload: true,
                    controller: {
                        CustomerLiabilitiesLists: result.CustomerLiabilitiesLists,
                        loadData: function (filter) {
                            return $.grep(this.CustomerLiabilitiesLists, function (CustomerLiabilitiesList) {
                                return (!filter.CustomerName || CustomerLiabilitiesList.CustomerName.toLowerCase().indexOf(filter.CustomerName.toLowerCase()) > -1);
                            });
                        }
                    },
                    fields: fields
                };
                $.extend(options, IMSC.grid_options);
                $("#listGrid").jsGrid(options);
                IMSC.filteOnKeyPress("#listGrid");
            }
            formSubmitHandler = function () {
                manageCustomerLiabilities()
            };
            var editCustomerLiabilities = function (dilogType, CustomerLiabilities) {
                if (CustomerLiabilities.CustomerId > 0) {
                    $("#CustomerLists").val(CustomerLiabilities.CustomerId);
                    $("#Date").val(CustomerLiabilities.Date);
                    $("#Amount").val(CustomerLiabilities.Amount);
                    $("#btnSubmit").text("Update")
                }
                else {
                    $("#CustomerId").val("0");
                    $("#btnSubmit").text("Submit")
                }
            };
        });
    };

    return scope;
})(smm || {});

