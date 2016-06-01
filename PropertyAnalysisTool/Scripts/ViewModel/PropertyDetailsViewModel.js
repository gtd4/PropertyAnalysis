var PropertyDetailsViewModel = function (data) {
    var _vm = {};

    ko.mapping.fromJS(data, null, _vm);

    _vm.calculatedPrice = ko.pureComputed(
        {
            read: function () {
                return _vm.price();
            },
            write: function (value) {
                _vm.price(_vm.processWrittenValueInt(value));
                var calcRent = _vm.UpdateRent();
                _vm.initialRent(calcRent);
            },

        });

    _vm.calculatedYield = ko.pureComputed({
        read: function () {

            return _vm.initialYieldPercentage();
        },
        write: function (value) {
            var type = true;
            _vm.initialYieldPercentage(_vm.processWrittenValueFloat(value), type);
            var calcRent = _vm.UpdateRent(); //Math.round(_vm.initialYieldPercentage() / 100 * _vm.price() / (52 - _vm.initialVacancyRate()));
            _vm.initialRent(calcRent);
        },

    });

    _vm.calculatedRent = ko.pureComputed(
    {
        read: function () {

            return _vm.initialRent();
        },
        write: function (value) {
            _vm.initialRent(_vm.processWrittenValueInt(value));
            var calcYield = Math.round(_vm.initialRent() * (52 - _vm.initialVacancyRate()) / _vm.price() * 100);
            _vm.initialYieldPercentage(calcYield);

        }


    });

    _vm.calculatedVacancyRate = ko.pureComputed(
           {
               read: function () {
                   return _vm.initialVacancyRate();
               },
               write: function (value) {
                   _vm.initialVacancyRate(_vm.processWrittenValueInt(value));
                   _vm.initialRent(_vm.UpdateRent());


               },
           });

    _vm.calculatedInterestRate = ko.pureComputed(
        {
            read: function () {
                return _vm.initialInterestRate();
            },
            write: function (value) {

                _vm.initialInterestRate(_vm.processWrittenValueFloat(value), true);
                var annualInterest = Math.round(_vm.price() * _vm.initialInterestRate() / 100);
                _vm.annualInterestCost(annualInterest);

            }
        });

    _vm.calculatedRentToCoverInterest = ko.computed(function () {
        return Math.round(_vm.annualInterestCost() / (52 - _vm.initialVacancyRate()));
    });

    _vm.calculateSurplusBeforeExpenses = ko.computed(function () {
        return Math.round(_vm.initialRent() - _vm.calculatedRentToCoverInterest());
    });

    _vm.calculatedRates = ko.pureComputed(
        {
            read: function () {
                return _vm.initialRates();
            },
            write: function (value) {

                _vm.initialRates(_vm.processWrittenValueInt(value));
                debugger;
                var totalExpense = _vm.UpdateTotalExpense();
                _vm.totalInitialExpense(totalExpense);


            }
        });

    _vm.calculatedMaintenance = ko.pureComputed(
        {
            read: function () {
                return _vm.initialRepairs();
            },
            write: function (value) {
                _vm.initialRepairs(_vm.processWrittenValueInt(value));
                var totalExpense = _vm.UpdateTotalExpense();
                _vm.totalInitialExpense(totalExpense);
            },
        });

    _vm.calculatedInsurance = ko.pureComputed(
        {
            read: function () {
                return _vm.initialInsurance();
            },
            write: function (value) {
                _vm.initialInsurance(_vm.processWrittenValueInt(value));

                var totalExpense = _vm.UpdateTotalExpense();
                _vm.totalInitialExpense(totalExpense);

            },
        });

    _vm.calculatedPropertyManagement = ko.pureComputed(
        {
            read: function () {

                return _vm.propertyManagementAmount();
            },
            write: function (value) {
                _vm.propertyManagementAmount(_vm.processWrittenValueInt(value));

                var totalExpense = _vm.UpdateTotalExpense();
                _vm.totalInitialExpense(totalExpense);
            },
        });

    _vm.calculatedRentToCoverMortgageAndExpenses = ko.computed(function () {
        return Math.round((_vm.totalInitialExpense() + _vm.annualInterestCost()) / (52 - _vm.initialVacancyRate()));
    });

    _vm.calculatedSurplusAfterExpenses = ko.computed(function () {
        return Math.round(_vm.initialRent() - _vm.calculatedRentToCoverMortgageAndExpenses());
    });

    _vm.processWrittenValueInt = function (value) {

        value = parseInt(value);

        return isNaN(value) ? 0 : value;
    };

    _vm.processWrittenValueFloat = function (value) {

        value = parseFloat(value);

        return isNaN(value) ? 0 : value;
    }

    _vm.UpdateTotalExpense = function () {
        return Math.round(_vm.initialRates() + _vm.initialRepairs() + _vm.initialInsurance() + _vm.propertyManagementAmount());
    }


    _vm.UpdateRent = function () {
        return Math.round(_vm.initialYieldPercentage() / 100 * _vm.price() / (52 - _vm.initialVacancyRate()));
    }



    return _vm;
};